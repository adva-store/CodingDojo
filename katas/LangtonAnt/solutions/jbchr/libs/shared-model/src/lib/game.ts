export type State = 'w' | 's';
export type Direction = 'n' | 'o' | 's' | 'w' | ' ';

export type Field = `${Direction}${State}`;

export type Game = Field[][];
