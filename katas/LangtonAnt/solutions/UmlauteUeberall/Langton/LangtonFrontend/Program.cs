using System.Threading;

using Console = System.Console;

namespace LangtonFrontend
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Willkommen zu Langtons Ameise - Backend");
			Console.WriteLine("Sobald das Programm gestartet wurde lässt es sich vorzeitig beenden wenn Escape gedrückt wird");
			Console.WriteLine("Bevor es losgehen kann benötigen wir ein paar Eingaben");
			int speed = Helper.fu_SafeNumberInput("Gewünschte Darstellungsgeschwindigkeit in Millisekunden", 1, 1000000);

			string[] lines = Helper.fu_SafeFileRead("Gewünschte Datei");

			CField f = new CField(lines[0]);
			Console.WriteLine($"Feldgröße: {f.XSize} * {f.YSize}");
			Console.WriteLine($"Startposition: {f.pu_pos}");
			Console.WriteLine($"Startrichung: {fi_CharForDir(f.pu_dir)}");
			Console.WriteLine($"Anzahl der Spielzüge: {lines.Length}");

			Console.WriteLine("Drücke eine beliebige Taste um das Programm zu starten");
			Console.Read();

			bool isRunning = true;

			Thread inputThread = new Thread(() =>
			{
				if (Console.ReadKey(true).Key == System.ConsoleKey.Escape)
					isRunning = false;
			});
			inputThread.Start();

			Console.Clear();
			Console.SetCursorPosition(0, f.XSize);
			Console.WriteLine("Drücke Escape zum beenden");


			for(int i = 0; i < lines.Length && isRunning; i++)
			{
				if (string.IsNullOrEmpty(lines[i]))
					break;

				f = new CField(lines[i]);
				f.fu_Draw();
				Thread.Sleep(speed);
			}
		}

		private static char fi_CharForDir(EDirection _dir)
		{
			switch (_dir)
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
					throw new System.ArgumentException($"Direction {_dir} not valid");
			}
		}
	}
}
