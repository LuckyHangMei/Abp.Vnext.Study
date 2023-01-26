using Volo.Abp.Account;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace Hsl_Blog;

[DependsOn(
    typeof(Hsl_BlogApplicationModule),
    typeof(AbpAccountHttpApiModule),
    typeof(AbpIdentityHttpApiModule),
    typeof(AbpPermissionManagementHttpApiModule),
    typeof(AbpTenantManagementHttpApiModule),
    typeof(AbpFeatureManagementHttpApiModule),
    typeof(AbpSettingManagementHttpApiModule)
    )]
public class Hsl_BlogHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
       // ConfigureAutoApiControllers();
        //ConfigureLocalization();
    }
    #region 
    /// <summary>
    /// 自动生成api控制器
    /// </summary>
    private void ConfigureAutoApiControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(Hsl_BlogApplicationModule).Assembly);
        });
    }
    #endregion
    private void ConfigureLocalization()
    {
      /*  Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<Hsl_BlogResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });*/
    }
}
