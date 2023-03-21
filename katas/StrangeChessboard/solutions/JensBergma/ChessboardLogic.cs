public class ChessboardLogic{
    public ChessboardLogic(){}
    public Tuple<double, double> CreateChessboard(List<double> cs, List<double> rs)
    {
        double currenColumnPos = 0;
        double currentRowPos = 0;
        List<ChessField> chessFields = new List<ChessField>();

        //create all Chessfields for all Columns and Rows
        foreach(var currentHight in cs)
        {
            //reset currenColumnPos for every row
            currenColumnPos = 0;
            foreach(var currentWidth in rs)
            {
                chessFields.Add(new ChessField(){
                    Volume = currentHight * currentWidth,
                    WhiteField = ((currenColumnPos + currentRowPos) % 2 == 0)
                });

                currenColumnPos++;
            }
            currentRowPos++;
        }

        //return sum of all white and black volumes
        return new Tuple<double, double>(chessFields.Where(cs => cs.WhiteField).Sum(cf => cf.Volume), chessFields.Where(cs => !cs.WhiteField).Sum(cf => cf.Volume));
    }
}