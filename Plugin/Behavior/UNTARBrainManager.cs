using DrakiaXYZ.BigBrain.Brains;
using EFT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TacticalToasterUNTARGH.Behavior.Layers;

namespace TacticalToasterUNTARGH.Behavior
{
    public static class UNTARBrainManager
    {
        private static readonly string[] commonVanillaLayersToRemove = new string[]
        {
            "Help",
            "AdvAssaultTarget",
            "Hit",
            "Simple Target",
            "Pmc",
            "AssaultHaveEnemy",
            "Assault Building",
            "Enemy Building",
            "PushAndSup",
            "Pursuit",
        };

        public static void AddUntarBrainLayers()
        {
            var untarBrainList = new List<string>() { "PMC", "FollowerGluharProtect", "ExUsec", "Assault" };
            var untarTypes = UNTAREnums.UNTARTypes.ConvertAll<WildSpawnType>(type => (WildSpawnType)type.wildSpawnType);

            BrainManager.AddCustomLayer(typeof(GoToCheckpointLayer), untarBrainList, 4, untarTypes);
        }

        public static void AddUntarBrainLayersSAIN()
        {
            var untarBrainList = new List<string>() { "PMC", "FollowerGluharProtect", "ExUsec", "Assault" };
            var untarTypes = UNTAREnums.UNTARTypes.ConvertAll<WildSpawnType>(type => (WildSpawnType)type.wildSpawnType);

            var layers = new List<string>()
            {
                "Request",
                //"FightReqNull",
                //"PeacecReqNull",
                "KnightFight",
                //"PtrlBirdEye",
				"PmcBear",
                "PmcUsec",
                "StationaryWS"
            };
            layers.AddRange(commonVanillaLayersToRemove);

            BrainManager.RemoveLayers(layers, untarBrainList, untarTypes);
            BrainManager.AddCustomLayer(typeof(GoToCheckpointLayer), untarBrainList, 4, untarTypes);

            Plugin.LogSource.LogMessage("Removed common vanilla layers from UNTAR brains.");
        }
    }
}
