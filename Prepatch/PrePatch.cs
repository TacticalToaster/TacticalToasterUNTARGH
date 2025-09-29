using BepInEx;
using System.Collections.Generic;

namespace TacticalToasterUNTARGH.Prepatches
{
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
