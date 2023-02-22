namespace LangtonAnt.Frontend
{
  public class Visualisierung
  {
    private int distanceSidesEdge = 2;
    private int distanceTopBottomEdge = 1;
    private int distanceSidesValues = 3;
    private int distanceTopBottomValues = 1;


    public void ShowFields()
    {
    }

    protected static int origRow;
    protected static int origCol;

    protected static void WriteAt(string s, int x, int y)
    {
      try
      {
        Console.SetCursorPosition(origCol + x, origRow + y);
        Console.Write(s);
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
    }


    List<string> verlauf = new List<string>();
    int spielbrettGröße = 11;
    int visualStepDelayInMS = 500;

    public void Show(List<string> verlauf, int spielbrettGröße, int timerDelayInMilliseconds)
    {
      // Clear the screen, then save the top and left coordinates.
      Console.Clear();
      origRow = Console.CursorTop;
      origCol = Console.CursorLeft;

      WriteFrame(spielbrettGröße);

      foreach (string round in verlauf) 
      {        
        WriteFieldByRound(round, spielbrettGröße);
        Thread.Sleep(timerDelayInMilliseconds);
      }
      
      Console.WriteLine();
      Console.WriteLine();
      Console.WriteLine();
      Console.WriteLine();
      Console.WriteLine();
    }

    public void WriteFrame(int spielbrettGröße) 
    {
      int frameSize = spielbrettGröße + 4;
      int xMaxFrameSize = (spielbrettGröße * distanceSidesValues) + (2 * distanceSidesEdge);
      int yMaxFrameSize = (spielbrettGröße * distanceTopBottomValues) + (2 * distanceTopBottomEdge) +1;

      // Ecken
      WriteAt("+", 0, 0);
      WriteAt("+", 0, yMaxFrameSize);
      WriteAt("+", xMaxFrameSize, 0);
      WriteAt("+", xMaxFrameSize, yMaxFrameSize);

      // linker + rechter Rand      
      for (int i = 1; i < yMaxFrameSize; i++) 
      {
        WriteAt("|", 0, i);
        WriteAt("|", xMaxFrameSize, i);
      }

      // oberer + unterer Rand      
      for (int i = 1; i < xMaxFrameSize; i++)
      {
        WriteAt("-", i, 0);
        WriteAt("-", i, yMaxFrameSize);
      }
    }

    public void WriteFieldByRound(string roundCode, int spielbrettGröße) 
    {    
      roundCode = roundCode.Replace("(","");
      roundCode = roundCode.Replace(")", "");
      string[] fieldCodes = roundCode.Split(",");

      List<string> rowList = new List<string>();
      List<List<string>> fieldRowsList = new List<List<string>>();            
      foreach (string fieldCode in fieldCodes) 
      {
        rowList.Add(fieldCode.Trim());

        if (rowList.Count == spielbrettGröße)
        {
          fieldRowsList.Add(rowList);
          rowList = null;
          rowList = new List<string>();
        }
      }

      int x;
      int y = distanceTopBottomEdge;
      int xPos = 0;
      int yPos = 0;

      foreach (List<string> fieldRow in fieldRowsList) 
      {
        x = distanceSidesEdge;
        foreach (string field in fieldRow) 
        {
          xPos = (x * distanceSidesValues) - 2*distanceSidesEdge;
          yPos = (y * distanceTopBottomValues) + distanceTopBottomEdge;

          string writingCode = field.Length == 2 ? field.ToUpper() : field; // das Feld mit der Ameise soll groß geschrieben werden, welches als einziges 2 Zeichen hat
          writingCode = writingCode.PadLeft(distanceSidesValues, ' ');
          WriteAt(writingCode, xPos, yPos);
          x++;
        }
        y++;
      }
    }
  }
}