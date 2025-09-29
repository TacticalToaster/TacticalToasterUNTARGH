import { DependencyContainer, Lifecycle } from "tsyringe";
import { UntarDBController } from "../UntarDBController";


export class DiContainer {
    public static register(container: DependencyContainer): void {
        container.register<UntarDBController>("UntarDBController", UntarDBController, {
            lifecycle: Lifecycle.Singleton
        });
    }
}