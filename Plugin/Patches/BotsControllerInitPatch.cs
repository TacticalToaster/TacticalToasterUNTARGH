using EFT;
using SPT.Reflection.Patching;
using System.Reflection;
using TacticalToasterUNTARGH.Components;

namespace TacticalToasterUNTARGH.Patches
{
    public class BotsControllerInitPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(BotsController).GetMethod(nameof(BotsController.Init), BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPostfix]
        protected static void PatchPostfix(BotsController __instance)
        {
            Plugin.LogSource.LogInfo("BotsController initialized, initializing UntarCheckpointManager...");
            MonoBehaviourSingleton<UntarCheckpointManager>.Instance.InitRaid();
        }
    }
}
