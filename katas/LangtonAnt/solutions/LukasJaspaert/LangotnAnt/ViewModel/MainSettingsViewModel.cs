using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LangotnAnt.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangotnAnt.ViewModel;

public partial class MainSettingsViewModel:BaseViewModel
{
    [ObservableProperty]
    int gridLength = 10;

    [ObservableProperty]
    int startX = 5;

    [ObservableProperty]
    int startY = 5;

    [ObservableProperty]
    Direction gazeDirection = Direction.n;

    [ObservableProperty]
    Array allDirections = Enum.GetValues(typeof(Direction));

    [ObservableProperty]
    int endTurn = 3;

    [ObservableProperty]
    int animationSpeed = 1;

    [RelayCommand]
    async Task StartLangtonsAnt()
    {
        // create ant
        Ant ant = new Ant()
        {
            GazeDirection = GazeDirection,
            Position = new Model.Point(StartX,StartY),
            RuleSet = new Dictionary<Color, Rule>()
            {
                {
                    Colors.White,
                    new Rule(Colors.Black,turnRight: true)
                },
                {
                    Colors.Black,
                    new Rule(Colors.White,turnLeft: true)
                },

            }
        };
        // create field
        ObservableCollection<AntField> antFields = new();
        ColumnDefinitionCollection columnDefinitions = new();
        RowDefinitionCollection rowDefinitions = new();
        for(int x = 0; x < GridLength; x++)
        {
            columnDefinitions.Add(new ColumnDefinition());
            rowDefinitions.Add(new RowDefinition());
            for (int y = 0; y < GridLength; y++)
            {
                antFields.Add(new AntField(new Model.Point(x, y), Colors.White) { HasAnt=ant.Position.X==x && ant.Position.Y==y });
            }
        }

        var parameters = new Dictionary<string, object>() {
            {"Fields", antFields },
            {"LangtonsAnt", ant },
            {"EndTurn",EndTurn },
            {"Columns",columnDefinitions},
            {"Rows",rowDefinitions},

        };
        // navigate
        await Shell.Current.GoToAsync("/AntField",parameters);
    }
}
