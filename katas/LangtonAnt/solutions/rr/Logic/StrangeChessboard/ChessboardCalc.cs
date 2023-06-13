namespace StrangeChessboard;

/// <summary>
/// A class to calculate areas covered by white and black fields on "strange" chessboards. 
/// </summary>
public class StrangeChessboardCalc
{
	/// <summary>
	/// Creates a new instance of the calculator.
	/// Note: this class has been tested with array sizes up to 20,000.
	/// Note: both arrays must have the same size.
	/// </summary>
	/// <param name="colSizes">array with sizes (widths) of columns</param>
	/// <param name="rowSizes">array with sized (heights) of rows</param>
	public StrangeChessboardCalc(long[] colSizes, long [] rowSizes)
	{
		// Both arrays must be the same size, the chessboards are always square.
		if(colSizes.Length != rowSizes.Length)
		{
			throw new ArgumentException("Both arrays must be the same size.");
		}

		_colSizes = colSizes;
		_rowSizes = rowSizes;
	}

	public long[] ColSizes => _colSizes;
	public long[] RowSizes => _rowSizes;

	readonly long[] _colSizes;
	readonly long[] _rowSizes;

	/// <summary>
	/// Returns the number of columns of the board.
	/// </summary>
	public int NumColumns => _colSizes.Length;

	/// <summary>
	/// Returns the number of rows of the board.
	/// </summary>
	public int NumRows => _rowSizes.Length;

	/// <summary>
	/// Gets the total area occupied by squares of a given color on the chessboard.
	/// </summary>
	/// <returns>a tuple containing the total white and total black area</returns>
	public (long totalWhiteArea, long totalBlackArea) CalculateTotalAreaSize()
	{
		long whiteArea = 0;
		long blackArea = 0;
		for(var row = 0; row < NumRows; row++)
		{
			for(var col = 0; col < NumColumns; col++)
			{
				if(IsWhite(col, row))
				{
					whiteArea += _colSizes[col] * _rowSizes[row];
				}
				else
				{
					blackArea += _colSizes[col] * _rowSizes[row];
				}
			}
		}

		return (whiteArea, blackArea);
	}


	/// <summary>
	/// Returns wheter a cell if white or black.
	/// </summary>
	/// <param name="col">column zero based</param>
	/// <param name="row">row zero based</param>
	/// <returns></returns>
	public bool IsWhite(long col, long row) => (col + row) % 2 == 0;
}