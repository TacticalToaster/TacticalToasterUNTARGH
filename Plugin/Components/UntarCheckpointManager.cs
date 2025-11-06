using Comfort.Common;
using EFT;
using Newtonsoft.Json;
using SPT.Common.Http;
using SPT.SinglePlayer.Utils.InRaid;
using System.Collections.Generic;
using TacticalToasterUNTARGH.Controllers;
using TacticalToasterUNTARGH.Models;
using UnityEngine;

namespace TacticalToasterUNTARGH.Components
{
    public class UntarCheckpointManager : MonoBehaviourSingleton<UntarCheckpointManager>
    {
        public Dictionary<BotZone, UntarCheckpoint> ZoneCheckpoints = new Dictionary<BotZone, UntarCheckpoint>();

        public void InitRaid()
        {
            Plugin.LogSource.LogInfo("Initializing UntarCheckpointManager for raid...");
            LoadCheckpoints();
        }

        public void FindAndAssignCheckpoint(BotOwner botOwner)
        {
            Plugin.LogSource.LogInfo($"[{botOwner.Profile.Nickname}] Finding and assigning checkpoint...");

            var botSpawner = Singleton<IBotGame>.Instance.BotsController.BotSpawner;
            var botZone = botOwner.SpawnBotZone != null ? botOwner.SpawnBotZone : botSpawner.GetClosestZone(botOwner.Position, out float dist);

            Plugin.LogSource.LogInfo($"[{botOwner.Profile.Nickname}] Spawn Bot zone: {(botZone != null ? botZone.NameZone : "None")}");

            if (botZone != null)
            {
                var checkpoint = GetCheckpointForZone(botZone);
                if (checkpoint != null)
                {
                    AssignBotToCheckpoint(botOwner, checkpoint);
                }
            }
        }

        public void AssignBotToCheckpoint(BotOwner botOwner, UntarCheckpoint checkpoint)
        {
            var botUntarManager = botOwner.GetOrAddUntarManager();
            botUntarManager.SetAssignedCheckpoint(checkpoint);
            checkpoint.AssignedBots.Add(botOwner);
        }

        public UntarCheckpoint GetCheckpointForZone(BotZone zone)
        {
            if (ZoneCheckpoints.TryGetValue(zone, out var checkpoint))
            {
                Plugin.LogSource.LogInfo($"Found checkpoint {checkpoint.Position} for zone {zone.NameZone}");
                return checkpoint;
            }
            return null;
        }

        public void LoadCheckpoints()
        {
            var result = RequestHandler.GetJson("/untar/checkpoints");
            Plugin.LogSource.LogInfo($"Loading checkpoints from config... {RaidChangesUtil.LocationId}");
            var mainConfig = JsonConvert.DeserializeObject<MainConfig>(result);

            if (!mainConfig.locations.ContainsKey(RaidChangesUtil.LocationId.ToLower()))
            {
                Plugin.LogSource.LogWarning($"No configuration found for location {RaidChangesUtil.LocationId}");
                return;
            }

            var checkpointConfig = mainConfig.locations[RaidChangesUtil.LocationId.ToLower()].checkpoint;

            ZoneCheckpoints.Clear();

            if (!checkpointConfig.enableCheckpoints)
                return;

            var botSpawner = Singleton<IBotGame>.Instance.BotsController.BotSpawner;

            foreach (var zoneConfig in checkpointConfig.checkpointZones)
            {
                var zone = botSpawner.GetZoneByName(zoneConfig.checkpointZone);
                if (zone != null)
                {
                    var untarCheckpoint = new UntarCheckpoint
                    (
                        zone,
                        new Vector3(zoneConfig.x, zoneConfig.y, zoneConfig.z),
                        zoneConfig.checkpointRadius
                    );

                    ZoneCheckpoints[zone] = untarCheckpoint;
                }
            }
        }
    }
}
