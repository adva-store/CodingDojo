import Clock from './clock/clock'
import './App.css'
import { Subscribe } from '@react-rxjs/core'

function App (): JSX.Element {
  return (
   <Subscribe>
      <Clock></Clock>
    </Subscribe>
  )
}

export default App
