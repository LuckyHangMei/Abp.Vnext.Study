using Hsl_Blog.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Hsl_Blog.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class Hsl_BlogPageModel : AbpPageModel
{
    protected Hsl_BlogPageModel()
    {
        LocalizationResourceType = typeof(Hsl_BlogResource);
    }
}
