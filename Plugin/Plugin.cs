using BepInEx;
using BepInEx.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using EFT;
using MoreBotsAPI.Components;
using TacticalToasterUNTARGH.Components;
using TacticalToasterUNTARGH.Patches;
using UnityEngine;

namespace TacticalToasterUNTARGH;

// first string below is your plugin's GUID, it MUST be unique to any other mod. Read more about it in BepInEx docs. Be sure to update it if you copy this project.
[BepInDependency("xyz.drakia.bigbrain", BepInDependency.DependencyFlags.SoftDependency)]
[BepInDependency("me.sol.sain", BepInDependency.DependencyFlags.SoftDependency)]
[BepInDependency("com.morebotsapi.tacticaltoaster")]
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

        new TarkovInitPatch().Enable();
        new BotOwnerActivatePatch().Enable();
        new BotsControllerInitPatch().Enable();
        new UNTARShootGroundWarnPatch().Enable();
        
        var untarEnums = new List<int> { 1170, 1171, 1172, 1173 }
            .ConvertAll(x => (WildSpawnType)x);

        MonoBehaviourSingleton<HuntManager>.Instance.AddHuntRoles(new List<WildSpawnType>()
        {
            WildSpawnType.pmcBot
        }, untarEnums);

        this.GetOrAddComponent<UntarCheckpointManager>();
    }
}
