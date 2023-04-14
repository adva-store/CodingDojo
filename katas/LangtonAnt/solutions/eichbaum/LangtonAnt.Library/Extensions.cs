using System;
namespace LangtonAnt.Library;

public static class Extensions
{
	public static string GetAsString(this Direction direction)
	{
		switch (direction)
		{
			case Direction.North:
				return "n";
			case Direction.East:
				return "o";
			case Direction.South:
				return "s";
			case Direction.West:
				return "w";
			default:
				throw new ArgumentException();
		}
	}

	public static string GetAsString(this Color color)
	{
		switch (color)
		{
			case Color.Black:
				return "s";
			case Color.White:
				return "w";
			default:
				throw new ArgumentException();
		}
	}
}