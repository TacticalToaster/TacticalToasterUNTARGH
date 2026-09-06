using Mono.Cecil;
using MoreBotsAPI;
using System.Collections.Generic;

namespace Prepatch.Prepatches
{
    public static class CustomTypesPatch
    {
        private const int BaseBrainType = 9;

        private const int RiflemanId = 1170;
        private const int SquadLeaderId = 1171;
        private const int MarksmanId = 1172;
        private const int OfficerId = 1173;

        private static readonly List<int> ExcludedDifficulties = new()
        {
            0,
            2,
            3
        };

        private static readonly List<int> UntarGroup = new()
        {
            RiflemanId,
            SquadLeaderId,
            MarksmanId,
            OfficerId
        };

        public static IEnumerable<string> TargetDLLs { get; } = new[]
        {
            "Assembly-CSharp.dll"
        };

        public static void Patch(ref AssemblyDefinition assembly)
        {
            RegisterBot(assembly, RiflemanId, "followeruntar", "UNTAR");
            RegisterBot(assembly, SquadLeaderId, "bossuntarlead", "UNTAR");
            RegisterBot(assembly, MarksmanId, "followeruntarmarksman", "UNTAR");
            RegisterBot(assembly, OfficerId, "bossuntarofficer", "UNTAR");

            CustomWildSpawnTypeManager.AddSuitableGroup(UntarGroup);
        }

        private static void RegisterBot(
            AssemblyDefinition assembly,
            int id,
            string botDbKey,
            string role)
        {
            var bot = new CustomWildSpawnType(
                id,
                botDbKey,
                role,
                BaseBrainType,
                true,
                true,
                false);

            bot.SetCountAsBossForStatistics(false);
            bot.SetShouldUseFenceNoBossAttack(false, false);
            bot.SetExcludedDifficulties(ExcludedDifficulties);

            CustomWildSpawnTypeManager.RegisterWildSpawnType(bot, assembly);
        }
    }
}