using CommunityToolkit.Mvvm.ComponentModel;

namespace StrangeChessboard.Models
{
    public partial class ChessBoard : ObservableObject
    {
        public ChessBoard(int maxLine)
        {
            Lines = new List<ChessLine>(maxLine);
        }

        [ObservableProperty]
        private IList<ChessLine> lines;

        public int BlackSurface() => Surface(CellColor.Back);

        public int WhiteSurface() => Surface(CellColor.White);

        public int Surface() => Lines.Sum(p => p.Cells.Select(p => p.GetSurface())
                                        .Sum());


        private int Surface(CellColor cellColor)
        {
            return Lines.Sum(p => p.Cells.Where(p => p.Color == cellColor)
                                        .Select(p => p.GetSurface())
                                        .Sum());
        }

        internal void RaisePropertiesChanged()
        {
            OnPropertyChanged(nameof(Lines));
            OnPropertyChanged();
        }

    }
}

