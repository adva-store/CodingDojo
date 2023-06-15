using System.IO;

using Console = System.Console;

namespace LangtonFrontend
{
	public static class Helper
	{
		public static int fu_SafeNumberInput(string _text)
		{
			return fu_SafeNumberInput(_text, int.MinValue, int.MaxValue);
		}

		public static int fu_SafeNumberInput(string _text, int _min, int _max)
		{
			string outputText;
			if (_min == int.MinValue && _max == int.MaxValue)
			{
				outputText = _text + ": ";
			}
			else
			{
				outputText = _text + $" ({_min}-{_max}): ";
			}

			int returnValue;
			while (true)
			{
				Console.Write(outputText);
				if (int.TryParse(Console.ReadLine(), out returnValue))
				{
					if (returnValue >= _min && returnValue <= _max)
					{
						return returnValue;
					}
					Console.WriteLine($"Eingabe ist nicht im Wertebereich {_min}-{_max}");
				}
				else
				{
					Console.WriteLine("Eingabe ist keine Nummer");
				}
			}
		}

		public static string[] fu_SafeFileRead(string _text)
		{
			string fileName;
			while (true)
			{
				Console.Write("Gib einen gültigen Dateinamen ein: ");
				fileName = Console.ReadLine();
				if (File.Exists(fileName))
				{
					return File.ReadAllLines(fileName);
				}
				Console.WriteLine("Datei nicht lesbar/verfügbar");
			}
		}
	}
}
