export interface IMainConfig {
    debug: {
        logs: boolean
    },
    patrols: {
        minOfficerSize: number;
        minSecondLeaderSize: number;
        officerChance: number;
        secondLeaderChance: number;
    },
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