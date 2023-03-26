import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TimeIntervalBlockComponent } from './time-interval-block.component';

describe('TimeIntervalBlockComponent', () => {
  let component: TimeIntervalBlockComponent;
  let fixture: ComponentFixture<TimeIntervalBlockComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TimeIntervalBlockComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TimeIntervalBlockComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
