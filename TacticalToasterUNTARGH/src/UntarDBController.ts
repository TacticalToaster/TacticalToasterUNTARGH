import {DependencyContainer, inject, injectable} from "tsyringe"
// SPT Imports
import type { StaticRouterModService } from "@spt/services/mod/staticRouter/StaticRouterModService";
import type { IBossLocationSpawn } from "@spt/models/eft/common/ILocationBase";
import type { DatabaseService } from "@spt/services/DatabaseService";
import type { IBotConfig } from "@spt/models/spt/config/IBotConfig";
import { LogTextColor } from "@spt/models/spt/logging/LogTextColor";
import type { ConfigServer } from "@spt/servers/ConfigServer";
import { ConfigTypes } from "@spt/models/enums/ConfigTypes";
import type { RandomUtil } from "@spt/utils/RandomUtil";
import type { JsonUtil } from "@spt/utils/JsonUtil";
import { WildSpawnTypeNumber } from "@spt/models/enums/WildSpawnTypeNumber";
import { ILogger } from "@spt/models/spt/utils/ILogger";
import { IEmptyRequestData } from "@spt/models/eft/common/IEmptyRequestData";
import { HttpResponseUtil } from "@spt/utils/HttpResponseUtil";
// Configs
import untarBotType = require("../db/bots/types/test.json")
import untarBotInfo = require("../db/bots/info/untarInfo.json")
import untarLoadout from "../db/bots/types/test.json"
import untarMarksmanLoadout from "../db/bots/types/test.json"
import untarLeadLoadout from "../db/bots/types/untarlead.json"
import untarOfficerLoadout from "../db/bots/types/untarofficer.json"
import { BotHelper } from "@spt/helpers/BotHelper";
import { BotController } from "@spt/controllers/BotController";

@injectable()
export class UntarDBController {
    constructor(
        @inject("StaticRouterModService") protected staticRouter: StaticRouterModService,
        @inject("JsonUtil") protected jsonUtil: JsonUtil,
        @inject("RandomUtil") protected randomUtil: RandomUtil,
        @inject("ConfigServer") protected configServer: ConfigServer,
        @inject("DatabaseService") protected databaseService: DatabaseService,
        @inject("WinstonLogger") protected logger: ILogger,
        @inject("HttpResponseUtil") protected httpResponse: HttpResponseUtil,
        @inject("BotHelper") protected botHelper: BotHelper,
        @inject("BotController") protected botController: BotController
    ) {}

    public addUntarToFactory(): void {

        this.logger.info("Creating UNTAR spawn!");

        const dbTables = this.databaseService.getTables();
        const bossInfo: IBossLocationSpawn = {
            BossChance: 100,
            BossDifficult: "normal",
            BossEscortAmount: "1",
            BossEscortDifficult: "normal",
            BossEscortType: "followeruntar",
            BossName: "bossuntarofficer",
            BossPlayer: false,
            BossZone: "ZoneFactoryCenter",
            ForceSpawn: false,
            IgnoreMaxBots: true,
            RandomTimeSpawn: false,
            SpawnMode: [
                "regular",
                "pve"
            ],
            Supports: [
                {
                    "BossEscortAmount": "1",
                    "BossEscortDifficult": [
                        "normal"
                    ],
                    "BossEscortType": "bossuntarlead"
                },
                {
                    "BossEscortAmount": "4",
                    "BossEscortDifficult": [
                        "normal"
                    ],
                    "BossEscortType": "followeruntar"
                }
            ],
            Time: -1,
            TriggerId: "",
            TriggerName: ""
        }

        //dbTables.locations.bigmap.base.BossLocationSpawn = [];
        //dbTables.locations.bigmap.base.BossLocationSpawn.push(bossInfo);



        this.logger.info("UNTAR spawn should be created!");

    }

    public addUntarToDB(): void {
        this.logger.info("Adding UNTAR to database alongside enums.");

        (WildSpawnTypeNumber as any).FOLLOWERUNTAR = 1170;
        (WildSpawnTypeNumber as any).BOSSUNTARLEAD = 1171;
        (WildSpawnTypeNumber as any).FOLLOWERUNTARMARKSMAN = 1172;
        (WildSpawnTypeNumber as any).BOSSUNTAROFFICER = 1173;

        this.logger.logWithColor(`Extended WildSpawnTypeNumber enum with UNTAR types ${(WildSpawnTypeNumber as any).FOLLOWERUNTAR}.`, LogTextColor.CYAN);

        const dbTables = this.databaseService.getTables();
        const botConfig = this.configServer.getConfig<IBotConfig>(ConfigTypes.BOT);
        const presetBatch: any = botConfig.presetBatch;
        /*const stupidfuckingbotsettings = {
            "nvgIsActiveChanceDayPercent": 10,
            "nvgIsActiveChanceNightPercent": 90,
            "faceShieldIsActiveChancePercent": 100,
            "lightIsActiveDayChancePercent": 25,
            "lightIsActiveNightChancePercent": 90,
            "weaponSightWhitelist": {},
            "laserIsActiveChancePercent": 85,
            "randomisation": [],
            "blacklist": [],
            "whitelist": [],
            "weightingAdjustmentsByPlayerLevel": [],
            "weightingAdjustmentsByBotLevel": [],
            "forceStock": false,
            "weaponModLimits": {
                "scopeLimit": 1,
                "lightLaserLimit": 1
            }
        };*/

        botConfig.equipment["followeruntar"] = untarBotInfo.equipmentSettings;
        botConfig.itemSpawnLimits["followeruntar"] = {};
        botConfig.walletLoot["followeruntar"] = botConfig.walletLoot["followerSanitar"];
        presetBatch.followeruntar = 15;
        const loadout: any = untarLoadout;
        dbTables.bots.types["followeruntar"] = loadout;//this.jsonUtil.deserialize(this.jsonUtil.serialize(untarBotType));

        botConfig.equipment["followeruntarmarksman"] = untarBotInfo.equipmentSettings;
        botConfig.itemSpawnLimits["followeruntarmarksman"] = {};
        botConfig.walletLoot["followeruntarmarksman"] = botConfig.walletLoot["followerSanitar"];
        presetBatch.followeruntarmarksman = 15;
        const loadoutMarksman: any = untarMarksmanLoadout;
        dbTables.bots.types["followeruntarmarksman"] = loadoutMarksman;

        botConfig.equipment["bossuntarlead"] = untarBotInfo.equipmentSettings;
        botConfig.itemSpawnLimits["bossuntarlead"] = {};
        botConfig.walletLoot["bossuntarlead"] = botConfig.walletLoot["followerSanitar"];
        presetBatch.bossuntarlead = 15;
        const loadoutBossLead: any = untarLeadLoadout;
        dbTables.bots.types["bossuntarlead"] = loadoutBossLead;

        botConfig.equipment["bossuntarofficer"] = untarBotInfo.equipmentSettings;
        botConfig.itemSpawnLimits["bossuntarofficer"] = {};
        botConfig.walletLoot["bossuntarofficer"] = botConfig.walletLoot["followerSanitar"];
        presetBatch.bossuntarofficer = 15;
        const loadoutBossOfficer: any = untarOfficerLoadout;
        dbTables.bots.types["bossuntarofficer"] = loadoutBossOfficer;

        const locales = Object.values(this.databaseService.getLocales().global) as Record<string, string>[];

        for (const locale of locales)
        {
            locale["ScavRole/UNTAR"] = "UNTAR"
        }

        this.logger.info("UNTAR should be in database.");
    }

    public async getBotDifficulties(url: string, info: IEmptyRequestData, sessionID: string, output: string): Promise<string> {
        const botTypesDb = this.databaseService.getBots().types;
        // Convert output string back to object
        let resultTable = {};
        this.logger.info("Getting bot difficulties...");
        //this.logger.info("Output string: " + output);
        if (output) {
            try {
                resultTable = JSON.parse(output);
            } catch (e) {
                this.logger.warning("Failed to parse output string to object.");
            }
        }

        //const botTypesEnum = Object.keys(WildSpawnTypeNumber).filter((v) => Number.isNaN(Number(v)));
        const result = resultTable as Record<string, any>;

        const botTypes: string[] = [];

        this.logger.info(`Does untar exist in results? ${result["bossuntarofficer"] !== undefined}`);

        // Add missing types from botTypesDb
        for (const dbType of Object.keys(botTypesDb)) {
            if (!result[dbType]) {
                this.logger.info(`Missing bot type in result: ${dbType}`);
                botTypes.push(dbType);
            }
        }

        for (let botType of botTypes) {
            const enumType = botType.toLowerCase();
            this.logger.info(`Processing bot type: ${botType} as enum type: ${enumType}`);
            // pmcBEAR/pmcUSEC need to be converted into `usec`/`bear` so we can read difficulty settings from bots/types
            botType = this.botHelper.isBotPmc(botType)
                ? this.botHelper.getPmcSideByRole(botType).toLowerCase()
                : botType.toLowerCase();

            const botDetails = botTypesDb[botType];
            if (!botDetails?.difficulty) {
                this.logger.warning(`Unable to find bot: ${botType} difficulty values`);

                continue;
            }

            const botDifficulties = Object.keys(botDetails.difficulty);
            result[enumType] = {};
            for (const difficulty of botDifficulties) {
                result[enumType][difficulty] = this.botController.getBotDifficulty(enumType, difficulty, null, true);
            }
        }


        return this.httpResponse.noBody(result);
    }
}
