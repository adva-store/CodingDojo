using System;
namespace StrangeChessboard.Chessboard;

public class Drawable : IDrawable
{
	protected readonly Chessboard _chessboard;

	public Drawable(Chessboard chessboard)
	{
		_chessboard = chessboard;
	}

	public void Draw(ICanvas canvas, RectF dirtyRect)
	{
		var rect = dirtyRect;
		var singleItemSize = rect.Width / _chessboard.Columns.Sum();   // px size of single column

		float x;
		float y = 0;

		for (int row = 0; row < _chessboard.Rows.Count; row++)
		{
			if (row > 0)
				y += singleItemSize * _chessboard.Rows[row - 1];

			x = 0;

			var color = (row % 2 == 0) ? Colors.White : Colors.Black; // row start color

			for (int column = 0; column < _chessboard.Columns.Count; column++)
			{
				canvas.FillColor = color;

				if (column > 0)
					x += singleItemSize * _chessboard.Columns[column - 1];

				canvas.FillRectangle(x, y, singleItemSize * _chessboard.Columns[column], singleItemSize * _chessboard.Rows[row]);

				// toggle color
				color = color == Colors.White ? Colors.Black : Colors.White;
			}
		}

		canvas.StrokeColor = Colors.Black;
		canvas.DrawRectangle(rect);
	}
}
