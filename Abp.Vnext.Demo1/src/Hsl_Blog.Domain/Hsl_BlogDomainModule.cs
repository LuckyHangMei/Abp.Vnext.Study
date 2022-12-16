using Volo.Abp.Identity;
using Volo.Abp.Modularity;



namespace Hsl_Blog;

[DependsOn(
    typeof(AbpIdentityDomainModule)
)]
public class Hsl_BlogDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        /*
        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = MultiTenancyConsts.IsEnabled;
        });

#if DEBUG
        context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
#endif
        */
    }
}
