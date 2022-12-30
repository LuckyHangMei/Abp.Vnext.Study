using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsl_Blog.Authorize.Impl
{
    public class AuthTokenDto
    {
        // jwt token
        public string AccessToken { get; set; }

        // 用于刷新token的刷新令牌
        public string RefreshToken { get; set; }
    }
}
