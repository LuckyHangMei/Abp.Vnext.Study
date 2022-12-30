using Hsl_Blog.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace Hsl_Blog;

[DependsOn(
    typeof(Hsl_BlogDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(Hsl_BlogApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
public class Hsl_BlogApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        /*
         * 解释一下TokenValidationParameters参数的含义：
            ValidateIssuer：是否验证颁发者。ValidateAudience：是否验证访问群体。
            ValidateLifetime：是否验证生存期。
            ClockSkew：验证Token的时间偏移量。
            ValidateIssuerSigningKey：是否验证安全密钥。
            ValidAudience：访问群体。
            ValidIssuer：颁发者。
            IssuerSigningKey：安全密钥。
            GetBytes()是abp的一个扩展方法，可以直接使用。
         * */
        //context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //    .AddJwtBearer(options => {
        //        options.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuer = true,
        //            ValidateAudience = true,
        //            ValidateLifetime = true,
        //            ClockSkew = TimeSpan.FromSeconds(30),
        //            ValidateIssuerSigningKey = true,
        //            ValidAudience = AppSettings.JWT.Domain,
        //            ValidIssuer = AppSettings.JWT.Domain,
        //            IssuerSigningKey = new SymmetricSecurityKey(AppSettings.JWT.SecurityKey.GetBytes())
        //        };
        //    });
        //context.Services.AddAuthorization();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<Hsl_BlogApplicationModule>();
        });
    }

}
