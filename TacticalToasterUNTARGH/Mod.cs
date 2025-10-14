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
    public override SemanticVersioning.Version Version { get; init; } = new(2, 0, 0);
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

[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 1)]
public class UNTARModDBLoad(
    DatabaseService databaseService,
    UNTARLogger logger,
    UntarDBController untarDBController,
    UntarSpawnController untarSpawnController
    ) : IOnLoad
{
    Task IOnLoad.OnLoad()
    {
        untarDBController.AddUntarToDB();
        untarSpawnController.AdjustAllUntarSpawns();

        return Task.CompletedTask;
    }
}

[Injectable]
public class CustomDynamicRouter : DynamicRouter
{
    private static HttpResponseUtil _httpResponseUtil;
    private static UntarDBController _untarDBController;

    public CustomDynamicRouter(
        JsonUtil jsonUtil,
        HttpResponseUtil httpResponseUtil,
        UntarDBController untarDBController) : base(jsonUtil, GetCustomRoutes())
    {
        _httpResponseUtil = httpResponseUtil;
        _untarDBController = untarDBController;
    }
    private static List<RouteAction> GetCustomRoutes()
    {
        return [
            new RouteAction(
                "/singleplayer/settings/bot/difficulties",
                async (
                    url,
                    info,
                    sessionID,
                    output
                ) => {
                    var result = _untarDBController.getBotDifficulties(url, (EmptyRequestData)info, sessionID, output);
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