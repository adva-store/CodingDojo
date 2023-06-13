using Xunit;
using AntLogic;
using System.Diagnostics;

/// <summary>
/// Tests for Langton Ant.
/// </summary>
public class AntTests
{
    [Fact]
    public void Move_AntFacingWestOnWhiteCell_AntMovesOneStepNorth()
    {
        // Arrange
        var grid = new AntGrid(11);
        var ant = new Ant(grid)
        {
            Position = (5, 5),
            Direction = Ant.AntDirection.West,
            StepsLeft = 1
        };

        // Act
        ant.MoveSingleStep();

        // Assert
        Assert.Equal((5, 4), ant.Position);
    }

	[Fact]
	public void Big_grid_with_many_steps_finishes_in_time()
	{
		// Arrange
		var grid = new AntGrid(100);
		var ant = new Ant(grid)
		{
			Position = (5, 5),
			Direction = Ant.AntDirection.West,
			StepsLeft = 1000
		};

        // Act
        var sw = new Stopwatch();
        sw.Start();
        while (ant.StepsLeft > 0)
        {
            ant.MoveSingleStep();
        }
        sw.Stop();

        // Assert
        Assert.True(sw.ElapsedMilliseconds < 500, "Buy a faster workstation! ;-)");
	}

	[Fact]
    public void GridIsInDefinedState_After200Steps()
    {
        // Arrange
        var grid = new AntGrid(11);
        var ant = new Ant(grid)
        {
            Position = (5, 5),
			Direction = Ant.AntDirection.West,
			StepsLeft = 199
        };

        while(ant.StepsLeft > 0)
		{
			ant.MoveSingleStep();
		}

        // Assert
        var gridString = grid.GetGridAsString(ant);
		Assert.Equal("w,w,w,w,w,w,w,w,w,w,w,w,w,w,w,w,w,s,s,w,w,w,w,w,w,s,s,s,s,s,s,w,w,w,w,s,w,s,w,w,s,w,s,w,w,s,w,w,s,w,s,w,w,s,w,w,s,w,w,s,s,w,s,w,s,w,w,s,w,w,s,w,w,s,w,s,w,w,s,w,s,s,s,w,s,s,w,w,w,w,s,s,s,s,s,w,s,w,w,w,w,w,s,s,w,w,w,sw,w,w,w,w,w,w,w,w,w,w,w,w,w", gridString);
	}
}