using hslAbpDemo.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace hslAbpDemo.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(hslAbpDemoEntityFrameworkCoreModule),
    typeof(hslAbpDemoApplicationContractsModule)
    )]
public class hslAbpDemoDbMigratorModule : AbpModule
{
}
