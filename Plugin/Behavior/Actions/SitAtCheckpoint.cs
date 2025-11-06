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
        private GClass278 holdPosition;
        private GClass395 baseSteeringLogic;
        private float sitStart = -1f;
        private float sitDuration = 0f;

        public SitAtCheckpoint(BotOwner botOwner) : base(botOwner)
        {
            UntarManager = botOwner.GetOrAddUntarManager();
            holdPosition = new GClass278(BotOwner);
            baseSteeringLogic = new GClass395();
        }

        public override void Start()
        {
            //Plugin.LogSource.LogInfo($"[{BotOwner.Profile.Nickname}] Start Sitting at checkpoint.");
            sitStart = Time.time;
            sitDuration = UnityEngine.Random.Range(10f, 30f);

            if (GClass856.IsTrue100(50))
                BotOwner.SetPose(1);
            else
                BotOwner.SetPose(0);
        }

        public override void Stop()
        {
            //UntarManager.AtCheckpoint = false;
            //Plugin.LogSource.LogInfo($"[{BotOwner.Profile.Nickname}] Stop Sitting at checkpoint.");
            //UntarManager.ShouldSwitchCover = true;
            sitStart = -1f;
        }

        public override void Update(CustomLayer.ActionData data)
        {
            //Plugin.LogSource.LogInfo($"[{BotOwner.Profile.Nickname}] Sitting at checkpoint2 for {(sitDuration - stopwatch.ElapsedMilliseconds) / 1000f} more seconds. {stopwatch.ElapsedMilliseconds} {sitDuration} {UntarManager.AtCheckpoint}");
            UntarManager.UpdateGuardPoint();
            holdPosition.UpdateNodeByMain(data);
            baseSteeringLogic.Update(BotOwner);
            if (Time.time > sitStart + sitDuration)
            {
                UntarManager.ShouldSwitchCover = true;
                UntarManager.guardPointDirty = true;
                //Plugin.LogSource.LogInfo($"[{BotOwner.Profile.Nickname}] Done sitting at checkpoint.");
                sitDuration = UnityEngine.Random.Range(10f, 30f);
                sitStart = -1f;
            }
        }
    }
}
