using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace Hsl_Blog;

public class Hsl_BlogWebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<Hsl_BlogWebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
