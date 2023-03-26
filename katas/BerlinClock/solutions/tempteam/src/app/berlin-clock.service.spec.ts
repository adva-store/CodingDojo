import { TestBed } from '@angular/core/testing';

import { BerlinClockService } from './berlin-clock.service';

describe('BerlinClockService', () => {
  let service: BerlinClockService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BerlinClockService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
