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

        private GClass385 baseSteeringLogic = new GClass385();

        public GoToCheckpointAction(BotOwner botOwner) : base(botOwner)
        {
        }

        public override void Start()
        {
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
            BotOwner.Mover.GoToPoint(patrolPoint, true, 10f, true);
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
