import {DependencyContainer, inject, injectable} from "tsyringe"
import * as fs from "fs";
import * as path from "path";
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
import type { ILoadoutInfo, ILoadoutCategory, ILoadoutItem } from "./models/ILoadoutCategory";
import untarBotType = require("../db/bots/types/test.json")
import untarBotInfo = require("../db/bots/info/untarInfo.json")
import untarLoadout from "../db/bots/types/test.json"
import untarMarksmanLoadout from "../db/bots/types/test.json"
import untarLeadLoadout from "../db/bots/types/untarlead.json"
import untarOfficerLoadout from "../db/bots/types/untarofficer.json"
import loadouts from "../db/bots/loadouts/untar.json";
import { BotHelper } from "@spt/helpers/BotHelper";
import { BotController } from "@spt/controllers/BotController";
import { ItemHelper } from "@spt/helpers/ItemHelper";
import { IBotType } from "@spt/models/eft/common/tables/IBotType";

@injectable()
export class UntarDBController {
    constructor(
        @inject("StaticRouterModService") protected staticRouter: StaticRouterModService,
        @inject("ItemHelper") protected itemHelper: ItemHelper,
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
            BossZone: "BotZone",//"ZoneRoad",
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
                    "BossEscortAmount": "8",
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

        dbTables.locations.woods.base.BossLocationSpawn = [];
        dbTables.locations.factory4_day.base.BossLocationSpawn.push(bossInfo);

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
        presetBatch.followeruntar = 55;
        const loadout: any = untarLoadout;
        dbTables.bots.types["followeruntar"] = loadout;//this.jsonUtil.deserialize(this.jsonUtil.serialize(untarBotType));

        botConfig.equipment["followeruntarmarksman"] = untarBotInfo.equipmentSettings;
        botConfig.itemSpawnLimits["followeruntarmarksman"] = {};
        botConfig.walletLoot["followeruntarmarksman"] = botConfig.walletLoot["followerSanitar"];
        presetBatch.followeruntarmarksman = 55;
        const loadoutMarksman: any = untarMarksmanLoadout;
        dbTables.bots.types["followeruntarmarksman"] = loadout;

        botConfig.equipment["bossuntarlead"] = untarBotInfo.equipmentSettings;
        botConfig.itemSpawnLimits["bossuntarlead"] = {};
        botConfig.walletLoot["bossuntarlead"] = botConfig.walletLoot["followerSanitar"];
        presetBatch.bossuntarlead = 55;
        const loadoutBossLead: any = untarLeadLoadout;
        dbTables.bots.types["bossuntarlead"] = loadout;

        botConfig.equipment["bossuntarofficer"] = untarBotInfo.equipmentSettings;
        botConfig.itemSpawnLimits["bossuntarofficer"] = {};
        botConfig.walletLoot["bossuntarofficer"] = botConfig.walletLoot["followerSanitar"];
        presetBatch.bossuntarofficer = 55;
        const loadoutBossOfficer: any = untarOfficerLoadout;
        dbTables.bots.types["bossuntarofficer"] = loadout;

        this.processLoadouts("followeruntar", dbTables.bots.types["followeruntar"] as IBotType);
        this.processLoadouts("followeruntarmarksman", dbTables.bots.types["followeruntarmarksman"] as IBotType);
        this.processLoadouts("bossuntarlead", dbTables.bots.types["bossuntarlead"] as IBotType);
        this.processLoadouts("bossuntarofficer", dbTables.bots.types["bossuntarofficer"] as IBotType);

        const locales = Object.values(this.databaseService.getLocales().global) as Record<string, string>[];

        for (const locale of locales)
        {
            locale["ScavRole/UNTAR"] = "UNTAR"
        }

        this.logger.info("UNTAR should be in database.");
    }

    /**
     * Deep merge utility: recursively merges all properties from source into target
     */
    private deepMerge<T>(target: T, source: T): T {
        const result = { ...target };
        
        for (const key in source) {
            if (Object.prototype.hasOwnProperty.call(source, key)) {
                const sourceValue = source[key];
                const targetValue = result[key];
                
                // If both are objects (and not arrays), merge recursively
                if (
                    sourceValue && 
                    typeof sourceValue === "object" && 
                    !Array.isArray(sourceValue) &&
                    targetValue && 
                    typeof targetValue === "object" && 
                    !Array.isArray(targetValue)
                ) {
                    result[key] = this.deepMerge(targetValue, sourceValue);
                } else {
                    result[key] = sourceValue;
                }
            }
        }
        
        return result;
    }

    /**
     * Reads all JSON files from loadouts/common folder and combines them into a single ILoadoutInfo
     * @returns Combined ILoadoutInfo from all common loadout files
     */
    private loadCommonLoadouts(): ILoadoutInfo {
        const commonDir = path.resolve(__dirname, "..", "db", "bots", "loadouts", "common");
        let combinedLoadout: ILoadoutInfo = { equipment: {}, weapons: {}, categories: {} };

        // Check if common directory exists
        if (!fs.existsSync(commonDir)) {
            this.logger.warning(`Common loadouts directory not found: ${commonDir}`);
            return combinedLoadout;
        }

        // Read all JSON files in the common directory
        const files = fs.readdirSync(commonDir).filter(f => f.endsWith(".json"));

        this.logger.info(`Found ${files.length} common loadout file(s) to merge`);

        for (const file of files) {
            try {
                const filePath = path.join(commonDir, file);
                const fileContent = fs.readFileSync(filePath, "utf-8");
                const loadoutData = JSON.parse(fileContent) as ILoadoutInfo;

                // Deep merge each file into the combined loadout
                combinedLoadout = this.deepMerge(combinedLoadout, loadoutData);

                this.logger.info(`Merged common loadout file: ${file}`);
            } catch (error) {
                this.logger.error(`Failed to load common loadout file ${file}: ${error}`);
            }
        }

        return combinedLoadout;
    }

    public processLoadouts(type: string, botData: IBotType): void {
        const commonLoadout: ILoadoutInfo = this.loadCommonLoadouts();
        const loadoutInfo: ILoadoutInfo = loadouts as ILoadoutInfo;

        const combinedInfo: ILoadoutInfo = this.deepMerge(commonLoadout, loadoutInfo);

        botData.inventory.mods = {};

        for (const slot of Object.keys(loadoutInfo.equipment)) {
            const slotData = loadoutInfo.equipment[slot];
            for (const item of Object.keys(slotData)) {
                const itemData = slotData[item];

                if (!botData.inventory.equipment[slot]) {
                    botData.inventory.equipment[slot] = {};
                }

                botData.inventory.equipment[slot][itemData.id] = itemData.chance ? itemData.chance : 100;

                this.processItem(itemData.id, itemData.slots, type, combinedInfo, {});
            }
        }
    }

    public addModsToSlot(item: string, slot: string, mods: string[], type: string): void {
        const dbTables = this.databaseService.getTables();
        if (dbTables.bots.types[type].inventory.mods[item] === undefined) {
            dbTables.bots.types[type].inventory.mods[item] = {};
        }

        if (dbTables.bots.types[type].inventory.mods[item][slot] === undefined) {
            dbTables.bots.types[type].inventory.mods[item][slot] = [];
        }


        for (const mod of mods) {
            if (!dbTables.bots.types[type].inventory.mods[item][slot].includes(mod)) {
                dbTables.bots.types[type].inventory.mods[item][slot].push(mod);
            }
        }
    }

    public processItem(item: string, slots: Record<string, string[]>, type: string, loadoutInfo: ILoadoutInfo, children?: Record<string, ILoadoutItem>): void {
        if (!children) {
            children = {};
        }
        
        for (const slot of Object.keys(slots)) {
            const categories = slots[slot];
            for (const category of categories) {
                //const categoryData = categories[category];
                const itemsInSlot = this.processCategoryOrItem(item, category, type, loadoutInfo, children);
                
                this.addModsToSlot(item, slot, itemsInSlot, type);
            }
        }
    }

    public processCategoryOrItem(item: string, categoryOrItem: string, type: string, loadoutInfo: ILoadoutInfo, children?: Record<string, ILoadoutItem>): string[] {
        if (children && children[categoryOrItem]) {
            return this.processCategoryOrItem(item, children[categoryOrItem].id, type, loadoutInfo, children);
        }
        else if (loadoutInfo.categories[categoryOrItem]) {
            return this.processCategory(item, categoryOrItem, loadoutInfo, type, children);
        }
        else if (this.itemHelper.isItemInDb(categoryOrItem)) {
            return [categoryOrItem];
        }

        return [];
    }

    public processCategory(item: string, category: string, loadoutInfo: ILoadoutInfo, type: string, children?: Record<string, any>): string[] {
        if (!children) {
            children = {};
        }
        //this.processChildren(children, loadoutInfo.categories[item][category]);

        const allItems: string[] = [];
        const categoryData = loadoutInfo.categories[category];

        for (const categoryItem of Object.keys(categoryData)) {
            const itemData = categoryData[categoryItem];

            this.processChildren(children, itemData, loadoutInfo, type);

            allItems.push(itemData.id);

            if (itemData.slots) {
                this.processItem(itemData.id, itemData.slots ? itemData.slots : {}, type, loadoutInfo, itemData.children);
            }
        }


        return allItems;
    }

    public processChildren(children: Record<string, ILoadoutItem>, parent: ILoadoutItem, loadoutInfo: ILoadoutInfo, type: string): void {
        if (parent.children != undefined) {
            for (const child of Object.keys(parent.children)) {
                const childData = parent.children[child];
                children[child] = childData;
                this.processChildren(children, childData, loadoutInfo, type);

                this.logger.info(`Processed child: ${childData.id} ${child} of parent: ${parent.id} ${childData.slots ? childData.slots : "no slots"} ${children["compm4"]}`);

                if (childData.slots) {
                    this.processItem(childData.id, childData.slots ? childData.slots : {}, type, loadoutInfo, children);
                }
            }
        }
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
