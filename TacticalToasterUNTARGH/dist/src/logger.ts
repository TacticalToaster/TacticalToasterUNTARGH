import { LogTextColor } from "@spt/models/spt/logging/LogTextColor";
import { ILogger } from "@spt/models/spt/utils/ILogger";
import { inject, injectable } from "tsyringe";

import mainConfig = require("../config/main.json");

@injectable()
export class UNTARLogger {
    prefix: string;
    constructor(
        @inject("WinstonLogger") protected logger: ILogger
    ) {
        this.prefix = "[UNTARGH]";
    }

    public info(message: string): void {
        if (mainConfig.debug.logs) {
            this.logger.info(`${this.prefix}: ${message}`);
        }
    }

    public error(message: string): void {
        this.logger.error(`${this.prefix}[ERROR]: ${message}`);
    }

    public warn(message: string): void {
        this.logger.warning(`${this.prefix}[WARN]: ${message}`);
    }

    public logWithColor(message: string, color: LogTextColor): void {
        this.logger.logWithColor(`${this.prefix}: ${message}`, color);
    }
}