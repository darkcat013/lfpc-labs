export interface GrammarTextFieldInfo {
    key: string;
    value: string;
}
export interface Relation {
    from?: string;
    to?: string;
    edgeLabel?: string;
}

export interface Relations {
    relations: Relation[]
}

export interface MapStringStringArr {
    map: Map<string, string[]>
}