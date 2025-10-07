export interface ILoadoutCategory {

}

export interface ILoadoutItem {
    /** Unique identifier for the category */
    id: string;
    /** Display name for the category */
    chance?: number;
    /** Parent category ID, if applicable */
    children?: Record<string, ILoadoutItem>;
    /** Array of child category IDs */
    slots?: Record<string, string[]>;
}

export interface ILoadoutInfo {
    equipment?: Record<string, Record<string, ILoadoutItem>>;
    weapons?: Record<string, Record<string, string[]>>;
    categories: Record<string, Record<string, ILoadoutItem>>;
}