import { DependencyContainer } from "tsyringe";

import { ILogger } from "@spt/models/spt/utils/ILogger";
import { LogTextColor } from "@spt/models/spt/logging/LogTextColor";
import { LogBackgroundColor } from "@spt/models/spt/logging/LogBackgroundColor";
import { UntarDBController } from "./controllers/UntarDBController";
import { IPostDBLoadMod } from "@spt/models/external/IPostDBLoadMod";
import { IPreSptLoadMod } from "@spt/models/external/IPreSptLoadMod";
import { DiContainer } from "./di/Container";
import { WildSpawnTypeNumber } from "@spt/models/enums/WildSpawnTypeNumber";
import { DynamicRouterModService } from "@spt/services/mod/dynamicRouter/DynamicRouterModService";
import { IEmptyRequestData } from "@spt/models/eft/common/IEmptyRequestData";
import { UntarSpawnController } from "./controllers/UntarSpawnController";
import { StaticRouterModService } from "@spt/services/mod/staticRouter/StaticRouterModService";

class UNTARGHMod implements IPreSptLoadMod, IPostDBLoadMod
{
    public preSptLoad(container: DependencyContainer): void {
        const staticRouterService = container.resolve<StaticRouterModService>("StaticRouterModService");

        DiContainer.register(container);

        const untarSpawn = container.resolve<UntarSpawnController>("UntarSpawnController");

        staticRouterService.registerStaticRouter(
            "UNTARRaidStart",
            [
                {
                    url: "/client/match/local/end",
                    action: async (url: string, info: any, sessionID: string, output: string) => {
                        untarSpawn.adjustAllUntarSpawns();
                        return output;
                    }
                }
            ],
            "UNTAR"
        );

        //const untarDBController = container.resolve<UntarDBController>("UntarDBController");
        //untarDBController.addUntarToFactory();
    }

    // Code added here will load BEFORE the server has started loading
    public postDBLoad(container: DependencyContainer)
    {
        // get the logger from the server container
        const logger = container.resolve<ILogger>("WinstonLogger");

        this.pushBotData(container);
    }

    private pushBotData(container: DependencyContainer): void
    {
        const untarDBController = container.resolve<UntarDBController>("UntarDBController");
        const untarSpawn = container.resolve<UntarSpawnController>("UntarSpawnController");

        untarDBController.addUntarToDB();
        untarSpawn.adjustAllUntarSpawns();
        //untarDBController.addUntarToFactory();

    }
}

export const mod = new UNTARGHMod();
