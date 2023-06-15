namespace Langton
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

		public static SVector2 operator +(SVector2 _v1, SVector2 _v2)
		{
			return new SVector2(_v1.X + _v2.X, _v1.Y + _v2.Y);
		}

		public static SVector2 operator +(SVector2 _v1, EDirection _dir)
		{
			switch (_dir)
			{
				case EDirection.LEFT:
					return _v1 + new SVector2(-1, 0);
				case EDirection.UP:
					return _v1 + new SVector2(0, -1);
				case EDirection.RIGHT:
					return _v1 + new SVector2(1, 0);
				case EDirection.DOWN:
					return _v1 + new SVector2(0, 1);
				default:
					throw new System.ArgumentException($"Direction {_dir} not valid");
			}
		}

		public SVector2 fu_DonutClamp(int _worldSize)
		{
			return new SVector2((X + _worldSize) % _worldSize, 
								(Y + _worldSize) % _worldSize);
		}
	}
}
