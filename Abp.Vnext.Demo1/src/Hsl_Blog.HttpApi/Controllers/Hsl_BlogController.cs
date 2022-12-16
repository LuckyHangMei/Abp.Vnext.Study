using Volo.Abp.AspNetCore.Mvc;

namespace Hsl_Blog.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class Hsl_BlogController : AbpControllerBase
{
    protected Hsl_BlogController()
    {
        //LocalizationResource = typeof(Hsl_BlogResource);
    }
}
