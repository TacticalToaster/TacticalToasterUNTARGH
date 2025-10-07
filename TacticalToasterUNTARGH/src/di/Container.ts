import { DependencyContainer, Lifecycle } from "tsyringe";
import { UntarDBController } from "../UntarDBController";
import { UntarSpawnController } from "../controllers/UntarSpawnController";


export class DiContainer {
    public static register(container: DependencyContainer): void {
        container.register<UntarDBController>("UntarDBController", UntarDBController, {
            lifecycle: Lifecycle.Singleton
        });

        container.register<UntarSpawnController>("UntarSpawnController", UntarSpawnController, {
            lifecycle: Lifecycle.Singleton
        });
    }
}