import { BerlinClockTime } from "../models/berlin-clock";
import { integerDivision } from "./math";

/**
 * Computes berlin clock time
 * @param date Date
 * @returns an array, starting at the bottom of the berlin clock (minutes) and ending at the (seconds)
 */
export function getBerlineClockTime(date: Date): BerlinClockTime {
  const [hours5, hours1] = integerDivision(date.getHours(), 5);
  const [minutes15, minutes1] = integerDivision(date.getMinutes(), 5);

  return [minutes1, minutes15, hours1, hours5, date.getSeconds() % 2];
}
