namespace LangtonFrontend
{
	internal struct SVector2
	{
		public int X;
		public int Y;

		public SVector2(int _x, int _y)
		{
			X = _x;
			Y = _y;
		}

		public override string ToString()
		{
			return $"{X}/{Y}";
		}
	}
}
