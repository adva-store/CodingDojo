using System.Collections;
using System.Text;

namespace AntLogic;

/// <summary>
/// Represents a grid of cells.
/// </summary>
public class AntGrid
{
	/// <summary>
	/// Possible colors of cells in the grid.
	/// </summary>
	public enum CellColor
	{
		White,
		Black	
	}

	/// <summary>
	/// Creates a new square grid with the specified dimension used as width and height.
	/// </summary>
	/// <param name="dimension">width and height of the grid</param>
	public AntGrid(int dimension = 11) : this(dimension, dimension)
	{
	}

	/// <summary>
	/// Creates a new grid with the specified width and height.
	/// </summary>
	/// <param name="width">width of the grid</param>
	/// <param name="height">height of the grid</param>
	public AntGrid(int width, int height)
	{
		_width = width;
		_height = height;
		_cells = new CellColor[width, height];
	}

	public int Width => _width;
	public int Height => _height;

	int _width;
	int _height;

	CellColor[,] _cells;

	public CellColor this[int x, int y]
	{
		get => _cells[x, y];
		set => _cells[x, y] = value;
	}
}