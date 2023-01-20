/**
 * Computes quotient and remainder for two given numbers based on integer division
 * @param x
 * @param y
 * @returns quotient and remainder as array
 */
export function integerDivision(x: number, y: number) {
  if (y === 0) {
    throw new Error("Cannot divide by 0");
  }
  return [Math.floor(x / y), x % y];
}
