namespace StrangeChessboard.Models
{
    public static class ChessBuilder
    {
        internal static ChessBuilderSized New(int size)
        {
            return new ChessBuilderSized(size);
        }
    }
}

