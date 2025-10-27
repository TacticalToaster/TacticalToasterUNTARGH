using EFT;
using SPT.Reflection.Patching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TacticalToasterUNTARGH.Components;
using TacticalToasterUNTARGH.Prepatches;

namespace TacticalToasterUNTARGH.Patches
{
    public class FixRaidEndSpawnType : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(LocationStatisticsCollectorAbstractClass).GetMethod(nameof(LocationStatisticsCollectorAbstractClass.OnDeath), BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPostfix]
        protected static void PatchPostfix(LocationStatisticsCollectorAbstractClass __instance)
        {
            var role = __instance.Profile_0.EftStats.DeathCause.Role;
            if (role.IsUNTAR())
                __instance.Profile_0.EftStats.DeathCause.Role = WildSpawnType.assault;
        }
    }
}
