using System;
using System.Collections.Generic;
using System.Linq;

namespace StrangeChessboard
{
	internal class StrangeChessboardCalculator
	{
		/*
		 * Calculates the total areas of white and black fields of the "Strange chessboard".
		 */
		internal static void GetAreasOfStrangeChessboard(int[] fieldWidths, int[] fieldHeights, out (int total_white_area, int total_black_area) areas)
		{
			List<int> areasList = new List<int>();

			for (int i = 0; i < fieldWidths.Length; i++)
			{
				for (int j = 0; j < fieldHeights.Length; j++)
				{
					// Add the areas to a single big list.
					areasList.Add(fieldWidths[i] * fieldHeights[j]);
				}
			}

			// Odd areas are white (counting from 0). Even ones are black. We sum them up.
			int whiteAreaCalculated = areasList.Where((item, index) => index % 2 == 0).Sum();
			int blackAreaCalculated = areasList.Where((item, index) => index % 2 != 0).Sum();
			areas = (whiteAreaCalculated, blackAreaCalculated);
		}
	}
}