import { Column } from "@/models/berlin-clock";
import * as THREE from "three";
import { Vector3 } from "@react-three/fiber";

/**
 * Computes the element width for equally distributes elements with a certain margin
 * in a row with a given width
 * @returns The width of each element
 */
export const computeElementWidth = ({
  rowWidth,
  elementCount,
  elementMargin,
}: {
  rowWidth: number;
  elementCount: number;
  elementMargin: number;
}) => {
  return (rowWidth - (elementCount - 1) * elementMargin) / elementCount;
};

/**
 * Computes the position of a single element in the berlin clocks 4 rows
 */
export const computeElementPosition = ({
  rowWidth,
  elementWidth,
  row,
  col,
  boxMarginX,
  boxMarginY,
}: {
  rowWidth: number;
  elementWidth: number;
  row: number;
  col: number;
  boxMarginX: number;
  boxMarginY: number;
}): Vector3 => {
  /* Center the elements horizontally */
  const xOffset = -rowWidth / 2 + elementWidth / 2;
  /* Center the elements (roughly) vertically */

  /* Compute position of the element */
  const x = xOffset + col * elementWidth + col * boxMarginX;
  const y = row + row * boxMarginY;

  return new THREE.Vector3(x, y, 0);
};

/**
 * Computes the width and position for each of the berlin clocks lower four rows
 *
 * @param rowWidth
 * @param boxMarginX
 * @param boxMarginY
 * @returns an arraw of rows that contain for each row another array containing the position and width of each element
 */
export const computeBerlinClockRows = (
  rowWidth: number,
  boxMarginX: number,
  boxMarginY: number
): Column[][] => {
  const rows: Column[][] = [];
  for (let row = 0; row < 4; row++) {
    const cols = [];

    /* For each row build columns, with corresponding lengths  */
    let elementCount = row === 1 ? 11 : 5;
    let elementWidth = computeElementWidth({
      rowWidth,
      elementCount,
      elementMargin: boxMarginX,
    });
    for (let col = 0; col < elementCount; col++) {
      cols.push({
        position: computeElementPosition({
          rowWidth,
          elementWidth,
          row,
          col,
          boxMarginX,
          boxMarginY,
        }),
        width: elementWidth,
      });
    }

    rows.push(cols);
  }
  return rows;
};
