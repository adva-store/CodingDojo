using System;
namespace StrangeChessboard.Chessboard;

public class Chessboard
{
	public readonly List<int> Columns;
	public readonly List<int> Rows;

	public Chessboard(string columns, string rows)
	{
		Columns = GetIntList(columns);
		Rows = GetIntList(rows);
	}

	public int Width => 300;    // fixed
	public int Height
	{
		get
		{
			var singleItemSize = (float)Width / Columns.Sum();   // px size of single column
			return (int)(singleItemSize * Rows.Sum());
		}
	}

	public Drawable Drawable => new(this);

	public Tuple<int, int> TotalArea
	{
		get
		{
			var whiteArea = 0;
			var blackArea = 0;
			for (int row = 0; row < Rows.Count; row++)
			{
				var color = (row % 2 == 0) ? Colors.White : Colors.Black; // row start color

				for (int column = 0; column < Columns.Count; column++)
				{
					var area = Columns[column] * Rows[row];
					if (color == Colors.White)
						whiteArea += area;
					else
						blackArea += area;

					// toggle color
					color = color == Colors.White ? Colors.Black : Colors.White;
				}
			}
			return new(whiteArea, blackArea);
		}
	}

	private List<int> GetIntList(string stringList)
	{
		var result = new List<int>();
		var items = stringList.Split(',');
		foreach (var item in items)
		{
			if (int.TryParse(item.Trim(), out int value))
				result.Add(value);
		}
		return result;
	}
}