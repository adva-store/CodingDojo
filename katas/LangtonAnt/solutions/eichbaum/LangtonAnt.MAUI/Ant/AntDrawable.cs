using System;
using LangtonAnt.Library;

namespace LangtonAnt.MAUI.Ant;

public class AntDrawable : IDrawable
{
	private string _grid;
	private int _size;

	public AntDrawable(int size, string grid)
	{
		_grid = grid;
		_size = size;
	}

	public void Draw(ICanvas canvas, RectF dirtyRect)
	{
		var rect = dirtyRect;
		var grid = _grid.Split(',');

		var singleItemSize = rect.Width / _size;

		float x;
		float y = 0;
		var fieldIndex = 0;

		canvas.StrokeColor = Colors.Black;

		for (var row = 0; row < _size; row++)
		{
			x = 0;

			for (var column = 0; column < _size; column++)
			{
				var field = string.Empty;
				if (fieldIndex < grid.Length - 1)
					field = grid[fieldIndex];
				fieldIndex++;

				if (field.Length > 1)
					canvas.FillColor = Colors.Red;  // ant is here
				else if (field == Library.Color.Black.GetAsString())
					canvas.FillColor = Colors.Black;
				else
					canvas.FillColor = Colors.Transparent;

				canvas.FillRectangle(x, y, singleItemSize, singleItemSize);
				canvas.DrawRectangle(x, y, singleItemSize, singleItemSize);

				x += singleItemSize;
			}

			y += singleItemSize;
		}
	}
}