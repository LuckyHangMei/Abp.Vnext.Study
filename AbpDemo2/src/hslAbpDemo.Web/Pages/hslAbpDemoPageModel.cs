using hslAbpDemo.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace hslAbpDemo.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class hslAbpDemoPageModel : AbpPageModel
{
    protected hslAbpDemoPageModel()
    {
        LocalizationResourceType = typeof(hslAbpDemoResource);
    }
}
