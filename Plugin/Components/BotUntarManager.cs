﻿using Comfort.Common;
using EFT;
using SPT.SinglePlayer.Utils.InRaid;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TacticalToasterUNTARGH.Models;
using TacticalToasterUNTARGH.Prepatches;
using UnityEngine;

namespace TacticalToasterUNTARGH.Components
{
    public class BotUntarManager : MonoBehaviour
    {
        public BotOwner botOwner;
        public bool AtCheckpoint = false;
        public bool ShouldSwitchCover = false;
        public bool guardPointDirty = true;
        public bool hasTeleportedToCheckpoint = false;
        public CustomNavigationPoint guardPoint;

        private UntarCheckpoint assignedCheckpoint;
        private Stopwatch stopwatch = new Stopwatch();

        public void Init(BotOwner botOwner)
        {
            this.botOwner = botOwner;

            if (!botOwner.Profile.Info.Settings.Role.IsUNTAR())
                return;


            if (RaidTimeUtil.GetSecondsSinceSpawning() <= 120)
            {
                Plugin.LogSource.LogInfo($"[{botOwner.Profile.Nickname}] Attempting to find and assign checkpoint on spawn.");
                MonoBehaviourSingleton<UntarCheckpointManager>.Instance.FindAndAssignCheckpoint(botOwner);
                Plugin.LogSource.LogInfo($"[{botOwner.Profile.Nickname}] Assigned checkpoint: {(HasAssignedCheckpoint() ? GetAssignedCheckpoint().Zone.NameZone : "None")}");

                if (HasAssignedCheckpoint())
                {
                    botOwner.Mover.Teleport(assignedCheckpoint.Position);
                    botOwner.Mover.GoToPoint(assignedCheckpoint.Position, true, 1f, true);
                }
            }
        }

        public void OnBotActivate()
        {
            if (!botOwner.Profile.Info.Settings.Role.IsUNTAR())
                return;

            stopwatch.Restart();
            CheckPlayerLoyalty();
        }

        public void CheckPlayerLoyalty()
        {
            var allBots = Singleton<IBotGame>.Instance.BotsController.Players;
            var player = Singleton<GameWorld>.Instance.AllAlivePlayersList[0];

            var pkLevel = player.Profile.GetTraderLoyalty("5935c25fb3acc3127c3d8cd9"); // PK loyalty level

            Plugin.LogSource.LogInfo($"Checking PK loyalty {pkLevel}. Is the bot active? {botOwner.AIData.BotOwner.BotState}");

            if (pkLevel < 3)
                return;

            foreach (IPlayer bot in allBots)
            {
                if ((!bot.AIData.IsAI || bot.AIData.BotOwner.BotState == EBotState.Active) && bot.IsAI && bot.Profile.Info.Settings.Role.IsUNTAR())
                {
                    bot.AIData.BotOwner.BotsGroup.RemoveEnemy(player);
                    bot.AIData.BotOwner.BotsGroup.AddAlly(player);

                    foreach (Player potentialGroupMember in Singleton<GameWorld>.Instance.AllAlivePlayersList)
                    {
                        if (!string.IsNullOrEmpty(potentialGroupMember.GroupId) && potentialGroupMember.GroupId == player.GroupId)
                        {
                            bot.AIData.BotOwner.BotsGroup.RemoveEnemy(potentialGroupMember);
                            bot.AIData.BotOwner.BotsGroup.AddAlly(potentialGroupMember);
                        }
                    }

                    if (bot.AIData.BotOwner.BotState == EBotState.Active)
                    {
                        player.AIData.BotOwner.Brain.BaseBrain.CalcActionNextFrame();
                    }
                }
            }
        }

        public CustomNavigationPoint GetCheckpointCoverPoint()
        {
            var checkpoint = GetAssignedCheckpoint();
            var patrolPoint = checkpoint.Position;
            var randomPoint = patrolPoint + (Vector3)UnityEngine.Random.insideUnitCircle * checkpoint.Radius;

            var searchData = new CoverSearchData(randomPoint, botOwner.CoverSearchInfo, CoverShootType.hide, (checkpoint.Radius * .5f) * (checkpoint.Radius * .5f), 0f, CoverSearchType.distToToCenter, null, null, null, ECheckSHootHide.shootAndHide, new CoverSearchDefenceDataClass(botOwner.Settings.FileSettings.Cover.MIN_DEFENCE_LEVEL), PointsArrayType.byShootType, true);
            return botOwner.BotsGroup.CoverPointMaster.GetCoverPointMain(searchData, true);
        }

        public void UpdateGuardPoint()
        {
            if (guardPointDirty)
            {
                Plugin.LogSource.LogInfo($"[{botOwner.Profile.Nickname}] Updating guard point...");
                guardPoint = GetCheckpointCoverPoint();
                guardPointDirty = false;
            }
        }

        public void SetGuardPoint(CustomNavigationPoint point)
        {
            Plugin.LogSource.LogInfo($"[{botOwner.Profile.Nickname}] Setting guard point to {point.Position}");
            guardPoint = point;
        }

        public bool CanDoCheckpointActions()
        {
            //Plugin.LogSource.LogInfo($"[{botOwner.Profile.Nickname}] can do checkpoint action? {HasAssignedCheckpoint() && stopwatch.ElapsedMilliseconds >= 5000}");
            return HasAssignedCheckpoint() && stopwatch.ElapsedMilliseconds >= 5000;
        }

        public bool HasAssignedCheckpoint()
        {
            return assignedCheckpoint != null;
        }

        public UntarCheckpoint GetAssignedCheckpoint()
        {
            return assignedCheckpoint;
        }

        public void SetAssignedCheckpoint(UntarCheckpoint checkpoint)
        {
            assignedCheckpoint = checkpoint;
        }
    }
}
