import { coordsToPos, getY } from './grid-helper';
describe('utils/grid-helper', () => {
  describe('getY', () => {
    it('returns y', () => {
      expect(getY(4, 3)).toEqual(1);
    });
  });

  describe('getx', () => {
    it('returns y', () => {
      expect(getY(4, 3)).toEqual(1);
    });
  });

  describe('coordsToPos', () => {
    it('correctly calculates the position', () => {
      expect(coordsToPos(2, 1, 3)).toEqual(5);
    });
  });
});
