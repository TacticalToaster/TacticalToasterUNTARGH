using BepInEx.Bootstrap;
using EFT;
using EFT.InputSystem;
using SPT.Reflection.Patching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TacticalToasterUNTARGH.Behavior;
using TacticalToasterUNTARGH.Interop;

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
            UNTARBrainManager.AddUntarBrainLayers();

            bool sainLoaded = Chainloader.PluginInfos.ContainsKey("me.sol.sain");

            if (sainLoaded)
            {
                Logger.LogMessage("SAIN detected, initializing SAIN interop for UNTARGH.");
                new SAINInterop().Init();
            }
        }
    }
}
