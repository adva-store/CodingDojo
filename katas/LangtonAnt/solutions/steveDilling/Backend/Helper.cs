namespace Backend;

public static class Helper
{
    public static string GetAntDirection(AntDirection direction)
    {
        switch(direction)
        {
            case AntDirection.North:
                return "n";
            case AntDirection.East:
                return "o";
            case AntDirection.South:
                return "s";
            case AntDirection.West:
                return "w";
            default: return "n";
        }
    }
}