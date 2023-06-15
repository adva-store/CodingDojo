using System.Linq;

using Console = System.Console;
using Math = System.Math;

namespace LangtonFrontend
{
	internal class CField
	{
		private bool[,] mi_field;
		public EDirection pu_dir { get; private set; }
		public SVector2 pu_pos { get; private set; }

		public int XSize => mi_field.GetLength(0);
		public int YSize => mi_field.GetLength(1);

		public CField(string _line)
		{
			string[] parts = _line.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
			int sqrt = (int)Math.Sqrt(parts.Length);

			mi_field = new bool[sqrt, sqrt];
			int x, y;
			for (int i = 0; i < parts.Length; i++)
			{
				x = i % sqrt;
				y = i / sqrt;
				mi_field[x, y] = parts[i].Last() == 's';
				if (parts[i].Length > 1)
				{
					pu_pos = new SVector2(x, y);
					char dir = parts[i].First();
					switch (dir)
					{
						case 'n':
							pu_dir = EDirection.UP;
							break;
						case 'o':
							pu_dir = EDirection.RIGHT;
							break;
						case 's':
							pu_dir = EDirection.DOWN;
							break;
						case 'w':
							pu_dir = EDirection.LEFT;
							break;
					}
				}
			}
		}

		

		public void fu_Draw()
		{
			Console.CursorVisible = false;
			for (int y = 0; y < mi_field.GetLength(1); y++)
			{
				for (int x = 0; x < mi_field.GetLength(0); x++)
				{
					Console.SetCursorPosition(x, y);
					Console.BackgroundColor = mi_field[x, y] ? System.ConsoleColor.Black : System.ConsoleColor.White;
					if (pu_pos.X == x && pu_pos.Y == y)
					{
						Console.ForegroundColor = System.ConsoleColor.Red;
						Console.Write(fi_CharForDir());
						Console.ResetColor();
					}
					else
					{
						Console.Write(' ');
					}
				}
			}
		}

		private char fi_CharForDir()
		{
			switch (pu_dir)
			{
				case EDirection.LEFT:
					return '←';
				case EDirection.UP:
					return '↑';
				case EDirection.RIGHT:
					return '→';
				case EDirection.DOWN:
					return '↓';
				default:
					throw new System.ArgumentException($"Direction {pu_dir} not valid");
			}
		}
	}
}
