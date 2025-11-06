using BepInEx;

namespace TacticalToasterUNTARGH.Prepatches
{
    [BepInDependency("com.morebotsapiprepatch.tacticaltoaster", BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin(ClientInfo.UNTARGHPreLoadGUID, ClientInfo.UNTARGHPreLoadName, ClientInfo.UNTARGHVersion)]
    public class UNTARPrePatch : BaseUnityPlugin
    {
        public static UNTARPrePatch Instance;

        public void Awake()
        {
            Instance = this;
        }
    }
}
