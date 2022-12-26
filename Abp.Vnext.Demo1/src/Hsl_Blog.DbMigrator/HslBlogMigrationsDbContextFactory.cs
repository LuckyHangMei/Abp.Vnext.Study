using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsl_Blog.DbMigrator
{
    public class HslBlogMigrationsDbContextFactory : IDesignTimeDbContextFactory<HslBlogMigrationsDbContext>
    {
        public HslBlogMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();
            DbContextOptionsBuilder<HslBlogMigrationsDbContext> builder = new();
            builder.UseMySql(configuration.GetConnectionString("Default"), ServerVersion.Create(new Version(8, 0, 29), serverType: ServerType.MySql));
            return new HslBlogMigrationsDbContext(builder.Options);
        }
        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
    }
}
