using Newtonsoft.Json;

public struct LangtonTile
{
    public bool IsBlack = false;
    public int PosX = 0;
    public int PosY = 0;

    public LangtonTile(int x, int y, bool isBlack = false)
    {
        PosX = x;
        PosY = y;
        IsBlack = isBlack;
    }
}

public enum Direction
{
    North, East, West, South
}

public struct Ant
{
    public int X;
    public int Y;
    public int Max;
    public Direction LookDirection;

    public Ant(int maximum, int x, int y, Direction dir)
    {
        X = x;
        Y = y;
        Max = maximum;
        LookDirection = dir;
    }

    public bool TurnAndCheck( bool turnLeft)
    {
        switch (LookDirection) 
        {
            case Direction.North:
                if (turnLeft)
                {
                    LookDirection = Direction.West;
                    if (X - 1 < 0)
                    {
                        return false;
                    }
                }
                else
                {
                    LookDirection = Direction.East;
                    if (X + 1 >= Max)
                    {
                        return false;
                    }
                }
                return true;
            case Direction.East:
                if (turnLeft)
                {
                    LookDirection = Direction.North;
                    if (Y + 1 < 0)
                    {
                        return false;
                    }
                }
                else
                {
                    LookDirection = Direction.South;
                    if (Y - 1 >= Max)
                    {
                        return false;
                    }
                }
                return true;
            case Direction.South:
                if (turnLeft)
                {
                    LookDirection = Direction.East;
                    if (X + 1 >= Max)
                    {
                        return false;
                    }
                }
                else
                {
                    LookDirection = Direction.West;
                    if (X - 1 < 0)
                    {
                        return false;
                    }
                }         
                return true;   
            case Direction.West:
                if (turnLeft)
                {
                    LookDirection = Direction.South;
                    if (Y + 1 >= Max)
                    {
                        return false;
                    }
                }
                else
                {
                    LookDirection = Direction.North;
                    if (Y - 1 < 0)
                    {
                        return false;
                    }
                }
                if (X - 1 < 0)
                {
                    return false;
                }
                return true;
            default:
                return true;
        }  
    }
}

public class OutputForJson
{
    public int Width;
    public List<Ant> AntPosistions = new List<Ant>();
    public List<LangtonTile> TilesToChange = new List<LangtonTile>();
}

public class Langton
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var langton = new Langton();
    }

    public LangtonTile[,] board;

    public Langton()
    {
        GetInput();
    }


    public void GetInput()
    {
        Console.WriteLine("Please enter data like this: width, startposx, startposy, direction(n,e,w,s), turns");

        string input = Console.ReadLine();

        if (string.IsNullOrEmpty(input))
        {
            GetInput();
        }
        else
        {
            var inputs = input.Split(",");
            if (inputs.Length < 5)
            {
                GetInput();
            }
            else
            {
                Calculate(input);
            }
        }
    }

    public void Calculate(string input)
    {
        var outputForJson = new OutputForJson();
        var inputs = input.Split(",");
        var max = int.Parse(inputs[0]);
        outputForJson.Width = max;
        var Map = new List<List<LangtonTile>>();

        for (int x = 0; x < max; x++)
        {
            var rowList = new List<LangtonTile>();
            for (int y = 0; y < max; y++)
            {
               
                rowList.Add(new LangtonTile(x,y));
            }
            Map.Add(rowList);
        }

        var AntDirection = ConvertToDirection(inputs[3]);
        var ActiveAnt = new Ant(max, int.Parse(inputs[1]), int.Parse(inputs[2]), AntDirection);
        var turns = int.Parse(inputs[4]);

        outputForJson.AntPosistions.Add(ActiveAnt);
        for (int i = 0; i < turns-1; i++)
        {
            bool checking = false;
            while (!checking)
            {
                checking = ActiveAnt.TurnAndCheck(Map[ActiveAnt.Y][ActiveAnt.X].IsBlack);
            }
            var tileToChange = new LangtonTile(ActiveAnt.Y, ActiveAnt.X, !Map[ActiveAnt.Y][ActiveAnt.X].IsBlack);
            Map[ActiveAnt.Y][ActiveAnt.X] = tileToChange;
            switch (ActiveAnt.LookDirection)
            {
                case Direction.North:
                    ActiveAnt.Y = ActiveAnt.Y - 1;
                    break;
                case Direction.East:
                    ActiveAnt.X = ActiveAnt.X + 1;
                    break;
                case Direction.West:
                    ActiveAnt.X = ActiveAnt.X - 1;
                    break;
                case Direction.South:
                    ActiveAnt.Y = ActiveAnt.Y + 1;
                    break;
                default:
                    break;
            }

            outputForJson.AntPosistions.Add(ActiveAnt);
            outputForJson.TilesToChange.Add(tileToChange);
        }
        string workingDirectory = Environment.CurrentDirectory;

        string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
        
        var jsonString = JsonConvert.SerializeObject(outputForJson);
        File.WriteAllText(projectDirectory + "/output.json", jsonString);

    }

    public Direction ConvertToDirection(string dirInput)
    {
        dirInput = dirInput.ToLower();
        switch (dirInput) 
        {
            case "n":
                return Direction.North;
            case "e":
                return Direction.East;
            case "w":
                return Direction.West;
            case "s":
                return Direction.South;           
            default:
                return Direction.North;
        }
    }
}