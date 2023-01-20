import { Test } from '@nestjs/testing';

import { LangtonAntService } from './langton-ant.service';

describe('LangtonAntService', () => {
  let service: LangtonAntService;

  beforeEach(async () => {
    const moduleRef = await Test.createTestingModule({
      controllers: [],
      providers: [LangtonAntService],
    }).compile();

    service = moduleRef.get<LangtonAntService>(LangtonAntService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should compute simple starting game', () => {
    expect(service.computeGame(3, 1, 1, 'n', 1)).toEqual([
      [' w', ' w', ' w', ' w', 'nw', ' w', ' w', ' w', ' w'],
    ]);
  });

  it('should compute simple starting game with 3 iterations', () => {
    expect(service.computeGame(3, 1, 1, 'n', 3)).toEqual([
      [' w', ' w', ' w', ' w', 'nw', ' w', ' w', ' w', ' w'],
      [' w', ' w', ' w', ' w', ' s', 'ow', ' w', ' w', ' w'],
      [' w', ' w', ' w', ' w', ' s', ' s', ' w', ' w', 'sw'],
    ]);
  });

  // One test is enough as going over boundary is already covered in moveAnt tests
  it('should go over boundaries (west)', () => {
    expect(service.computeGame(3, 2, 1, 'n', 2)).toEqual([
      [' w', ' w', ' w', ' w', ' w', 'nw', ' w', ' w', ' w'],
      [' w', ' w', ' w', 'ow', ' w', ' s', ' w', ' w', ' w'],
    ]);
  });
});
