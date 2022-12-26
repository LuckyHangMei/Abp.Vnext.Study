using Hsl_Blog.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Hsl_Blog.DbMigrator;

/// <summary>
/// 主要做数据库迁移，用code-first方式创建数据库表
/// </summary>
[DependsOn(
   // typeof(AbpAutofacModule),
    typeof(Hsl_BlogEntityFrameworkCoreModule)
   // typeof(Hsl_BlogApplicationContractsModule)
    )]
public class Hsl_BlogDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<HslBlogMigrationsDbContext>();
       // Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
