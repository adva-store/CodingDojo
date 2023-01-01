import { bind, StateObservable } from '@react-rxjs/core'
import { distinctUntilChanged, map, Observable } from 'rxjs'
import Bar from './bar/bar'
import './clock.css'
import { TimeService } from './timeService'

const timeService: TimeService = new TimeService()

const hours1: Observable<number> = timeService.hours$.pipe(
  map((val: number) => Math.floor(val / 5)),
  distinctUntilChanged()
)

const hours2: Observable<number> = timeService.hours$.pipe(
  map((val: number) => val % 5),
  distinctUntilChanged()
)

const minutes1: Observable<number> = timeService.minutes$.pipe(
  map((val: number) => Math.floor(val / 5)),
  distinctUntilChanged()
)

const minutes2: Observable<number> = timeService.minutes$.pipe(
  map((val: number) => val % 5),
  distinctUntilChanged()
)

const [useSeconds]: [() => number, StateObservable<number>] = bind(timeService.seconds$)
const [useHours1]: [() => number, StateObservable<number>] = bind(hours1)
const [useHours2]: [() => number, StateObservable<number>] = bind(hours2)
const [useMinutes1]: [() => number, StateObservable<number>] = bind(minutes1)
const [useMinutes2]: [() => number, StateObservable<number>] = bind(minutes2)

function Clock (): JSX.Element {
  const circleToggled: boolean = Boolean(useSeconds() % 2)
  const hoursTop: boolean[] = new Array(4).fill(0).map(
    (_, idx) => idx < useHours1()
  )

  const hoursBottom: boolean[] = new Array(4).fill(0).map(
    (_, idx) => idx < useHours2()
  )

  const minutesTop: boolean[] = new Array(15).fill(0).map(
    (_, idx) => idx < useMinutes1()
  )

  const minutesBottom: boolean[] = new Array(4).fill(0).map(
    (_, idx) => idx < useMinutes2()
  )

  return (
     <div className="clock">
      <div className='seconds-container'>
          <div className={`circle ${circleToggled ? 'circle--toggled' : ''}`}>
          </div>
        </div>
        <div className='hours-container'>
          <div className='hours-top'>
            <Bar blocks={hoursTop}></Bar>
          </div>
          <div className='hours-bottom'>
            <Bar blocks={hoursBottom}></Bar>
          </div>
        </div>
        <div className='minutes-container'>
          <div className='minutes-top'>
            <Bar blocks={minutesTop}></Bar>
          </div>
          <div className='minutes-bottom'>
            <Bar blocks={minutesBottom}></Bar>
          </div>
        </div>
    </div>
  )
}

export default Clock
