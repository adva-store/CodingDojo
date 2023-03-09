using LangtonAnt.backend;

namespace LangtonAnt.ViewModels;
/// <summary>
/// MVVM-ViewModel für das Dasbord
/// </summary>
internal class DashboardViewModel : BaseViewModel
{
    /// <summary>
    /// Initialisiert die MVVM-Commands und das LangtonAnt-Feld
    /// </summary>
    public void Initialize()
    {
        Title = "LangtonAnt Dashboard"; 
        Data = new Matchfield();
        LoadDataCommand = new Command(() => Data.StartIteractions());
        SaveAsFileCommand = new Command(() => { Data.StopIteractions(); Data.SaveToFile("LangtonAnt.cache.txt"); });
        LoadFromFileCommand = new Command(() => { Data.StopIteractions(); Data.LoadFromFile("LangtonAnt.cache.txt"); });
    }
    public IMatchfield Data { get; private set; }
    public Command SaveAsFileCommand { get; private set; }
    public Command LoadFromFileCommand { get; private set; }

}
