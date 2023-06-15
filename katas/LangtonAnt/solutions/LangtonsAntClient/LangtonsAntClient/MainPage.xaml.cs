using LangtonsAntClient.DataService;
using LangtonsAntClient.Models;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using System.Data.Common;

namespace LangtonsAntClient;

public partial class MainPage : ContentPage
{
    private RestDataService _restDataService;
    private LangtonsAnt _langtonsAnt;
    private int _fieldSize;
    private const int _gridSize = 500;
    private const int _startFieldSize = 15;
    IDispatcherTimer _timer;

    public MainPage()
    {
        InitializeComponent();
        Loaded += MainPage_Loaded;

    }

    #region Methods
    private void Initialize()
    {
        _restDataService = new RestDataService();
        _langtonsAnt = new LangtonsAnt();
        _fieldSize = _startFieldSize;
        SizeSlider.Value = _fieldSize;
        _timer = Application.Current.Dispatcher.CreateTimer();
        _timer.Tick += (s, e) => TimerTick();
    }

    void TimerTick()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            _langtonsAnt = await _restDataService.GetNextStepBackend();
            RefreshPlayField();
        });
    }

    private void InitializePlayField()
    {
        //clear grid
        PlayFieldGrid.RowDefinitions.Clear();
        PlayFieldGrid.ColumnDefinitions.Clear();
        PlayFieldGrid.Children.Clear();

        //calculate cell size
        int length = _gridSize / _fieldSize;

        //set Column and RowDefinitions
        for (int i = 0; i < _fieldSize; i++)
        {
            ColumnDefinition column = new ColumnDefinition();
            column.Width = length;
            PlayFieldGrid.ColumnDefinitions.Add(column);
        }
        for (int i = 0; i < _fieldSize; i++)
        {
            RowDefinition row = new RowDefinition();
            row.Height = length;
            PlayFieldGrid.RowDefinitions.Add(row);
        }

        //create objects for all cells
        for (int y = 0; y < _fieldSize; y++)
        {
            for (int x = 0; x < _fieldSize; x++)
            {
                Label label = new Label()
                {
                    BackgroundColor = Colors.White,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,  
                };
                Grid.SetRow(label, y);
                Grid.SetColumn(label, x);
                PlayFieldGrid.Children.Add(label);
            }
        }
    }
    private void RefreshPlayField()
    {
        if (!string.IsNullOrEmpty(_langtonsAnt.ErrMessage))
        {
            DisplayAlert("Achtung!", _langtonsAnt.ErrMessage, "OK");
            TimerStop();
            return;
        }
        var fieldString = _langtonsAnt.ResultText.Split(',');
        if (PlayFieldGrid.Children.Count != fieldString.Length - 1)
            return;
        for (int i = 0; i < PlayFieldGrid.Children.Count; i++)
        {
            var box = PlayFieldGrid.Children[i] as Label;
            box.Text = String.Empty;
            string tile = fieldString[i];

            //draw ant
            if (tile.Length == 2)//if length == 2 -> cell with ant
            {
                switch (tile[0])
                {
                    case 'n':
                        box.Text = "↑";
                        break;
                    case 'o':
                        box.Text = "→";
                        break;
                    case 's':
                        box.Text = "↓";
                        break;
                    case 'w':
                        box.Text = "←"; ;
                        break;

                }
                //Just use second character
                tile = tile[1..];
            }

            //set cell color
            switch (tile)
            {
                case "w":
                    box.BackgroundColor = Colors.White;
                    break;
                case "s":
                    box.BackgroundColor = Colors.DarkGray;
                    break;

            }
        }
    }

    private void TimerStart()
    {

        TimerButton.Text = "Timer stoppen";
        _timer.Interval = TimeSpan.FromMilliseconds(1000 - SpeedSlider.Value);
        _timer.Start();
    }

    private void TimerStop()
    {
        TimerButton.Text = "Timer starten";
        _timer.Stop();
    }

    #endregion Methods

    #region Events
    private void MainPage_Loaded(object sender, EventArgs e)
    {
        Initialize();
    }

    private void SizeSlider_ValueChanged(object sender, ValueChangedEventArgs args)
    {
        _fieldSize = (int)args.NewValue;
        InitializePlayField();
    }

    async private void InitializeButton_Clicked(object sender, EventArgs e)
    {
        //Todo: instead of exception catching, validate entries
        try
        {
            _langtonsAnt = new LangtonsAnt
            {
                EdgeLength = (int)SizeSlider.Value,
                NumberOfSteps = Convert.ToInt32(NumberOfStepsEntry.Text),
                StartX = Convert.ToInt32(StartXEntry.Text),
                StartY = Convert.ToInt32(StartYEntry.Text),
                StartDirection = DirectionPicker.SelectedItem.ToString().Substring(0, 1).ToLower(),//ToDo: Use Dictionary instead
            };
        }
        catch (Exception)
        {
            await DisplayAlert("Start nicht möglich!", "Bitte korrigiere deine Eingaben.", "OK");
            return;
        }
        _langtonsAnt = await _restDataService.InitializeLangtonsAntBackend(_langtonsAnt);
        RefreshPlayField();
    }

    async private void NextStepButton_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_langtonsAnt.ResultText))
            return;
        _langtonsAnt = await _restDataService.GetNextStepBackend();
        RefreshPlayField();
    }

    private void TimerButton_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_langtonsAnt.ResultText))
            return;
        if (_timer.IsRunning)
        {
            TimerStop();
        }
        else
        {
            TimerStart();
        }

    }

    #endregion Events



}

