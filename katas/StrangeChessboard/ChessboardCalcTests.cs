using Xunit;

namespace StrangeChessboard;

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
		
		// Test with N = 20,000
		long maxRepeats = 4000;

		for(var i = 0; i < maxRepeats; i++)
		{
			colSizes.AddRange(new long[] {3, 1, 2, 7, 1 });
			rowSizes.AddRange(new long[] {1, 8, 4, 5, 2 });
		}
		
		long expectedTotalArea = 4480000000;
		var sumColWidths = colSizes.Sum();
		var sumRowHeights = rowSizes.Sum();

		var calculator = new StrangeChessboardCalc(colSizes.ToArray(), rowSizes.ToArray());

		// Act
		(long totalWhiteArea, long totalBlackArea) = calculator.CalculateTotalAreaSize();

		// Assert
		Assert.Equal(expectedTotalArea, sumColWidths * sumRowHeights);
	}
}