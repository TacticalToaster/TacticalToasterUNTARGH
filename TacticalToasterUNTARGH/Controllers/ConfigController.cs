

using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Helpers;
using System.Reflection;
using TacticalToasterUNTARGH;

[Injectable(InjectionType.Singleton)]
public class ConfigController
{
    public MainConfig ModConfig;

    public readonly ModHelper _modHelper;

    public ConfigController(ModHelper modHelper)
    {
        _modHelper = modHelper;

        var pathToMod = _modHelper.GetAbsolutePathToModFolder(Assembly.GetExecutingAssembly());

        ModConfig = _modHelper.GetJsonDataFromFile<MainConfig>(pathToMod, "config.jsonc");
    }
}