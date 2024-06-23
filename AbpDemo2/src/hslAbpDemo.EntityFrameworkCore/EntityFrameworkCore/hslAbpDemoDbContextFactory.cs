using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace hslAbpDemo.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class hslAbpDemoDbContextFactory : IDesignTimeDbContextFactory<hslAbpDemoDbContext>
{
    public hslAbpDemoDbContext CreateDbContext(string[] args)
    {
        hslAbpDemoEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

         var builder = new DbContextOptionsBuilder<hslAbpDemoDbContext>()
            .UseMySql(configuration.GetConnectionString("Default"),ServerVersion.Create(Version.Parse("8.0.29"),ServerType.MySql));


        return new hslAbpDemoDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../hslAbpDemo.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
