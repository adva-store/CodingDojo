using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;


// Class containing tests for the "Strange chessboard" areas calculation.
namespace StrangeChessboard
{
	[TestClass]
	public class ChessboardCalculationUnitTest
	{
		[TestMethod]
		public void SimpleTest1()
		{
			int[] fieldsWidths = { 10, 3, 5 };
			int[] fieldHeights = { 3, 8, 22 };

			int whiteArea = 10 * 3 + 10 * 22 + 3 * 8 + 5 * 3 + 5 * 22;
			int blackArea = 10 * 8 + 3 * 3 + 3 * 22 + 5 * 8;
			RunChessboardCalculation(fieldsWidths, fieldHeights, whiteArea, blackArea);
		}

		private void RunChessboardCalculation(int[] fieldsWidths, int[] fieldHeights, int expectedWhiteArea, int expecterBlackArea)
		{
			StrangeChessboardCalculator.GetAreasOfStrangeChessboard(fieldsWidths, fieldHeights, out (int total_white_area, int total_black_area) areas);

			Assert.AreEqual(expectedWhiteArea, areas.total_white_area);
			Assert.AreEqual(expecterBlackArea, areas.total_black_area);

			Assert.AreEqual(fieldHeights.Sum() * fieldsWidths.Sum(), areas.total_black_area + areas.total_white_area);
		}

		[TestMethod]
		public void SimpleTest2()
		{
			// Test from the desctiption of the Kata.
			int[] fieldsWidths = { 3, 1, 2, 7, 1 };
			int[] fieldHeights = { 1, 8, 4, 5, 2 };

			int whiteArea = 146;
			int blackArea = 134;

			RunChessboardCalculation(fieldsWidths, fieldHeights, whiteArea, blackArea);
		}


		[TestMethod]
		public void Test3()
		{
			// All the fields have a size of 1. Even length/height of the chessboard. White and black are equal.
			int[] fieldsWidths = Enumerable.Repeat(1, 3000).ToArray();
			int[] fieldHeights = Enumerable.Repeat(1, 3000).ToArray();

			int whiteArea = 4500000;
			int blackArea = 4500000;

			RunChessboardCalculation(fieldsWidths, fieldHeights, whiteArea, blackArea);
		}

		[TestMethod]
		public void Test4()
		{
			// All the fields have a size of 1. Odd length/height of the chessboard. There is one more white field than black.
			int[] fieldsWidths = Enumerable.Repeat(1, 3001).ToArray();
			int[] fieldHeights = Enumerable.Repeat(1, 3001).ToArray();

			int whiteArea = 4503001;
			int blackArea = 4503000;

			RunChessboardCalculation(fieldsWidths, fieldHeights, whiteArea, blackArea);
		}


	}
}
