namespace AntLogic;

public class Program
{
    public static async Task Main(string[] args)
    {
        var grid = new AntGrid(11);
		var ant = new Ant(grid)
		{
			Position = (5, 5),
			Direction = Ant.AntDirection.West,
			StepsLeft = 199
		};

		while(ant.StepsLeft > 0)
		{
			Console.Clear();
			grid.DrawAsAsciiArt(ant);
			Console.WriteLine(grid.GetGridAsString(ant));
			await Task.Delay(50);
			ant.Move();
		}

		if(grid.GetGridAsString(ant) == "w,w,w,w,w,w,w,w,w,w,w,w,w,w,w,w,w,s,s,w,w,w,w,w,w,s,s,s,s,s,s,w,w,w,w,s,w,s,w,w,s,w,s,w,w,s,w,w,s,w,s,w,w,s,w,w,s,w,w,s,s,w,s,w,s,w,w,s,w,w,s,w,w,s,w,s,w,w,s,w,s,s,s,w,s,s,w,w,w,w,s,s,s,s,s,w,s,w,w,w,w,w,s,s,w,w,w,sw,w,w,w,w,w,w,w,w,w,w,w,w,w")
		{
			Console.WriteLine("Success!");
		}
		else
		{
			Console.WriteLine("Failure!");
		}
	}
}