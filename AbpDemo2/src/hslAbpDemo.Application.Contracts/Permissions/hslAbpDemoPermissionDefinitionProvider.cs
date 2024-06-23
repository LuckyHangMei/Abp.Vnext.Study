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

        var booksPermission = myGroup.AddPermission(hslAbpDemoPermissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(hslAbpDemoPermissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(hslAbpDemoPermissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(hslAbpDemoPermissions.Books.Delete, L("Permission:Books.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<hslAbpDemoResource>(name);
    }
}
