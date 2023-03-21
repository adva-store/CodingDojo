public class JensBergmaLangton {

    private static readonly string _filePathAndName = "result.txt";
    public static void Main()
    {
        JensBergmaLangton jbl = new JensBergmaLangton();

        Console.WriteLine("Langton Ant");
        int size = jbl.IntConsoleReadLine("Bitte Spielfeldgroesse angeben: ");

        //create all Panels for the requested size
        List<PanelDto> playBoard = new List<PanelDto>();
        for(int i = 0; i < size; i++)
        {
            for(int y = 0; y < size; y++)
            {
                playBoard.Add(new PanelDto(){
                    Column = y,
                    Row = i
                });
            }
        }

        //get needed Parameter
        int antRow = jbl.IntConsoleReadLine("Reihe der Ameise angeben: ");
        int antColumn = jbl.IntConsoleReadLine("Spalte der Ameise angeben: ");
        var antDirection = jbl.CompassConsoleReadLine("Richtung der Ameise angeben: ");
        int numberOfMoves = jbl.IntConsoleReadLine("Bitte Anzahl Zuege angeben: ");

        //set the position of the first Ant/Move
        var currentAntPos = playBoard.FirstOrDefault(pb => pb.Column == antColumn && pb.Row == antRow);
        if(currentAntPos == null || antDirection == Compass.None)
        {
            Console.WriteLine("Ungueltige Eingabe -- Abbruch");
            Environment.Exit(0);
        }

        currentAntPos.Direction = antDirection;
        currentAntPos.IsCurrentPos = true;

        //start the Game
        WriteToFile("Neues Spiel gestartet");

        var moveCount = 0;
        jbl.PrintBoard(playBoard);

        //loop to play until the Limit is reached
        do
        {
            jbl.MakeMove(playBoard);
            jbl.PrintBoard(playBoard);
            moveCount++;
        } while(moveCount <= numberOfMoves);

        WriteToFile("Spielend");
        Console.WriteLine("Spielend");
    }

    private static void WriteToFile(string line)
    {
        using StreamWriter file = new(_filePathAndName, append: true);
        file.WriteLine(line);
    }

    private void PrintBoard(List<PanelDto> panelDtos)
    {
        int countPanels = panelDtos.Count;
        string output = string.Empty;

        //build string for output
        panelDtos.OrderBy(pd => pd.Row).ThenBy(pd => pd.Column).ToList().ForEach(p => {
            if(p.IsCurrentPos == true)
                output += $",{p.Direction.ToString().ToLower()}{p.Color}";
            else
                output += $",{p.Color}";
        });

        Console.WriteLine(output[1..]);
        WriteToFile(output[1..]);
    }

    private void MakeMove(List<PanelDto> panelDtos)
    {
        //get current position of the Ant
        var currentAntPos = panelDtos.First(pb => pb.IsCurrentPos);

        PanelDto newPos = new PanelDto();
            try
            {
                //checks every possibility for direction of the Ant and the underground color
                switch((currentAntPos.Direction, currentAntPos.Color))
                {
                    //South - White
                    //Nort - Black
                    case (Compass.S, 'w'):
                    case (Compass.N, 'b'):
                        newPos = panelDtos.First(pb => pb.Column == currentAntPos.Column - 1 && pb.Row == currentAntPos.Row);
                        newPos.Direction = Compass.W;
                        break;
                    //West - White
                    //East - Black
                    case (Compass.W, 'w'):
                    case (Compass.O, 'b'):
                        newPos = panelDtos.First(pb => pb.Column == currentAntPos.Column && pb.Row == currentAntPos.Row - 1);
                        newPos.Direction = Compass.N;
                        break;
                    //North - White
                    //South - Black
                    case (Compass.N, 'w'):
                    case (Compass.S, 'b'):
                        newPos = panelDtos.First(pb => pb.Column == currentAntPos.Column + 1 && pb.Row == currentAntPos.Row);
                        newPos.Direction = Compass.O;
                        break;
                    //East - White
                    //West - Black
                    case (Compass.O, 'w'):
                    case (Compass.W, 'b'):
                        newPos = panelDtos.First(pb => pb.Column == currentAntPos.Column && pb.Row == currentAntPos.Row + 1);
                        newPos.Direction = Compass.S;
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Grenze Erreicht -- Abbruch");
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }

            //set vales from the old field
            newPos.IsCurrentPos = true;
            currentAntPos.IsCurrentPos = false;
            currentAntPos.Color = currentAntPos.Color == 'w' ? 'b' : 'w';
    }

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

    public int IntConsoleReadLine(string message)
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

    enum Compass { N, O, S, W, None }

    private class PanelDto
    {
        public char Color {get; set;} = 'w';
        public bool IsCurrentPos {get; set;} = false;
        public Compass Direction {get; set;} = Compass.None;
        public int Row {get; set;}
        public int Column {get; set;}
    }
}