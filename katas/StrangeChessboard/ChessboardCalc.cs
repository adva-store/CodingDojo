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
		bool isWhite = true;

		for(var row = 0; row < NumRows; row++)
		{
			for(var col = 0; col < NumColumns; col++)
			{
				if(isWhite)
				{
					whiteArea += _colSizes[col] * _rowSizes[row];
				}
				else
				{
					blackArea += _colSizes[col] * _rowSizes[row];
				}
				isWhite = !isWhite;
			}

			// If the number of columns is even we need to reset the toggle
			// giving is the current background color because it would be 
			if(NumColumns % 2 == 0)
			{
				isWhite = false;
			}
		}

		return (whiteArea, blackArea);
	}
}