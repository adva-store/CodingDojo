using Xunit;
using AntLogic;

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
        ant.Move();

        // Assert
        Assert.Equal((5, 4), ant.Position);
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
			ant.Move();
		}

        // Assert
        Assert.Equal("w,w,w,w,w,w,w,w,w,w,w,w,w,w,w,w,w,s,s,w,w,w,w,w,w,s,s,s,s,s,s,w,w,w,w,s,w,s,w,w,s,w,s,w,w,s,w,w,s,w,s,w,w,s,w,w,s,w,w,s,s,w,s,w,s,w,w,s,w,w,s,w,w,s,w,s,w,w,s,w,s,s,s,w,s,s,w,w,w,w,s,s,s,s,s,w,s,w,w,w,w,w,s,s,w,w,w,sw,w,w,w,w,w,w,w,w,w,w,w,w,w", grid.GetGridAsString(ant));
    }
}