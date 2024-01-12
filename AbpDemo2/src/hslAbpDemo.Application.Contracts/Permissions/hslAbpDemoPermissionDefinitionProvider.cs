using hslAbpDemo.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace hslAbpDemo.Permissions;

public class hslAbpDemoPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(hslAbpDemoPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(hslAbpDemoPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<hslAbpDemoResource>(name);
    }
}
