using DrakiaXYZ.BigBrain.Brains;
using EFT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TacticalToasterUNTARGH.Behavior.Actions
{
    internal class GoToCheckpointAction : CustomLogic
    {
        public static Vector3 patrolPoint = new Vector3(-140f, -1f, 410f);

        private CustomNavigationPoint guardPoint;
        private GClass385 baseSteeringLogic = new GClass385();
        private float checkDelay;

        private bool teleported = false;

        public GoToCheckpointAction(BotOwner botOwner) : base(botOwner)
        {
        }

        public override void Start()
        {
            BotOwner.StopMove();
            BotOwner.PatrollingData.Status = PatrolStatus.pause;
            BotOwner.Memory.BotCurrentCoverInfo.SetCover(null, false);
            BotOwner.Memory.SetCoverPoints(null, "");
            base.Start();
        }

        public override void Stop()
        {
            base.Stop();
        }

        public override void Update(CustomLayer.ActionData data)
        {
            UpdateBotMovement();
            UpdateSteering();
            BotOwner.MagazineChecker.ManualUpdate();

            if (guardPoint == null)
            {
                var searchData = new CoverSearchData(patrolPoint, BotOwner.CoverSearchInfo, CoverShootType.hide, 35f * 35f, 0f, CoverSearchType.distToToCenter, null, null, patrolPoint, ECheckSHootHide.shootAndHide, new CoverSearchDefenceDataClass(BotOwner.Settings.FileSettings.Cover.MIN_DEFENCE_LEVEL), PointsArrayType.byShootType, true);
                guardPoint = BotOwner.BotsGroup.CoverPointMaster.GetCoverPointMain(searchData, true);
                BotOwner.Memory.BotCurrentCoverInfo.SetCover(guardPoint, true);
                BotOwner.Memory.SetCoverPoints(guardPoint, "");
            }

            if (teleported == false)
            {
                Plugin.LogSource.LogMessage("Erm, teleporting!");
                BotOwner.Mover.Teleport(guardPoint.Position);
                BotOwner.Mover.GoToPoint(guardPoint.Position, true, 1f, true);
                BotOwner.Memory.ComeToPoint();
                BotOwner.StopMove();
                teleported = true;
                return;
            }

            if (Time.time > checkDelay)
            {
                if (BotOwner.Mover.IsComeTo(0.6f, true, guardPoint))
                {
                    BotOwner.Sprint(false, false);
                    BotOwner.Memory.ComeToPoint();
                    return;
                }
                BotOwner.Mover.GoToPoint(guardPoint.Position, true, 1f, true);
                checkDelay = Time.time + 0.75f;
            }
        }

        public void UpdateBotMovement() 
        {
            BotOwner.SetPose(1f);
            BotOwner.BotLay.GetUp(true);

            BotOwner.SetTargetMoveSpeed(1f);
            BotOwner.Mover.Sprint(true);
        }

        public void UpdateSteering()
        {
            BotOwner.Steering.LookToMovingDirection();
            baseSteeringLogic.Update(BotOwner);
        }
    }
}
