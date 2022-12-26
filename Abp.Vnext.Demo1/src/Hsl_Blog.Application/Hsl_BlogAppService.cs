using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace Hsl_Blog;

/* Inherit your application services from this class.
 */
public abstract class Hsl_BlogAppService : ApplicationService
{
    protected Hsl_BlogAppService()
    {
        //LocalizationResource = typeof(Hsl_BlogResource);
    }
}
