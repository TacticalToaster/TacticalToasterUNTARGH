export interface IMainConfig {
    locations: {
        [map: string]: {
            enablePatrols: boolean;
            patrolChance: number;
            patrolAmount: number;
            patrolMin: number;
            patrolMax: number;
            patrolZones: string[];
            patrolTimeMin: number;
            patrolTimeMax: number;
        };
    };
}