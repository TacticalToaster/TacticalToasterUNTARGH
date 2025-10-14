using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Models.Eft.Common;
using SPTarkov.Server.Core.Models.Enums;
using SPTarkov.Server.Core.Models.Spt.Config;
using SPTarkov.Server.Core.Models.Spt.Mod;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Servers;
using SPTarkov.Server.Core.Services;
using SPTarkov.Server.Core.Utils;
using SPTarkov.Server.Core.Utils.Json;

namespace TacticalToasterUNTARGH.Controllers;

[Injectable(InjectionType.Singleton)]
public class UntarSpawnController(
    JsonUtil jsonUtil,
    RandomUtil randomUtil,
    ConfigController configController,
    DatabaseService databaseService,
    UNTARLogger logger,
    HttpResponseUtil httpResponse)
{
    private readonly JsonUtil _jsonUtil = jsonUtil;
    private readonly RandomUtil _randomUtil = randomUtil;
    private readonly DatabaseService _databaseService = databaseService;
    private readonly UNTARLogger _logger = logger;
    private readonly HttpResponseUtil _httpResponse = httpResponse;
    private readonly ConfigController _configController = configController;

    public void AdjustAllUntarSpawns()
    {
        try
        {
            var tables = _databaseService.GetTables();
            var mainConfig = _configController.ModConfig;

            foreach (var map in mainConfig.locations.Keys)
            {
                _logger.Info($"Adjusting UNTAR spawns for {map}.");

                if (!tables.Locations.GetDictionary().ContainsKey(map))
                {
                    _logger.Info($"No location data found for {map}. Skipping UNTAR spawn adjustment.");
                    continue;
                }

                var mapConfig = mainConfig.locations[map];
                var spawns = tables.Locations.GetDictionary()[map].Base.BossLocationSpawn;

                // Remove existing UNTAR spawns
                spawns.RemoveAll(x => x.BossName.Contains("untar"));

                if (mapConfig.enablePatrols)
                {
                    _logger.Info($"Enabling UNTAR patrols for {map}.");
                    var validZones = new List<string>(mapConfig.patrolZones);

                    for (int i = 0; i < mapConfig.patrolAmount; i++)
                    {
                        var patrolSize = _randomUtil.GetInt(mapConfig.patrolMin, mapConfig.patrolMax);
                        var patrol = GeneratePatrol(patrolSize, mainConfig.debug.spawnAlways ? 100 : mapConfig.patrolChance);

                        patrol.BossZone = _randomUtil.GetArrayValue(validZones);
                        validZones.Remove(patrol.BossZone);

                        if (!validZones.Any())
                        {
                            validZones = new List<string>(mapConfig.patrolZones);
                        }

                        patrol.Time = _randomUtil.GetInt(mapConfig.patrolTimeMin, mapConfig.patrolTimeMax);

                        if (mainConfig.debug.spawnInstantlyAlways)
                        {
                            _logger.Info($"Instantly spawning UNTAR patrol for {map}.");
                            patrol.Time = -1;
                        }

                        spawns.Add(patrol);

                        _logger.Info($"Added ({mapConfig.patrolChance}% chance) UNTAR patrol of size {patrolSize} to {map} in zone {patrol.BossZone} with a spawn time of {patrol.Time} seconds.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.Error($"Error adjusting UNTAR spawns: {ex.Message}");
            throw;
        }
    }

    /*
    public string AdjustUntarSpawns(string url, object info, string sessionID, string output)
    {
        try
        {
            var tables = _databaseService.GetTables();
            var appContext = ApplicationContext.Instance;
            var match = appContext.GetLatestValue<IGetRaidConfigurationRequestData>(ContextVariableType.RAID_CONFIGURATION);
            var map = match.Location.ToLower();

            _logger.Info($"Adjusting UNTAR spawns for {map}.");

            if (!tables.Locations.ContainsKey(map))
            {
                _logger.Info($"No location data found for {map}. Skipping UNTAR spawn adjustment.");
                return output;
            }

            var spawns = tables.Locations[map].Base.BossLocationSpawn;

            if (MainConfig.Locations[map].EnablePatrols)
            {
                var mapConfig = MainConfig.Locations[map];
                spawns.RemoveAll(x => x.BossName.Contains("untar"));

                var validZones = new List<string>(mapConfig.PatrolZones);

                for (int i = 0; i < mapConfig.PatrolAmount; i++)
                {
                    var patrolSize = _randomUtil.GetInt(mapConfig.PatrolMin, mapConfig.PatrolMax);
                    var patrol = GeneratePatrol(patrolSize, mapConfig.PatrolChance);

                    patrol.BossZone = _randomUtil.GetArrayValue(validZones);
                    validZones.Remove(patrol.BossZone);

                    if (!validZones.Any())
                    {
                        validZones = new List<string>(mapConfig.PatrolZones);
                    }

                    patrol.Time = _randomUtil.GetInt(mapConfig.PatrolTimeMin, mapConfig.PatrolTimeMax);
                    spawns.Add(patrol);

                    _logger.Info($"Added ({mapConfig.PatrolChance}% chance) UNTAR patrol of size {patrolSize} to {map} in zone {patrol.BossZone} with a spawn time of {patrol.Time} minutes.");
                }
            }
        }
        catch (Exception ex)
        {
            _logger.Error($"Error adjusting UNTAR spawns: {ex.Message}");
            throw;
        }

        return output;
    }
    */

    private BossLocationSpawn GeneratePatrol(int patrolSize, float chance)
    {
        var bossType = "bossuntarlead";
        var secondLeader = string.Empty;
        var followers = patrolSize - 1;
        var mainConfig = _configController.ModConfig;

        _logger.Info($"Generating UNTAR patrol of size {patrolSize}.");

        if (patrolSize >= mainConfig.patrols.minOfficerSize && _randomUtil.GetChance100(mainConfig.patrols.officerChance))
        {
            _logger.Info("UNTAR patrol leader changed to Officer.");
            bossType = "bossuntarofficer";
        }

        if (patrolSize >= mainConfig.patrols.minSecondLeaderSize && _randomUtil.GetChance100(mainConfig.patrols.secondLeaderChance))
        {
            _logger.Info("UNTAR patrol second leader added.");
            secondLeader = "bossuntarlead";
            followers--;
        }

        var supportsList = new List<BossSupport>();

        

        if (!string.IsNullOrEmpty(secondLeader))
        {
            supportsList.Add(new BossSupport
            {
                BossEscortAmount = "1",
                BossEscortDifficulty = new ListOrT<string>(null, "normal"),
                BossEscortType = secondLeader
            });
        }

        supportsList.Add(new BossSupport
        {
            BossEscortAmount = followers.ToString(),
            BossEscortDifficulty = new ListOrT<string>(null, "normal"),
            BossEscortType = "followeruntar"
        });

        var bossInfo = new BossLocationSpawn
        {
            BossChance = chance,
            BossDifficulty = "normal",
            BossEscortAmount = "1",
            BossEscortDifficulty = "normal",
            BossEscortType = "followeruntar",
            BossName = bossType,
            IsBossPlayer = false,
            BossZone = string.Empty,
            ForceSpawn = false,
            IgnoreMaxBots = true,
            IsRandomTimeSpawn = false,
            SpawnMode = new[] { "regular", "pve" },
            Supports = supportsList,
            Time = -1,
            TriggerId = string.Empty,
            TriggerName = string.Empty
        };

        return bossInfo;
    }
}