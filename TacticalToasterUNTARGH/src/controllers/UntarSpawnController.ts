import { ApplicationContext } from "@spt/context/ApplicationContext";
import { ContextVariableType } from "@spt/context/ContextVariableType";
import { IGetRaidConfigurationRequestData } from "@spt/models/eft/match/IGetRaidConfigurationRequestData";
import { ILogger } from "@spt/models/spt/utils/ILogger";
import { ConfigServer } from "@spt/servers/ConfigServer";
import { DatabaseService } from "@spt/services/DatabaseService";
import { StaticRouterModService } from "@spt/services/mod/staticRouter/StaticRouterModService";
import { HttpResponseUtil } from "@spt/utils/HttpResponseUtil";
import { JsonUtil } from "@spt/utils/JsonUtil";
import { RandomUtil } from "@spt/utils/RandomUtil";
import { container, inject, injectable } from "tsyringe";
import { IMainConfig } from '../models/IMainConfig';
import { IBossLocationSpawn } from "../../types/models/eft/common/ILocationBase";
import { IBotConfig } from "@spt/models/spt/config/IBotConfig";
import { ConfigTypes } from "@spt/models/enums/ConfigTypes";

import mainConfig = require("../../config/main.json");
import { UNTARLogger } from "../logger";

//const mainConfig: IMainConfig = require("../../config/main.json");

@injectable()
export class UntarSpawnController {
    constructor(
        @inject("StaticRouterModService") protected staticRouter: StaticRouterModService,
        @inject("JsonUtil") protected jsonUtil: JsonUtil,
        @inject("RandomUtil") protected randomUtil: RandomUtil,
        @inject("ConfigServer") protected configServer: ConfigServer,
        @inject("DatabaseService") protected databaseService: DatabaseService,
        @inject("WinstonLogger") protected wLogger: ILogger,
        @inject("HttpResponseUtil") protected httpResponse: HttpResponseUtil,
        @inject("UNTARLogger") protected logger: UNTARLogger
    ) {}

    public adjustAllUntarSpawns() {
        try {
            const dbService = container.resolve<DatabaseService>("DatabaseService");
            const tables = dbService.getTables();
            const botConfig = this.configServer.getConfig<IBotConfig>(ConfigTypes.BOT);

            for (const map in mainConfig.locations) {
                this.logger.info(`Adjusting UNTAR spawns for ${map}.`);

                const mapConfig = mainConfig.locations[map];

                if (!tables.locations[map]) {
                    this.logger.info(`No location data found for ${map}. Skipping UNTAR spawn adjustment.`);
                    return;
                }

                const spawns = tables.locations[map].base.BossLocationSpawn as IBossLocationSpawn[];

                tables.locations[map].base.BossLocationSpawn = spawns.filter(
                    x => x.BossName.search("untar") === -1
                );

                if (mainConfig.locations[map] && mainConfig.locations[map].enablePatrols) {
                    this.logger.info(`Enabling UNTAR patrols for ${map}.`);

                    let validZones: string[] = mapConfig.patrolZones;

                    for (let i = 0; i < mapConfig.patrolAmount; i++) {
                        const patrolSize = this.randomUtil.getInt(mapConfig.patrolMin, mapConfig.patrolMax);
                        const patrol: IBossLocationSpawn = this.generatePatrol(
                            patrolSize,
                            mapConfig.patrolChance
                        );

                        patrol.BossZone = this.randomUtil.getArrayValue(validZones);
                        validZones = validZones.filter(zone => zone !== patrol.BossZone);

                        if (validZones.length === 0) {
                            validZones = mapConfig.patrolZones;
                        }

                        patrol.Time = this.randomUtil.getInt(
                            mapConfig.patrolTimeMin,
                            mapConfig.patrolTimeMax
                        );

                        if (mainConfig.debug.spawnInstantlyAlawys) {
                            this.logger.info(`Instantly spawning UNTAR patrol for ${map}.`);
                            patrol.Time = -1;
                        }

                        tables.locations[map].base.BossLocationSpawn.push(patrol);

                        this.logger.info(`Added (${mapConfig.patrolChance}% chance) UNTAR patrol of size ${patrolSize} to ${map} in zone ${patrol.BossZone} with a spawn time of ${patrol.Time} seconds.`);

                        //if (this.randomUtil.getChance100(mainConfig.locations[map].patrolChance)) {
                        //const patrolAmount = this.randomUtil.getInt(
                        //    mainConfig.locations[map].patrolMin,
                        //    mainConfig.locations[map].patrolMax
                        //);
                        //}
                    }
                    
                }
            }
        }
        catch (error) {
            this.logger.error(`Error adjusting untar spawns: ${error}`);
            throw error;
        }
    }

    public async adjustUntarSpawns(url: string, info: any, sessionID: string, output: string) {

        try {
            const dbService = container.resolve<DatabaseService>("DatabaseService");
            const tables = dbService.getTables();
            const botConfig = this.configServer.getConfig<IBotConfig>(ConfigTypes.BOT);
            const appContext = container.resolve<ApplicationContext>("ApplicationContext");
            const match = appContext.getLatestValue(ContextVariableType.RAID_CONFIGURATION).getValue<IGetRaidConfigurationRequestData>();
            const map = match.location.toLowerCase();
            this.logger.info(`Adjusting UNTAR spawns for ${map}.`);

            if (!tables.locations[map]) {
                this.logger.info(`No location data found for ${map}. Skipping UNTAR spawn adjustment.`);
                return output;
            }
            const spawns = tables.locations[map].base.BossLocationSpawn as IBossLocationSpawn[];

            if (mainConfig.locations[map] && mainConfig.locations[map].enablePatrols) {
                this.logger.info(`Enabling UNTAR patrols for ${map}.`);

                const mapConfig = mainConfig.locations[map];

                tables.locations[map].base.BossLocationSpawn = spawns.filter(
                    x => x.BossName.search("untar") === -1
                );

                let validZones: string[] = mapConfig.patrolZones;

                for (let i = 0; i < mapConfig.patrolAmount; i++) {
                    const patrolSize = this.randomUtil.getInt(mapConfig.patrolMin, mapConfig.patrolMax);
                    const patrol: IBossLocationSpawn = this.generatePatrol(
                        patrolSize,
                        mapConfig.patrolChance
                    );

                    patrol.BossZone = this.randomUtil.getArrayValue(validZones);
                    validZones = validZones.filter(zone => zone !== patrol.BossZone);

                    if (validZones.length === 0) {
                        validZones = mapConfig.patrolZones;
                    }

                    patrol.Time = this.randomUtil.getInt(
                        mapConfig.patrolTimeMin,
                        mapConfig.patrolTimeMax
                    );

                    tables.locations[map].base.BossLocationSpawn.push(patrol);

                    this.logger.info(`Added (${mapConfig.patrolChance}% chance) UNTAR patrol of size ${patrolSize} to ${map} in zone ${patrol.BossZone} with a spawn time of ${patrol.Time} minutes.`);

                    //if (this.randomUtil.getChance100(mainConfig.locations[map].patrolChance)) {
                    //const patrolAmount = this.randomUtil.getInt(
                    //    mainConfig.locations[map].patrolMin,
                    //    mainConfig.locations[map].patrolMax
                    //);
                    //}
                }
                
            }
        }
        catch (error) {
            this.logger.error(`Error adjusting untar spawns: ${error}`);
            throw error;
        }
        return output;
    }

    private generatePatrol(patrolSize: number, chance: number): IBossLocationSpawn {
        let bossType = "bossuntarlead";
        let secondLeader = "";
        let followers = patrolSize - 1;

        this.logger.info(`Generating UNTAR patrol of size ${patrolSize}.`);

        if (patrolSize >= mainConfig.patrols.minOfficerSize) {
            if (this.randomUtil.getChance100(mainConfig.patrols.officerChance)) {
                this.logger.info("UNTAR patrol leader changed to Officer.");
                bossType = "bossuntarofficer";
            }

            if (patrolSize >= mainConfig.patrols.minSecondLeaderSize) {
                if (this.randomUtil.getChance100(mainConfig.patrols.secondLeaderChance)) {
                    this.logger.info("UNTAR patrol second leader added.");
                    secondLeader = "bossuntarlead";
                    followers = followers - 1;
                }
            }
        }

        const bossInfo: IBossLocationSpawn = {
            BossChance: chance,
            BossDifficult: "normal",
            BossEscortAmount: "1",
            BossEscortDifficult: "normal",
            BossEscortType: "followeruntar",
            BossName: bossType,
            BossPlayer: false,
            BossZone: "",
            ForceSpawn: false,
            IgnoreMaxBots: true,
            RandomTimeSpawn: false,
            SpawnMode: [
                "regular",
                "pve"
            ],
            Supports: [],
            Time: -1,
            TriggerId: "",
            TriggerName: ""
        }

        if (secondLeader !== "") {
            bossInfo.Supports.push({
                "BossEscortAmount": "1",
                "BossEscortDifficult": [ "normal" ],
                "BossEscortType": secondLeader
            });
        }

        bossInfo.Supports.push({
            "BossEscortAmount": followers.toString(),
            "BossEscortDifficult": [ "normal" ],
            "BossEscortType": "followeruntar"
        });

        return bossInfo;
    }

    /*dynamicRouterModService.registerDynamicRouter(
            "UNTARDifficultiesRouter",
            [
                {
                    url: "/singleplayer/settings/bot/difficulties",
                    action: async (url: string, info: IEmptyRequestData, sessionID: string, output: string): Promise<string> => {
                        return untarDBController.getBotDifficulties(url, info, sessionID, output);
                    }
                }
            ],
            ""
        );*/
}