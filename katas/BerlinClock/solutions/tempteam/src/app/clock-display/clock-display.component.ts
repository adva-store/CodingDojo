import { Component } from '@angular/core';
import { BerlinClockService } from '../berlin-clock.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-clock-display',
  templateUrl: './clock-display.component.html',
  styleUrls: ['./clock-display.component.scss']
})
export class ClockDisplayComponent {
  berlinTime$: Observable<string[]>;

  constructor(private berlinClockService: BerlinClockService) {
    this.berlinTime$ = this.berlinClockService.getBerlinTime();
  }

}