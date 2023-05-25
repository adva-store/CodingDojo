using System.Text;

namespace AntLogic;

public static class AntLogicHelpers
{
	/// <summary>
	/// Returns the grid as a comma-separated string.
	/// </summary>
	/// <returns>the grid as a string</returns>
	public static string GetGridAsString(this AntGrid grid, Ant ant)
	{
		var sb = new StringBuilder();
		for(int y = 0; y < grid.Height; ++y)
		{
			for(int x = 0; x < grid.Width; ++x)
			{
				if(ant != null && ant.Position.x == x && ant.Position.y == y)
				{
					sb.Append(ant.Direction switch
					{
						Ant.AntDirection.North => "n",
						Ant.AntDirection.South => "s",
						Ant.AntDirection.West => "w",
						Ant.AntDirection.East => "e",
						_ => throw new InvalidOperationException()
					});
				}
				sb.Append(grid[x, y] == AntGrid.CellColor.White ? "w" : "s");
				sb.Append(",");
			}
		}

		sb.Remove(sb.Length - 1, 1);
		return sb.ToString();
	}

    public static void DrawAsAsciiArt(this AntGrid grid, Ant ant)
    {
        // Draw the top border
        Console.WriteLine("+" + new string('-', grid.Width) + "+");

        // Draw the grid and ant as ASCII art
        for (int y = 0; y < grid.Height; y++)
        {
            Console.Write("|"); // Left border

            for (int x = 0; x < grid.Width; x++)
            {
                bool isAntPosition = (x == ant.Position.x && y == ant.Position.y);
                var cellColor = grid[x, y];

                if (isAntPosition)
                {
                    Console.Write(GetAntDirectionSymbol(ant.Direction));
                }
                else if (cellColor == AntGrid.CellColor.Black)
                {
                    Console.Write("X");
                }
                else
                {
                    Console.Write("=");
                }
            }

            Console.WriteLine("|"); // Right border
        }

        // Draw the bottom border
        Console.WriteLine("+" + new string('-', grid.Width) + "+");

		Console.WriteLine($"Steps left: {ant.StepsLeft}");
    }

    public static char GetAntDirectionSymbol(Ant.AntDirection direction)
    {
        switch (direction)
        {
            case Ant.AntDirection.North:
                return '^';
            case Ant.AntDirection.South:
                return 'v';
            case Ant.AntDirection.West:
                return '<';
            case Ant.AntDirection.East:
                return '>';
            default:
                return '?';
        }
    }
}