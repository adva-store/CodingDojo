import {Component, Input} from '@angular/core';
import {TimeIntervalBlockType} from "../../../core/TimeIntervalBlockType";

@Component({
  selector: 'app-time-interval-block',
  templateUrl: './time-interval-block.component.html',
  styleUrls: ['./time-interval-block.component.scss']
})
export class TimeIntervalBlockComponent {

  @Input() type: TimeIntervalBlockType = 'single-hour';
  @Input() color = 'white';
}
