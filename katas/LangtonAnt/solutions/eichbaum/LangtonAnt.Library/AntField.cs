using System;
using LangtonAnt.Library.Models;

namespace LangtonAnt.Library;

public class AntField
{
	protected readonly List<List<Field>> _matrix;
	protected readonly int _size;
	protected int _currentRow;
	protected int _currentColumn;
	protected int _moves;
	protected Direction _currentDirection;

	public AntField(int size = 20, int? startRow = null, int? startColumn = null, Direction direction = Direction.North, int moves = 0)
	{
		_matrix = new();
		_size = size;
		_currentRow = startRow ?? (size / 2);
		_currentColumn = startColumn ?? (size / 2);
		_currentDirection = direction;
		_moves = 0;
		InitAntField();
		DoInitialMoves(moves);
	}

	private void InitAntField()
	{
		for (var row = 0; row < _size; row++)
		{
			_matrix.Add(new());
			for (var column = 0; column < _size; column++)
				_matrix[row].Add(new());
		}
	}

	private void DoInitialMoves(int moves)
	{
		for (var i = 0; i < moves; i++)
			if (!Move())
				break;
	}

	/// <summary>
	/// moves the ant
	/// </summary>
	/// <returns>false if reached wall, true otherwise</returns>
	public bool Move()
	{
		var field = _matrix[_currentRow][_currentColumn];

		// rotate
		if (field.Color == Color.White)
		{
			if (_currentDirection == Direction.West)
				_currentDirection = Direction.North;    // prevent overflow
			else
				_currentDirection++; // rotate right
		}
		else
		{
			if (_currentDirection == Direction.North)
				_currentDirection = Direction.West; // prevent "underflow"
			else
				_currentDirection--; // rotate left
		}

		// toggle color
		field.Color = field.Color == Color.Black ? Color.White : Color.Black;

		// move
		switch (_currentDirection)
		{
			case Direction.North:
				if (_currentRow <= 0)
					return false;
				_currentRow--;
				break;
			case Direction.East:
				if (_currentColumn >= Size - 1)
					return false;
				_currentColumn++;
				break;
			case Direction.South:
				if (_currentRow >= Size - 1)
					return false;
				_currentRow++;
				break;
			case Direction.West:
				if (_currentColumn <= 0)
					return false;
				_currentColumn--;
				break;
		}

		_moves++;

		return true;
	}

	public override string ToString()
	{
		var grid = string.Empty;
		for (var row = 0; row < _size; row++)
		{
			for (var column = 0; column < _size; column++)
			{
				var ant = string.Empty;
				if (row == _currentRow && column == _currentColumn)
					ant = _currentDirection.GetAsString();
				grid += $"{ant}{_matrix[row][column].Color.GetAsString()},";
			}
		}
		return grid.TrimEnd(',');
	}

	public void SaveToFile(string path) => System.IO.File.WriteAllText(path, ToString());
	public int Size => _size;
	public int Moves => _moves;
	public Direction Direction => _currentDirection;
}