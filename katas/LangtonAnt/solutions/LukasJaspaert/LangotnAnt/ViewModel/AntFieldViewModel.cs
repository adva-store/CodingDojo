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

[
    QueryProperty(nameof(Fields), nameof(Fields)),
    QueryProperty(nameof(LangtonsAnt), nameof(LangtonsAnt)),
    QueryProperty(nameof(EndTurn), nameof(EndTurn)),
    QueryProperty(nameof(Columns), nameof(Columns)),
    QueryProperty(nameof(Rows), nameof(Rows)),
]
public partial class AntFieldViewModel : BaseViewModel
{
    [ObservableProperty]
    ObservableCollection<AntField> fields;

    [ObservableProperty]
    int tickDelay = 500;

    [ObservableProperty]
    Ant langtonsAnt;

    [ObservableProperty]
    ColumnDefinitionCollection columns = new();
    [ObservableProperty]
    RowDefinitionCollection rows = new();

    [ObservableProperty]
    int currentTurn = 0;

    [ObservableProperty]
    int endTurn = 10;

    [ObservableProperty]
    bool isNotStarted = true;

    [RelayCommand]
    async Task ExecuteTurn()
    {
        IsNotStarted = false;
        while (CurrentTurn < EndTurn)
        {

            CurrentTurn++;
            var currentField = Fields.FirstOrDefault(field => field.Position.Equals(LangtonsAnt.Position));
            if (currentField == null)
            {
                await Shell.Current.DisplayAlert("Flucht", "Die Ameise hat das Feld verlassen", "Stop");
                return;
            }
            var activeRule = LangtonsAnt.RuleSet[currentField.FieldColor];
            currentField.FieldColor = activeRule.NewColor;
            if (activeRule.TurnLeft)
            {
                LangtonsAnt.GazeDirection = LangtonsAnt.GazeDirection.TurnLeft();
            }
            if (activeRule.TurnRight)
            {
                LangtonsAnt.GazeDirection = LangtonsAnt.GazeDirection.TurnRight();
            }
            LangtonsAnt.Position.Move(LangtonsAnt.GazeDirection);
            currentField.HasAnt = false;
            var newField = Fields.FirstOrDefault(field => field.Position.Equals(LangtonsAnt.Position));
            if (newField != null)
            {
                newField.HasAnt = true;
                Fields.Add(newField);
                Fields.Remove(newField);
            }
            Fields.Add(currentField);
            Fields.Remove(currentField);
            await Task.Delay(TickDelay);
        }
    }


}
