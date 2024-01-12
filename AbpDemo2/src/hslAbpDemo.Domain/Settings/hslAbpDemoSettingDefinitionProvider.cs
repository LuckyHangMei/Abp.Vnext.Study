using Volo.Abp.Settings;

namespace hslAbpDemo.Settings;

public class hslAbpDemoSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(hslAbpDemoSettings.MySetting1));
    }
}
