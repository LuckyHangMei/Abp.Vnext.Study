using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.EntityFrameworkCore.SqlServer;

using Volo.Abp.Modularity;

namespace Hsl_Blog.EntityFrameworkCore;

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

    }
}
