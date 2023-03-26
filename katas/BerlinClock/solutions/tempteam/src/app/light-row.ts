export type LightColor = 'Y' | 'R' | 'O';

export interface LightRowConfig {
  value: number;
  step: number;
  color: LightColor;
  length: number;
  specialColors?: SpecialColorPositions;
}

export interface SpecialColorPositions {
  [position: number]: LightColor;
}

export const QUARTER_MINUTE_POSITIONS: SpecialColorPositions = {
  3: 'R',
  6: 'R',
  9: 'R',
};

export class LightRow {
  constructor(private config: LightRowConfig) {}

  generate(): string {
    const { value, step, color, length, specialColors } = this.config;
    let row = '';

    for (let i = 0; i < length; i++) {
      if (i < Math.floor(value / step)) {
        row += specialColors && specialColors[i + 1] ? specialColors[i + 1] : color;
      } else {
        row += 'O';
      }
    }

    return row;
  }
}
