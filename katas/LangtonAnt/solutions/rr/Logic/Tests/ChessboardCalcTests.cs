using Xunit;

namespace StrangeChessboard;

/// <summary>
/// Tests for strange chessboard.
/// </summary>
public class ChessboardCalcTests
{
	[Fact]
	public void CalculateTotalArea_WithSizeOf1_ReturnsExpectedResults()
	{
		// Arrange
		long[] colSizes = { 3 };
		long[] rowSizes =  { 2 };
		long expectedBlackArea = 0;
		long expectedWhiteArea = 6;
		long expectedTotalArea = 6;
		var sumColWidths = colSizes.Sum();
		var sumRowHeights = rowSizes.Sum();

		var calculator = new StrangeChessboardCalc(colSizes, rowSizes);

		// Act
		(long totalWhiteArea, long totalBlackArea) = calculator.CalculateTotalAreaSize();

		// Assert
		Assert.Equal(expectedWhiteArea, totalWhiteArea);
		Assert.Equal(expectedBlackArea, totalBlackArea);
		Assert.Equal(expectedTotalArea, sumColWidths * sumRowHeights);
	}

	[Fact]
	public void CalculateTotalArea_WithGivenInputs_ReturnsExpectedResults()
	{
		// Arrange
		long[] colSizes = { 3, 1, 2, 7, 1 };
		long[] rowSizes = { 1, 8, 4, 5, 2 };
		long expectedBlackArea = 134;
		long expectedWhiteArea = 146;
		long expectedTotalArea = 280;
		var sumColWidths = colSizes.Sum();
		var sumRowHeights = rowSizes.Sum();

		var calculator = new StrangeChessboardCalc(colSizes, rowSizes);

		// Act
		(long totalWhiteArea, long totalBlackArea) = calculator.CalculateTotalAreaSize();

		// Assert
		Assert.Equal(expectedWhiteArea, totalWhiteArea);
		Assert.Equal(expectedBlackArea, totalBlackArea);
		Assert.Equal(expectedTotalArea, sumColWidths * sumRowHeights);
	}

	[Fact]
	public void CalculateTotalArea_WithLotsGivenInputs_ReturnsExpectedResults()
	{
		
		// Arrange
		var colSizes = new List<long>();
		var rowSizes = new List<long>();
		
		// Test with N = 40,000 (8,000 * 5 columns and rows)
		// On my Macbook Air M1 this test completes in 8.8 seconds.
		long maxRepeats = 8000;

		for(var i = 0; i < maxRepeats; i++)
		{
			colSizes.AddRange(new long[] {3, 1, 2, 7, 1 });
			rowSizes.AddRange(new long[] {1, 8, 4, 5, 2 });
		}

		Assert.Equal(40000, colSizes.Count);
		Assert.Equal(40000, rowSizes.Count);

		long expectedTotalArea = 17920000000;
		var sumColWidths = colSizes.Sum();
		var sumRowHeights = rowSizes.Sum();

		var calculator = new StrangeChessboardCalc(colSizes.ToArray(), rowSizes.ToArray());

		// Act
		(long totalWhiteArea, long totalBlackArea) = calculator.CalculateTotalAreaSize();

		// Assert
		Assert.Equal(expectedTotalArea, sumColWidths * sumRowHeights);
	}
}