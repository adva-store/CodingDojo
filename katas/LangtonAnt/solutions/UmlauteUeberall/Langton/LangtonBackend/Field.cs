using System.IO;
using System.Text;

using Console = System.Console;

namespace LangtonBackend
{
	internal class CField
	{
		private int mi_worldSize;
		private int mi_stepCount;

		private bool[,] mi_field;
		private EDirection mi_dir = EDirection.LEFT;
		private SVector2 mi_pos;

		public CField()
		{
			Console.WriteLine("Willkommen zu Langtons Ameise - Backend");
			Console.WriteLine("Bevor es losgehen kann benötigen wir ein paar Eingaben");
			mi_worldSize = Helper.fu_SafeNumberInput("Gewünschte Feldgröße", 1, 50);
			mi_pos = Helper.fu_SafeVectorInput("Gewünschte Startposition", new SVector2(0,0), new SVector2(mi_worldSize - 1, mi_worldSize - 1));

			mi_dir = Helper.fu_SafeDirectionInput("Gewünschte Startrichtung");

			mi_stepCount = Helper.fu_SafeNumberInput("Gewünschte Anzahl der Spielzüge", 1, int.MaxValue);

			mi_field = new bool[mi_worldSize, mi_worldSize];
		}

		public void fu_Run()
		{
			bool isRunning = true;

			StringBuilder sb = new StringBuilder();
			fi_AddFieldToString(sb);
			for (int count = 1; count < mi_stepCount && isRunning; count++)
			{
				fi_Calculate();
				fi_AddFieldToString(sb);
			}

			File.WriteAllText("output.save", sb.ToString());
			Console.WriteLine("output.save erfolgreich geschrieben");
			Console.WriteLine("Drücke eine beliebige Taste um das Programm zu beenden");
			Console.Read();
		}

		private void fi_Calculate()
		{
			// rotate
			mi_dir += mi_field[mi_pos.X, mi_pos.Y] ? -1 : 1;
			mi_dir = (EDirection)(((int)mi_dir + 4) % 4);

			// color
			mi_field[mi_pos.X, mi_pos.Y] = !mi_field[mi_pos.X, mi_pos.Y];

			// move
			mi_pos += mi_dir;
			mi_pos = mi_pos.fu_DonutClamp(mi_worldSize);
		}
		private char fi_CharForDir()
		{
			switch (mi_dir)
			{
				case EDirection.LEFT:
					return 'w';
				case EDirection.UP:
					return 'n';
				case EDirection.RIGHT:
					return 'o';
				case EDirection.DOWN:
					return 's';
				default:
					throw new System.ArgumentException($"Direction {mi_dir} not valid");
			}
		}

		private void fi_AddFieldToString(StringBuilder _sb)
		{
			for (int y = 0; y < mi_field.GetLength(1); y++)
			{
				for (int x = 0; x < mi_field.GetLength(0); x++)
				{
					if (mi_pos.X == x && mi_pos.Y == y)
					{
						_sb.Append(fi_CharForDir());
					}
					_sb.Append(mi_field[x, y] ? "s" : "w");
					_sb.Append(',');
				}
			}
			_sb.Append(System.Environment.NewLine);
		}
	}
}
