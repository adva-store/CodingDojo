import {
  moveAnt,
  rotateAnt,
  switchState,
  removeAnt,
  addAnt,
  stringify,
} from './game-helper';
describe('utils/game-helper', () => {
  describe('switchState', () => {
    it('switches white to black', () => {
      expect(switchState('nw')).toEqual('ns');
    });

    it('switches black to white', () => {
      expect(switchState(' s')).toEqual(' w');
    });
  });

  describe('removeAnt', () => {
    it('removes ant', () => {
      expect(removeAnt('nw')).toEqual(' w');
    });
  });

  describe('addAnt', () => {
    it('adds ant', () => {
      expect(addAnt(' w', 'n')).toEqual('nw');
    });
  });

  describe('moveAnt', () => {
    it('goes over boundaries (north)', () => {
      expect(moveAnt(2, 'n', 3)).toEqual(8);
    });

    it('goes over boundaries (east)', () => {
      expect(moveAnt(5, 'o', 3)).toEqual(3);
    });

    it('goes over boundaries (south)', () => {
      expect(moveAnt(6, 's', 3)).toEqual(0);
    });

    it('goes over boundaries (west)', () => {
      expect(moveAnt(3, 'w', 3)).toEqual(5);
    });
  });

  describe('rotateAnt', () => {
    it('rotates north => east', () => {
      expect(rotateAnt('n', 'w')).toEqual('o');
    });

    it('rotates north => west', () => {
      expect(rotateAnt('n', 's')).toEqual('w');
    });

    it('rotates east => south', () => {
      expect(rotateAnt('o', 'w')).toEqual('s');
    });

    it('rotates east => north', () => {
      expect(rotateAnt('o', 's')).toEqual('n');
    });

    it('rotates south => west', () => {
      expect(rotateAnt('s', 'w')).toEqual('w');
    });

    it('rotates south => east', () => {
      expect(rotateAnt('s', 's')).toEqual('o');
    });

    it('rotates west => north', () => {
      expect(rotateAnt('w', 'w')).toEqual('n');
    });

    it('rotates west => south', () => {
      expect(rotateAnt('w', 's')).toEqual('s');
    });
  });

  describe('stringify', () => {
    it('stringifies game ', () => {
      expect(
        stringify([
          [' w', ' w', ' w', ' w', ' w', 'nw', ' w', ' w', ' w'],
          [' w', ' w', ' w', 'ow', ' w', ' s', ' w', ' w', ' w'],
        ])
      ).toEqual(`w,w,w,w,w,nw,w,w,w;\nw,w,w,ow,w,s,w,w,w`);
    });
  });
});
