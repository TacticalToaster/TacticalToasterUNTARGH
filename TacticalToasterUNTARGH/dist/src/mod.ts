import { DependencyContainer } from "tsyringe";

import { ILogger } from "@spt/models/spt/utils/ILogger";
import { LogTextColor } from "@spt/models/spt/logging/LogTextColor";
import { LogBackgroundColor } from "@spt/models/spt/logging/LogBackgroundColor";
import { UntarDBController } from "./UntarDBController";
import { IPostDBLoadMod } from "@spt/models/external/IPostDBLoadMod";
import { IPreSptLoadMod } from "@spt/models/external/IPreSptLoadMod";
import { DiContainer } from "./di/Container";
import { WildSpawnTypeNumber } from "@spt/models/enums/WildSpawnTypeNumber";
import { DynamicRouterModService } from "@spt/services/mod/dynamicRouter/DynamicRouterModService";
import { IEmptyRequestData } from "@spt/models/eft/common/IEmptyRequestData";

class UNTARGHMod implements IPreSptLoadMod, IPostDBLoadMod
{
    public preSptLoad(container: DependencyContainer): void {
        const dynamicRouterModService = container.resolve<DynamicRouterModService>("DynamicRouterModService");

        DiContainer.register(container);

        const untarDBController = container.resolve<UntarDBController>("UntarDBController");

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

        //const untarDBController = container.resolve<UntarDBController>("UntarDBController");
        //untarDBController.addUntarToFactory();
    }

    // Code added here will load BEFORE the server has started loading
    public postDBLoad(container: DependencyContainer)
    {
        // get the logger from the server container
        const logger = container.resolve<ILogger>("WinstonLogger");

        logger.info("I am logging info!");
        logger.warning("I am logging a warning!");
        logger.error("I am logging an error!");
        logger.logWithColor("I am logging with color!", LogTextColor.YELLOW, LogBackgroundColor.RED);

        this.pushBotData(container);
    }

    private pushBotData(container: DependencyContainer): void
    {
        const untarDBController = container.resolve<UntarDBController>("UntarDBController");

        untarDBController.addUntarToDB();
        untarDBController.addUntarToFactory();

    }
}

export const mod = new UNTARGHMod();
