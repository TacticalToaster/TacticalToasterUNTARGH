using EFT;
using HarmonyLib;
using SPT.Reflection.Patching;
using System.Linq;
using System.Reflection;
using TacticalToasterUNTARGH.Prepatches;
using UnityEngine;

namespace TacticalToasterUNTARGH.Patches;

[HarmonyPatch]
internal class UNTARPatch : ModulePatch
{
    protected override MethodBase GetTargetMethod() =>
        typeof(GClass598).GetMethod("smethod_1", BindingFlags.Static | BindingFlags.Public);

    [PatchPrefix]
    private static bool Smethod1Prefix(BotDifficulty d, WildSpawnType role, bool external, ref BotSettingsComponents __result)
    {
        if (UNTAREnums.UNTARTypesDict.ContainsKey((int)role))//if (role == (WildSpawnType)UNTAREnums.BotUNTARValue) // GOTO: change 1170 to a variable defined in a common place
        {
            if (Plugin.untarText != null)
            {
                __result = BotSettingsComponents.Create(Plugin.untarText.text);

                //BotSettingsRepoClass.smethod_0(UNTARWildSpawnTypePatch.suitableList.Select(i => (WildSpawnType)i).ToList());
                return false;
            }
            else
            {
                Debug.LogError($"Failed to load UNTAR settings text asset.");
            }
        }
        return true;
    }
}
