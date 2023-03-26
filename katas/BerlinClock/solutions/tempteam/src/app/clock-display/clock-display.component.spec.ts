import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClockDisplayComponent } from './clock-display.component';

describe('ClockDisplayComponent', () => {
  let component: ClockDisplayComponent;
  let fixture: ComponentFixture<ClockDisplayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClockDisplayComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClockDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
