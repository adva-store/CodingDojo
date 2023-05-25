namespace AntLogic;

/// <summary>
/// Represents an ant sitting on a grid.
/// </summary>
public class Ant
{
	/// <summary>
	/// Enumerates the possible states of the ant.
	/// It can be on a white cell or on a black cell.
	/// </summary>
	public enum AntState
	{
		OnWhiteCell,
		OnBlackCell
	}

	/// <summary>
	/// The possible directions the ant can be facing.
	/// Use <see cref="Direction"/> to get current direction.
	/// </summary>
	public enum AntDirection
	{
		North,
		South,
		West,
		East
	}

	/// <summary>
	/// Current position of the ant.
	/// Grid coordinates are zero based with the origin (0,0) at the top left.
	/// </summary>
	public (int x, int y) Position { get; set; }

	/// <summary>
	/// The ant's current facing direction.
	/// </summary>
	public AntDirection Direction { get; set; }

	/// <summary>
	/// Number of steps the ant can move.
	/// </summary>
	public int StepsLeft { get; set; } = 1;

	/// <summary>
	/// Creates a new ant sitting on a grid.
	/// </summary>
	/// <param name="grid"></param>
	public Ant(AntGrid grid)
	{
		if (grid == null)
		{
			throw new ArgumentNullException(nameof(grid), "Every ant needs a grid to sit on. Basic biology.");
		}
		_grid = grid;
	}

	AntGrid _grid;

	/// <summary>
	/// Makes the ant move one step.
	/// Updates <see cref="StepsLeft"/>
	/// </summary>
	/// <returns>The current state of the grid as a string or an empty string if no steps left.</returns>
	public string MoveSingleStep()
	{
		if(StepsLeft <= 0)
		{
			return string.Empty;
		}

		if(_grid[Position.x, Position.y] == AntGrid.CellColor.White)
		{
			// We're on white. Turn 90 degrees to the right.
			TurnNinetyDegrees(left: false);
		}
		else
		{
			// We're on black. Turn 90 degrees to the left.
			TurnNinetyDegrees(left: true);
		}

		ToggleCellColorAtCurrentPosition();
		MoveIntoCurrentDirection();

		// If ant position is out of grid boundaries, wrap around.
		if(Position.x < 0)
		{
			Position = (_grid.Dimension - 1, Position.y);
		}
		else if(Position.x >= _grid.Dimension)
		{
			Position = (0, Position.y);
		}

		if(Position.y < 0)
		{
			Position = (Position.x, _grid.Dimension - 1);
		}
		else if(Position.y >= _grid.Dimension)
		{
			Position = (Position.x, 0);
		}

		StepsLeft--;

		var gridState = _grid.GetGridAsString(this);
		return gridState;
	}

	/// <summary>
	/// Turns the ant 90 degrees to the left or right based on the current facing direction.
	/// <see cref="Direction"/> to get current direction.
	/// </summary>
	/// <param name="left">True to turn left, false to turn right.</param>
	void TurnNinetyDegrees(bool left) =>
		Direction = Direction switch
		{
			AntDirection.North => left ? AntDirection.West : AntDirection.East,
			AntDirection.South => left ? AntDirection.East : AntDirection.West,
			AntDirection.West => left ? AntDirection.South : AntDirection.North,
			AntDirection.East => left ? AntDirection.North : AntDirection.South,
			_ => Direction
		};

	/// <summary>
	/// Toggles the cell color of the grid at the ant's current position.
	/// </summary>
	void ToggleCellColorAtCurrentPosition()
	{
		_grid[Position.x, Position.y] = _grid[Position.x, Position.y] == AntGrid.CellColor.White
			? AntGrid.CellColor.Black
			: AntGrid.CellColor.White;
	}

	/// <summary>
	/// Moves the ant one step into the current direction by updating its position 
	/// depending on the current facing direction <see cref="Direction"/>.
	/// </summary>
	void MoveIntoCurrentDirection()
	{
		Position = Direction switch
		{
			AntDirection.North => (Position.x, Position.y - 1),
			AntDirection.South => (Position.x, Position.y + 1),
			AntDirection.West => (Position.x - 1, Position.y),
			AntDirection.East => (Position.x + 1, Position.y),
			_ => Position
		};
	}

	public override string ToString() => $"[{nameof(Ant)}]: Position = {Position}, Direction = {Direction}";
}
