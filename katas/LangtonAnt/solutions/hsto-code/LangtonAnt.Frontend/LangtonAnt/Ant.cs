namespace LangtonAnt;

public class Ant
{
    private Direction direction;
    private readonly IList<Cell> cells;
    private Cell currentCell;
    private Cell previousCell;
    public Cell CurrentCell { get => currentCell;  }

    public Direction Direction { get => direction; }

    public Ant(IList<Cell> cells, Cell antStartCell, Direction antStartDirection)
    {
        this.cells = cells;
        this.currentCell = antStartCell;
        this.previousCell = antStartCell;
        this.direction = antStartDirection;
    }

    public void Move()
    {
        /*Auf einem weißen Feld drehe 90 Grad nach rechts; auf einem schwarzen Feld drehe 90 Grad nach links.
        Wechsle die Farbe des Feldes (weiß nach schwarz oder schwarz nach weiß).
        Schreite ein Feld in der aktuellen Blickrichtung fort.
         */
        previousCell = currentCell;
        switch (direction)
        {
            case Direction.North:
                switch (currentCell.Color)
                {
                    case CellColor.White:
                        currentCell = cells.SingleOrDefault(p => p.Point == currentCell.Right());
                        direction = Direction.East;
                        break;

                    case CellColor.Black:
                        currentCell = cells.SingleOrDefault(p => p.Point == currentCell.Left());
                        direction = Direction.West;
                        break;
                    default:
                        break;
                }

                break;

            case Direction.South:
                switch (currentCell.Color)
                {
                    case CellColor.White:
                        currentCell = cells.SingleOrDefault(p => p.Point == currentCell.Left());
                        direction = Direction.West;
                        break;

                    case CellColor.Black:
                        currentCell = cells.SingleOrDefault(p => p.Point == currentCell.Right());
                        direction = Direction.East;
                        break;
                    default:
                        break;
                }
                break;

            case Direction.West:
                switch (currentCell.Color)
                {
                    case CellColor.White:
                        currentCell = cells.SingleOrDefault(p => p.Point == currentCell.Top());
                        direction = Direction.North;
                        break;

                    case CellColor.Black:
                        currentCell = cells.SingleOrDefault(p => p.Point == currentCell.Bottom());
                        direction = Direction.South;
                        break;
                    default:
                        break;
                }
                break;

            case Direction.East:
                switch (currentCell.Color)
                {
                    case CellColor.White:
                        currentCell = cells.SingleOrDefault(p => p.Point == currentCell.Bottom());
                        direction = Direction.South;
                        break;

                    case CellColor.Black:
                        currentCell = cells.SingleOrDefault(p => p.Point == currentCell.Top());
                        direction = Direction.North;
                        break;
                    default:
                        break;
                }
                break;

            default:
                break;
        }

        if(currentCell == null)
        {
            //TODO 
        }
        else
        {
            previousCell.ChangeColor();
        }
    }
}
