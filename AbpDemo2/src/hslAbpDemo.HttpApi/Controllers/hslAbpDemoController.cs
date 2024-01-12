using hslAbpDemo.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace hslAbpDemo.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class hslAbpDemoController : AbpControllerBase
{
    protected hslAbpDemoController()
    {
        LocalizationResource = typeof(hslAbpDemoResource);
    }
}
