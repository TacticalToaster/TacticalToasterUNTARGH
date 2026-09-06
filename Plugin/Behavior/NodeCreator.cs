using CommonAssets.Scripts.AI.CustomBehaviourNodes.Debug;
using EFT;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalToasterUNTARGH.Behavior;

// Token: 0x02000363 RID: 867
public class NodeCreator
{
    // Token: 0x060014C0 RID: 5312 RVA: 0x002BD980 File Offset: 0x002BBB80
    public static AICoreNode CreateNode(BotLogicDecision type, BotOwner bot)
    {
        switch (type)
        {
            case BotLogicDecision.doorOpen:
                return new OpenDoorRequestDecision(bot);
            case BotLogicDecision.warnPlayer:
                return new WarnPlayerDecision(bot);
            case BotLogicDecision.shootToSmoke:
                return new AimingToSmoke(bot);
            case BotLogicDecision.holdPosition:
                return new HoldPosition(bot);
            case BotLogicDecision.runToCover:
                return new RunToCover(bot);
            case BotLogicDecision.attackMoving:
                return new AttackMoving(bot);
            case BotLogicDecision.attackMovingWithSuppress:
                return new AttackMovingWithSuppress(bot);
            case BotLogicDecision.shootFromPlace:
                return new ShootFromPlace(bot);
            case BotLogicDecision.goToEnemy:
                return new GoToEnemy(bot);
            case BotLogicDecision.heal:
                return new HealNode(bot);
            case BotLogicDecision.goToCoverPoint:
                return new GoToCoverPoint(bot);
            case BotLogicDecision.repairMalfunction:
                return new RepairMalfunctionNode(bot);
            case BotLogicDecision.goToCoverPointTactical:
                return new GoToCoverTactical(bot);
            case BotLogicDecision.goToPointTactical:
                return new GoToPointTacticalNode(bot);
            case BotLogicDecision.lay:
                return new LayNode(bot);
            case BotLogicDecision.search:
                return new SearchInvisibleEnemy(bot);
            case BotLogicDecision.shootFromCover:
                return new ShootFromCover(bot);
            case BotLogicDecision.dogFight:
                return new DogFightNode(bot);
            case BotLogicDecision.turnAwayLight:
                return new TurnAwayNode(bot);
            case BotLogicDecision.standBy:
                return new StandByNode(bot);
            case BotLogicDecision.suppressFire:
                return new ShootSuppressNode(bot);
            case BotLogicDecision.suppressGrenade:
                return new GrenadeSuppressNode(bot);
            case BotLogicDecision.throwGrenadeFromPlace:
                return new ThrowGrenadeRequestNode(bot);
            case BotLogicDecision.runAndThrowGrenadeFromPlace:
                return new ThrowGrenadeFromPlaceNode(bot);
            case BotLogicDecision.runToEnemy:
                return new RunToEnemy(bot);
            case BotLogicDecision.runToEnemyZigZag:
                return new RunToEnemyZigZag(bot);
            case BotLogicDecision.goToEnemyZigZag:
                return new GoToEnemyZigZag(bot);
            case BotLogicDecision.goToPoint:
                return new GoToSomePoint(bot);
            case BotLogicDecision.panicSitting:
                return new PanicSitNode(bot);
            case BotLogicDecision.runToStationary:
                return new RunToStationary(bot);
            case BotLogicDecision.shootFromStationary:
                return new ShootFromStationary(bot);
            case BotLogicDecision.suppressStationary:
                return new SuppressStationaryNode(bot);
            case BotLogicDecision.healStimulators:
                return new StimulatorsNode(bot);
            case BotLogicDecision.axeTarget:
                return new PatrolAxeTarget(bot);
            case BotLogicDecision.healAnotherTarget:
                return new HealAnotherNode(bot);
            case BotLogicDecision.oneMeleeAttack:
                return new OneMeleeAttackNode(bot);
            case BotLogicDecision.grenadeSuicide:
                return new GrenadeSuicideNode(bot);
            case BotLogicDecision.leaveMap:
                return new GoLeaveNode(bot);
            case BotLogicDecision.deadBody:
                return new DeadBodiesWorkNode(bot);
            case BotLogicDecision.friendlyTilt:
                return new FriendlyTiltNode(bot);
            case BotLogicDecision.eatDrink:
                return new EatDrinkNode(bot);
            case BotLogicDecision.watchSecondWeapon:
                return new WatchSecondWeaponNode(bot);
            case BotLogicDecision.peaceHardAim:
                return new PeaceHardAimNode(bot);
            case BotLogicDecision.peaceLook:
                return new PeaceLookNode(bot);
            case BotLogicDecision.gesture:
                return new GestureNode(bot);
            case BotLogicDecision.peaceful:
                return new PeacefulNode(bot);
            case BotLogicDecision.botDropItem:
                return new PatrolDropItemsNode(bot);
            case BotLogicDecision.botTakeItem:
                return new PatrolTakeItemsNode(bot);
            case BotLogicDecision.followerPatrol:
                return new PatrollingFollower(bot);
            case BotLogicDecision.alternativePatrol:
                return new PatrollingAlternative(bot);
            case BotLogicDecision.simplePatrol:
                return new PatrolSimpleNode(bot);
            case BotLogicDecision.runAwayGrenade:
                return new RunAwayGrenade(bot);
            case BotLogicDecision.runAwayArtillery:
                return new RunAwayArtillery(bot);
            case BotLogicDecision.runAwayBTR:
                return new RunAwayBTR(bot);
            case BotLogicDecision.followMeRequest:
                return new GoToFollowRequest(bot);
            case BotLogicDecision.runToCoverZigZag:
                return new RunToCoverZigZag(bot);
            case BotLogicDecision.flashed:
                return new FlashedNode(bot);
            case BotLogicDecision.teleportToCover:
                return new TeleportNode(bot);
            case BotLogicDecision.crawl:
                return new CrawlNode(bot);
            case BotLogicDecision.moveStealthy:
                return new MoveStealthy(bot);
            case BotLogicDecision.plantMine:
                return new PlantMineNode(bot);
            case BotLogicDecision.attackMovingFlank:
                return new AttackMovingFlank(bot);
            case BotLogicDecision.deactivateMine:
                return new DeactivateMineNode(bot);
            case BotLogicDecision.goToLootPointNode:
                return new GoToLootPointNode(bot);
            case BotLogicDecision.goToExfiltrationPointNode:
                return new GoToExfiltrationPointNode(bot);
            case BotLogicDecision.khorovodChristmasEvent:
                return new BotKhorovodNode(bot);
            case BotLogicDecision.doGiftChristmasEvent:
                return new GiftNode(bot);
            case BotLogicDecision.summon:
                return new SummonNode(bot);
            case BotLogicDecision.followPlayer:
                return new PlayerFollowNode(bot);
            case BotLogicDecision.debugMove:
                return new DebugMoveNode(bot);
            case BotLogicDecision.debugRun:
                return new DebugRunToCover(bot);
            case BotLogicDecision.debugDrop:
                return new DebugBotDropItemNode(bot);
            case BotLogicDecision.debugTake:
                return new DebugBotTakeItemNode(bot);
            case BotLogicDecision.debugGestus:
                return new GestureNode(bot);
            case BotLogicDecision.debugMeleeChange:
                return new DebugGetMeleeNode(bot);
            case BotLogicDecision.debugGrenade:
                return new DebugGrenadeNode(bot);
            case BotLogicDecision.debugLay:
                return new DebugLayNode(bot);
            case BotLogicDecision.debugMelee:
                return new DebugMeleeAttackNode(bot);
            case BotLogicDecision.debugShuttle:
                return new DebugMoveShuttleNode(bot);
            case BotLogicDecision.debugTacticalShuttle:
                return new DebugMoveShuttleTacticalNode(bot);
            case BotLogicDecision.debugRotateHead:
                return new DebugRotateHeadNode(bot);
            case BotLogicDecision.debugRotate:
                return new DebugRotateNode(bot);
            case BotLogicDecision.debugRotateLay:
                return new DebugRotateLayNode(bot);
            case BotLogicDecision.debugRunToPoint:
                return new DebugRunToPointNode(bot);
            case BotLogicDecision.debugShoot:
                return new DebugShootNode(bot);
            case BotLogicDecision.debugStationary:
                return new DebugStationaryNode(bot);
            case BotLogicDecision.debugWeaponChange:
                return new DebugWeaponChangeNode(bot);
            case BotLogicDecision.debugStationaryInstantTake:
                return new DebugStationaryInstantNode(bot);
            case BotLogicDecision.debugRunToCloseCover:
                return new DebugRunToCloseCoverNode(bot);
            case BotLogicDecision.debugZigZagRunNode:
                return new DebugZigZagRunNode(bot);
            case BotLogicDecision.debugMeds:
                return new DebugMedsNode(bot);
            case BotLogicDecision.debugtacticalMove:
                return new DebugAttackMovingTactical(bot);
            case BotLogicDecision.debugToggleLauncher:
                return new DebugUnderbarrelLauncherNode(bot);
            default:
                if (!AIActionsList._noNodeCollectedError.Contains(type))
                {
                    AIActionsList._noNodeCollectedError.Add(type);
                    Debug.LogError("Action:" + type.ToString() + " have no node");
                }
                return null;
        }
    }

    // Token: 0x060014C1 RID: 5313 RVA: 0x002BDDD0 File Offset: 0x002BBFD0
    public static Dictionary<BotLogicDecision, AICoreNode> ActionsList(BotOwner bot)
    {
        Dictionary<BotLogicDecision, AICoreNode> dictionary = new Dictionary<BotLogicDecision, AICoreNode>();
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.holdPosition, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.goToCoverPoint, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.attackMoving, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.attackMovingWithSuppress, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.shootFromPlace, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.simplePatrol, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.followerPatrol, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.lay, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.plantMine, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.crawl, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.moveStealthy, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.attackMovingFlank, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.teleportToCover, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.runToCover, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.goToEnemy, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.runToEnemy, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.runToStationary, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.suppressStationary, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.shootFromStationary, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.dogFight, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.search, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.shootFromCover, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.deactivateMine, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.runAwayGrenade, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.runAwayBTR, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.runToEnemyZigZag, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.shootToSmoke, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.suppressFire, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.followPlayer, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.heal, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.repairMalfunction, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.goToPoint, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.goToPointTactical, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.axeTarget, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.oneMeleeAttack, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.grenadeSuicide, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.warnPlayer, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.doorOpen, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.panicSitting, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.healStimulators, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.healAnotherTarget, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.deadBody, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.friendlyTilt, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.eatDrink, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.watchSecondWeapon, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.gesture, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.peaceful, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.followMeRequest, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.peaceHardAim, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.peaceLook, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.suppressGrenade, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.runAndThrowGrenadeFromPlace, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.throwGrenadeFromPlace, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.alternativePatrol, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.botDropItem, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.goToLootPointNode, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.goToExfiltrationPointNode, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.botTakeItem, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.flashed, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.standBy, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.turnAwayLight, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.leaveMap, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.runToCoverZigZag, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.summon, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.khorovodChristmasEvent, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.doGiftChristmasEvent, bot);
        AIActionsList.AddNodeTo(dictionary, BotLogicDecision.goToCoverPointTactical, bot);
        return dictionary;
    }

    // Token: 0x060014C2 RID: 5314 RVA: 0x00183BCB File Offset: 0x00181DCB
    public static void smethod_0(Dictionary<BotLogicDecision, AICoreNode> dictionary, BotLogicDecision botLogicDecision, BotOwner bot)
    {
        dictionary.Add(botLogicDecision, AIActionsList.CreateNode(botLogicDecision, bot));
    }

    // Token: 0x04000FBD RID: 4029
    [NonSerialized]
    public static HashSet<BotLogicDecision> HashSet_0 = new HashSet<BotLogicDecision>();
}
