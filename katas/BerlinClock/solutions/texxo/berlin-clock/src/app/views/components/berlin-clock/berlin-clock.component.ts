import { Component, OnInit } from '@angular/core';
import { TimeProviderService } from "../../../core/services/time-provider.service";

@Component({
  selector: 'app-berlin-clock',
  templateUrl: './berlin-clock.component.html',
  styleUrls: ['./berlin-clock.component.scss']
})
export class BerlinClockComponent implements OnInit{

  currentHours = 0;
  currentMinutes = 0;
  statusLightColor = 'white';

  public constructor(
    private readonly timeProviderService: TimeProviderService
  ) {}

  ngOnInit(): void {
    setInterval(() => {
      this.statusLightColor = this.statusLightColor === 'white' ? 'yellow' : 'white';
      this.currentHours = this.timeProviderService.getCurrentHours();
      this.currentMinutes = this.timeProviderService.getCurrentMinutes();
    }, 1000)
  }
}
