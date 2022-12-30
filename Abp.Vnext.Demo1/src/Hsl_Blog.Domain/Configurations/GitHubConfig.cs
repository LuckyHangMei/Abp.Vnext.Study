using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsl_Blog.Configurations
{
    /// <summary>
    /// 分析一下，我们接入GitHub身份认证授权整个流程下来分以下几步
    ///根据参数生成GitHub重定向的地址，跳转到GitHub登录页，进行登录
    ///登录成功之后会跳转到我们的回调地址，回调地址会携带code参数
    ///拿到code参数，就可以换取到access_token
    ///有了access_token，可以调用GitHub获取用户信息的接口，得到当前登录成功的用户信息
    ///开始之前，先将GitHub的API简单处理一下。
    /// </summary>
    public class GitHubConfig
    {
        /// <summary>
        /// GET请求，跳转GitHub登录界面，获取用户授权，得到code
        /// </summary>
        public static string API_Authorize = "https://github.com/login/oauth/authorize";
        /// <summary>
        /// POST请求，根据code得到access_token
        /// </summary>
        public static string API_AccessToken = "https://github.com/login/oauth/access_token";

        /// <summary>
        /// GET请求，根据access_token得到用户信息
        /// </summary>
        public static string API_User = "https://api.github.com/user";

        /// <summary>
        /// Github UserId
        /// </summary>
        public static int UserId = AppSettings.GitHub.UserId;
        /// <summary>
        /// Client ID
        /// </summary>
        public static string Client_ID = AppSettings.GitHub.Client_ID;

        /// <summary>
        /// Client Secret
        /// </summary>
        public static string Client_Secret = AppSettings.GitHub.Client_Secret;

        /// <summary>
        /// Authorization callback URL
        /// </summary>
        public static string Redirect_Uri = AppSettings.GitHub.Redirect_Uri;

        /// <summary>
        /// Application name
        /// </summary>
        public static string ApplicationName = AppSettings.GitHub.ApplicationName;

    }
}
