namespace LangtonAntKataApp.Pages;

[QueryProperty(nameof(MoveSpeed), "movespeed")]
public partial class LangtonAntsFieldPage : ContentPage
{ 
    private const int fieldLength = 11;
    int moveSpeed;
    public int MoveSpeed
    {
        get => moveSpeed;
        set
        {
            moveSpeed = value;
            
        }
    }

    public LangtonAntsFieldPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (moveSpeed > 0)
        {
            Task.Run(_FieldMovment);
        }
    }

    private void _FieldMovment()
    {
        try
        {
            var antTravelMoves = LangtonAntAlgorythmus.GetAuntsTravelling(fieldLength, new AntCoordinates(5,5), AntDirections.North, 30);
            foreach (var antTravelMove in antTravelMoves)
            {
                var actualGrid = _DrawGrid(antTravelMove);
                Device.BeginInvokeOnMainThread(() =>
                {
                    fieldBorder.Content = actualGrid;
                });
                Thread.Sleep(MoveSpeed * 1000);
            }
        }
        catch (Exception xx)
        {
            ;
        }
        
    }

    private Grid _DrawGrid(string field)
    {
        Grid drawedGrid = new Grid();
        string[] splittedField = field.Split(',');
        int definedColumns = 0;
        for (int i = 0, actualRow = 0; actualRow < fieldLength && i < splittedField.Length; actualRow++)
        {

            drawedGrid.AddRowDefinition(new RowDefinition());
            for (int j = 0; j < fieldLength; j++, i++)
            {
                if (i >= splittedField.Length || string.IsNullOrEmpty(splittedField[i]))
                    break;

                if (definedColumns < fieldLength)
                {
                    drawedGrid.AddColumnDefinition(new ColumnDefinition());
                    definedColumns++;
                }
                string strColor = splittedField[i];
                string strDirection = string.Empty;
                if (strColor.Length > 1)
                {
                    strDirection = strColor.Substring(0,1);
                    strColor = strColor.Substring(1);
                }
                drawedGrid.Add(_GetBox(strColor), j, actualRow);
                if (!string.IsNullOrEmpty(strDirection))
                {
                    drawedGrid.Add(_GetAnt(strDirection), j, actualRow);
                }
            }
        }
        return drawedGrid;
    }

    private Border _GetBox(string strColor)
    {
        Color boxColor = Colors.White;

        if (strColor == "s")
            boxColor = Colors.Black;

        return new Border
        {
            Stroke = new SolidColorBrush(Colors.Black),
            StrokeThickness = 1,
            Content = new BoxView
            {
                Color = boxColor,
                HeightRequest = 30,
                WidthRequest = 30,
            }
        };
    }

    private Image _GetAnt(string strDirection)
    {
        AntDirections direction = LangtonAntAlgorythmus.GetDirectionFromString(strDirection);
        double rotationDegree = 0;
        switch (direction)
        {
            case AntDirections.East:
                rotationDegree = 90; 
                break;
            case AntDirections.South:
                rotationDegree = 180;
                break;
            case AntDirections.West:
                rotationDegree = 270;
                break;
        }
        return new Image { HeightRequest = 20, WidthRequest = 20, Source = "ant.svg" , Rotation = rotationDegree };
    }
}