using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Hsl_Blog;
/// <summary>
/// .Domain.Shared层相当于.Domain的一个扩展一样，这里放一下项目用到的枚举、公共常量等内容，需要引用我们的.Domain项目
/// </summary>
[DependsOn(
    typeof(AbpIdentityDomainSharedModule)  
    )]
public class Hsl_BlogDomainSharedModule : AbpModule
{
    
}
