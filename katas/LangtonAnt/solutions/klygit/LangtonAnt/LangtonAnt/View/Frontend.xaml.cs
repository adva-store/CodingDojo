using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static LangtonAnt.GameData;

namespace LangtonAnt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Frontend : ContentPage
    {
        public Frontend()
        {
            var gameData = App.ServiceProvider.GetService<IGameDataManager>().GameData;

            InitializeComponent();

            var gridView = new GridView(gameData.GridSize);
            contentContainer.Content = gridView;

            var viewModel = App.ServiceProvider.GetService<FrontendViewModel>();
            viewModel.OnIndexCurrentChangedAction = (indexCurrentStep) =>
            {
                var listDrawData = gameData.GetListDrawDataByIndex(indexCurrentStep);
                gridView.Draw(listDrawData);
            };
            BindingContext = viewModel;

            Title = viewModel.DisplayAntStartPos;
        }

        private class GridView : ContentView
        {
            private readonly Grid grid;
            private readonly ContentView[,] gridChild;

            private readonly int gridSize;

            internal GridView(int gridSize)
            {
                this.gridSize = gridSize;

                HorizontalOptions = LayoutOptions.FillAndExpand;
                VerticalOptions = LayoutOptions.FillAndExpand;
                BackgroundColor = Color.White;

                gridChild = new ContentView[gridSize, gridSize];

                var contentContainer = new ContentView()
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                };
                Content = contentContainer;

                grid = new Grid()
                {
                    ColumnSpacing = 1,
                    RowSpacing = 1,
                    BackgroundColor = Color.White,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                };
                contentContainer.Content = grid;

                for (var indexGridSize = 0; indexGridSize < gridSize; indexGridSize++)
                {
                    grid.RowDefinitions.Add(new RowDefinition());
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                }

                SizeChanged += (sender, eventArgs) =>
                {
                    var width = Width;
                    var height = Height;

                    if (width == contentContainer.WidthRequest && height == contentContainer.HeightRequest)
                        return;

                    var minSize = (int)((width < height ? width : height) - 0.5f);

                    contentContainer.WidthRequest = minSize;
                    contentContainer.HeightRequest = minSize;
                };
            }

            internal void Draw(List<DrawData> listDrawData)
            {
                var imgResManager = App.ServiceProvider.GetService<IImgResManager>();
                var indexItem = 0;
                for (var indexX = 0; indexX < gridSize; indexX++)
                {
                    for (var indexY = 0; indexY < gridSize; indexY++)
                    {
                        var drawDataLoop = listDrawData[indexItem];


                        if (drawDataLoop.CellValue == CellValue.White)
                            SetBackgroundColor(indexX, indexY, Color.White);
                        else if (drawDataLoop.CellValue == CellValue.Black)
                            SetBackgroundColor(indexX, indexY, Color.Black);
                        else
                            SetBackgroundColor(indexX, indexY, Color.Yellow);

                        if (drawDataLoop.AntDirectionValue != null)
                        {
                            if (drawDataLoop.AntDirectionValue == AntDirectionValue.North)
                                SetAnt(indexX, indexY, imgResManager.GetImgSrc(ImgResHelper.ic_ant_up));
                            else if (drawDataLoop.AntDirectionValue == AntDirectionValue.East)
                                SetAnt(indexX, indexY, imgResManager.GetImgSrc(ImgResHelper.ic_ant_right));
                            else if (drawDataLoop.AntDirectionValue == AntDirectionValue.South)
                                SetAnt(indexX, indexY, imgResManager.GetImgSrc(ImgResHelper.ic_ant_down));
                            else if (drawDataLoop.AntDirectionValue == AntDirectionValue.West)
                                SetAnt(indexX, indexY, imgResManager.GetImgSrc(ImgResHelper.ic_ant_left));
                            else
                                SetAnt(indexX, indexY, null);
                        }

                        indexItem++;
                    }
                }
            }

            private void SetBackgroundColor(int indexX, int indexY, Color color)
            {
                var gridChildView = GetChildViewAt(indexX, indexY);
                if (gridChildView == null && color == Color.White)
                    return;

                if (gridChildView == null)
                {
                    gridChildView = new ContentView() { };
                    Add(gridChildView, indexX, indexY);
                }

                gridChildView.BackgroundColor = color;
                gridChildView.Content = null;
            }

            private void SetAnt(int indexX, int indexY, ImageSource imgSrcAnt)
            {
                var gridChildView = GetChildViewAt(indexX, indexY);
                if (gridChildView == null)
                {
                    gridChildView = new ContentView() { };
                    Add(gridChildView, indexX, indexY);
                }

                gridChildView.Content = imgSrcAnt == null ? null : new Image() { Source = imgSrcAnt };
            }

            private void Add(ContentView view, int indexX, int indexY)
            {
                grid.Children.Add(view, indexX, indexY);
                gridChild[indexX, indexY] = view;
            }

            private ContentView GetChildViewAt(int indexX, int indexY)
            {
                return gridChild[indexX, indexY];
            }
        }
    }
}