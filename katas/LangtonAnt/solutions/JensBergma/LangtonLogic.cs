public class LangtonLogic
{
    private readonly string _filePathAndName = "result.txt";
    public LangtonLogic(string filePathAndName)
    {
        _filePathAndName = filePathAndName;
    }

    public void CreateGame()
    {
        //get needed Parameter
        int gameBoardSize = IntConsoleReadLine("Spielfeldgroesse angeben: ");
        int antRow = IntConsoleReadLine("Reihe der Ameise angeben: ");
        int antColumn = IntConsoleReadLine("Spalte der Ameise angeben: ");
        var antDirection = CompassConsoleReadLine("Richtung der Ameise angeben: ");
        int numberOfMoves = IntConsoleReadLine("Anzahl Zuege angeben: ");

        CreateGame(gameBoardSize, antRow, antColumn, antDirection, numberOfMoves);
    }

    public void CreateGame(int gameBoardSize, int antRow, int antColumn, Compass antDirection, int numberOfMoves)
    {
         //start the Game
        WriteToFile("Neues Spiel gestartet", true);

        //create all Tiles for the requested size
        List<GameBoardTile> playBoard = new List<GameBoardTile>();
        for(int x = 0; x < gameBoardSize; x++)
        {
            for(int y = 0; y < gameBoardSize; y++)
            {
                playBoard.Add(new GameBoardTile(){
                    Column = y,
                    Row = x
                });
            }
        }

        //set the position of the first Ant/Move
        var currentAntPos = playBoard.FirstOrDefault(pb => pb.Column == antColumn && pb.Row == antRow);
        if(currentAntPos == null || antDirection == Compass.None)
        {
            Console.WriteLine("Ungueltige Eingabe -- Abbruch");
            Environment.Exit(0);
        }

        currentAntPos.Direction = antDirection;
        currentAntPos.IsCurrentPos = true;

        var moveCount = 0;
        PrintBoard(playBoard, gameBoardSize);

        //loop to play until the Limit is reached
        do
        {
            MakeMove(playBoard);
            PrintBoard(playBoard, gameBoardSize);
            moveCount++;
        } while(moveCount < numberOfMoves);

        WriteToFile("Spielend", true);
    }

     private void MakeMove(List<GameBoardTile> playBoard)
    {
        //get current position of the Ant
        var currentAntPos = playBoard.First(pb => pb.IsCurrentPos);

        GameBoardTile? newPos = null;
            try
            {
                //checks every possibility for direction of the Ant and the underground color
                switch((currentAntPos.Direction, currentAntPos.Color))
                {
                    //South - White
                    //Nort - Black
                    case (Compass.S, 'w'):
                    case (Compass.N, 's'):
                        newPos = playBoard.First(pb => pb.Column == currentAntPos.Column - 1 && pb.Row == currentAntPos.Row);
                        newPos.Direction = Compass.W;
                        break;
                    //West - White
                    //East - Black
                    case (Compass.W, 'w'):
                    case (Compass.O, 's'):
                        newPos = playBoard.First(pb => pb.Column == currentAntPos.Column && pb.Row == currentAntPos.Row - 1);
                        newPos.Direction = Compass.N;
                        break;
                    //North - White
                    //South - Black
                    case (Compass.N, 'w'):
                    case (Compass.S, 's'):
                        newPos = playBoard.First(pb => pb.Column == currentAntPos.Column + 1 && pb.Row == currentAntPos.Row);
                        newPos.Direction = Compass.O;
                        break;
                    //East - White
                    //West - Black
                    case (Compass.O, 'w'):
                    case (Compass.W, 's'):
                        newPos = playBoard.First(pb => pb.Column == currentAntPos.Column && pb.Row == currentAntPos.Row + 1);
                        newPos.Direction = Compass.S;
                        break;
                }
            }
            //if .First gets an error -> Out of Boarder = End Game
            catch (Exception ex)
            {
                Console.WriteLine("Grenze Erreicht -- Abbruch");
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }

            //set vales from the old field
            if(newPos != null)
                newPos.IsCurrentPos = true;
            currentAntPos.IsCurrentPos = false;
            currentAntPos.Direction = Compass.None;
            currentAntPos.Color = currentAntPos.Color == 'w' ? 's' : 'w';
    }

    #region InputFunctions
    //Custom input function to check int input
    private int IntConsoleReadLine(string message)
    {
        Console.WriteLine(message);

        int response = -1;
        bool noProb = int.TryParse(Console.ReadLine(), out response);

        if(!noProb || response < 0)
        {
            Console.WriteLine("Ungueltige Eingabe -- Abbruch");
            Environment.Exit(0);
        }

        return response;
    }

    //Custom input function to check Compass input
    private Compass CompassConsoleReadLine(string message)
    {
        Console.WriteLine(message);

        var input = Console.ReadLine();

        if(string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Ungueltige Eingabe -- Abbruch");
            Environment.Exit(0);
        }

        switch(input.ToLower())
        {
            case "w":
            case "west":
            case "westen":
                return Compass.W;
            case "s":
            case "south":
            case "sueden":
                return Compass.S;
            case "n":
            case "north":
            case "norden":
                return Compass.N;
            case "o":
            case "east":
            case "osten":
                return Compass.O;
            default:
                return Compass.None;
        }
    }

    #endregion

    #region OutputFunctions
    private void WriteToFile(string line, bool writeToConsole = false, bool paramAppend = true)
    {
        using StreamWriter file = new(_filePathAndName, append: paramAppend);
        file.WriteLine(line);

        if(writeToConsole)
            Console.WriteLine(line);
    }

    private void PrintBoard(List<GameBoardTile> playBoard, int limit, bool withNewLine = false)
    {
        int countPanels = playBoard.Count;
        string output = string.Empty;
        int count = 0;

        //build string for output
        playBoard.OrderBy(pd => pd.Row).ThenBy(pd => pd.Column).ToList().ForEach(p => {
            if(p.IsCurrentPos == true)
                output += $"{p.Direction.ToString().ToLower()}{p.Color},";
            else
                output += $"{p.Color},";

            if(withNewLine && count == limit - 1)
            {
                output += System.Environment.NewLine;
                count = 0;
            }
            else
            {
                count++;
            }
        });

        //to kill last comma -> cut of last char
        WriteToFile(output.TrimEnd(','), true);
    }
    #endregion
}