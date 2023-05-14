namespace Backend;

public class Ant
{
    public Ant(AntDirection direction, int xPos, int yPos)
    {
        Direction = direction;
        XPos = xPos;
        YPos = yPos;
    }
    public AntDirection Direction { private set; get; }
    public int XPos { private set; get; }
    public int YPos { private set; get; }
    
    public void ChangeDirection(FieldColor color)
    {
        int newDirection = 0;
        if (color == FieldColor.White)
        {
            newDirection = (int)Direction+1;
            if (newDirection > (int)AntDirection.West)
            {
                newDirection = (int)AntDirection.North;
            }
        }
        else
        {
            newDirection = (int)Direction - 1;
            if (newDirection < (int)AntDirection.North)
            {
                newDirection = (int)AntDirection.West;
            }
        }

        Direction = (AntDirection)newDirection;
    }

    public bool CanMove(int fieldDimension)
    {
        switch (Direction)
        {
            case AntDirection.North:
                return YPos - 1 >= 0;
            case AntDirection.East:
                return XPos + 1 < fieldDimension;
            case AntDirection.South:
                return YPos + 1 < fieldDimension;
            case AntDirection.West:
                return XPos - 1 >= 0;
            default:
                return true;
        }
    }
    
    public void Move()
    {
        switch (Direction)
        {
            case AntDirection.North:
                YPos -= 1;
                break;
            case AntDirection.East:
                XPos += 1;
                break;
            case AntDirection.South:
                YPos += 1;
                break;
            case AntDirection.West:
                XPos -= 1;
                break;
            default:
                return ;
        }
    }
}