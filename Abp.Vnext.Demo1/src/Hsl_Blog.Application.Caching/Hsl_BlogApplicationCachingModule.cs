using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;

namespace Hsl_Blog.Application.Caching
{
    /// <summary>
    /// abp.vnext 框架要求，需要每个项目，添加模块,
    /// 此模块是缓存添加的模块
    /// </summary>
    [DependsOn(typeof(AbpCachingModule),
        typeof(Hsl_BlogDomainModule))]
    public class Hsl_BlogApplicationCachingModule: AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);
        }
    }
}
