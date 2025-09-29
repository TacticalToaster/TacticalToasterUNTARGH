using Comfort.Common;
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
    public class UNTARFenceLoyaltyPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod() =>
        AccessTools.Method(typeof(BotGroupWarnData), nameof(BotGroupWarnData.method_9));

        [PatchPostfix]
        public static void PatchPostfix(ref bool __result, BotGroupWarnData __instance, Player enemyInfo)
        {
            var role = __instance.Boss.Profile.Info.Settings.Role;

            //Logger.LogMessage($"I got a sus... {role}");

            if (role.IsUNTAR())
            {
                //Logger.LogInfo($"UNTAR bot {__instance.Boss.Profile.Nickname} should warn {enemyInfo.Profile.Nickname} {__result}.");
                __result = false;
            }

            //Logger.LogMessage($"Should be normal. {role}");
        }
    }

    // GOTO: Figure out why enabling and having these uncommented broke the above patch and made it so the patch wasn't applied to warn logic.

    /*[HarmonyPatch]
    public class UNTARWarnmethod8 : ModulePatch
    {
        protected override MethodBase GetTargetMethod() =>
        AccessTools.Method(typeof(BotGroupWarnData), nameof(BotGroupWarnData.method_8));

        [PatchPostfix]
        public static void PatchPostfix(BotGroupWarnData __instance, Dictionary<int, BotSettingsClass> warnTargets)
        {
            var player = Singleton<GameWorld>.Instance.AllAlivePlayersList[0];
            var usecWarn = (player.Profile.Info.Side == EPlayerSide.Usec && __instance.Boss.Settings.FileSettings.Mind.DEFAULT_USEC_BEHAVIOUR.HasFlag(EWarnBehaviour.Warn));
            var longBool = !player.AIData.IsAI && ((player.Profile.Info.Side == EPlayerSide.Bear && __instance.Boss.Settings.FileSettings.Mind.DEFAULT_BEAR_BEHAVIOUR.HasFlag(EWarnBehaviour.Warn)) || (player.Profile.Info.Side == EPlayerSide.Usec && __instance.Boss.Settings.FileSettings.Mind.DEFAULT_USEC_BEHAVIOUR.HasFlag(EWarnBehaviour.Warn)) || (player.Profile.Info.Side == EPlayerSide.Savage && __instance.Boss.Settings.FileSettings.Mind.DEFAULT_SAVAGE_BEHAVIOUR.HasFlag(EWarnBehaviour.Warn)));
            Logger.LogMessage($"In method8 {(int)__instance.Boss.Profile.Info.Settings.Role} {!__instance.method_9(player)} {!player.AIData.IsAI} {longBool} {usecWarn}");
            foreach (var bot in warnTargets.Values.ToList())
            {
                Logger.LogWarning($"Bot in warn list: {bot.Player.Id} {bot.Player.IsAI} {bot.Player.AIData.BotOwner.Profile.Nickname} {bot.Player.IsYourPlayer} {bot.Player.AIData.BotOwner.Profile.Info.Settings.Role}.");
            }
        }
    }

    [HarmonyPatch]
    public class UNTARWarnmethod10 : ModulePatch
    {
        protected override MethodBase GetTargetMethod() =>
        AccessTools.Method(typeof(BotGroupWarnData), nameof(BotGroupWarnData.method_10));

        [PatchPostfix]
        public static void PatchPostfix(BotGroupWarnData __instance, ref List<BotSettingsClass> __result)
        {
            foreach (var bot in __result.ToList())
            {
                Logger.LogWarning($"Bot in warn list: {bot.Player.IsAI} {bot.Player.AIData.BotOwner.Profile.Nickname} {bot.Player.Id} {bot.Player.AIData.BotOwner.Profile.Info.Settings.Role}.");
            }
        }
    }*/
}
