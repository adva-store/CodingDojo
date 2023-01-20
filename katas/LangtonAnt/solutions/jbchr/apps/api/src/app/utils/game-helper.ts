import { Direction, Field, Game, State } from '@langton-ant/shared-model';
import { coordsToPos, getX, getY } from './grid-helper';

/**
 * Switches the state of a field
 */
export const switchState = (field: Field): Field =>
  `${field[0]}${field[1] === 'w' ? 's' : 'w'}` as Field;

/**
 * Removes ant from field
 */
export const removeAnt = (field: Field): Field => ` ${field[1]}` as Field;

/**
 * Adds ant to a field
 */
export const addAnt = (field: Field, dir: Direction): Field =>
  `${dir}${field[1]}` as Field;

/**
 * Computes the next position of the ant
 */
export const moveAnt = (pos: number, dir: Direction, length: number) => {
  const x = getX(pos, length);
  const y = getY(pos, length);

  switch (dir) {
    case 'n':
      return coordsToPos(x, (y - 1 + length) % length, length);
    case 'o':
      return coordsToPos((x + 1) % length, y, length);
    case 's':
      return coordsToPos(x, (y + 1) % length, length);
    default:
      return coordsToPos((x - 1 + length) % length, y, length);
  }
};

const dirMap: { [key: number]: Direction } = {
  0: 'n',
  1: 'o',
  2: 's',
  3: 'w',
};

const reverseDirMap = Object.entries(dirMap).reduce((acc, [key, val]) => {
  acc[val] = parseInt(key);
  return acc;
}, {});

/**
 * Computes the next direction of the ant
 */
export const rotateAnt = (dir: Direction, state: State) => {
  // Map direction to an integer so we can do math with it
  const dirNr = reverseDirMap[dir];
  const newDirNr = state === 'w' ? dirNr + 1 : dirNr - 1;

  // Remap the direction
  return dirMap[(newDirNr + 4) % 4] as Direction;
};

/**
 * Stringifies a game to a single string
 */
export const stringify = (game: Game): string =>
  game.map((turn) => turn.join(',').replace(/ /g, '')).join(';\n');
