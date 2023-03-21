public class Program {
    public static void Main()
    {
        ChessboardLogic chessboardLogic = new();
        Console.WriteLine("Start");

        List<double> cs = new(){1, 8, 4, 5, 2};
        List<double> rs = new(){3, 1, 2, 7, 1};

        var response = chessboardLogic.CreateChessboard(cs , rs);
        Console.WriteLine($"White Fields {response.Item1.ToString()}");
        Console.WriteLine($"Black Fields {response.Item2.ToString()}");

        Console.WriteLine($"SumTogether (Result): {(response.Item1 + response.Item2).ToString()}");
        Console.WriteLine($"Result must be the same as: {(cs.Sum() * rs.Sum()).ToString()}");
    }
}
