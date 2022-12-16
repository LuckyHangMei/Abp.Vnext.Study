using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Hsl_Blog.Web;

[Dependency(ReplaceServices = true)]
public class Hsl_BlogBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Hsl_Blog";
}
