using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrangeChess;

public partial class MainPageViewModel: ObservableObject
{
    [ObservableProperty]
    string rawColumnWidths = "3, 1, 2, 7, 1";

    [ObservableProperty]
    string rawRowHeights = "1, 8, 4, 5, 2";


    [RelayCommand]
    async Task Calculate()
    {
        var columnWidths = RawColumnWidths.Split(",").Select(x => int.Parse(x)).ToList();
        var rowHeights = RawRowHeights.Split(",").Select(x => int.Parse(x)).ToList();
        if (columnWidths.Count != rowHeights.Count || columnWidths.Count ==0) {
            await Shell.Current.DisplayAlert("Fehler", "Ungültige Eingabe: Beide Listen müssen mindestens ein Element enthalten und gleich lang sein", "OK");
            return;
        }
        decimal blackArea = 0;
        decimal whiteArea = 0;
        for (int column = 0; column < columnWidths.Count; column++)
        {
            for (int row = 0;  row < rowHeights.Count; row++)
            {
                if ((column+row) % 2 == 0)
                {
                    whiteArea += columnWidths[column] * rowHeights[row];
                }
                else
                {
                    blackArea += columnWidths[column] * rowHeights[row];
                }
            }
        }

        await Shell.Current.DisplayAlert("Ergebnis", $"{blackArea} Schwarze Fläche\n{whiteArea} Weiße Fläche", "OK");
    }
}
