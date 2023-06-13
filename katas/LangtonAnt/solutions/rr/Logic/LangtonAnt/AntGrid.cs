using System.Text;

namespace AntLogic;

/// <summary>
/// Represents a grid of cells an ant can run around on.
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
	public AntGrid(int dimension = 11)
	{
		_dimension = dimension;
		_cells = new CellColor[dimension, dimension];
	}

	/// <summary>
	/// Width/Height of the grid.
	/// </summary>
	public int Dimension => _dimension;
	readonly int _dimension;

	CellColor[,] _cells;

	/// <summary>
	/// Returns the color of a cell at any given position.
	/// </summary>
	public CellColor this[int x, int y]
	{
		get => _cells[x, y];
		set => _cells[x, y] = value;
	}

	public override string ToString() => $"[{nameof(AntGrid)}]: Dimension = {Dimension}";
}