using CommunityToolkit.Mvvm.ComponentModel;

namespace StrangeChessboard.Models
{
    public partial class ChessLine : ObservableObject
    {
        public ChessLine(int maxLime)
        {
            Cells = new List<ChessCell>(maxLime);
        }

        [ObservableProperty]
        private IList<ChessCell> cells;
    }
}

