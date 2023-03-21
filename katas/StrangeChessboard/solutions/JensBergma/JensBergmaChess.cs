public class JensBergmaChess {
    public static void Main()
    {
        JensBergmaChess jbc = new JensBergmaChess();
        Console.WriteLine("Start");

        List<double> cs = new List<double>();
        List<double> rs = new List<double>();

        //random numbers for testing
        for(int i = 0; i<300000; i++)
        {
            Random random1 = new Random();
            cs.Add(random1.Next(1000));
        }

        for(int i = 0; i<300000; i++)
        {
            Random random1 = new Random();
            rs.Add(random1.Next(1000));
        }


        //var response = jbc.StrangeChessboard(new List<int>() {1, 8, 4, 5, 2}, new List<int>() {3, 1, 2, 7, 1});
        var response = jbc.StrangeChessboard(cs, rs);
        Console.WriteLine(response.Item1.ToString());
        Console.WriteLine(response.Item2.ToString());

        Console.WriteLine($"SumTogether {(response.Item1 + response.Item2).ToString()}");
        Console.WriteLine($"Correct from Test {(cs.Sum() * rs.Sum()).ToString()}");
    }

    private Tuple<double, double> StrangeChessboard(List<double> cs, List<double> rs)
    {
        double columnCount = 0;
        double rowCount = 0;
        List<ChessField> chessFields = new List<ChessField>();

        //create all Chessfields for all Columns and Rows
        foreach(var currentHight in cs)
        {
            rowCount = 0;
            foreach(var currentWidth in rs)
            {
                chessFields.Add(new ChessField(){
                    Column = columnCount,
                    Row = rowCount,
                    Hight = currentHight,
                    Width = currentWidth,
                    Volume = currentHight*currentWidth,
                    WhiteField = ((columnCount + rowCount) % 2 == 0)
                });

                rowCount++;
            }
            columnCount++;
        }

        //return sum of all white and black volumes
        return new Tuple<double, double>(chessFields.Where(cs => cs.WhiteField).Sum(cf => cf.Volume), chessFields.Where(cs => !cs.WhiteField).Sum(cf => cf.Volume));
    }

    private class ChessField
    {
        public double Row {get; set;}
        public double Column {get; set;}
        public double Hight {get; set;}
        public double Width {get; set;}
        public double Volume {get; set;}
        public bool WhiteField {get; set;}
    }
}
