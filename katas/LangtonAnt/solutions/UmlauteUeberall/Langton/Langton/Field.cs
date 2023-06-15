using System.Threading;

using Console = System.Console;
using Math = System.Math;

namespace Langton
{
	internal class CField
	{
		private static readonly int WORLD_SIZE = 11;
		public static int SLEEP_TIME = 500;

		private bool[,] mi_field;
		private EDirection mi_dir = EDirection.LEFT;
		private SVector2 mi_pos;

		public CField()
		{
			mi_field = new bool[WORLD_SIZE, WORLD_SIZE];
			mi_pos = new SVector2((int) Math.Ceiling(WORLD_SIZE / 2.0f), (int)Math.Ceiling(WORLD_SIZE / 2.0f));
		}

		public void fu_Run()
		{
			bool isRunning = true;

			Thread inputThread = new Thread(() =>
			{
				if (Console.ReadKey(true).Key == System.ConsoleKey.Escape)
					isRunning = false;
			});
			inputThread.Start();

			while (isRunning)
			{
				fi_Draw();
				Thread.Sleep(SLEEP_TIME);
				fi_Calculate();
			}
		}

		private void fi_Calculate()
		{
			// rotate
			mi_dir += mi_field[mi_pos.X, mi_pos.Y] ? -1 : 1;
			mi_dir = (EDirection) (((int)mi_dir + 4) % 4);

			// color
			mi_field[mi_pos.X, mi_pos.Y] = !mi_field[mi_pos.X, mi_pos.Y];

			// move
			mi_pos += mi_dir;
			mi_pos = mi_pos.fu_DonutClamp(WORLD_SIZE);
		}

		private void fi_Draw()
		{
			Console.CursorVisible = false;
			for(int y = 0; y < mi_field.GetLength(1); y++)
			{
				for(int x = 0; x < mi_field.GetLength(0); x++)
				{
					Console.SetCursorPosition(x, y);
					Console.BackgroundColor = mi_field[x, y] ? System.ConsoleColor.Black : System.ConsoleColor.White;
					if (mi_pos.X == x && mi_pos.Y == y)
					{
						Console.ForegroundColor = System.ConsoleColor.Red;
						Console.Write(fi_CharForDir());
					}
					else
					{
						Console.Write(' ');
					}
				}
				Console.SetCursorPosition(0, WORLD_SIZE);
				Console.ResetColor();
			}

			Console.WriteLine("Press Escape for exiting");
		}

		private char fi_CharForDir()
		{
			switch (mi_dir)
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
					throw new System.ArgumentException($"Direction {mi_dir} not valid");
			}
		}
	}
}
