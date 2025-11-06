using SPTarkov.DI.Annotations;
using SPTarkov.Server;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Models.Eft.Common;
using SPTarkov.Server.Core.Models.Spt.Mod;
using SPTarkov.Server.Core.Services;
using SPTarkov.Server.Core.Utils;
using System.Reflection;
using TacticalToasterUNTARGH.Controllers;

namespace TacticalToasterUNTARGH;

public record ModMetadata : AbstractModMetadata
{
    public override string ModGuid { get; init; } = "com.untargh.tacticaltoaster";
    public override string Name { get; init; } = "UNTAR Go Home!";
    public override string Author { get; init; } = "TacticalToaster";
    public override List<string>? Contributors { get; init; } = new() { };
    public override SemanticVersioning.Version Version { get; init; } = new(3, 0, 0);
    public override SemanticVersioning.Range SptVersion { get; init; } = new("~4.0.0");
    public override List<string>? Incompatibilities { get; init; }
    public override Dictionary<string, SemanticVersioning.Range>? ModDependencies { get; init; }
    public override string? Url { get; init; }
    public override bool? IsBundleMod { get; init; }
    public override string License { get; init; } = "MIT";
}

[Injectable(TypePriority = OnLoadOrder.PreSptModLoader + 1)]
public class UNTARModPreload : IOnLoad
{
    public static MainConfig ModConfig = new();

    private readonly ModHelper _modHelper;

    public UNTARModPreload(
        ModHelper modHelper
        )
    {
        _modHelper = modHelper;
    }

    Task IOnLoad.OnLoad()
    {
        var pathToMod = _modHelper.GetAbsolutePathToModFolder(Assembly.GetExecutingAssembly());

        ModConfig = _modHelper.GetJsonDataFromFile<MainConfig>(pathToMod, "config.jsonc");

        return Task.CompletedTask;
    }
}

[Injectable(InjectionType = InjectionType.Singleton, TypePriority = MoreBotsServer.MoreBotsLoadOrder.LoadBots)]
public class UntarGoHomeBots(
    MoreBotsServer.MoreBotsAPI moreBotsLib,
    MoreBotsServer.Services.MoreBotsCustomBotTypeService customBotTypeService,
    MoreBotsServer.Services.MoreBotsCustomBotConfigService customBotConfigService,
    MoreBotsServer.Services.FactionService factionService,
    MoreBotsServer.Services.LoadoutService loadoutService,
    WTTServerCommonLib.WTTServerCommonLib commonLib,
    IReadOnlyList<SptMod> modList,
    UntarSpawnController untarSpawnController
) : IOnLoad
{
    public async Task OnLoad()
    {
        var typeList = new List<string> {
            "followeruntar",
            "bossuntarlead",
            "followeruntarmarksman",
            "bossuntarofficer"
        };

        var assembly = Assembly.GetExecutingAssembly();

        // Load base bot types
        await customBotTypeService.CreateCustomBotTypes(assembly);
        await customBotConfigService.LoadCustomBotConfigsShared(assembly, "untar", typeList);

        // Load the loadouts for the bots
        await loadoutService.LoadLoadouts(assembly);

        // Replace some values in the bot types
        await customBotTypeService.LoadBotTypeReplace(Assembly.GetExecutingAssembly(), "lastnames", typeList);

        // Add couturier mod related stuff
        if (modList.Any(mod => mod.ModMetadata.ModGuid == "com.turbodestroyer.couturier"))
        {
            // Replace the appearance settings of the bots so they use couturier clothes
            await customBotTypeService.LoadBotTypeReplace(Assembly.GetExecutingAssembly(), "untar_couturier", typeList);
        }

        factionService.AddRevengeByFaction(typeList, "untar");

        // Add enemies based on factions
        factionService.AddEnemyByFaction(typeList, "cultists");
        factionService.AddEnemyByFaction(typeList, "infected");

        // Add UNTAR as enemies to those some factions
        factionService.AddEnemyByFaction("savage", "untar");
        factionService.AddEnemyByFaction("cultists", "untar");
        factionService.AddEnemyByFaction("infected", "untar");

        if (modList.Any(mod => mod.ModMetadata.ModGuid == "com.ruafcomehome.tacticaltoaster"))
        {
            factionService.AddWarnByFaction(typeList, "ruaf");
        }

        // Use WTT to add locales
        await commonLib.CustomLocaleService.CreateCustomLocales(Assembly.GetExecutingAssembly());

        // Add UNTAR to spawns
        untarSpawnController.AdjustAllUntarSpawns();

        await Task.CompletedTask;
    }
}

[Injectable(InjectionType = InjectionType.Singleton, TypePriority = MoreBotsServer.MoreBotsLoadOrder.LoadFactions)]
public class UntarGoHomeLoadFaction(
    MoreBotsServer.Services.FactionService factionService
) : IOnLoad
{
    public async Task OnLoad()
    {
        // Create the new RUAF faction
        factionService.Factions.Add("untar", new Faction()
        {
            Name = "untar",
            BotTypes =
            {
                (WildSpawnType)1170,
                (WildSpawnType)1171,
                (WildSpawnType)1172,
                (WildSpawnType)1173
            }
        });

        await Task.CompletedTask;
    }
}

[Injectable]
public class CustomDynamicRouter : DynamicRouter
{
    private static HttpResponseUtil _httpResponseUtil;
    private static ConfigController _configController;

    public CustomDynamicRouter(
        JsonUtil jsonUtil,
        HttpResponseUtil httpResponseUtil,
        ConfigController configController) : base(jsonUtil, GetCustomRoutes())
    {
        _httpResponseUtil = httpResponseUtil;
        _configController = configController;
    }
    private static List<RouteAction> GetCustomRoutes()
    {
        return [
            new RouteAction(
                "/untar/checkpoints",
                async (
                    url,
                    info,
                    sessionID,
                    output
                ) => {
                    var result = _configController.ModConfig;
                    return await new ValueTask<string>(_httpResponseUtil.NoBody(result));
                }
            )
        ];
    }
}

[Injectable]
public class CustomStaticRouter : StaticRouter
{
    private static HttpResponseUtil _httpResponseUtil;
    private static UntarSpawnController _untarSpawnController;

    public CustomStaticRouter(
        UntarSpawnController untarSpawnController,
        JsonUtil jsonUtil,
        HttpResponseUtil httpResponseUtil) : base(jsonUtil, GetCustomRoutes())
    {
        _httpResponseUtil = httpResponseUtil;
        _untarSpawnController = untarSpawnController;
    }

    private static List<RouteAction> GetCustomRoutes()
    {
        return
        [
            new RouteAction(
                "/client/match/local/end",
                async (
                    url,
                    info,
                    sessionID,
                    output
                ) => {
                    _untarSpawnController.AdjustAllUntarSpawns();
                    return await new ValueTask<object>(output ?? string.Empty);
                }
            )
        ];
    }
}