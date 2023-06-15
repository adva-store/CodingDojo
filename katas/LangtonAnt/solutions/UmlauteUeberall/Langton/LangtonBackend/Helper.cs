using Console = System.Console;

namespace LangtonBackend
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

		public static SVector2 fu_SafeVectorInput(string _text, SVector2 _min, SVector2 _max)
		{
			int xSize = fu_SafeNumberInput(_text + " X", _min.X, _max.X);
			int ySize = fu_SafeNumberInput(_text + " Y", _min.Y, _max.Y);

			return new SVector2(xSize, ySize);
		}

		public static EDirection fu_SafeDirectionInput(string _text)
		{
			string inputS;
			while (true) 
			{ 
				Console.Write(_text + " (n,o,s,w): ");
				inputS = Console.ReadLine();
				if (inputS.ToLower() == "n")
				{
					return EDirection.UP;
				}
				if (inputS.ToLower() == "o")
				{
					return EDirection.RIGHT;
				}
				if (inputS.ToLower() == "s")
				{
					return EDirection.DOWN;
				}
				if (inputS.ToLower() == "w")
				{
					return EDirection.LEFT;
				}

				Console.WriteLine("Eingabe ungültig");
			}
		}
	}
}
