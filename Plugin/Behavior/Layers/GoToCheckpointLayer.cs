using DrakiaXYZ.BigBrain.Brains;
using EFT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TacticalToasterUNTARGH.Behavior.Actions;
using UnityEngine;

namespace TacticalToasterUNTARGH.Behavior.Layers
{
    internal class GoToCheckpointLayer : CustomLayer
    {
        public static float innerRadius = 10f;
        public static float outerRadius = 30f;
        public static Vector3 patrolPoint = new Vector3(-140f, -1f, 410f);

        public GoToCheckpointLayer(BotOwner botOwner, int priority) : base(botOwner, priority)
        {
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
            return BotOwner.Position.SqrDistance(patrolPoint) < innerRadius;
        }

        public override bool IsCurrentActionEnding()
        {
            return false;
        }
    }
}
