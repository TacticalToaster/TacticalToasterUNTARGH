using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFT;
using UnityEngine;

namespace TacticalToasterUNTARGH.Behavior;

// Token: 0x02000363 RID: 867
public class NodeCreator
{
    // Token: 0x060015D4 RID: 5588 RVA: 0x00295C08 File Offset: 0x00293E08
    public static GClass168 CreateNode(BotLogicDecision type, BotOwner bot)
    {
        switch (type)
        {
            case BotLogicDecision.doorOpen:
                return new GClass252(bot);
            case BotLogicDecision.warnPlayer:
                return new GClass282(bot);
            case BotLogicDecision.shootToSmoke:
                return new GClass178(bot);
            case BotLogicDecision.holdPosition:
                return new GClass271(bot);
            case BotLogicDecision.runToCover:
                return new GClass221(bot);
            case BotLogicDecision.attackMoving:
                return new GClass198(bot);
            case BotLogicDecision.attackMovingWithSuppress:
                return new GClass199(bot);
            case BotLogicDecision.shootFromPlace:
                return new GClass269(bot);
            case BotLogicDecision.goToEnemy:
                return new GClass216(bot);
            case BotLogicDecision.heal:
                return new GClass190(bot);
            case BotLogicDecision.goToCoverPoint:
                return new GClass205(bot);
            case BotLogicDecision.repairMalfunction:
                return new GClass266(bot);
            case BotLogicDecision.goToCoverPointTactical:
                return new GClass231(bot);
            case BotLogicDecision.goToPointTactical:
                return new GClass232(bot);
            case BotLogicDecision.lay:
                return new GClass191(bot);
            case BotLogicDecision.search:
                return new GClass228(bot);
            case BotLogicDecision.shootFromCover:
                return new GClass270(bot);
            case BotLogicDecision.dogFight:
                return new GClass196(bot);
            case BotLogicDecision.turnAwayLight:
                return new GClass281(bot);
            case BotLogicDecision.standBy:
                return new GClass275(bot);
            case BotLogicDecision.suppressFire:
                return new GClass274(bot);
            case BotLogicDecision.suppressGrenade:
                return new GClass188(bot);
            case BotLogicDecision.throwGrenadeFromPlace:
                return new GClass280(bot);
            case BotLogicDecision.runAndThrowGrenadeFromPlace:
                return new GClass279(bot);
            case BotLogicDecision.runToEnemy:
                return new GClass220(bot);
            case BotLogicDecision.runToEnemyZigZag:
                return new GClass219(bot);
            case BotLogicDecision.goToEnemyZigZag:
                return new GClass218(bot);
            case BotLogicDecision.goToPoint:
                return new GClass212(bot);
            case BotLogicDecision.panicSitting:
                return new GClass253(bot);
            case BotLogicDecision.runToStationary:
                return new GClass227(bot);
            case BotLogicDecision.shootFromStationary:
                return new GClass273(bot);
            case BotLogicDecision.suppressStationary:
                return new GClass277(bot);
            case BotLogicDecision.healStimulators:
                return new GClass276(bot);
            case BotLogicDecision.axeTarget:
                return new GClass239(bot);
            case BotLogicDecision.healAnotherTarget:
                return new GClass189(bot);
            case BotLogicDecision.oneMeleeAttack:
                return new GClass235(bot);
            case BotLogicDecision.grenadeSuicide:
                return new GClass187(bot);
            case BotLogicDecision.leaveMap:
                return new GClass236(bot);
            case BotLogicDecision.deadBody:
                return new GClass195(bot);
            case BotLogicDecision.friendlyTilt:
                return new GClass255(bot);
            case BotLogicDecision.eatDrink:
                return new GClass254(bot);
            case BotLogicDecision.watchSecondWeapon:
                return new GClass264(bot);
            case BotLogicDecision.peaceHardAim:
                return new GClass260(bot);
            case BotLogicDecision.peaceLook:
                return new GClass261(bot);
            case BotLogicDecision.gesture:
                return new GClass256(bot);
            case BotLogicDecision.peaceful:
                return new GClass259(bot);
            case BotLogicDecision.botDropItem:
                return new GClass257(bot);
            case BotLogicDecision.botTakeItem:
                return new GClass258(bot);
            case BotLogicDecision.followerPatrol:
                return new GClass241(bot);
            case BotLogicDecision.alternativePatrol:
                return new GClass240(bot);
            case BotLogicDecision.simplePatrol:
                return new GClass243(bot);
            case BotLogicDecision.runAwayGrenade:
                return new GClass225(bot);
            case BotLogicDecision.runAwayArtillery:
                return new GClass223(bot);
            case BotLogicDecision.runAwayBTR:
                return new GClass224(bot);
            case BotLogicDecision.followMeRequest:
                return new GClass208(bot);
            case BotLogicDecision.runToCoverZigZag:
                return new GClass222(bot);
            case BotLogicDecision.flashed:
                return new GClass181(bot);
            case BotLogicDecision.teleportToCover:
                return new GClass251(bot);
            case BotLogicDecision.crawl:
                return new GClass200(bot);
            case BotLogicDecision.moveStealthy:
                return new GClass203(bot);
            case BotLogicDecision.plantMine:
                return new GClass265(bot);
            case BotLogicDecision.attackMovingFlank:
                return new GClass202(bot);
            case BotLogicDecision.deactivateMine:
                return new GClass194(bot);
            case BotLogicDecision.goToLootPointNode:
                return new GClass238(bot);
            case BotLogicDecision.goToExfiltrationPointNode:
                return new GClass237(bot);
            case BotLogicDecision.khorovodChristmasEvent:
                return new GClass184(bot);
            case BotLogicDecision.doGiftChristmasEvent:
                return new GClass185(bot);
            case BotLogicDecision.summon:
                return new GClass186(bot);
            case BotLogicDecision.followPlayer:
                return new GClass197(bot);
            case BotLogicDecision.debugMove:
                return new GClass245(bot);
            case BotLogicDecision.debugRun:
                return new GClass248(bot);
            case BotLogicDecision.debugDrop:
                return new GClass284(bot);
            case BotLogicDecision.debugTake:
                return new GClass285(bot);
            case BotLogicDecision.debugGestus:
                return new GClass256(bot);
            case BotLogicDecision.debugMeleeChange:
                return new GClass288(bot);
            case BotLogicDecision.debugGrenade:
                return new GClass289(bot);
            case BotLogicDecision.debugLay:
                return new GClass290(bot);
            case BotLogicDecision.debugMelee:
                return new GClass292(bot);
            case BotLogicDecision.debugShuttle:
                return new GClass246(bot);
            case BotLogicDecision.debugTacticalShuttle:
                return new GClass233(bot);
            case BotLogicDecision.debugRotateHead:
                return new GClass293(bot);
            case BotLogicDecision.debugRotate:
                return new GClass294(bot);
            case BotLogicDecision.debugRotateLay:
                return new GClass295(bot);
            case BotLogicDecision.debugRunToPoint:
                return new GClass249(bot);
            case BotLogicDecision.debugShoot:
                return new GClass297(bot);
            case BotLogicDecision.debugStationary:
                return new GClass298(bot);
            case BotLogicDecision.debugWeaponChange:
                return new GClass300(bot);
            case BotLogicDecision.debugStationaryInstantTake:
                return new GClass299(bot);
            case BotLogicDecision.debugRunToCloseCover:
                return new GClass247(bot);
            case BotLogicDecision.debugZigZagRunNode:
                return new GClass250(bot);
            case BotLogicDecision.debugMeds:
                return new GClass291(bot);
            case BotLogicDecision.debugtacticalMove:
                return new GClass234(bot);
            case BotLogicDecision.debugToggleLauncher:
                return new GClass301(bot);
            default:
                if (!GClass522.hashSet_0.Contains(type))
                {
                    GClass522.hashSet_0.Add(type);
                    Debug.LogError("Action:" + type.ToString() + " have no node");
                }
                return null;
        }
    }

    // Token: 0x060015D5 RID: 5589 RVA: 0x00296058 File Offset: 0x00294258
    public static Dictionary<BotLogicDecision, GClass168> ActionsList(BotOwner bot)
    {
        Dictionary<BotLogicDecision, GClass168> dictionary = new Dictionary<BotLogicDecision, GClass168>();
        GClass522.smethod_0(dictionary, BotLogicDecision.holdPosition, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.goToCoverPoint, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.attackMoving, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.attackMovingWithSuppress, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.shootFromPlace, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.simplePatrol, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.followerPatrol, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.lay, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.plantMine, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.crawl, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.moveStealthy, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.attackMovingFlank, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.teleportToCover, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.runToCover, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.goToEnemy, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.runToEnemy, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.runToStationary, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.suppressStationary, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.shootFromStationary, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.dogFight, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.search, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.shootFromCover, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.deactivateMine, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.runAwayGrenade, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.runAwayBTR, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.runToEnemyZigZag, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.shootToSmoke, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.suppressFire, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.followPlayer, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.heal, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.repairMalfunction, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.goToPoint, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.goToPointTactical, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.axeTarget, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.oneMeleeAttack, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.grenadeSuicide, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.warnPlayer, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.doorOpen, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.panicSitting, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.healStimulators, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.healAnotherTarget, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.deadBody, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.friendlyTilt, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.eatDrink, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.watchSecondWeapon, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.gesture, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.peaceful, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.followMeRequest, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.peaceHardAim, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.peaceLook, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.suppressGrenade, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.runAndThrowGrenadeFromPlace, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.throwGrenadeFromPlace, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.alternativePatrol, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.botDropItem, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.goToLootPointNode, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.goToExfiltrationPointNode, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.botTakeItem, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.flashed, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.standBy, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.turnAwayLight, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.leaveMap, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.runToCoverZigZag, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.summon, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.khorovodChristmasEvent, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.doGiftChristmasEvent, bot);
        GClass522.smethod_0(dictionary, BotLogicDecision.goToCoverPointTactical, bot);
        return dictionary;
    }

    // Token: 0x060015D6 RID: 5590 RVA: 0x00179E28 File Offset: 0x00178028
    public static void smethod_0(Dictionary<BotLogicDecision, GClass168> dictionary, BotLogicDecision botLogicDecision, BotOwner bot)
    {
        dictionary.Add(botLogicDecision, GClass522.CreateNode(botLogicDecision, bot));
    }

    // Token: 0x04001140 RID: 4416
    [NonSerialized]
    public static HashSet<BotLogicDecision> hashSet_0 = new HashSet<BotLogicDecision>();
}
