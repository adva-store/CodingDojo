// See https://aka.ms/new-console-template for more information
using System;

namespace Backend;

class Program
{
    static void Main(params string[] args)
    {
        int dimension = 0;
        int xPos = -1;
        int yPos = -1;
        AntDirection view = AntDirection.None;
        int moves = 0;
        
        foreach (string arg in args)
        {
            var split = arg.Split("=", StringSplitOptions.RemoveEmptyEntries);
            if (split.Length == 2)
            {
                switch (split[0])
                {
                    case "d":
                        int.TryParse(split[1], out dimension);
                        break;
                    case "x":
                        int.TryParse(split[1], out xPos);
                        break;
                    case "y":
                        int.TryParse(split[1], out yPos);
                        break;
                    case "v":
                        switch (split[1])
                        {
                            case "n":
                                view = AntDirection.North;
                                break;
                            case "e":
                                view = AntDirection.East;
                                break;
                            case "s":
                                view = AntDirection.South;
                                break;
                            case "w":
                                view = AntDirection.West;
                                break;
                            default:
                                view = AntDirection.None;
                                break;
                        }
                        break;
                    case "m":
                        int.TryParse(split[1], out moves);
                        break;
                }
            }
        }

        string? input = string.Empty;
        while (dimension < 2)
        {
            Console.WriteLine("Please insert dimension: ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!int.TryParse(input, out dimension) || dimension < 2)
                {
                    Console.WriteLine("Input must be a number greater or equal than 2");
                }
            }
        }

        while (xPos < 0 || xPos > dimension-1)
        {
            Console.WriteLine("Please insert start x coordinate: ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!int.TryParse(input, out xPos) || xPos < 0 || xPos > dimension-1)
                {
                    Console.WriteLine("Input must be a number greater or equal than 0 and lower {0}", dimension-1);
                }
            }
        }

        while (yPos < 0 || yPos > dimension - 1)
        {
            Console.WriteLine("Please insert start y coordinate: ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!int.TryParse(input, out yPos) || yPos < 0 || yPos > dimension-1)
                {
                    Console.WriteLine("Input must be a number greater or equal than 0 and lower {0}", dimension-1);
                }
            }
        }

        while (view == AntDirection.None)
        {
            Console.WriteLine("Please insert ant direction (n=north, e=east, s=south, w=west): ");
            input = Console.ReadLine();
            switch (input)
            {
                case "n":
                    view = AntDirection.North;
                    break;
                case "e":
                    view = AntDirection.East;
                    break;
                case "s":
                    view = AntDirection.South;
                    break;
                case "w":
                    view = AntDirection.West;
                    break;
                default:
                    view = AntDirection.None;
                    break;
            }
        }
        while(moves <= 0)
        {
            Console.WriteLine("Please insert move count: ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!int.TryParse(input, out moves) || moves <= 0)
                {
                    Console.WriteLine("Input must be a number greater than 0");
                }
            }
        }
        Console.WriteLine("Input complete - calculating moves:");
        var map = new Map(dimension, xPos, yPos, view, moves);
        map.CalculateMoves();
        
        Console.WriteLine("Save the output to file? [y/N]:");
        input = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(input) && string.Equals(input, "y", StringComparison.InvariantCultureIgnoreCase))
        {
            map.SaveHistory($"LangtonAnt_{DateTime.Now:yyyyMMdd-HHmmss}.txt");
        }
    }
}