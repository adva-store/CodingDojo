using System.ComponentModel;

namespace LangtonAnt.backend;
/// <summary>
/// Die Langton Ameise Backend
/// das Interface der haupt-Komponente des Backends
/// Realisiert die Entkoppelung der Komponenten
/// Projekt: Langton's Ant Assessment
/// Autor: Petar Peev i.A. von Oliver Arentzen, advastore SE
/// </summary>
internal interface IMatchfield
{
    int EdgeLenght { get; set; }
    CellAddress AnpStartPosition { get; set; }
    CellAddress CurrentAntPosition { get; set; }
    Outlook AntCurrentOutlook { get; set; }
    int MaxMovesCount { get; set; }
    int CurrentMovesCount { get; set; }
    int DelayTime { get; set; }
    string CurrentResult { get; }
    void StartIteractions();
    void StopIteractions();
    void SaveToFile(string fileName);
    void LoadFromFile(string fileName);

    event PropertyChangedEventHandler PropertyChanged;
}
