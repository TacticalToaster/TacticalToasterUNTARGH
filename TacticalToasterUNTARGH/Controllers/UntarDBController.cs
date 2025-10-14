using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Controllers;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Eft.Common;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Models.Eft.ItemEvent;
using SPTarkov.Server.Core.Models.Enums;
using SPTarkov.Server.Core.Models.Spt.Config;
using SPTarkov.Server.Core.Models.Spt.Logging;
using SPTarkov.Server.Core.Servers;
using SPTarkov.Server.Core.Services;
using SPTarkov.Server.Core.Utils;
using SPTarkov.Server.Core.Utils.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TacticalToasterUNTARGH.Models;

namespace TacticalToasterUNTARGH.Controllers;

[Injectable(InjectionType.Singleton)]
public class UntarDBController
{
    private readonly ItemHelper _itemHelper;
    private readonly JsonUtil _jsonUtil;
    private readonly RandomUtil _randomUtil;
    private readonly ConfigServer _configServer;
    private readonly DatabaseService _databaseService;
    private readonly ILogger _wLogger;
    private readonly HttpResponseUtil _httpResponse;
    private readonly BotHelper _botHelper;
    private readonly BotController _botController;
    private readonly ConfigController _configController;
    private readonly ModHelper _modHelper;
    private readonly UNTARLogger _logger;

    public UntarDBController(
        ModHelper modHelper,
        ItemHelper itemHelper,
        JsonUtil jsonUtil,
        RandomUtil randomUtil,
        ConfigServer configServer,
        DatabaseService databaseService,
        ILogger wLogger,
        HttpResponseUtil httpResponse,
        BotHelper botHelper,
        BotController botController,
        ConfigController configController,
        UNTARLogger logger)
    {
        _modHelper = modHelper;
        _itemHelper = itemHelper;
        _jsonUtil = jsonUtil;
        _randomUtil = randomUtil;
        _configServer = configServer;
        _databaseService = databaseService;
        _wLogger = wLogger;
        _httpResponse = httpResponse;
        _botHelper = botHelper;
        _botController = botController;
        _configController = configController;
        _logger = logger;
    }

    public void AddUntarToFactory()
    {
        _logger.Info("Creating UNTAR spawn!");

        var dbTables = _databaseService.GetTables();
        var bossInfo = new BossLocationSpawn
        {
            BossChance = 100,
            BossDifficulty = "normal",
            BossEscortAmount = "1",
            BossEscortDifficulty = "normal",
            BossEscortType = "followeruntar",
            BossName = "bossuntarofficer",
            IsBossPlayer = false,
            BossZone = "BotZone",
            ForceSpawn = false,
            IgnoreMaxBots = true,
            IsRandomTimeSpawn = false,
            SpawnMode = new[] { "regular", "pve" },
            Supports = new List<BossSupport>
            {
                new BossSupport
                {
                    BossEscortAmount = "1",
                    BossEscortDifficulty = new ListOrT<string>(null, "normal"),
                    BossEscortType = "bossuntarlead"
                },
                new BossSupport
                {
                    BossEscortAmount = "8",
                    BossEscortDifficulty = new ListOrT<string>(null, "normal"),
                    BossEscortType = "followeruntar"
                }
            },
            Time = -1,
            TriggerId = "",
            TriggerName = ""
        };

        dbTables.Locations.Factory4Day.Base.BossLocationSpawn.Clear();
        dbTables.Locations.Factory4Day.Base.BossLocationSpawn.Add(bossInfo);

        _logger.Info("UNTAR spawn should be created!");
    }

    public void AddUntarToDB()
    {
        _logger.Info("Adding UNTAR to database alongside enums.");

        /*WildSpawnType.FOLLOWERUNTAR = 1170;
        WildSpawnTypeNumber.BOSSUNTARLEAD = 1171;
        WildSpawnTypeNumber.FOLLOWERUNTARMARKSMAN = 1172;
        WildSpawnTypeNumber.BOSSUNTAROFFICER = 1173;*/
        //WildSpawnType

        _logger.Info("Extended WildSpawnTypeNumber enum with UNTAR types.");

        var pathToMod = _modHelper.GetAbsolutePathToModFolder(Assembly.GetExecutingAssembly());
        var pathToBotsDb = System.IO.Path.Combine(pathToMod, "db", "bots");
        var untarBotInfo = _modHelper.GetJsonDataFromFile<EquipmentFilters>(System.IO.Path.Combine(pathToBotsDb, "info"), "untarInfo.json");

        var dbTables = _databaseService.GetTables();
        var botConfig = _configServer.GetConfig<BotConfig>();
        var presetBatch = botConfig.PresetBatch;

        botConfig.Equipment["followeruntar"] = untarBotInfo;
        botConfig.ItemSpawnLimits["followeruntar"] = new Dictionary<MongoId, double>();
        presetBatch["followeruntar"] = 55;
        dbTables.Bots.Types["followeruntar"] = _modHelper.GetJsonDataFromFile<BotType>(System.IO.Path.Combine(pathToBotsDb, "types"), "test.json");

        botConfig.Equipment["followeruntarmarksman"] = untarBotInfo;
        botConfig.ItemSpawnLimits["followeruntarmarksman"] = new Dictionary<MongoId, double>();
        presetBatch["followeruntarmarksman"] = 55;
        dbTables.Bots.Types["followeruntarmarksman"] = _modHelper.GetJsonDataFromFile<BotType>(System.IO.Path.Combine(pathToBotsDb, "types"), "test.json");

        botConfig.Equipment["bossuntarlead"] = untarBotInfo;
        botConfig.ItemSpawnLimits["bossuntarlead"] = new Dictionary<MongoId, double>();
        presetBatch["bossuntarlead"] = 55;
        dbTables.Bots.Types["bossuntarlead"] = _modHelper.GetJsonDataFromFile<BotType>(System.IO.Path.Combine(pathToBotsDb, "types"), "untarlead.json");

        botConfig.Equipment["bossuntarofficer"] = untarBotInfo;
        botConfig.ItemSpawnLimits["bossuntarofficer"] = new Dictionary<MongoId, double>();
        presetBatch["bossuntarofficer"] = 55;
        dbTables.Bots.Types["bossuntarofficer"] = _modHelper.GetJsonDataFromFile<BotType>(System.IO.Path.Combine(pathToBotsDb, "types"), "untarofficer.json");

        ProcessLoadouts(
            "followeruntar",
            dbTables.Bots.Types["followeruntar"],
            _modHelper.GetJsonDataFromFile<LoadoutInfo>(System.IO.Path.Combine(pathToBotsDb, "loadouts"), "followeruntar.json")
        );
        ProcessLoadouts(
            "followeruntarmarksman",
            dbTables.Bots.Types["followeruntarmarksman"],
            _modHelper.GetJsonDataFromFile<LoadoutInfo>(System.IO.Path.Combine(pathToBotsDb, "loadouts"), "followeruntar.json")
        );
        ProcessLoadouts(
            "bossuntarlead",
            dbTables.Bots.Types["bossuntarlead"],
            _modHelper.GetJsonDataFromFile<LoadoutInfo>(System.IO.Path.Combine(pathToBotsDb, "loadouts"), "bossuntarlead.json")
        );
        ProcessLoadouts(
            "bossuntarofficer",
            dbTables.Bots.Types["bossuntarofficer"],
            _modHelper.GetJsonDataFromFile<LoadoutInfo>(System.IO.Path.Combine(pathToBotsDb, "loadouts"), "bossuntarofficer.json")
        );

        foreach (var locale in _databaseService.GetLocales().Global.Values)
        {
            //locale["ScavRole/UNTAR"] = "UNTAR";
            locale.AddTransformer(lazyLoadedLocaleData =>
            {
                lazyLoadedLocaleData.Add("ScavRole/UNTAR", "UNTAR");

                return lazyLoadedLocaleData;
            });
        }

        _logger.Info("UNTAR should be in database.");
    }

    public Dictionary<string, Dictionary<string, DifficultyCategories>>? getBotDifficulties(string url, EmptyRequestData info, string sessionID, string output)
    {
        try
        {
            var dbTables = _databaseService.GetTables();
            var botDifficulties = dbTables.Bots.Types;
            string[] untarTypes = { "followeruntar", "followeruntarmarksman", "bossuntarlead", "bossuntarofficer" };

            Dictionary<string, Dictionary<string, DifficultyCategories>> result = new();

            if (output != null && output != string.Empty)
            {
                result = _jsonUtil.Deserialize<Dictionary<string, Dictionary<string, DifficultyCategories>>>(output);
            }

            if (botDifficulties == null || !botDifficulties.Any())
            {
                _logger.Warn("Bot difficulties data is missing or empty.");
                return null;
            }

            foreach (var botType in untarTypes)
            {
                var botData = botDifficulties.ContainsKey(botType) ? botDifficulties[botType] : null;

                if (!result.ContainsKey(botType))
                {
                    result[botType] = new Dictionary<string, DifficultyCategories>();
                }

                result[botType]["easy"] = botData.BotDifficulty["normal"];
                result[botType]["normal"] = botData.BotDifficulty["normal"];
                result[botType]["hard"] = botData.BotDifficulty["normal"];
                result[botType]["impossible"] = botData.BotDifficulty["normal"];
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.Error($"Error retrieving bot difficulties: {ex.Message}");
            return null;
        }
    }

    private T DeepMerge<T>(T target, T source)
    {
        if (target == null || source == null)
            return target;

        var result = _jsonUtil.Deserialize<T>(_jsonUtil.Serialize<T>(target));

        foreach (var property in source.GetType().GetProperties())
        {
            var sourceValue = property.GetValue(source);
            var targetValue = property.GetValue(result);

            if (sourceValue != null && targetValue != null &&
                sourceValue.GetType().IsClass && !sourceValue.GetType().IsArray)
            {
                property.SetValue(result, DeepMerge(targetValue, sourceValue));
            }
            else
            {
                property.SetValue(result, sourceValue);
            }
        }

        return result;
    }

    private LoadoutInfo LoadCommonLoadouts()
    {
        var pathToMod = _modHelper.GetAbsolutePathToModFolder(Assembly.GetExecutingAssembly());
        var commonDir = System.IO.Path.Combine(pathToMod, "db", "bots", "loadouts", "common");
        var combinedLoadout = new LoadoutInfo
        {
            Equipment = new Dictionary<string, Dictionary<string, LoadoutItem>>(),
            Weapons = new Dictionary<string, Dictionary<string, List<string>>>(),
            Categories = new Dictionary<string, Dictionary<string, LoadoutItem>>()
        };

        if (!Directory.Exists(commonDir))
        {
            _logger.Warn($"Common loadouts directory not found: {commonDir}");
            return combinedLoadout;
        }

        var files = Directory.GetFiles(commonDir, "*.json");
        _logger.Info($"Found {files.Length} common loadout file(s) to merge");

        foreach (var file in files)
        {
            try
            {
                var fileContent = File.ReadAllText(file);
                var loadoutData = _jsonUtil.Deserialize<LoadoutInfo>(fileContent);

                combinedLoadout = DeepMerge(combinedLoadout, loadoutData);
                _logger.Info($"Merged common loadout file: {System.IO.Path.GetFileName(file)}");
            }
            catch (Exception ex)
            {
                _logger.Error($"Failed to load common loadout file {System.IO.Path.GetFileName(file)}: {ex.Message}");
            }
        }

        return combinedLoadout;
    }

    public void ProcessLoadouts(string type, BotType botData, LoadoutInfo baseLoadout)
    {
        var commonLoadout = LoadCommonLoadouts();
        var combinedInfo = DeepMerge(commonLoadout, baseLoadout);

        botData.BotInventory.Mods = new Dictionary<MongoId, Dictionary<string, HashSet<MongoId>>>();

        foreach (var slot in combinedInfo.Equipment.Keys)
        {
            var slotData = combinedInfo.Equipment[slot];
            foreach (var item in slotData.Keys)
            {
                var itemData = slotData[item];

                if (Enum.TryParse<EquipmentSlots>(slot, out var equipmentSlot))
                {
                    if (!botData.BotInventory.Equipment.ContainsKey(equipmentSlot))
                    {
                        botData.BotInventory.Equipment[equipmentSlot] = new Dictionary<MongoId, double>();
                    }
                    botData.BotInventory.Equipment[equipmentSlot][itemData.Id] = itemData.Chance ?? 100;
                }

                //botData.BotInventory.Equipment[slot][itemData.Id] = itemData.Chance ?? 100;

                var children = new Dictionary<string, LoadoutItem>();
                ProcessChildren(children, itemData, combinedInfo, type);
                ProcessItem(itemData.Id, itemData.Slots, type, combinedInfo, children);
            }
        }
    }

    public void AddModsToSlot(string item, string slot, List<string> mods, string type)
    {
        var dbTables = _databaseService.GetTables();
        if (!dbTables.Bots.Types[type].BotInventory.Mods.ContainsKey(item))
        {
            dbTables.Bots.Types[type].BotInventory.Mods[item] = new Dictionary<string, HashSet<MongoId>>();
        }

        if (!dbTables.Bots.Types[type].BotInventory.Mods[item].ContainsKey(slot))
        {
            dbTables.Bots.Types[type].BotInventory.Mods[item][slot] = new HashSet<MongoId>();
        }

        foreach (var mod in mods)
        {
            if (!dbTables.Bots.Types[type].BotInventory.Mods[item][slot].Contains(mod))
            {
                dbTables.Bots.Types[type].BotInventory.Mods[item][slot].Add(mod);
            }
        }
    }

    public void ProcessItem(string item, Dictionary<string, List<string>> slots, string type, LoadoutInfo loadoutInfo, Dictionary<string, LoadoutItem> children = null)
    {
        children ??= new Dictionary<string, LoadoutItem>();

        foreach (var slot in slots.Keys)
        {
            var categories = slots[slot];
            foreach (var category in categories)
            {
                var itemsInSlot = ProcessCategoryOrItem(item, category, type, loadoutInfo, children);
                AddModsToSlot(item, slot, itemsInSlot, type);
            }
        }
    }

    public List<string> ProcessCategoryOrItem(string item, string categoryOrItem, string type, LoadoutInfo loadoutInfo, Dictionary<string, LoadoutItem> children = null)
    {
        if (children != null && children.ContainsKey(categoryOrItem))
        {
            return ProcessCategoryOrItem(item, children[categoryOrItem].Id, type, loadoutInfo, children);
        }
        else if (loadoutInfo.Categories.ContainsKey(categoryOrItem))
        {
            return ProcessCategory(item, categoryOrItem, loadoutInfo, type, children);
        }
        else if (_itemHelper.IsItemInDb(categoryOrItem))
        {
            return new List<string> { categoryOrItem };
        }

        return new List<string>();
    }

    public List<string> ProcessCategory(string item, string category, LoadoutInfo loadoutInfo, string type, Dictionary<string, LoadoutItem> children = null)
    {
        children ??= new Dictionary<string, LoadoutItem>();

        var allItems = new List<string>();
        var categoryData = loadoutInfo.Categories[category];

        foreach (var categoryItem in categoryData.Keys)
        {
            var itemData = categoryData[categoryItem];

            ProcessChildren(children, itemData, loadoutInfo, type);
            allItems.Add(itemData.Id);

            if (itemData.Slots != null)
            {
                ProcessItem(itemData.Id, itemData.Slots, type, loadoutInfo, itemData.Children);
            }
        }

        return allItems;
    }

    public void ProcessChildren(Dictionary<string, LoadoutItem> children, LoadoutItem parent, LoadoutInfo loadoutInfo, string type)
    {
        if (parent.Children != null)
        {
            foreach (var child in parent.Children.Keys)
            {
                var childData = parent.Children[child];
                children[child] = childData;
                ProcessChildren(children, childData, loadoutInfo, type);

                if (childData.Slots != null)
                {
                    ProcessItem(childData.Id, childData.Slots, type, loadoutInfo, children);
                }
            }
        }
    }
}