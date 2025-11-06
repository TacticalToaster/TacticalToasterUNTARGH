using EFT;
using System.Collections.Generic;

namespace TacticalToasterUNTARGH
{
    public static class WildSpawnTypeExtensions
    {
        public static List<int> UNTAREnums = new List<int> { 1170, 1171, 1172, 1173 };

        public static bool IsUNTAR(WildSpawnType type)
        {
            return UNTAREnums.Contains((int)type);
        }
    }
}
