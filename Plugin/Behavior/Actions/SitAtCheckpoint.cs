using DrakiaXYZ.BigBrain.Brains;
using EFT;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TacticalToasterUNTARGH.Components;
using TacticalToasterUNTARGH.Controllers;

namespace TacticalToasterUNTARGH.Behavior.Actions
{
    internal class SitAtCheckpoint : CustomLogic
    {
        protected BotUntarManager untarManager { get; private set; }
        private GClass278 holdPosition;
        private GClass395 baseSteeringLogic;
        private Stopwatch stopwatch;
        private float sitDuration = 0f; // milliseconds

        public SitAtCheckpoint(BotOwner botOwner) : base(botOwner)
        {
            untarManager = botOwner.GetOrAddUntarManager();
            holdPosition = new GClass278(BotOwner);
            baseSteeringLogic = new GClass395();
            stopwatch = new Stopwatch();
        }

        public override void Start()
        {
            //Plugin.LogSource.LogInfo($"[{BotOwner.Profile.Nickname}] Start Sitting at checkpoint.");
            stopwatch.Restart();
            sitDuration = UnityEngine.Random.Range(10 * 1000f, 30 * 1000f);

            BotOwner.SetPose(UnityEngine.Random.Range(0f, 1f));
        }

        public override void Stop()
        {
            //untarManager.AtCheckpoint = false;
            //Plugin.LogSource.LogInfo($"[{BotOwner.Profile.Nickname}] Stop Sitting at checkpoint.");
            //untarManager.ShouldSwitchCover = true;
            stopwatch.Stop();
        }

        public override void Update(CustomLayer.ActionData data)
        {
            //Plugin.LogSource.LogInfo($"[{BotOwner.Profile.Nickname}] Sitting at checkpoint2 for {(sitDuration - stopwatch.ElapsedMilliseconds) / 1000f} more seconds. {stopwatch.ElapsedMilliseconds} {sitDuration} {untarManager.AtCheckpoint}");
            untarManager.UpdateGuardPoint();
            holdPosition.UpdateNodeByMain(data);
            baseSteeringLogic.Update(BotOwner);
            if (stopwatch.ElapsedMilliseconds >= sitDuration)
            {
                untarManager.ShouldSwitchCover = true;
                untarManager.guardPointDirty = true;
                //Plugin.LogSource.LogInfo($"[{BotOwner.Profile.Nickname}] Done sitting at checkpoint.");
                sitDuration = UnityEngine.Random.Range(10 * 1000f, 30 * 1000f);
                stopwatch.Reset();
            }
        }
    }
}
