using DrakiaXYZ.BigBrain.Brains;
using EFT;
using System;
using TacticalToasterUNTARGH.Behavior.Actions;
using TacticalToasterUNTARGH.Components;
using TacticalToasterUNTARGH.Controllers;
using UnityEngine;

namespace TacticalToasterUNTARGH.Behavior.Layers
{
    internal class GoToCheckpointLayer : CustomLayer
    {
        protected BotUntarManager untarManager { get; private set; }

        public static float innerRadius = 10f;
        public static float outerRadius = 45f;
        public static Vector3 patrolPoint = new Vector3(-140f, -1f, 410f);

        public Type lastAction;
        public Type nextAction;
        public string nextActionReason;

        public GoToCheckpointLayer(BotOwner botOwner, int priority) : base(botOwner, priority)
        {
            untarManager = botOwner.GetOrAddUntarManager();
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Stop()
        {
            untarManager.guardPointDirty = true;
            untarManager.AtCheckpoint = false;
            untarManager.ShouldSwitchCover = false;
            base.Stop();
        }

        public override string GetName()
        {
            return "GuardCheckpoint";
        }

        public void setNextAction(Type actionType, string reason)
        {
            nextAction = actionType;
            nextActionReason = reason;
        }

        public override Action GetNextAction()
        {
            lastAction = nextAction;

            return new Action(lastAction, nextActionReason);
        }

        public override bool IsActive()
        {
            if (!untarManager.CanDoCheckpointActions())
                return false;

            
            getNextAction();
            return true;
        }

        public void getNextAction()
        {
            lastAction = nextAction;

            if (untarManager.AtCheckpoint)
            {
                if (untarManager.ShouldSwitchCover)
                {
                    nextAction = typeof(SwitchCheckpointCover);
                    nextActionReason = "SwitchCover";
                    return;
                }
                nextAction = typeof(SitAtCheckpoint);
                nextActionReason = "AtCheckpoint";
                return;
            }
            nextAction = typeof(GoToCheckpoint);
            nextActionReason = "ToCheckpoint";
        }

        public override bool IsCurrentActionEnding()
        {
            return nextAction != lastAction;
        }
    }
}
