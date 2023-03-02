using System.Numerics;
using System.Text;

namespace LangtonAnt;

public class Playfield
{
    private readonly Ant ant;
    private readonly int playFieldCellNumber;
    private readonly string outputPath;
    private readonly bool appendPointInFile;
    private readonly IList<Cell> cells;

    public Playfield( int playFieldCellNumber,Point startPoint, Direction direction, string outputPath, bool appendPointInFile = false)
    {
        if (string.IsNullOrWhiteSpace(outputPath))
        {
            throw new ArgumentException($"'{nameof(outputPath)}' cannot be null or whitespace.", nameof(outputPath));
        }

        if(playFieldCellNumber < 3)
        {
            throw new ArgumentException("tbdl");
        }

        if(Directory.Exists(outputPath) == false)
        {
            throw new DirectoryNotFoundException(outputPath);
        }

        this.playFieldCellNumber = playFieldCellNumber;
        this.outputPath = outputPath;
        this.appendPointInFile = appendPointInFile;
        this.cells = new List<Cell>(playFieldCellNumber * playFieldCellNumber);
        for (int x = 0; x < playFieldCellNumber; x++)
        {
            for (int y = 0; y < playFieldCellNumber; y++)
            {
                this.cells.Add ( new Cell()
                {
                    Point = new Point(x,y),
                });
            }
        }
        //???
        Cell antStartCell = cells.SingleOrDefault(p=> p.Point == startPoint);
        ant = new Ant(cells,antStartCell ,direction);
    }


    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < cells.Count; i++)
        {
            Cell item = this.cells[i];
            if (item.Point == this.ant.CurrentCell.Point)
            {
                builder.Append($"{this.ant.Direction.AsString()}");
            }

            builder.Append($"{item.ToString()}");
            if (appendPointInFile)
            {
                builder.Append($"[X={item.Point.X},Y={item.Point.Y}]");
            }
            if(i != cells.Count - 1)
            {
                builder.Append(",");
            }
        }

        return builder.ToString();
    }

    public void Start(int moveCount)
    {
        using StreamWriter streamWriter = new StreamWriter(Path.Combine(this.outputPath, "LangtonAnt.txt"));
        int move = 0;
        string text = ToString();
        Console.WriteLine(text);
        streamWriter.WriteLine(text);
        while (move < moveCount)
        {
            this.ant.Move();
            text = ToString();
            Console.WriteLine(text);
            streamWriter.WriteLine(text);
            move++;
        }
    }
}
