import { Game } from '@langton-ant/shared-model';

export const parse = (game: string): Game => {
  return (
    game
      // Remove whitespace
      .replace(/ /g, '')
      // Remove line break
      .replace(/\n/g, '')
      // Parse turns
      .split(';')
      // Parse fields
      .map((turn: string) => turn.split(',')) as Game
  );
};
