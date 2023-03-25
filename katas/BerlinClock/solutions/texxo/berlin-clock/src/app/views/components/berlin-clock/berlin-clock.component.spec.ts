import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BerlinClockComponent } from './berlin-clock.component';

describe('BerlinClockComponent', () => {
  let component: BerlinClockComponent;
  let fixture: ComponentFixture<BerlinClockComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BerlinClockComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BerlinClockComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
