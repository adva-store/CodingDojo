import { Injectable } from '@angular/core';
import { timer, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { LightRow, LightColor, QUARTER_MINUTE_POSITIONS } from './light-row';


@Injectable({
  providedIn: 'root'
})
export class BerlinClockService {
  constructor() {}

  getBerlinTime(): Observable<string[]> {
    return timer(0, 1000).pipe(
      map(() => {
        const now = new Date();
        const hours = now.getHours();
        const minutes = now.getMinutes();
        const seconds = now.getSeconds();
  
        return [
          this.getSecondIndicator(seconds),
          new LightRow({ value: hours, step: 5, color: 'R', length: 4 }).generate(),
          new LightRow({ value: hours % 5, step: 1, color: 'R', length: 4 }).generate(),
          new LightRow({ value: minutes, step: 5, color: 'Y', length: 11, specialColors: QUARTER_MINUTE_POSITIONS }).generate(),
          new LightRow({ value: minutes % 5, step: 1, color: 'Y', length: 4 }).generate(),
        ];
      })
    );
  }

  private getSecondIndicator(seconds: number): LightColor {
    return seconds % 2 === 0 ? 'Y' : 'O';
  }
}