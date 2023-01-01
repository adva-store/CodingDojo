import './bar.css'

interface IBarProps {
  blocks: boolean[]
}

function Bar ({ blocks }: IBarProps): JSX.Element {
  return (
      <>
        { blocks.map((active: boolean, idx: number) =>
            <div key={idx} className={`item ${active ? 'item-active' : ''}`}></div>
        )}
      </>
  )
}

export default Bar
