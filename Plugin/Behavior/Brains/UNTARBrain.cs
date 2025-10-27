﻿using System;
using Comfort.Common;
using EFT;

namespace TacticalToasterUNTARGH.Behavior.Brains
{
    // This is a copy of the raider brain
    public class UNTARBrain : BaseBrain
    {
        // Token: 0x06001E6B RID: 7787 RVA: 0x002BF318 File Offset: 0x002BD518
        public UNTARBrain(BotOwner owner, bool withPursuit)
            : base(owner)
        {
            var gclass = new GClass48(owner, 80);
            base.method_0(5, gclass, true);
            var gclass2 = new GClass118(owner, 78);
            base.method_0(12, gclass2, true);
            var gclass3 = new GClass84(owner, 70);
            base.method_0(9, gclass3, true);
            var gclass4 = new GClass142(owner, 60);
            base.method_0(1, gclass4, true);
            var gclass5 = new GClass39(owner, 50);
            base.method_0(6, gclass5, true);
            var gclass6 = new GClass139(owner, 30);
            base.method_0(4, gclass6, true);
            if (withPursuit)
            {
                var @class = new Class105(owner, 25);
                base.method_0(13, @class, true);
            }
            var gclass7 = new GClass166(owner, 15);
            base.method_0(14, gclass7, true);
            var class2 = new Class99(owner, 9, false);
            base.method_0(3, class2, true);
            var gclass8 = new GClass132(owner, 3);
            base.method_0(8, gclass8, true);
            //GClass132 stayPos = new GClass132(owner, 0, true, CoverLevel.Stay, false);
            //base.method_0(2, stayPos, true);
            var gclass9 = new GClass86(owner, 2); // PatrolFollower
            base.method_0(7, gclass9, true);
            var gclass10 = new GClass133(owner, 0); // PatrolAssault (for the leader of group)
            base.method_0(2, gclass10, true);
            if (this.Owner.Boss.IamBoss)
            {
                Singleton<BotEventHandler>.Instance.OnKill += this.method_6;
            }
        }

        // Token: 0x06001E6C RID: 7788 RVA: 0x0017EE2E File Offset: 0x0017D02E
        public override GClass671 EventsPriority()
        {
            return new GClass671(-1, 75, 55, 76, 56, -1);
        }

        // Token: 0x06001E6D RID: 7789 RVA: 0x002BF450 File Offset: 0x002BD650
        public void method_6(IPlayer killer, IPlayer target)
        {
            if (this.Owner.Boss.IamBoss && Singleton<GameWorld>.Instance.GetAlivePlayerByProfileID(target.ProfileId) == this.Owner.GetPlayer)
            {
                foreach (Player player in Singleton<GameWorld>.Instance.allAlivePlayersByID.Values)
                {
                    if (!player.AIData.IsAI)
                    {
                        this.Owner.BotsGroup.AddEnemy(player, EBotEnemyCause.pmcBossKill);
                    }
                }
                this.Owner.BotsGroup.SetAggressiveToAllNewPlayers(true);
                Singleton<BotEventHandler>.Instance.OnKill -= this.method_6;
            }
        }

        // Token: 0x06001E6E RID: 7790 RVA: 0x0017E7D0 File Offset: 0x0017C9D0
        public override string ShortName()
        {
            return "UNTAR";
        }

        // Token: 0x040016B0 RID: 5808
        [NonSerialized]
        public const int int_0 = 1;

        // Token: 0x040016B1 RID: 5809
        [NonSerialized]
        public const int int_1 = 2;

        // Token: 0x040016B2 RID: 5810
        [NonSerialized]
        public const int int_2 = 3;

        // Token: 0x040016B3 RID: 5811
        [NonSerialized]
        public const int int_3 = 4;

        // Token: 0x040016B4 RID: 5812
        [NonSerialized]
        public const int int_4 = 5;

        // Token: 0x040016B5 RID: 5813
        [NonSerialized]
        public const int int_5 = 6;

        // Token: 0x040016B6 RID: 5814
        [NonSerialized]
        public const int int_6 = 7;

        // Token: 0x040016B7 RID: 5815
        [NonSerialized]
        public const int int_7 = 8;

        // Token: 0x040016B8 RID: 5816
        [NonSerialized]
        public const int int_8 = 9;

        // Token: 0x040016B9 RID: 5817
        [NonSerialized]
        public const int int_9 = 12;

        // Token: 0x040016BA RID: 5818
        [NonSerialized]
        public const int int_10 = 13;

        // Token: 0x040016BB RID: 5819
        [NonSerialized]
        public const int int_11 = 14;
    }

}
