using DrakiaXYZ.BigBrain.Brains;
using EFT;
using System.Text;
using TacticalToasterUNTARGH.Components;
using TacticalToasterUNTARGH.Controllers;

namespace TacticalToasterUNTARGH.Behavior.Actions
{
    internal class SwitchCheckpointCover : GoToCustomAction
    {
        protected BotUntarManager UntarManager { get; private set; }

        public SwitchCheckpointCover(BotOwner botOwner) : base(botOwner)
        {
            UntarManager = botOwner.GetOrAddUntarManager();
        }

        public override void Start()
        {
            BotOwner.Memory.SetCoverPoints(UntarManager.guardPoint, "");

            base.Start();
        }

        public override void Update(CustomLayer.ActionData data)
        {
            UntarManager.UpdateGuardPoint();
            BotOwner.Memory.SetCoverPoints(UntarManager.guardPoint, "");

            base.Update(data);

            if (BotOwner.Mover.IsComeTo(.5f, true, UntarManager.guardPoint))
            {
                UntarManager.ShouldSwitchCover = false;
            }
        }

        public override void BuildDebugText(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine($"-- SwitchCheckpointCover -- {UntarManager.guardPoint.Position}");
            base.BuildDebugText(stringBuilder);
        }

        public override CustomNavigationPoint GetGoToPoint()
        {
            return UntarManager.guardPoint;
        }
    }
}
