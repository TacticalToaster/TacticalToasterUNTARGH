using EFT;
using SPT.Reflection.Patching;
using System.Reflection;
using TacticalToasterUNTARGH.Controllers;

namespace TacticalToasterUNTARGH.Patches
{
    public class BotOwnerActivatePatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(BotOwner).GetMethod(nameof(BotOwner.method_10), BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPostfix]
        protected static void PatchPostfix(BotOwner __instance)
        {
            if (!WildSpawnTypeExtensions.IsUNTAR(__instance.Profile.Info.Settings.Role))
                return;

            var manager = __instance.GetOrAddUntarManager();
            manager.OnBotActivate();
        }
    }
}
