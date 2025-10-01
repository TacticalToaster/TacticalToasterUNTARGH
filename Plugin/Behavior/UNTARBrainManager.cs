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
        public static void AddUntarBrainLayers()
        {
            var untarBrainList = new List<string>() { "UNTAR" };
            var untarTypes = UNTAREnums.UNTARTypes.ConvertAll<WildSpawnType>(type => (WildSpawnType)type.wildSpawnType);
            //BrainManager.AddCustomLayer(typeof(GoToCheckpointLayer), new List<string>() { "PMC", "ExUsec" }, 99, UNTAREnums.UNTARTypes.ConvertAll<WildSpawnType>(type => (WildSpawnType)type.wildSpawnType));
            BrainManager.AddCustomLayer(typeof(GoToCheckpointLayer), untarBrainList, 4, untarTypes);
            //BrainManager.AddCustomLayer(typeof(GClass132), untarBrainList, 98);

            //BrainManager.RemoveLayer("GoToCheckpoint", untarBrainList);
            //BrainManager.RestoreLayer("GoToCheckpoint", untarBrainList, untarTypes);
        }
    }
}
