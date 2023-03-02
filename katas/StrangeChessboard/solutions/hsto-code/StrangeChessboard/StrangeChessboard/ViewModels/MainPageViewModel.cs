using System;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using StrangeChessboard.Models;

namespace StrangeChessboard.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private int chessBoardize = 5;

        [ObservableProperty]
        private string cs = "3, 1, 2, 7, 1";

        [ObservableProperty]
        private string rs = "1, 8, 4, 5, 2";

        [ObservableProperty]
        private int whiteSurface;

        [ObservableProperty]
        private int blackSurface;

        [ObservableProperty]
        private int chessBoardSurface;

        [ObservableProperty]
        private ChessBoard chessBoard;
        private Grid chessGrid;

        public MainPageViewModel(Grid chessGrid)
        {
            this.chessGrid = chessGrid;
        }

        [RelayCommand(CanExecute = nameof(CanGenerate))]
        private void Generate()
        {
            int[] csArray = Cs.Split(',', StringSplitOptions.TrimEntries).Select(p => int.Parse(p)).ToArray();
            int[] rsArray = Rs.Split(',', StringSplitOptions.TrimEntries).Select(p => int.Parse(p)).ToArray();
            ChessBoard = ChessBuilder.New(ChessBoardize)
                                  .WithCS(csArray)
                                  .WithRS(rsArray)
                                  .Build();

            ChessBoard.RaisePropertiesChanged();

            BlackSurface = ChessBoard.BlackSurface();
            WhiteSurface = ChessBoard.WhiteSurface();
            ChessBoardSurface = ChessBoard.Surface();


            Grid grid = new Grid()
            {
            };
            for (int i = 0; i < csArray.Sum(); i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition(new GridLength(70)));
            }

            for (int i = 0; i < rsArray.Sum(); i++)
            {
                grid.RowDefinitions.Add(new RowDefinition(new GridLength(70)));
            }


            for (int i = 0; i < ChessBoard.Lines.Count; i++)
            {
                ChessLine line = ChessBoard.Lines[i];
                int shiftBottom = 0;
                if (i > 0)
                {
                    shiftBottom = ChessBoard.Lines.TakeWhile(p => ChessBoard.Lines.IndexOf(p) < i).Sum(p => p.Cells[0].Height );
                }

                for (int j = 0; j < line.Cells.Count; j++)
                {
                    ChessCell cell = line.Cells[j];
                    var shiftRight = line.Cells.TakeWhile(p => line.Cells.IndexOf(p) < j).Sum(p => p.Width);


                    StackLayout stack = new StackLayout()
                    {
                        BackgroundColor = ConvertColor(cell.Color),
                        Spacing = 0,

                    };

                    Grid.SetRow(stack, shiftBottom);
                    Grid.SetColumn(stack, shiftRight);

                    Grid.SetRowSpan(stack, cell.Height );
                    Grid.SetColumnSpan(stack, cell.Width);


                    stack.Children.Add(new Label()
                    {
                        Text = $"{cell.Width} * {cell.Height}",
                        TextColor = Colors.Red,
                        BackgroundColor = ConvertColor(cell.Color),
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center              
                    });

                    grid.Children.Add(stack);
                }
            }

            MainThread.BeginInvokeOnMainThread(() =>
            {
                chessGrid.Children.Clear();
                chessGrid.Children.Add(grid);
            });
        }

        private Color ConvertColor(CellColor color)
        {
            switch (color)
            {
                case CellColor.White:
                    return Colors.White;

                case CellColor.Back:
                    return Colors.Black;
                default:
                    throw new NotSupportedException(color.ToString("F"));
            }
        }

        private bool CanGenerate()
        {
            string[] csArray = Cs.Split(',', StringSplitOptions.TrimEntries);
            int csLength = csArray.Length;
            string[] rsArray = Rs.Split(',', StringSplitOptions.TrimEntries);
            int rsLength = rsArray.Length;
            return string.IsNullOrWhiteSpace(Cs) == false
                && string.IsNullOrWhiteSpace(Rs) == false
                && rsLength == csLength
                && csLength == ChessBoardize
                && rsArray.All(p => int.TryParse(p, out int value))
                && csArray.All(p => int.TryParse(p, out int value));
        }
    }
}

