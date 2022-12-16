using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Hsl_Blog.Swagger
{
    [DependsOn(typeof(Hsl_BlogDomainModule))]
    public class Hsl_BlogSwaggerModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSwagger();
           // base.ConfigureServices(context);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            context.GetApplicationBuilder().UseSwaggerUI();
            //base.OnApplicationInitialization(context);
        }
    }
}
