using DrakiaXYZ.BigBrain.Brains;
using EFT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TacticalToasterUNTARGH.Components;
using TacticalToasterUNTARGH.Controllers;
using UnityEngine;

namespace TacticalToasterUNTARGH.Behavior.Actions
{
    internal class GoToCheckpoint : CustomLogic
    {
        protected BotUntarManager untarManager { get; private set; }

        private GClass395 baseSteeringLogic;
        private float checkDelay;
        private GClass212 goToCoverPoint;

        private bool teleported = false;

        public GoToCheckpoint(BotOwner botOwner) : base(botOwner)
        {
            untarManager = botOwner.GetOrAddUntarManager();
            goToCoverPoint = new GClass212(BotOwner);
            baseSteeringLogic = new GClass395();
        }

        public override void Start()
        {
            //var patrolPoint = untarManager.GetAssignedCheckpoint().Position;
            //var randomPoint = patrolPoint + (Vector3)UnityEngine.Random.insideUnitCircle * 25f;

           // Plugin.LogSource.LogInfo($"[{BotOwner.Profile.Nickname}] Start going to checkpoint 3.");

            //var searchData = new CoverSearchData(randomPoint, BotOwner.CoverSearchInfo, CoverShootType.hide, 35f * 35f, 0f, CoverSearchType.distToToCenter, null, null, null, ECheckSHootHide.shootAndHide, new CoverSearchDefenceDataClass(BotOwner.Settings.FileSettings.Cover.MIN_DEFENCE_LEVEL), PointsArrayType.byShootType, true);
            untarManager.UpdateGuardPoint();
            BotOwner.Memory.SetCoverPoints(untarManager.guardPoint, "");

            BotOwner.AimingManager.CurrentAiming.LoseTarget();


            /*if (teleported == false)
            {
                Plugin.LogSource.LogMessage("Erm, teleporting!");
                BotOwner.Mover.Teleport(guardPoint.Position);
                BotOwner.Mover.GoToPoint(guardPoint.Position, true, 1f, true);
                //BotOwner.Memory.ComeToPoint();
                //BotOwner.StopMove();
                teleported = true;
            }*/

            base.Start();
        }

        public override void Stop()
        {
            //Plugin.LogSource.LogInfo($"[{BotOwner.Profile.Nickname}] Stop going to checkpoint 3.");

            BotOwner.Mover.Sprint(false);
            base.Stop();
        }

        public override void Update(CustomLayer.ActionData data)
        {
            untarManager.UpdateGuardPoint();
            BotOwner.Memory.SetCoverPoints(untarManager.guardPoint, "");
            goToCoverPoint.UpdateNodeByMain(data);

            
            //Plugin.LogSource.LogInfo($"Patrol status update | {BotOwner.GoToSomePointData.Point} : {BotOwner.GoToSomePointData.HaveTarget()} : {BotOwner.GoToSomePointData.PointhRefreshed} | tp:{BotOwner.Mover.TargetPoint} dcp:{BotOwner.Mover.DirCurPoint} rdp:{BotOwner.Mover.RealDestPoint}");

            UpdateBotMovement();
            UpdateSteering();

            if (BotOwner.Mover.IsComeTo(.7f, true, untarManager.guardPoint))
            {
                untarManager.AtCheckpoint = true;
            }
            //BotOwner.MagazineChecker.ManualUpdate();

        }

        public void UpdateBotMovement() 
        {
            BotOwner.SetPose(1f);
            BotOwner.BotLay.GetUp(true);

            BotOwner.Mover.Sprint(true);
            BotOwner.SetTargetMoveSpeed(1f);
        }

        public void UpdateSteering()
        {
            BotOwner.Steering.LookToMovingDirection();
            baseSteeringLogic.Update(BotOwner);
        }

        public override void BuildDebugText(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine($"-- GoToCheckpointAction -- {untarManager.guardPoint.Position}");
            base.BuildDebugText(stringBuilder);
        }
    }
}
