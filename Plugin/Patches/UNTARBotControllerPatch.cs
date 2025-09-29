using EFT;
using HarmonyLib;
using SPT.Reflection.Patching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TacticalToasterUNTARGH.Prepatches;

namespace TacticalToasterUNTARGH.Patches
{
    [HarmonyPatch]
    public class UNTARBotControllerPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod() =>
        AccessTools.Method(typeof(BotSettingsRepoClass), nameof(BotSettingsRepoClass.Init));

        static bool hasRun = false;

        [PatchPostfix]
        public static void PatchPostfix()
        {
            if (hasRun) return;

            var newList = new List<WildSpawnType>();

            foreach (int val in UNTARWildSpawnTypePatch.suitableList)
            {
                Logger.LogInfo($"Adding {val} to BotSettingsRepo suitable list.");
                newList.Add((WildSpawnType)val);
            }

            hasRun = true;

            BotSettingsRepoClass.smethod_0(newList);
        }
    }
}
