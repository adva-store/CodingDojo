namespace StrangeChessboard.Models
{
    public class ChessBuilderSized
    {
        private int size;
        private int[] cs;
        private int[] rs;

        public ChessBuilderSized(int size)
        {
            this.size = size;
        }

        internal ChessBoard Build()
        {
            ChessBoard chessBoard = new ChessBoard(size);
            CellColor cellColor = CellColor.White;
            for (int i = 0; i < rs.Length; i++)
            {
                ChessLine chessLine = new ChessLine(size);
                for (int j = 0; j < cs.Length; j++)
                {
                    chessLine.Cells.Add(new ChessCell
                    {
                        Color = cellColor,
                        Height = rs[i],
                        Width = cs[j],
                        X = i,
                        Y = j
                    });
                    cellColor = NextColor(cellColor);
                }
                chessBoard.Lines.Add(chessLine);
            }

            return chessBoard;
        }

        private CellColor NextColor(CellColor cellColor)
        {
            switch (cellColor)
            {
                case CellColor.White:
                    return CellColor.Back;

                case CellColor.Back:
                    return CellColor.White;
                default:
                    throw new NotSupportedException(cellColor.ToString("F"));
            }
        }

        internal ChessBuilderSized WithCS(params int[] cs)
        {
            if (cs.Length != size)
                throw new ArgumentException("tbdl");

            this.cs = cs;
            return this;
        }

        internal ChessBuilderSized WithRS(params int[] rs)
        {
            if (rs.Length != size)
                throw new ArgumentException("tbdl");

            this.rs = rs;
            return this;
        }
    }
}

