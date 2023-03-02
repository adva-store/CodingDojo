namespace LangtonAnt;

public static class DirectionExtensions
{

    public static string AsString(this Direction self)
    {
        switch (self)
        {
            case Direction.North:
                return "n";
            case Direction.South:
                return "s";
            case Direction.West:
                return "w";
            case Direction.East:
                return "o";
            default:
                throw new NotSupportedException(self.ToString());
        }
    }

    public static Direction FromChar(this char self)
    {
        switch (self)
        {
            case 'n':
                return Direction.North;
            case 's':
                return Direction.South;
            case 'w':
                return Direction.West;
            case 'o':
                return Direction.East;
            default:
                throw new NotSupportedException(self.ToString());
        }
    }
}
