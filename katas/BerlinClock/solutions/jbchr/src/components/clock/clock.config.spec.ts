import {
  computeElementWidth,
  computeBerlinClockRows,
  computeElementPosition,
} from "./clock.config";

import * as THREE from "three";

describe("clock.config", () => {
  describe("computeElementWidth", () => {
    it("computes element width", () => {
      expect(
        computeElementWidth({
          rowWidth: 9,
          elementCount: 5,
          elementMargin: 1,
        })
      ).toEqual(1);
    });
  });

  describe("computeElementPosition", () => {
    it("computes rows (first in row, first in col, no margins)", () => {
      expect(
        computeElementPosition({
          rowWidth: 10,
          elementWidth: 1,
          row: 0,
          col: 0,
          boxMarginX: 0,
          boxMarginY: 0,
        })
      ).toEqual(new THREE.Vector3(-4.5, 0, 0));
    });

    it("computes rows (second in row, second in col, no margins)", () => {
      expect(
        computeElementPosition({
          rowWidth: 10,
          elementWidth: 1,
          row: 1,
          col: 1,
          boxMarginX: 0,
          boxMarginY: 0,
        })
      ).toEqual(new THREE.Vector3(-3.5, 1, 0));
    });

    it("computes rows (first in row, first in col, margins)", () => {
      expect(
        computeElementPosition({
          rowWidth: 10,
          elementWidth: 1,
          row: 0,
          col: 0,
          boxMarginX: 0.1,
          boxMarginY: 0.1,
        })
      ).toEqual(new THREE.Vector3(-4.5, 0, 0));
    });

    it("computes rows (second in row, second in col, margins)", () => {
      expect(
        computeElementPosition({
          rowWidth: 10,
          elementWidth: 1,
          row: 1,
          col: 1,
          boxMarginX: 0.1,
          boxMarginY: 0.1,
        })
      ).toEqual(new THREE.Vector3(-3.4, 1.1, 0));
    });
  });
});
