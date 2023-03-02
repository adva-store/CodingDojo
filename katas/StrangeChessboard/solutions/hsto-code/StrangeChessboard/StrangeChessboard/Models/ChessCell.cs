using System;

using CommunityToolkit.Mvvm.ComponentModel;

namespace StrangeChessboard.Models
{
    public partial class ChessCell : ObservableObject
    {
        [ObservableProperty]
        private int x;

        [ObservableProperty]
        private int y;

        [ObservableProperty]
        private CellColor color;

        [ObservableProperty]
        private int width;

        [ObservableProperty]
        private int height;

        public int GetSurface() => Width * Height;
    }
}

