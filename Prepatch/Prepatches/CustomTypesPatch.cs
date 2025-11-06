using Mono.Cecil;
using MoreBotsAPI;
using System.Collections.Generic;

namespace Prepatch.Prepatches
{
    public static class CustomTypesPatch
    {
        public static IEnumerable<string> TargetDLLs { get; } = new[] { "Assembly-CSharp.dll" };

        public static void Patch(ref AssemblyDefinition assembly)
        {
            var untarBrains = new List<string>() { "PMC", "ExUsec" };
            var untarLayers = new List<string>() {
                "Request",
                //"FightReqNull",
                //"PeacecReqNull",
                "KnightFight",
                //"PtrlBirdEye",
				"PmcBear",
                "PmcUsec",
                "StationaryWS",
                "ExURequest"
            };

            int untarBrainInt = 24;//9;

            // rifleman
            var untarBot = new CustomWildSpawnType(1170, "followeruntar", "UNTAR", untarBrainInt, true, true, false);

            untarBot.SetCountAsBossForStatistics(false);
            untarBot.SetShouldUseFenceNoBossAttack(false, false);
            untarBot.SetExcludedDifficulties(new List<int> { 0, 2, 3 });

            SAINSettings settings = new SAINSettings(untarBot.WildSpawnTypeValue)
            {
                Name = "UNTAR Follower",
                Description = "An UNTAR grunt.",
                Section = "UNTAR",
                BaseBrain = "PMC",
                BrainsToApply = untarBrains,
                LayersToRemove = untarLayers
            };

            untarBot.SetSAINSettings(settings);

            CustomWildSpawnTypeManager.RegisterWildSpawnType(untarBot, assembly);

            // senior rifleman
            untarBot = new CustomWildSpawnType(1171, "bossuntarlead", "UNTAR", untarBrainInt, true, true, false);

            untarBot.SetCountAsBossForStatistics(false);
            untarBot.SetShouldUseFenceNoBossAttack(false, false);
            untarBot.SetExcludedDifficulties(new List<int> { 0, 2, 3 });

            settings = new SAINSettings(untarBot.WildSpawnTypeValue)
            {
                Name = "UNTAR Squad Leader",
                Description = "An UNTAR squad leader.",
                Section = "UNTAR",
                BaseBrain = "PMC",
                BrainsToApply = untarBrains,
                LayersToRemove = untarLayers
            };

            untarBot.SetSAINSettings(settings);

            CustomWildSpawnTypeManager.RegisterWildSpawnType(untarBot, assembly);

            // autorifleman
            untarBot = new CustomWildSpawnType(1172, "followeruntarmarksman", "UNTAR", untarBrainInt, true, true, false);

            untarBot.SetCountAsBossForStatistics(false);
            untarBot.SetShouldUseFenceNoBossAttack(false, false);
            untarBot.SetExcludedDifficulties(new List<int> { 0, 2, 3 });

            settings = new SAINSettings(untarBot.WildSpawnTypeValue)
            {
                Name = "UNTAR Marksman",
                Description = "An UNTAR marksman.",
                Section = "UNTAR",
                BaseBrain = "PMC",
                BrainsToApply = untarBrains,
                LayersToRemove = untarLayers
            };

            untarBot.SetSAINSettings(settings);

            CustomWildSpawnTypeManager.RegisterWildSpawnType(untarBot, assembly);

            // grenadier
            untarBot = new CustomWildSpawnType(1173, "bossuntarofficer", "UNTAR", untarBrainInt, true, true, false);

            untarBot.SetCountAsBossForStatistics(false);
            untarBot.SetShouldUseFenceNoBossAttack(false, false);
            untarBot.SetExcludedDifficulties(new List<int> { 0, 2, 3 });

            settings = new SAINSettings(untarBot.WildSpawnTypeValue)
            {
                Name = "UNTAR Officer",
                Description = "An UNTAR officer.",
                Section = "UNTAR",
                BaseBrain = "PMC",
                BrainsToApply = untarBrains,
                LayersToRemove = untarLayers
            };

            untarBot.SetSAINSettings(settings);

            CustomWildSpawnTypeManager.RegisterWildSpawnType(untarBot, assembly);

            CustomWildSpawnTypeManager.AddSuitableGroup(new List<int> { 1170, 1171, 1172, 1173 });
        }

    }
}
