using System.Collections.Generic;
using System.Linq;

using Console = System.Console;

namespace StrangeChessboard
{
	internal class Program
	{
		static void Main(string[] args)
		{
			List<int> cs = new List<int>() { 3, 1, 2, 7, 1 };
			List<int> rs = new List<int>() { 1, 8, 4, 5, 2 };

			if (cs.Count != rs.Count)
			{
				throw new System.InvalidOperationException("Beide Listen müssen gleich lang sein");
			}

			var result = fu_Calculate(cs, rs);

			Console.WriteLine(result);

			Console.ReadKey();
		}

		public static (int white, int black) fu_Calculate(List<int> _cs, List<int> _rs)
		{
			bool curWhite = true;
			List<int> whites = new List<int>();
			List<int> blacks = new List<int>();

			for (int y = 0; y < _rs.Count; y++)
			{
				for (int x = 0; x < _cs.Count; x++)
				{
					if (curWhite)
					{
						whites.Add(_cs[x] * _rs[y]);
					}
					else
					{
						blacks.Add(_cs[x] * _rs[y]);
					}
					curWhite = !curWhite;
				}
			}

			return (whites.Sum(), blacks.Sum());
		}
	}
}
