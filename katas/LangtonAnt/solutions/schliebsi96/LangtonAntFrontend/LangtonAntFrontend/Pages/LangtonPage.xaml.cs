using LangtonAntFrontend.Classes;
using Newtonsoft.Json;

namespace LangtonAntFrontend.Pages;

public partial class LangtonPage : ContentPage
{
    InputFromJson _data = new InputFromJson();
    private int _duration;
    private Grid _myGrid = new Grid();
    private int _rowNum;
    private int _columnNum;
    List<List<LangtonTile>> _map = new List<List<LangtonTile>>();

    Microsoft.Maui.Controls.Image _ant = new Microsoft.Maui.Controls.Image
    {
        WidthRequest = 20,
        HeightRequest = 20,
        Source = ImageSource.FromFile("ant.png"),
        Margin = new Thickness(1, 1, 1, 1)
    };

    public LangtonPage(string json, string duration)
    {
        InitializeComponent();

        _data = JsonConvert.DeserializeObject<InputFromJson>(json);
        _duration = int.Parse(duration);

        CreateInitialMap();
    }
    
    public async void ButtonClicked(object sender, EventArgs e)
    {
        Btn.IsEnabled = false;

        Scroller.Content = _myGrid;
        await Task.Delay(_duration);

        for (int t = 0; t < _data.TilesToChange.Count; t++)
        {          
            _myGrid.Children.Clear();

            for (int i = 0; i < _rowNum; i++)
            {
                for (int j = 0; j < _columnNum; j++)
                {
                    Label label = new Label
                    {
                        WidthRequest = 20,
                        HeightRequest = 20,  
                        Text = "_",                  
                        Margin = new Thickness(1,1,1,1),
                        
                    };
                    if (_map[i][j].IsBlack)
                    {
                        label.Text = "#";
                    }
                    if (_data.TilesToChange[t].PosX == i && _data.TilesToChange[t].PosY == j)
                    {
                        label.Text = _data.TilesToChange[t].IsBlack ? "#" : "_";
                        _map[_data.TilesToChange[t].PosX][_data.TilesToChange[t].PosY] 
                            = new LangtonTile(_data.TilesToChange[t].PosX, _data.TilesToChange[t].PosY, _data.TilesToChange[t].IsBlack);
                    }
                    _myGrid.Add(label,j,i);
                }
            }

            switch (_data.AntPosistions[t+1].LookDirection)
            {
                case Enums.Direction.North:
                    _ant.Rotation = 0;
                    break;
                case Enums.Direction.South:
                    _ant.Rotation = 180;
                    break;
                case Enums.Direction.West:
                    _ant.Rotation = 270;
                    break;
                case Enums.Direction.East:
                    _ant.Rotation = 90;
                    break;
            }
            _myGrid.Add(_ant, _data.AntPosistions[t+1].X, _data.AntPosistions[t+1].Y);

            Scroller.Content = _myGrid;
            await Task.Delay(_duration);
        }
    }

    public void CreateInitialMap()
    {
        RowDefinitionCollection rowDefinitions = new RowDefinitionCollection();
        ColumnDefinitionCollection columnDefinitions = new ColumnDefinitionCollection();

        _rowNum = _data.Width;
        _columnNum = _data.Width;
        for (int i = 0; i < _rowNum; i++)
        {
            rowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
        }
        for (int i = 0; i < _columnNum; i++)
        {
            columnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
        }
        _myGrid.RowDefinitions = rowDefinitions;

        _myGrid.ColumnDefinitions = columnDefinitions;

        _map = new List<List<LangtonTile>>();
        for (int x = 0; x < _data.Width; x++)
        {
            var rowList = new List<LangtonTile>();
            for (int y = 0; y < _data.Width; y++)
            {

                rowList.Add(new LangtonTile(x, y));
            }
            _map.Add(rowList);
        }
        BuildGrid();
        switch (_data.AntPosistions[0].LookDirection)
        {
            case Enums.Direction.North:
                _ant.Rotation = 0;
                break;
            case Enums.Direction.South:
                _ant.Rotation = 180;
                break;
            case Enums.Direction.West:
                _ant.Rotation = 270;
                break;
            case Enums.Direction.East:
                _ant.Rotation = 90;
                break;
        }
        _myGrid.Add(_ant, _data.AntPosistions[0].X, _data.AntPosistions[0].Y);
        Scroller.Content = _myGrid;
    }

    public void BuildGrid()
    {
        for (int i = 0; i < _rowNum; i++)
        {
            for (int j = 0; j < _columnNum; j++)
            {
                Label label = new Label
                {
                    WidthRequest = 20,
                    HeightRequest = 20,
                    Text = "_",
                    Margin = new Thickness(1, 1, 1, 1),

                };
                _myGrid.Add(label, j, i);
            }
        }
    }
}