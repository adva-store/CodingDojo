/**
 * Utilities to operate on a 1d array in 2d
 */

/**
 * Converts x y positions to a position on a 1d array
 */
export const coordsToPos = (x: number, y: number, boardLength: number) => {
  return y * boardLength + x;
};

/**
 * Converts position on 1d array to y value in 2d array
 */
export const getY = (x: number, boardLength: number): number =>
  Math.floor(x / boardLength);

/**
 * Converts position on 1d array to x value in 2d array
 */
export const getX = (x: number, boardLength: number): number =>
  Math.abs(x % boardLength);
