using System.Text;

namespace AntLogic;

public static class AntLogicHelpers
{
	/// <summary>
	/// Returns the grid as a comma-separated string.
	/// </summary>
	/// <param name="ant">Reference to an (optional) ant</param>
	/// <returns>the grid as a string</returns>
	public static string GetGridAsString(this AntGrid grid, Ant ant)
	{
		var sb = new StringBuilder();
		for (int y = 0; y < grid.Dimension; ++y)
		{
			for (int x = 0; x < grid.Dimension; ++x)
			{
				// If the ant is sitting on the cell, prefix the cell's color with the ant's
				// facing direction.
				if (ant != null && ant.Position.x == x && ant.Position.y == y)
				{
					sb.Append(ant.Direction switch
					{
						Ant.AntDirection.North => 'n',
						Ant.AntDirection.South => 's',
						Ant.AntDirection.West => 'w',
						Ant.AntDirection.East => 'e',
						_ => throw new InvalidOperationException()
					});
				}
				sb.Append(grid[x, y] == AntGrid.CellColor.White ? 'w' : 's');
				sb.Append(',');
			}
		}

		sb.Remove(sb.Length - 1, 1);
		return sb.ToString();
	}

	/// <summary>
	/// Helper method to output a visual representation of the board to the console.
	/// Useful for debugging.
	/// </summary>
	/// <param name="grid"></param>
	/// <param name="ant"></param>
	public static void DrawAsAsciiArt(this AntGrid grid, Ant ant)
    {
        // Draw the top border
        Console.WriteLine("+" + new string('-', grid.Dimension) + "+");

        // Draw the grid and ant as ASCII art
        for (int y = 0; y < grid.Dimension; y++)
        {
            Console.Write("|"); // Left border

            for (int x = 0; x < grid.Dimension; x++)
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
        Console.WriteLine("+" + new string('-', grid.Dimension) + "+");

		Console.WriteLine($"Steps left: {ant.StepsLeft}");
    }

    /// <summary>
    /// Helper method used when debugging to the console.
    /// Outputs a directional character based on the ant's current direction.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
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