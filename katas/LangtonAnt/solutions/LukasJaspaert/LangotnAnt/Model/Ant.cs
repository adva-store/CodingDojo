

using CommunityToolkit.Mvvm.ComponentModel;

namespace LangotnAnt.Model;

public enum Direction
{
    n,o,s,w
}

public static class DirectionExtension
{
    public static Direction TurnLeft(this Direction direction) => direction switch
    {
        Direction.n => Direction.o,
        Direction.o => Direction.s,
        Direction.s => Direction.w,
        Direction.w => Direction.n,
    };

    public static Direction TurnRight(this Direction direction) => direction switch
    {
        Direction.n => Direction.w,
        Direction.o => Direction.n,
        Direction.s => Direction.o,
        Direction.w => Direction.s,

    };
}

public class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public override string ToString()
    {
        return $"{X}/{Y}";
    }

    public Point(int x, int y)
    {
        X = x;
        Y = y;

        
    }


    public void Move(Direction direction)
    {
        switch (direction)
        {
            case Direction.n:
                Y -= 1;
                break;
            case Direction.o:
                X += 1;
                break;
            case Direction.s:
                Y += 1;
                break;
            case Direction.w:
                X -= 1;
                break;
        }
    }

    public override bool Equals(object obj)
    {
        return obj is Point point &&
               X == point.X &&
               Y == point.Y;
    }
}

public class Rule
{
    public Color NewColor { get; set; }
    public bool TurnLeft { get; set; }
    public bool TurnRight { get; set;}

    public Rule(Color newColor, bool turnLeft=false, bool turnRight=false)
    {
        NewColor = newColor;
        TurnLeft = turnLeft;
        TurnRight = turnRight;
    }
}

public class Ant:ObservableObject
{
    public Point Position { get; set; }
    public Direction GazeDirection { get; set; }

    public Dictionary<Color,Rule> RuleSet { get; set; }

    public int ImageRotation
    {
        get => GazeDirection switch
        {
            Direction.w => 0,
            Direction.n => 90,
            Direction.o => 180,
            Direction.s => 270,
        };
    }

}
