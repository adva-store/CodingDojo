import { Observable, interval, map, startWith, distinctUntilChanged } from 'rxjs'
export class TimeService {
  public readonly hours$: Observable<number>
  public readonly minutes$: Observable<number>
  public readonly seconds$: Observable<number>

  private readonly timer$: Observable<Date> = interval(1000).pipe(
    map(() => new Date()),
    startWith(new Date())
  )

  public constructor () {
    this.hours$ = this.timer$.pipe(
      map((val: Date) => val.getHours()),
      distinctUntilChanged()
    )

    this.minutes$ = this.timer$.pipe(
      map((val: Date) => val.getMinutes()),
      distinctUntilChanged()
    )

    this.seconds$ = this.timer$.pipe(
      map((val: Date) => val.getSeconds()),
      distinctUntilChanged()
    )
  }
}
