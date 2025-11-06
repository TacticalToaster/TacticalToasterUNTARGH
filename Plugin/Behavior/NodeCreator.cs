using EFT;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalToasterUNTARGH.Behavior;

// Token: 0x02000363 RID: 867
public class NodeCreator
{
    // Token: 0x060014C0 RID: 5312 RVA: 0x002BD980 File Offset: 0x002BBB80
    public static BotNodeAbstractClass CreateNode(BotLogicDecision type, BotOwner bot)
    {
        switch (type)
        {
            case BotLogicDecision.doorOpen:
                return new GClass259(bot);
            case BotLogicDecision.warnPlayer:
                return new GClass289(bot);
            case BotLogicDecision.shootToSmoke:
                return new GClass185(bot);
            case BotLogicDecision.holdPosition:
                return new GClass278(bot);
            case BotLogicDecision.runToCover:
                return new GClass228(bot);
            case BotLogicDecision.attackMoving:
                return new GClass205(bot);
            case BotLogicDecision.attackMovingWithSuppress:
                return new GClass206(bot);
            case BotLogicDecision.shootFromPlace:
                return new GClass276(bot);
            case BotLogicDecision.goToEnemy:
                return new GClass223(bot);
            case BotLogicDecision.heal:
                return new GClass197(bot);
            case BotLogicDecision.goToCoverPoint:
                return new GClass212(bot);
            case BotLogicDecision.repairMalfunction:
                return new GClass273(bot);
            case BotLogicDecision.goToCoverPointTactical:
                return new GClass238(bot);
            case BotLogicDecision.goToPointTactical:
                return new GClass239(bot);
            case BotLogicDecision.lay:
                return new GClass198(bot);
            case BotLogicDecision.search:
                return new GClass235(bot);
            case BotLogicDecision.shootFromCover:
                return new GClass277(bot);
            case BotLogicDecision.dogFight:
                return new GClass203(bot);
            case BotLogicDecision.turnAwayLight:
                return new GClass288(bot);
            case BotLogicDecision.standBy:
                return new GClass282(bot);
            case BotLogicDecision.suppressFire:
                return new GClass281(bot);
            case BotLogicDecision.suppressGrenade:
                return new GClass195(bot);
            case BotLogicDecision.throwGrenadeFromPlace:
                return new GClass287(bot);
            case BotLogicDecision.runAndThrowGrenadeFromPlace:
                return new GClass286(bot);
            case BotLogicDecision.runToEnemy:
                return new GClass227(bot);
            case BotLogicDecision.runToEnemyZigZag:
                return new GClass226(bot);
            case BotLogicDecision.goToEnemyZigZag:
                return new GClass225(bot);
            case BotLogicDecision.goToPoint:
                return new GClass219(bot);
            case BotLogicDecision.panicSitting:
                return new GClass260(bot);
            case BotLogicDecision.runToStationary:
                return new GClass234(bot);
            case BotLogicDecision.shootFromStationary:
                return new GClass280(bot);
            case BotLogicDecision.suppressStationary:
                return new GClass284(bot);
            case BotLogicDecision.healStimulators:
                return new GClass283(bot);
            case BotLogicDecision.axeTarget:
                return new GClass246(bot);
            case BotLogicDecision.healAnotherTarget:
                return new GClass196(bot);
            case BotLogicDecision.oneMeleeAttack:
                return new GClass242(bot);
            case BotLogicDecision.grenadeSuicide:
                return new GClass194(bot);
            case BotLogicDecision.leaveMap:
                return new GClass243(bot);
            case BotLogicDecision.deadBody:
                return new GClass202(bot);
            case BotLogicDecision.friendlyTilt:
                return new GClass262(bot);
            case BotLogicDecision.eatDrink:
                return new GClass261(bot);
            case BotLogicDecision.watchSecondWeapon:
                return new GClass271(bot);
            case BotLogicDecision.peaceHardAim:
                return new GClass267(bot);
            case BotLogicDecision.peaceLook:
                return new GClass268(bot);
            case BotLogicDecision.gesture:
                return new GClass263(bot);
            case BotLogicDecision.peaceful:
                return new GClass266(bot);
            case BotLogicDecision.botDropItem:
                return new GClass264(bot);
            case BotLogicDecision.botTakeItem:
                return new GClass265(bot);
            case BotLogicDecision.followerPatrol:
                return new GClass248(bot);
            case BotLogicDecision.alternativePatrol:
                return new GClass247(bot);
            case BotLogicDecision.simplePatrol:
                return new GClass250(bot);
            case BotLogicDecision.runAwayGrenade:
                return new GClass232(bot);
            case BotLogicDecision.runAwayArtillery:
                return new GClass230(bot);
            case BotLogicDecision.runAwayBTR:
                return new GClass231(bot);
            case BotLogicDecision.followMeRequest:
                return new GClass215(bot);
            case BotLogicDecision.runToCoverZigZag:
                return new GClass229(bot);
            case BotLogicDecision.flashed:
                return new GClass188(bot);
            case BotLogicDecision.teleportToCover:
                return new GClass258(bot);
            case BotLogicDecision.crawl:
                return new GClass207(bot);
            case BotLogicDecision.moveStealthy:
                return new GClass210(bot);
            case BotLogicDecision.plantMine:
                return new GClass272(bot);
            case BotLogicDecision.attackMovingFlank:
                return new GClass209(bot);
            case BotLogicDecision.deactivateMine:
                return new GClass201(bot);
            case BotLogicDecision.goToLootPointNode:
                return new GClass245(bot);
            case BotLogicDecision.goToExfiltrationPointNode:
                return new GClass244(bot);
            case BotLogicDecision.khorovodChristmasEvent:
                return new GClass191(bot);
            case BotLogicDecision.doGiftChristmasEvent:
                return new GClass192(bot);
            case BotLogicDecision.summon:
                return new GClass193(bot);
            case BotLogicDecision.followPlayer:
                return new GClass204(bot);
            case BotLogicDecision.debugMove:
                return new GClass252(bot);
            case BotLogicDecision.debugRun:
                return new GClass255(bot);
            case BotLogicDecision.debugDrop:
                return new GClass291(bot);
            case BotLogicDecision.debugTake:
                return new GClass292(bot);
            case BotLogicDecision.debugGestus:
                return new GClass263(bot);
            case BotLogicDecision.debugMeleeChange:
                return new GClass295(bot);
            case BotLogicDecision.debugGrenade:
                return new GClass296(bot);
            case BotLogicDecision.debugLay:
                return new GClass297(bot);
            case BotLogicDecision.debugMelee:
                return new GClass299(bot);
            case BotLogicDecision.debugShuttle:
                return new GClass253(bot);
            case BotLogicDecision.debugTacticalShuttle:
                return new GClass240(bot);
            case BotLogicDecision.debugRotateHead:
                return new GClass300(bot);
            case BotLogicDecision.debugRotate:
                return new GClass301(bot);
            case BotLogicDecision.debugRotateLay:
                return new GClass302(bot);
            case BotLogicDecision.debugRunToPoint:
                return new GClass256(bot);
            case BotLogicDecision.debugShoot:
                return new GClass304(bot);
            case BotLogicDecision.debugStationary:
                return new GClass305(bot);
            case BotLogicDecision.debugWeaponChange:
                return new GClass307(bot);
            case BotLogicDecision.debugStationaryInstantTake:
                return new GClass306(bot);
            case BotLogicDecision.debugRunToCloseCover:
                return new GClass254(bot);
            case BotLogicDecision.debugZigZagRunNode:
                return new GClass257(bot);
            case BotLogicDecision.debugMeds:
                return new GClass298(bot);
            case BotLogicDecision.debugtacticalMove:
                return new GClass241(bot);
            case BotLogicDecision.debugToggleLauncher:
                return new GClass308(bot);
            default:
                if (!BotActionNodesClass.HashSet_0.Contains(type))
                {
                    BotActionNodesClass.HashSet_0.Add(type);
                    Debug.LogError("Action:" + type.ToString() + " have no node");
                }
                return null;
        }
    }

    // Token: 0x060014C1 RID: 5313 RVA: 0x002BDDD0 File Offset: 0x002BBFD0
    public static Dictionary<BotLogicDecision, BotNodeAbstractClass> ActionsList(BotOwner bot)
    {
        Dictionary<BotLogicDecision, BotNodeAbstractClass> dictionary = new Dictionary<BotLogicDecision, BotNodeAbstractClass>();
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.holdPosition, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.goToCoverPoint, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.attackMoving, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.attackMovingWithSuppress, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.shootFromPlace, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.simplePatrol, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.followerPatrol, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.lay, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.plantMine, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.crawl, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.moveStealthy, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.attackMovingFlank, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.teleportToCover, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.runToCover, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.goToEnemy, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.runToEnemy, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.runToStationary, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.suppressStationary, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.shootFromStationary, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.dogFight, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.search, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.shootFromCover, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.deactivateMine, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.runAwayGrenade, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.runAwayBTR, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.runToEnemyZigZag, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.shootToSmoke, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.suppressFire, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.followPlayer, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.heal, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.repairMalfunction, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.goToPoint, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.goToPointTactical, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.axeTarget, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.oneMeleeAttack, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.grenadeSuicide, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.warnPlayer, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.doorOpen, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.panicSitting, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.healStimulators, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.healAnotherTarget, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.deadBody, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.friendlyTilt, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.eatDrink, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.watchSecondWeapon, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.gesture, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.peaceful, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.followMeRequest, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.peaceHardAim, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.peaceLook, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.suppressGrenade, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.runAndThrowGrenadeFromPlace, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.throwGrenadeFromPlace, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.alternativePatrol, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.botDropItem, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.goToLootPointNode, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.goToExfiltrationPointNode, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.botTakeItem, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.flashed, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.standBy, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.turnAwayLight, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.leaveMap, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.runToCoverZigZag, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.summon, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.khorovodChristmasEvent, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.doGiftChristmasEvent, bot);
        BotActionNodesClass.smethod_0(dictionary, BotLogicDecision.goToCoverPointTactical, bot);
        return dictionary;
    }

    // Token: 0x060014C2 RID: 5314 RVA: 0x00183BCB File Offset: 0x00181DCB
    public static void smethod_0(Dictionary<BotLogicDecision, BotNodeAbstractClass> dictionary, BotLogicDecision botLogicDecision, BotOwner bot)
    {
        dictionary.Add(botLogicDecision, BotActionNodesClass.CreateNode(botLogicDecision, bot));
    }

    // Token: 0x04000FBD RID: 4029
    [NonSerialized]
    public static HashSet<BotLogicDecision> HashSet_0 = new HashSet<BotLogicDecision>();
}
