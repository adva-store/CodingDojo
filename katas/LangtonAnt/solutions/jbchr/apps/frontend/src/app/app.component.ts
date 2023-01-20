import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {
  combineLatest,
  concatMap,
  delay,
  from,
  map,
  of,
  startWith,
  Subject,
  switchMap,
} from 'rxjs';
import { parse } from './utils/game-helpers';

/**
 * Displays the langtons ant game.
 * User can uplaod a file with the config
 * and also control the speed
 */
@Component({
  selector: 'langton-ant-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AppComponent {
  // Expose math to be used in template
  Math = Math;

  // Form controls for speed and file uplaod
  formGroup = new FormGroup({
    speed: new FormControl(1000, [Validators.required]),
    file: new FormControl('', [Validators.required]),
  });

  // Observable that contains current value of speed control
  speedControlValue$ = this.formGroup.controls['speed'].valueChanges.pipe(
    startWith(this.formGroup.controls['speed'].value)
  );

  turnsRawSubject = new Subject<string>();
  // Contains parsed turns
  turns$ = this.turnsRawSubject.asObservable().pipe(map((game) => parse(game)));

  // Emit single turns with given delay when either turns or speed change
  currentTurn$ = combineLatest([this.turns$, this.speedControlValue$]).pipe(
    switchMap(([turns, speed]) =>
      from(turns).pipe(concatMap((turn) => of(turn).pipe(delay(speed))))
    )
  );

  onFileChanged(e: Event) {
    const file = (e.target as HTMLInputElement).files?.[0];

    if (!file) {
      return;
    }

    const reader = new FileReader();
    reader.onload = () => {
      this.turnsRawSubject.next(reader.result as string);
    };

    reader.readAsText(file);
  }

  /**
   * Computes the rotation based on a given direction
   */
  getRotation(dir: string): number {
    switch (dir) {
      case 'o':
        return 90;
      case 's':
        return 180;
      case 'w':
        return 270;
    }
    return 0;
  }
}
