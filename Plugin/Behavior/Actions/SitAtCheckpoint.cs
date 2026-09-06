using DrakiaXYZ.BigBrain.Brains;
using EFT;
using System.Diagnostics;
using TacticalToasterUNTARGH.Components;
using TacticalToasterUNTARGH.Controllers;
using UnityEngine;

namespace TacticalToasterUNTARGH.Behavior.Actions
{
    internal class SitAtCheckpoint : CustomLogic
    {
        protected BotUntarManager UntarManager { get; private set; }
        private HoldPosition holdPosition;
        private LookAround baseSteeringLogic;
        private float sitStart = -1f;
        private float sitDuration = 0f;

        public SitAtCheckpoint(BotOwner botOwner) : base(botOwner)
        {
            UntarManager = botOwner.GetOrAddUntarManager();
            holdPosition = new HoldPosition(BotOwner);
            baseSteeringLogic = new LookAround();
        }

        public override void Start()
        {
            //Plugin.LogSource.LogInfo($"[{BotOwner.Profile.Nickname}] Start Sitting at checkpoint.");
            sitStart = Time.time;
            sitDuration = UnityEngine.Random.Range(10f, 30f);

            if (MyExtensions.IsTrue100(50))
                BotOwner.SetPose(1);
            else
                BotOwner.SetPose(0);
            
            BotOwner.GetPlayer.MovementContext.SetPatrol(true);
        }

        public override void Stop()
        {
            //UntarManager.AtCheckpoint = false;
            //Plugin.LogSource.LogInfo($"[{BotOwner.Profile.Nickname}] Stop Sitting at checkpoint.");
            //UntarManager.ShouldSwitchCover = true;
            sitStart = -1f;
            BotOwner.GetPlayer.MovementContext.SetPatrol(false);
        }

        public override void Update(CustomLayer.ActionData data)
        {
            UntarManager.UpdateGuardPoint();
            holdPosition.UpdateNodeByMain(data);
            baseSteeringLogic.Update(BotOwner);
            if (Time.time > sitStart + sitDuration)
            {
                UntarManager.ShouldSwitchCover = true;
                UntarManager.guardPointDirty = true;
                sitDuration = UnityEngine.Random.Range(10f, 30f);
                sitStart = -1f;
            }
        }
    }
}
