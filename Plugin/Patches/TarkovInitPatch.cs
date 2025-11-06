using DrakiaXYZ.BigBrain.Brains;
using EFT;
using EFT.InputSystem;
using SPT.Reflection.Patching;
using System.Collections.Generic;
using System.Reflection;
using TacticalToasterUNTARGH.Behavior.Layers;

namespace TacticalToasterUNTARGH.Patches
{
    public class TarkovInitPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(TarkovApplication).GetMethod(nameof(TarkovApplication.Init), BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPostfix]
        protected static void PatchPostfix(IAssetsManager assetsManager, InputTree inputTree)
        {
            var untarBrainList = new List<string>() { "PMC", "ExUsec" };
            var untarTypes = new List<int>() { 1170, 1171, 1172, 1173 }.ConvertAll(x => (WildSpawnType)x);

            BrainManager.AddCustomLayer(typeof(GoToCheckpointLayer), untarBrainList, 4, untarTypes);
        }
    }
}
