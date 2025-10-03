using DrakiaXYZ.BigBrain.Brains;
using EFT;
using SAIN;
using SAIN.Helpers;
using SAIN.Preset;
using SAIN.Layers;
using SAIN.Layers.Combat;
using SAIN.Preset.GlobalSettings.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TacticalToasterUNTARGH.Interop
{
    internal class SAINInterop
    {
        public void Init()
        {
            CreateUNTARBotTypes();
        }

        public void AddSAINLayers()
        {
            //AIBrains.Others.Add(Brain.UNTAR);

            //BrainManager.AddCustomLayer(typeof(DebugLayer), untarBrainList, 99, untarTypes);
            //BrainManager.AddCustomLayer(typeof(SAINAvoidThreatLayer), untarBrainList, 80, untarTypes);
            //BrainManager.AddCustomLayer(typeof(ExtractLayer), untarBrainList, settings.SAINExtractLayerPriority, untarTypes);
            //BrainManager.AddCustomLayer(typeof(CombatSquadLayer), untarBrainList, settings.SAINCombatSquadLayerPriority, untarTypes);
            //BrainManager.AddCustomLayer(typeof(CombatSoloLayer), untarBrainList, settings.SAINCombatSoloLayerPriority, untarTypes);
        }

        public void CreateUNTARBotTypes()
        {
            Type _botType = Type.GetType("SAIN.Preset.BotType, SAIN");

            if (_botType == null)
            {
                Plugin.LogSource.LogError("SAIN BotType type not found, cannot create UNTAR bot types.");
                return;
            }

            var followeruntar = new BotType()
            {
                Name = "UNTAR Follower",
                Description = "An UNTAR grunt.",
                Section = "UNTAR",
                WildSpawnType = (WildSpawnType)UNTAREnums.UNTARTypes.Find(bot => bot.typeName == "followeruntar").wildSpawnType,
                BaseBrain = "ExUsec"
            };

            var bossuntarlead = new BotType()
            {
                Name = "UNTAR Squad Leader",
                Description = "An UNTAR squad leader.",
                Section = "UNTAR",
                WildSpawnType = (WildSpawnType)UNTAREnums.UNTARTypes.Find(bot => bot.typeName == "bossuntarlead").wildSpawnType,
                BaseBrain = "ExUsec"
            };

            var followeruntarmarksman = new BotType()
            {
                Name = "UNTAR Marksman",
                Description = "An UNTAR marksman.",
                Section = "UNTAR",
                WildSpawnType = (WildSpawnType)UNTAREnums.UNTARTypes.Find(bot => bot.typeName == "followeruntarmarksman").wildSpawnType,
                BaseBrain = "ExUsec"
            };

            var bossuntarofficer = new BotType()
            {
                Name = "UNTAR Officer",
                Description = "An UNTAR officer.",
                Section = "UNTAR",
                WildSpawnType = (WildSpawnType)UNTAREnums.UNTARTypes.Find(bot => bot.typeName == "bossuntarofficer").wildSpawnType,
                BaseBrain = "ExUsec"
            };

            BotTypeDefinitions.BotTypesList.Add(followeruntar);
            BotTypeDefinitions.BotTypesList.Add(bossuntarlead);
            BotTypeDefinitions.BotTypesList.Add(followeruntarmarksman);
            BotTypeDefinitions.BotTypesList.Add(bossuntarofficer);
        }
    }
}
