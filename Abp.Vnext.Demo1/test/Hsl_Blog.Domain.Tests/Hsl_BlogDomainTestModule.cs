using Hsl_Blog.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Hsl_Blog;

[DependsOn(
    typeof(Hsl_BlogEntityFrameworkCoreTestModule)
    )]
public class Hsl_BlogDomainTestModule : AbpModule
{

}
