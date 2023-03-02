using System;
using System.Runtime.ConstrainedExecution;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace LangtonAnt.Frontend.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        private class AntPosition
        {
            public Direction Direction { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
        }

        private List<AntPosition> antPositions = new List<AntPosition>();


        private readonly Grid playgroudGrid;
        [ObservableProperty]
        private string inputFile;

        [ObservableProperty]
        private int moveSpeed = 3;

        [ObservableProperty]
        private int playfieldLength;

        [ObservableProperty]
        private Point startPosition;

        [ObservableProperty]
        private Direction direction;

        [ObservableProperty]
        private int moveCount;

        public MainPageViewModel(Grid playgroudGrid)
        {
            this.playgroudGrid = playgroudGrid;
        }

        [RelayCommand]
        private async Task Start()
        {
            foreach (var item in antPositions.Skip(1))
            {
                await Delay();

                await DrawAsync(item);
            }


            await DrawAsync(antPositions.ElementAt(0));
        }

        private async Task Delay()
        {
            var delay = TimeSpan.FromMilliseconds(500);
            switch (MoveSpeed)
            {
                case 1:
                default:
                    delay = TimeSpan.FromMilliseconds(1000);
                    break;
                case 2:
                    delay = TimeSpan.FromMilliseconds(900);
                    break;
                case 3:
                    delay = TimeSpan.FromMilliseconds(800);
                    break;
                case 4:
                    delay = TimeSpan.FromMilliseconds(700);
                    break;
                case 5:
                    delay = TimeSpan.FromMilliseconds(500);
                    break;
            }

            await Task.Delay(delay);
        }

        private async Task DrawAsync(AntPosition antMove)
        {
            Grid grid = new Grid()
            {
            };
            for (int i = 0; i < PlayfieldLength; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition(new GridLength(70)));
            }

            for (int i = 0; i < PlayfieldLength; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition(new GridLength(70)));
            }


            for (int i = 0; i < PlayfieldLength; i++)
            {
                for (int j = 0; j < PlayfieldLength; j++)
                {
                    StackLayout stack = new StackLayout()
                    {
                        BackgroundColor = Colors.LightGray,
                        Spacing = 0,
                        Margin = new Thickness(2)
                    };

                    Grid.SetRow(stack, i);
                    Grid.SetColumn(stack, j);

                    stack.Children.Add(new Label()
                    {
                        Text = i == antMove.Y && j == antMove.X ? $"Ant" : "",
                        TextColor = Colors.Red,
                        BackgroundColor = Colors.Red,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center
                    });

                    grid.Children.Add(stack);
                }
            }

            await MainThread.InvokeOnMainThreadAsync(() =>
               {
                   playgroudGrid.Children.Clear();
                   playgroudGrid.Children.Add(grid);
               });
        }

        [RelayCommand]
        private async Task ImportFile()
        {
            antPositions.Clear();
            try
            {
                PickOptions options = new()
                {
                    PickerTitle = "Please select a LangtonAnt file",
                };
                var result = await FilePicker.Default.PickAsync(options);
                if (result != null)
                {
                    if (result.FileName.EndsWith("txt", StringComparison.OrdinalIgnoreCase))
                    {
                        using var stream = await result.OpenReadAsync();
                        using var streamReader = new StreamReader(stream);
                        int lineCount = 0;
                        while (streamReader.EndOfStream == false)
                        {
                            var line = await streamReader.ReadLineAsync();
                            if (string.IsNullOrWhiteSpace(line))
                                continue;

                            var lineSplit = line.Split(",");
                            if (lineCount == 0)
                            {
                                PlayfieldLength = Convert.ToInt32(Math.Sqrt(lineSplit.Length));
                            }

                            var playfieldSplit = lineSplit.Chunk(PlayfieldLength);
                            for (int i = 0; i < playfieldSplit.Count(); i++)
                            {
                                var items = playfieldSplit.ElementAt(i).ToList();
                                var positionEntry = items.SingleOrDefault(p => p.Length > 1);
                                var indexOfAnt = items.IndexOf(positionEntry);

                                if (indexOfAnt != -1)
                                {
                                    AntPosition antPosition = new AntPosition()
                                    {
                                        Direction = positionEntry[0].FromChar(),
                                        X = i,
                                        Y = indexOfAnt
                                    };

                                    antPositions.Add(antPosition);
                                    break;
                                }
                            }


                            lineCount++;
                        }

                        MoveCount = lineCount - 1;
                        if (MoveCount > 0)
                        {
                            AntPosition antMove = antPositions.ElementAt(0);
                            this.Direction = antMove.Direction;
                            this.StartPosition = new Point(antMove.X, antMove.Y);


                            await DrawAsync(antPositions.ElementAt(0));

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
            }
        }


    }
}

