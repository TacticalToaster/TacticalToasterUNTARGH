using BepInEx.Logging;
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

namespace TacticalToasterUNTARGH.Patches
{
    public class UNTARRolePatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(StandartBotBrain), nameof(StandartBotBrain.Activate));
        }

        [PatchPrefix]
        public static bool PatchPrefix(out WildSpawnType __state, StandartBotBrain __instance, BotOwner ___botOwner_0)
        {
            __state = ___botOwner_0.Profile.Info.Settings.Role;

            //Logger.LogInfo($"Checking if {__state} is {(WildSpawnType)UNTAREnums.BotUNTARValue} {___botOwner_0.Profile.Info.Settings.IsBossOrFollower()}.");

            try
            {
                if (UNTAREnums.UNTARTypesDict.ContainsKey((int)__state))//if (__state == (WildSpawnType)UNTAREnums.BotUNTARValue)
                {
                    var untarBot = UNTAREnums.UNTARTypesDict[(int)__state];
                    //Logger.LogInfo($"Changing role from {__state} to {(WildSpawnType)untarBot.brain} for UNTAR bot.");
                    ___botOwner_0.Profile.Info.Settings.Role = (WildSpawnType)untarBot.brain; // WildSpawnType.followerSanitar;
                    //Logger.LogInfo($"Checking {___botOwner_0.Profile.Info.Settings.IsBossOrFollower()}.");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in UNTARRolePatch Prefix: {ex.Message}");
            }

            return true;
        }

        [PatchPostfix]
        public static void PatchPostfix(WildSpawnType __state, BotOwner ___botOwner_0)
        {
            if (UNTAREnums.UNTARTypesDict.ContainsKey((int)__state))//if (__state == (WildSpawnType)UNTAREnums.BotUNTARValue)
            {
                //Logger.LogInfo($"Restoring role back to {__state} for UNTAR bot.");
                ___botOwner_0.Profile.Info.Settings.Role = __state;

                if (Singleton<GameWorld>.Instance == null)
                {
                    Logger.LogWarning("GameWorld instance is null, cannot check local player.");
                    return;
                }
                var localPlayer = Singleton<GameWorld>.Instance.RegisteredPlayers.FirstOrDefault(x => x.IsYourPlayer);

                if (localPlayer == null)
                {
                    Logger.LogWarning("Local player not found, cannot check if bot is a player threat.");
                    return;
                }

                //Logger.LogInfo($"Checking 2 {___botOwner_0.Profile.Info.Settings.IsBossOrFollower()} {___botOwner_0.Profile.Info.Settings.BotDifficulty} {___botOwner_0.Settings.FileSettings.Mind.DEFAULT_USEC_BEHAVIOUR} {___botOwner_0.Settings.IsPlayerWarn(localPlayer)} {BotBoss.IsFollowerSuitableForBoss(___botOwner_0.Profile.Info.Settings.Role, (WildSpawnType)1173)} {BotBoss.IsFollowerSuitableForBoss(___botOwner_0.Profile.Info.Settings.Role, (WildSpawnType)1170)}.");
            }
        }
    }
}
