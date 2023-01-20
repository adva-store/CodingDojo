import { integerDivision } from "./math";

describe("Math", () => {
  describe("integerDivision", () => {
    it("0 / 4", () => {
      expect(integerDivision(0, 4)).toEqual([0, 0]);
    });

    it("4 / 0", () => {
      expect(() => integerDivision(4, 0)).toThrow();
    });

    it("4 / 3", () => {
      expect(integerDivision(4, 3)).toEqual([1, 1]);
    });

    it("11 / 4", () => {
      expect(integerDivision(11, 4)).toEqual([2, 3]);
    });
  });
});
