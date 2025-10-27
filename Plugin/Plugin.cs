using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using EFT;
using HarmonyLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TacticalToasterUNTARGH.Components;
using TacticalToasterUNTARGH.Interop;
using TacticalToasterUNTARGH.Patches;
using UnityEngine;

namespace TacticalToasterUNTARGH;

// first string below is your plugin's GUID, it MUST be unique to any other mod. Read more about it in BepInEx docs. Be sure to update it if you copy this project.
[BepInDependency("xyz.drakia.bigbrain", "1.3.2")]
[BepInDependency("me.sol.sain", BepInDependency.DependencyFlags.SoftDependency)]
[BepInPlugin(ClientInfo.UNTARGHGUID, ClientInfo.UNTARGHPluginName, ClientInfo.UNTARGHVersion)]
public class Plugin : BaseUnityPlugin
{
    public static ManualLogSource LogSource;

    //public static string modPath = Path.Combine(Environment.CurrentDirectory, "user", "mods", "UNTARGH");
    public static string pluginPath = Path.Combine(Environment.CurrentDirectory, "BepInEx", "plugins", "UNTARGH");
    public static string resourcePath = Path.Combine(pluginPath, "Resources");
    public static string untarJsonPath = Path.Combine(pluginPath, "normalUNTARSettings.json");
    internal static TextAsset untarText;

    // BaseUnityPlugin inherits MonoBehaviour, so you can use base unity functions like Awake() and Update()
    private void Awake()
    {
        // save the Logger to variable so we can use it elsewhere in the project
        LogSource = Logger;
        LogSource.LogInfo("plugin loaded!");

        FieldInfo excludedDifficultiesField = typeof(LocalBotSettingsProviderClass).GetField("Dictionary_1", BindingFlags.Static | BindingFlags.Public) ?? throw new InvalidOperationException("ExcludedDifficulties field not found.");
        var excludedDifficulties = (Dictionary<WildSpawnType, List<BotDifficulty>>)excludedDifficultiesField.GetValue(null);

        var untarDifficulties = new List<BotDifficulty> {
            BotDifficulty.easy,
            BotDifficulty.hard,
            BotDifficulty.impossible
        };

        foreach (var botType in UNTAREnums.UNTARTypes)
        {
            if (!excludedDifficulties.ContainsKey((WildSpawnType)botType.wildSpawnType))
            {
                excludedDifficulties.Add((WildSpawnType)botType.wildSpawnType, untarDifficulties);
                Logger.LogInfo($"Successfully added {botType.typeName} to the excluded difficulties list");
            }
            Traverse.Create(typeof(BotSettingsRepoClass)).Field<Dictionary<WildSpawnType, GClass790>>("Dictionary_0").Value.Add((WildSpawnType)botType.wildSpawnType, new GClass790(true, true, false, $"ScavRole/{botType.scavRole}", (ETagStatus)0));
        }

        /*if (!excludedDifficulties.ContainsKey((WildSpawnType)UNTAREnums.BotUNTARValue))
        {
            excludedDifficulties.Add((WildSpawnType)UNTAREnums.BotUNTARValue, untarDifficulties);
            Logger.LogInfo("Successfully added UNTAR to the excluded difficulties list");
        }
        Traverse.Create(typeof(BotSettingsRepoClass)).Field<Dictionary<WildSpawnType, GClass769>>("dictionary_0").Value.Add((WildSpawnType)UNTAREnums.BotUNTARValue, new GClass769(true, true, false, $"ScavRole/{UNTAREnums.BotUNTARRole}", (ETagStatus)0));*/

        LoadUNTARSettings();

        bool sainLoaded = Chainloader.PluginInfos.ContainsKey("me.sol.sain");

        if (sainLoaded)
        {
            Logger.LogMessage("SAIN detected, initializing SAIN interop for UNTARGH.");
            new SAINInterop().Init();
            //new SAINPatch().Enable();
        }
        else
        {
            Logger.LogMessage("SAIN not detected, skipping SAIN interop for UNTARGH.");
        }

        new TarkovInitPatch().Enable();
        new FixRaidEndSpawnType().Enable();
        new UNTARRolePatch().Enable();
        new UNTARBotControllerPatch().Enable();
        new UNTARShootGroundWarnPatch().Enable();
        new UNTARFenceLoyaltyPatch().Enable();
        new BotOwnerActivatePatch().Enable();
        new BotsControllerInitPatch().Enable();

        int oldWildSpawnTypeConverter = Array.FindIndex<JsonConverter>(JsonSerializerSettingsClass.Converters, c => c.GetType() == typeof(GClass1866<WildSpawnType>));
        LogSource.LogInfo($"Old WildSpawnTypeFromInt converter index: {oldWildSpawnTypeConverter} {JsonSerializerSettingsClass.Converters[oldWildSpawnTypeConverter]}");
        JsonSerializerSettingsClass.Converters[oldWildSpawnTypeConverter] = new WildSpawnTypeFromInt<WildSpawnType>(true);

        /*JsonConverter[] newArray = new JsonConverter[JsonSerializerSettingsClass.Converters.Length + 1];
        newArray[0] = new WildSpawnTypeFromInt<WildSpawnType>(false);
        Array.Copy(JsonSerializerSettingsClass.Converters, 0, newArray, 1, JsonSerializerSettingsClass.Converters.Length);

        JsonSerializerSettingsClass.SerializerSettings.Converters = newArray;*/

        //JsonSerializerSettingsClass.Converters.AddItem(new WildSpawnTypeFromInt<WildSpawnType>(false));
        //JsonSerializerSettingsClass.Converters.

        this.GetOrAddComponent<UntarCheckpointManager>();
    }

    public static void LoadUNTARSettings()
    {
        if (File.Exists(untarJsonPath))
        {
            try
            {
                string untarSettingsJson = File.ReadAllText(untarJsonPath);
                untarText = new TextAsset(untarSettingsJson);
                LogSource.LogInfo("UNTAR bot settings loaded successfully");
            }
            catch (Exception ex)
            {
                LogSource.LogError($"Error loading UNTAR bot settings from {untarJsonPath}: {ex.Message}");
            }
        }
        else
        {
            LogSource.LogInfo($"UNTAR bot settings file not found at {untarJsonPath}");
        }
    }
}
