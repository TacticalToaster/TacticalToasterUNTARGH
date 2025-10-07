import { DependencyContainer, Lifecycle } from "tsyringe";
import { UntarDBController } from "../controllers/UntarDBController";
import { UntarSpawnController } from "../controllers/UntarSpawnController";
import { UNTARLogger } from "../logger";


export class DiContainer {
    public static register(container: DependencyContainer): void {
        container.register<UNTARLogger>("UNTARLogger", UNTARLogger, {
            lifecycle: Lifecycle.Singleton
        });

        container.register<UntarDBController>("UntarDBController", UntarDBController, {
            lifecycle: Lifecycle.Singleton
        });

        container.register<UntarSpawnController>("UntarSpawnController", UntarSpawnController, {
            lifecycle: Lifecycle.Singleton
        });
    }
}