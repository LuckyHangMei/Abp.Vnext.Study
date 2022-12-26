using System;
using Hsl_Blog.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.EntityFrameworkCore.SqlServer;

using Volo.Abp.Modularity;

namespace Hsl_Blog.EntityFrameworkCore;

/// <summary>
/// 值得一提的是，ABP会自动为DbContext中的实体创建默认仓储. 需要在注册的时使用options添加AddDefaultRepositories()。
///默认情况下为每个实体创建一个仓储，如果想要为其他实体也创建仓储，可以将 includeAllEntities 设置为 true，
///然后就可以在服务中注入和使用 IRepository<TEntity> 或 IQueryableRepository<TEntity>
/// </summary>
[DependsOn(
    typeof(Hsl_BlogDomainModule),
    typeof(AbpEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCoreSqlServerModule),
    typeof(AbpEntityFrameworkCoreMySQLModule),
    typeof(AbpEntityFrameworkCorePostgreSqlModule),
    typeof(AbpEntityFrameworkCoreSqliteModule)
    )]
public class Hsl_BlogEntityFrameworkCoreModule : AbpModule
{

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
         
        context.Services.AddAbpDbContext<Hsl_BlogDbContext>(options => {
            //ABP会自动为DbContext中的实体创建默认仓储. 需要在注册的时使用options添加AddDefaultRepositories()
            //默认情况下为每个聚合根实体(AggregateRoot派生的子类)创建一个仓储.如果想要为其他实体也创建仓储，可以将 includeAllEntities 设置为 true`
            options.AddDefaultRepositories(true);
        });
        Configure<AbpDbContextOptions>(options => {
            switch (AppSettings.EnableDb)
            {
                case "MySQL":
                    options.UseMySQL();
                    break;
                case "SqlServer":
                    options.UseSqlServer();
                    break;
                case "PostgreSql":
                    options.UsePostgreSql();
                    break;
                case "Sqlite":
                    options.UseSqlite();
                    break;
                default:
                    options.UseMySQL();
                    break;
            }
        });
    }
}
