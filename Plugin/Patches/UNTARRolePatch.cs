using BepInEx.Logging;
using Comfort.Common;
using EFT;
using EFT.InputSystem;
using HarmonyLib;
using SPT.Reflection.Patching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DrakiaXYZ.BigBrain;
using TacticalToasterUNTARGH.Behavior;
using TacticalToasterUNTARGH.Behavior.Brains;

namespace TacticalToasterUNTARGH.Patches
{
    public class UNTARRolePatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(StandartBotBrain), nameof(StandartBotBrain.Activate));
        }

        private static BaseBrain GetUNTARBrain(BotOwner botOwner)
        {
            // GOTO: After I talk with Solarint and see if custom brain support can be added to SAIN, I'll assign the custom UNTAR brain here
            return new ExUsecBrainClass(botOwner);//new UNTARBrain(botOwner, false);//GClass340(botOwner, false);
        }

        private static AICoreAgentClass<BotLogicDecision> GetUNTARAgent(BotOwner botOwner, BaseBrain brain, StandartBotBrain __instance)
        {
            var name = botOwner.name + " " + botOwner.Profile.Info.Settings.Role.ToString();
            return new AICoreAgentClass<BotLogicDecision>(botOwner.BotsController.AICoreController, brain, NodeCreator.ActionsList(botOwner), botOwner.gameObject, name, new Func<BotLogicDecision, BotNodeAbstractClass>(__instance.method_0));
        }

        [PatchPostfix]
        [HarmonyPriority(Priority.First)] // Make sure this runs after BigBrain so we can override it
        //[HarmonyBefore(["xyz.drakia.bigbrain"])]
        public static void PatchPostfix(StandartBotBrain __instance, BotOwner ___botOwner_0)
        {
            if (UNTAREnums.UNTARTypesDict.ContainsKey((int)___botOwner_0.Profile.Info.Settings.Role))
            {
                Logger.LogMessage("Inserting our UNTAR brain. How exciting!");
                __instance.BaseBrain = GetUNTARBrain(___botOwner_0);
                __instance.Agent = GetUNTARAgent(___botOwner_0, __instance.BaseBrain, __instance);

                // This isn't critical, only knight, sanitar, and the attack event (khorvod generators, spring event, etc) behaviors use this
                //__instance.OnSetStrategy?.Invoke(__instance.BaseBrain);
            }

            //return false;
        }
        /*public static bool PatchPrefix(out WildSpawnType __state, StandartBotBrain __instance, BotOwner ___botOwner_0)
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
        }*/

        /*[PatchPostfix]
        public static void PatchPostfix(WildSpawnType __state, BotOwner ___botOwner_0)
        {
            if (UNTAREnums.UNTARTypesDict.ContainsKey((int)__state))//if (__state == (WildSpawnType)UNTAREnums.BotUNTARValue)
            {
                //Logger.LogInfo($"Restoring role back to {__state} for UNTAR bot.");
                ___botOwner_0.Profile.Info.Settings.Role = __state;
            }
        }*/
    }
}
