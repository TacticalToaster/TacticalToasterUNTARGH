using DrakiaXYZ.BigBrain.Brains;
using EFT;
using TacticalToasterUNTARGH.Behavior.Actions;
using UnityEngine;

namespace TacticalToasterUNTARGH.Behavior.Layers
{
    internal class GoToCheckpointLayer : CustomLayer
    {
        public static float innerRadius = 10f;
        public static float outerRadius = 45f;
        public static Vector3 patrolPoint = new Vector3(-140f, -1f, 410f);

        public bool isInside = false;
        public bool hasRun = false;
        public float runTime;

        public GoToCheckpointLayer(BotOwner botOwner, int priority) : base(botOwner, priority)
        {
        }

        public override void Start()
        {
            isInside = BotOwner.Position.SqrDistance(patrolPoint) <= (outerRadius * outerRadius);
            hasRun = true;
            runTime = Time.time + 10f;
            base.Start();
        }

        public override void Stop()
        {
            isInside = BotOwner.Position.SqrDistance(patrolPoint) <= (outerRadius * outerRadius);
            hasRun = false;
            base.Stop();
        }

        public override string GetName()
        {
            return "GoToCheckpoint";
        }

        public override Action GetNextAction()
        {
            return new Action(typeof(GoToCheckpointAction), "ToCheckpoint");
        }

        public override bool IsActive()
        {
            if (BotOwner.Position.SqrDistance(patrolPoint) <= (outerRadius * outerRadius))
            {
                if (!isInside && !hasRun)
                {
                    runTime = Time.time + 10f;
                    //isInside = true;
                }
            }
            //isInside = BotOwner.Position.SqrDistance(patrolPoint) < (outerRadius * outerRadius);
            //Plugin.LogSource.LogMessage($"Checking is active! {BotOwner.Position.SqrDistance(patrolPoint) > (innerRadius * innerRadius)}");
            // (!hasRun || Time.time < runTime);
            return (!isInside && BotOwner.Position.SqrDistance(patrolPoint) > (innerRadius * innerRadius) || runTime > Time.time) || BotOwner.Position.SqrDistance(patrolPoint) > (outerRadius * outerRadius) ;//BotOwner.Position.SqrDistance(patrolPoint) > (outerRadius * outerRadius);
        }

        public override bool IsCurrentActionEnding()
        {
            if (CurrentAction?.Type == typeof(GoToCheckpointAction))
            {
                return false;
            }

            return true;
        }
    }
}
