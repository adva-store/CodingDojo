namespace LangtonAnt;

public class Cell
{
    public Point Point { get; set; }
    public CellColor Color { get; set; } = CellColor.White;
    public bool IsTagged { get; set; }

    internal Point Right()
    {
      return  new Point(Point.X + 1, Point.Y);
    }

    internal Point Left()
    {
        return new Point(Point.X - 1, Point.Y);
    }

    internal Point Top()
    {
        return new Point(Point.X, Point.Y - 1);
    }

    internal Point Bottom()
    {
        return new Point(Point.X, Point.Y + 1);
    }

    internal void ChangeColor()
    {
        switch (Color)
        {
            case CellColor.White:
                Color = CellColor.Black;
                break;

            case CellColor.Black:
                Color = CellColor.White;
                break;
            default:
                break;
        }
    }

    public override string ToString()
    {
        string color = Color == CellColor.White ? "w" : "s";
        return  color;
    }
}
