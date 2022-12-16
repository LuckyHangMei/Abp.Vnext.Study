using Volo.Abp.Modularity;

namespace Hsl_Blog;

[DependsOn(
    typeof(Hsl_BlogApplicationModule),
    typeof(Hsl_BlogDomainTestModule)
    )]
public class Hsl_BlogApplicationTestModule : AbpModule
{

}
