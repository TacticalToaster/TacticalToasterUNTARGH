using SPTarkov.DI.Annotations;
using SPTarkov.Server;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Helpers.Server;
using SPTarkov.Server.Core.Models.Eft.Common;
using SPTarkov.Server.Core.Models.Spt.Mod;
using SPTarkov.Server.Core.Services;
using SPTarkov.Server.Core.Utils;
using System.Reflection;
using TacticalToasterUNTARGH.Controllers;

namespace TacticalToasterUNTARGH;

public record ModMetadata : IModMetadata
{
    public string ModGuid { get; init; } = "com.untargh.tacticaltoaster";
    public string Name { get; init; } = "UNTAR Go Home!";
    public string Author { get; init; } = "TacticalToaster";
    public List<string>? Contributors { get; init; } = new() { };
    public SemanticVersioning.Version Version { get; init; } = new(3, 2, 1);
    public SemanticVersioning.Range SptVersion { get; init; } = new("~4.1.5");
    public List<string>? Incompatibilities { get; init; }
    public Dictionary<string, SemanticVersioning.Range>? ModDependencies { get; init; } = new()
    {
        { "com.morebotsapi.tacticaltoaster", new SemanticVersioning.Range(">=2.1.1") },
        { "me.sol.sain", new SemanticVersioning.Range("~4.5.1") },
        { "com.wtt.commonlib", new SemanticVersioning.Range(">=3.0.0") }
    };
    public string? Url { get; init; }
    public bool HasPrepatcher { get; init; } = false;
    public string License { get; init; } = "MIT";
}

[Injectable(TypePriority = OnLoadOrder.Preload + 1)]
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

    Task IOnLoad.OnLoadAsync(CancellationToken cancellationToken)
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
    public async Task OnLoadAsync(CancellationToken cancellationToken)
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
        
        if (modList.Any(mod => mod.ModMetadata.ModGuid == "com.blackdiv.tacticaltoaster"))
        {
            factionService.AddWarnByFaction(typeList, "blackdiv");
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
    public async Task OnLoadAsync(CancellationToken cancellationToken)
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
            },
            RevengeAfterRaids = true,
            RevengeRaidAmount = 3
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
                    output,
                    _
                ) => {
                    var result = _configController.ModConfig;
                    return await new ValueTask<string>(_httpResponseUtil.NoBody(result));
                }
            )
        ];
    }
}

[Injectable(TypePriority = OnLoadOrder.Routers + 30)]
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
                    output,
                    _
                ) => {
                    _untarSpawnController.AdjustAllUntarSpawns();
                    return await new ValueTask<object>(output ?? string.Empty);
                }
            )
        ];
    }
}