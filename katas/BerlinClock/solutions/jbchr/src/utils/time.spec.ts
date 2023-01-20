import { getBerlineClockTime } from "./time";

const testCases: [string, number[]][] = [
  ["2023-01-19T00:00:00Z", [0, 0, 0, 0, 0]],
  ["2023-01-19T00:00:01Z", [0, 0, 0, 0, 1]],
  ["2023-01-19T23:59:59Z", [4, 11, 3, 4, 1]],
  ["2023-01-19T18:50:14.162Z", [0, 10, 3, 3, 0]],
];

describe("time", () => {
  describe("getBerlineClockTime", () => {
    test.each(testCases)(
      "Given %p as argument, returns %p",
      (date, expectedResult) => {
        expect(getBerlineClockTime(new Date(date))).toEqual(expectedResult);
      }
    );
  });
});
