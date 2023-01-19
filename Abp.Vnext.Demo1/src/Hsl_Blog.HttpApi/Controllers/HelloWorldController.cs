using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using static Hsl_Blog.HslBlogConsts;

namespace Hsl_Blog.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = Grouping.GroupName_v3)]
    public class HelloWorldController: AbpController
    {
        [HttpGet]
        [Route("Exception")]
        public string Exception()
        {
            throw new Exception("这是一个为实现的一场接口");
        }
    }
}
