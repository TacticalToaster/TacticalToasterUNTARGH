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
    internal class SwitchCheckpointCover : CustomLogic
    {
        protected BotUntarManager untarManager { get; private set; }

        private GClass395 baseSteeringLogic;
        private float checkDelay;
        private GClass212 goToCoverPoint;

        public SwitchCheckpointCover(BotOwner botOwner) : base(botOwner)
        {
            untarManager = botOwner.GetOrAddUntarManager();
            goToCoverPoint = new GClass212(BotOwner);
            baseSteeringLogic = new GClass395();
        }

        public override void Start()
        {
            //Plugin.LogSource.LogInfo($"[{BotOwner.Profile.Nickname}] Start switch at checkpoint 3.");
            BotOwner.Memory.SetCoverPoints(untarManager.guardPoint, "");

            base.Start();
        }

        public override void Stop()
        {
            //Plugin.LogSource.LogInfo($"[{BotOwner.Profile.Nickname}] Stop switch at checkpoint 3.");
            //untarManager.ShouldSwitchCover = false;
            BotOwner.Mover.Sprint(false);
            base.Stop();
        }

        public override void Update(CustomLayer.ActionData data)
        {
            untarManager.UpdateGuardPoint();
            BotOwner.Memory.SetCoverPoints(untarManager.guardPoint, "");
            goToCoverPoint.UpdateNodeByMain(data);

            if (BotOwner.Mover.IsComeTo(.7f, true, untarManager.guardPoint))
            {
                untarManager.ShouldSwitchCover = false;
            }

            UpdateBotMovement();
            UpdateSteering();

        }

        public void UpdateBotMovement()
        {
            BotOwner.SetPose(1f);
            BotOwner.BotLay.GetUp(true);

            //BotOwner.Mover.Sprint(true);
            BotOwner.SetTargetMoveSpeed(1f);
        }

        public void UpdateSteering()
        {
            BotOwner.Steering.LookToMovingDirection();
            baseSteeringLogic.Update(BotOwner);
        }
    }
}
