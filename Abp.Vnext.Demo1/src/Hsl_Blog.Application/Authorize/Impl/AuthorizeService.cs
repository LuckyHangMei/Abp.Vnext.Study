using Hsl_Blog.Configurations;
using Hsl_Blog.Dtos;
using Hsl_Blog.ToolKits.Base;
using Hsl_Blog.ToolKits.Extensions;
using Hsl_Blog.ToolKits.GitHub;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hsl_Blog.Authorize.Impl
{
    public class AuthorizeService : ServiceBase, IAuthorizeService
    {
        private const string RefreshTokenIdClaimType = "refresh_token_id";
        private readonly IHttpClientFactory _httpClient;
        private readonly IDistributedCache _distributedCache;
        private readonly SigningCredentials _signingCredentials;
        private readonly JwtBearerOptions _jwtBearerOptions;
        public AuthorizeService(IHttpClientFactory httpClient, SigningCredentials signingCredentials, IOptionsSnapshot<JwtBearerOptions> jwtBearerOptions, IDistributedCache distributedCache)
        {
            _httpClient = httpClient;
            _distributedCache = distributedCache;
            _signingCredentials = signingCredentials;
            _jwtBearerOptions = jwtBearerOptions.Get(JwtBearerDefaults.AuthenticationScheme);

        }
        public async Task<ServiceResult<string>> GenerateTokenAsync(string access_token)
        {
            var result = new ServiceResult<string>();
            if (string.IsNullOrEmpty(access_token))
            {
                result.IsFailed("access_token为空");
                return result;
            }
            //var url= $"{GitHubConfig.API_User}?access_token={access_token}";
            var url = $"{GitHubConfig.API_User}";
            using var client = _httpClient.CreateClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.14 Safari/537.36 Edg/83.0.478.13");
            client.DefaultRequestHeaders.Add("Authorization", "token " + access_token);
            var httpResponse = await client.GetAsync(url);
            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                result.IsFailed("access_token不正确");
                return result;
            }
            var content = await httpResponse.Content.ReadAsStringAsync();

            var user = content.FromJson<UserResponse>();
            if (user.IsNull())
            {
                result.IsFailed("未获取到用户数据");
                return result;
            }

            if (user.Id != GitHubConfig.UserId)
            {
                result.IsFailed("当前账号未授权");
                return result;
            }
            var claims = new[] {
                new Claim(ClaimTypes.Name,user.Name!=null?user.Name:"ceshi"),
                new Claim(ClaimTypes.Email, user.Email!=null?user.Email:"2313124@qq.com"),
                new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddMinutes(AppSettings.JWT.Expires)).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}")
                           };

            var key = new SymmetricSecurityKey(AppSettings.JWT.SecurityKey.SerializeUtf8());
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(
                                    issuer: AppSettings.JWT.Domain,
                                    audience: AppSettings.JWT.Domain,
                                    claims: claims,
                                    expires: DateTime.Now.AddMinutes(AppSettings.JWT.Expires),
                                    signingCredentials: creds);
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            result.IsSuccess(token);
            return await Task.FromResult(result);

        }
        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<ServiceResult<string>> GetAccessTokenAsync(string code)
        {
            var result = new ServiceResult<string>();

            if (string.IsNullOrEmpty(code))
            {
                result.IsFailed("code为空");
                return result;
            }

            var request = new AccessTokenRequest();

            var content = new StringContent($"code={code}&client_id={request.Client_ID}&redirect_uri={request.Redirect_Uri}&client_secret={request.Client_Secret}");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            using var client = _httpClient.CreateClient();
            var httpResponse = await client.PostAsync(GitHubConfig.API_AccessToken, content);

            var response = await httpResponse.Content.ReadAsStringAsync();

            if (response.StartsWith("access_token"))
                result.IsSuccess(response.Split("=")[1].Split("&").First());
            else
                result.IsFailed("code不正确");

            return result;
        }
        /// <summary>
        /// 获取登录地址(GitHub)
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<string>> GetLoginAddressAsync()
        {
            var result = new ServiceResult<string>();

            var request = new AuthorizeRequest();
            var address = string.Concat(new string[]
            {
                    GitHubConfig.API_Authorize,
                    "?client_id=", request.Client_ID,
                    "&scope=", request.Scope,
                    "&state=", request.State,
                    "&redirect_uri=", request.Redirect_Uri
            });

            result.IsSuccess(address);
            return await Task.FromResult(result);
        }

        #region /RefreshToken 刷新令牌/
        public async Task<AuthTokenDto> CreateAuthTokenAsync(UserDto user)
        {
            var result = new AuthTokenDto();
            //先创建refresh token
            var (refreshTokenId, refreshToken) = await CreateRefreshTokenAsync(user.Id);
            result.RefreshToken = refreshToken;
            // 再签发Jwt
            result.AccessToken = CreateJwtToken(user, refreshTokenId);
            return result;
        }
        private string CreateJwtToken(UserDto user, string refreshTokenId)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrEmpty(refreshTokenId)) throw new ArgumentNullException(nameof(refreshTokenId));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new List<Claim> {
                          new Claim(JwtClaimTypes.Id, user.Id),
                          new Claim(JwtClaimTypes.Name, user.UserName),
                          // 将 refresh token id 记录下来
                          new Claim(RefreshTokenIdClaimType, refreshTokenId)
                }),
                Issuer = "hsl",
                Audience = "hsl",
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = _signingCredentials
            };

            var handler = _jwtBearerOptions.SecurityTokenValidators.OfType<JwtSecurityTokenHandler>().FirstOrDefault()
        ?? new JwtSecurityTokenHandler();
            var securityToken = handler.CreateJwtSecurityToken(tokenDescriptor);
            var token = handler.WriteToken(securityToken);
            return token;

        }
        private async Task<(string refreshTokenId, string refreshToken)> CreateRefreshTokenAsync(string userId)
        {
            //refresh token id作为缓存Key
            var tokenId = Guid.NewGuid().ToString("N");
            // 生成refresh token
            var rnBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(rnBytes);
            var token = Convert.ToBase64String(rnBytes);

            // 设置refresh token的过期时间
            var options = new DistributedCacheEntryOptions();
            options.SetAbsoluteExpiration(TimeSpan.FromDays(2));

            //缓存 refresh token
            await _distributedCache.SetStringAsync(GetRefreshTokenKey(userId, tokenId), token, options);
            return (tokenId, token);
        }
        private string GetRefreshTokenKey(string userId, string refreshTokenId)
        {
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException(nameof(userId));
            if (string.IsNullOrEmpty(refreshTokenId)) throw new ArgumentNullException(nameof(refreshTokenId));
            return $"{userId}:{refreshTokenId}";
        }

        public Task<AuthTokenDto> RefreshAuthTokenAsync(AuthTokenDto token)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
