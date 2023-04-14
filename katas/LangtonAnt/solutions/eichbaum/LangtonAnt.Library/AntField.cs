﻿using System;
using LangtonAnt.Library.Models;

namespace LangtonAnt.Library;

public class AntField
{
	protected readonly List<List<Field>> _matrix;
	protected readonly int _size;
	protected int _currentRow;
	protected int _currentColumn;
	protected Direction _currentDirection;

	public AntField(int size, int startRow, int startColumn, Direction direction)
	{
		_matrix = new();
		_size = size;
		_currentRow = startRow;
		_currentColumn = startColumn;
		_currentDirection = direction;
		InitAntField();
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

		// toggle color
		field.Color = field.Color == Color.Black ? Color.White : Color.Black;

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

	public int Size => _size;
}