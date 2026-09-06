using EFT;
using HarmonyLib;
using SPT.Reflection.Patching;
using System.Reflection;
using UnityEngine;

namespace TacticalToasterUNTARGH.Patches
{
    // Make UNTAR bots shoot at the ground near the player when warning them instead of shooting them in the head.
    [HarmonyPatch]
    public class UNTARShootGroundWarnPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod() =>
        AccessTools.Method(typeof(WarnPlayerRequest), nameof(WarnPlayerRequest.CachePointToShoot));

        private static float nextSwitch = 0;
        private static Vector3 shootOffset = Vector3.zero;

        [PatchPrefix]
        public static bool PatchPrefix(WarnPlayerRequest __instance)
        {
            if (!WildSpawnTypeExtensions.IsUNTAR(__instance.Executor.Profile.Info.Settings.Role)) // only affect UNTAR bots
            {
                Logger.LogInfo($"Role {__instance.Executor.Profile.Info.Settings.Role} is not UNTAR, skipping shoot ground warning.");
                return true;
            }

            Logger.LogWarning("Making UNTAR bot shoot at the ground instead of the player when warning.");

            //BifacialTransform bone;
            var vectorBetween = (__instance.playerToWarn.Position - __instance.Executor.Position).normalized * .5f;
            vectorBetween = vectorBetween + __instance.Executor.Position;

            if (Time.time > nextSwitch)
            {
                if (MyExtensions.RandomBool(50)) // shoot ground
                {
                    //bone = __instance.playerToWarn.PlayerBones.LeftThigh2;
                    shootOffset = new Vector3(MyExtensions.Random(-2f, 2f), MyExtensions.Random(-3f, -1f), MyExtensions.Random(-2f, 2f));
                }
                else // shoot sky instead
                {
                    //bone = __instance.playerToWarn.PlayerBones.Head;
                    shootOffset = new Vector3(MyExtensions.Random(-3f, 3f), MyExtensions.Random(5f, 10f), MyExtensions.Random(-3f, 3f));
                }

                nextSwitch = Time.time + 3;
            }
            else
            {
                shootOffset = shootOffset + new Vector3(MyExtensions.Random(-1f, 1f), 0, MyExtensions.Random(-1f, 1f));
            }

            var vector = vectorBetween + shootOffset;

            __instance._cachedPointToShoot = vector;
            __instance.Executor.Steering.LookToPoint(vector);

            return false;
        }
    }
}
