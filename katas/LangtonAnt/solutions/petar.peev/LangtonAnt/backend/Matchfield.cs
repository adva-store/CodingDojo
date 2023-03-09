using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace LangtonAnt.backend;
/// <summary>
/// Die Langton Ameise Backend
/// das ist die Implementierung der haupt-Komponente des Backends
/// Projekt: Langton's Ant Assessment
/// Autor: Petar Peev i.A. von Oliver Arentzen, advastore SE
/// </summary>
internal class Matchfield : NotificationBase, IMatchfield
{
    #region private members
    private System.Timers.Timer procesTimer;
    private int _edgeLenght;
    private CellAddress _antStartPosition; 
    private Outlook _antCurrentOutlook;
    private Outlook _antStartOutlook;
    private int _movesCount;
    private int _delayTime;
    private int _currentMove;
    private string _currentResult;
    private BitArray _grid;
    private CellAddress _currentAntPosition;
    
    #endregion

    #region initial values
    const int InitialEdgeLenght = 20;
    const Outlook InitialAntStartOutlook = Outlook.East;
    const int InitialMovesCount = 500;
    const int IntialDelayTime = 500;
    #endregion

    /// <summary>
    /// Konstruktor 
    /// Lädt die Standardwerte aus der Initialkonstanten
    /// </summary>
    public Matchfield()
    {
        _edgeLenght = InitialEdgeLenght;
        _antCurrentOutlook = InitialAntStartOutlook;
        _antStartOutlook = InitialAntStartOutlook;
        _movesCount= InitialMovesCount;
        _delayTime = IntialDelayTime;
    }

    #region properties

    /// <summary>
    /// die Kantenlänge des Feldes 
    /// wird initaliziert duch die Konstante InitialEdgeLenght
    /// die Veränderung des Eigenschafts führt zu eine neue Initializierung des Feldes
    /// </summary>
    public int EdgeLenght { get => _edgeLenght; set { if (_edgeLenght != value) SetValueAndRefresh(ref _edgeLenght, value); } }
    /// <summary>
    /// die Startposition der Ameise 
    /// wird initaliziert duch das Helfte der Kantenlänge
    /// </summary>
    public CellAddress AnpStartPosition { get => _antStartPosition; set { if (_antStartPosition != value) SetProperty(ref _antStartPosition, value); } }
    /// <summary>
    /// die Start-Blickrichtung der Ameise 
    /// wird initaliziert duch Konstante InitialAntStartOutlook
    /// die Veränderung des Eigenschafts führt zu eine neue Initializierung des Feldes
    /// </summary>
    public Outlook AntStartOutlook { get => _antStartOutlook; set { if (_antStartOutlook != value) { AntCurrentOutlook = value; SetValueAndRefresh(ref _antStartOutlook, value); } } }
    /// <summary>
    /// die aktelle Blickrichtung der Ameise 
    /// aktualisiert sich nach jedem Zug
    /// </summary>
    public Outlook AntCurrentOutlook { get => _antCurrentOutlook; set { if (_antCurrentOutlook != value) SetProperty(ref _antCurrentOutlook, value); } }
    /// <summary>
    /// die maximale Anzahl der Züge 
    /// wird initaliziert duch Konstante InitialMovesCount
    /// die Veränderung des Eigenschafts führt zu eine neue Initializierung des Feldes
    /// nach dem Erreichen des Wertes werden die Iterationen angehalten
    /// </summary>
    public int MaxMovesCount { get => _movesCount; set { if (_movesCount != value) SetValueAndRefresh(ref _movesCount, value); } }
    /// <summary>
    /// die aktelle Anzahl der Züge
    /// aktualisiert sich nach jedem Zug
    /// </summary>
    public int CurrentMovesCount { get => _currentMove; set { if (_currentMove != value) SetProperty(ref _currentMove, value); } }
    /// <summary>
    ///die Zuggeschwindigkeit in Milisecons 
    /// wird initaliziert duch Konstante IntialDelayTime
    /// die Veränderung des Eigenschafts führt zu eine neue Initializierung des Feldes
    /// </summary>
    public int DelayTime { get => _delayTime; set { if (_delayTime != value) SetValueAndRefresh(ref _delayTime, value); } }
    /// <summary>
    /// die Ausgabe des Backend
    /// aktualisiert sich nach jedem Zug
    /// </summary>
    public string CurrentResult { get => _currentResult; set { SetProperty(ref _currentResult, value); } }
    /// <summary>
    /// die die aktuelle Position der Ameise
    /// aktualisiert sich nach jedem Zug
    /// </summary>
    public CellAddress CurrentAntPosition { get => _currentAntPosition; set { SetProperty(ref _currentAntPosition, value); } }
    /// <summary>
    /// eine Liste alle möglichen Blickrichtungen
    /// </summary>
    public ObservableCollection<string> OutlookItems { get; private set; } = new ObservableCollection<string>(Enum.GetNames(typeof(Outlook)));
    /// <summary>
    /// die aktelle Blickrichtung der Ameise als string
    /// </summary>
    public string AntSelectedStartOutlook { get => Enum.GetName(typeof(Outlook), _antStartOutlook); set { AntStartOutlook = (Outlook)Enum.Parse(typeof(Outlook), value); } }
    /// <summary>
    /// Kalkuliert den aktuellen Grid-Index
    /// </summary>
    private int _currentGridIndex => _currentAntPosition.Row * _edgeLenght + _currentAntPosition.Column;
    #endregion

    /// <summary>
    /// Initializiert das Feld, aktualisiert die Startposition und startet die Iterationen (die Züge)
    /// </summary>
    public void StartIteractions()
    {
        procesTimer?.Stop();
        Task.Delay(TimeSpan.FromSeconds(1));
        CurrentMovesCount = 0;
        CurrentAntPosition = new CellAddress { Column = _edgeLenght / 2, Row = _edgeLenght / 2 };
        AnpStartPosition = new CellAddress { Column = _edgeLenght / 2, Row = _edgeLenght / 2 };
        _grid = new BitArray(_edgeLenght * _edgeLenght);
        StartTimeTicker();
        RaisePropertyChanged(nameof(CurrentResult));
    }
    /// <summary>
    /// Haltet die Züge an
    /// </summary>
    public void StopIteractions()
    {
        procesTimer?.Stop();
    }
    /// <summary>
    /// Speichert die aktuelle Ausgabe in einer text-Datei in der AppDataDirectory
    /// </summary>
    /// <param name="fileName">Der Dateiname</param>
    public async void SaveToFile(string fileName)
    {
        try
        {
            await FileHelper.WriteTextFile(Path.Combine(FileSystem.Current.AppDataDirectory, fileName), CurrentResult);
        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception.Message);
            // ToDo: Error handling like LocalNotificationCenter.Current.Show(request);
            //throw new IOException(exception.Message);
        }
    }
    /// <summary>
    /// Lädt die gespeicherte Ausgabe aus einer text-Datei aus der AppDataDirectory
    /// </summary>
    /// <param name="fileName">Der Dateiname</param>
    public async void LoadFromFile(string fileName)
    {
        try
        {
            var filePath = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);
            if (FileHelper.FileExists(filePath))
                CurrentResult = await FileHelper.ReadTextFile(filePath);
        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception.Message);
            // ToDo: Error handling like LocalNotificationCenter.Current.Show(request);
            //throw new IOException(exception.Message);
        }
    }

    #region private methods
    /// <summary>
    /// Setzt das Vert eines Eigenschafts und initiliziert des Feldes neu
    /// </summary>
    /// <typeparam name="T">der Typ der Eigenschafts</typeparam>
    /// <param name="field">die Eigenschaft</param>
    /// <param name="value">das Wert zum Speichern</param>
    private void SetValueAndRefresh<T>(ref T field, T value)
    {
        SetProperty<T>(ref field, value);
        StartIteractions();
    }
    /// <summary>
    /// Initialisiert der Timer und startet die Züge
    /// </summary>
    private void StartTimeTicker()
    {
        if (procesTimer != null) 
            procesTimer.Elapsed -= ProcesTimer_Elapsed;

        procesTimer = new System.Timers.Timer(_delayTime);
        procesTimer.Elapsed += ProcesTimer_Elapsed;
        procesTimer.AutoReset = true;
        procesTimer.Enabled = true;
    }
    /// <summary>
    /// Time-Ticks handler
    /// </summary>
    /// <param name="sender">die Teimer-Komponente</param>
    /// <param name="e">Time-Tick Argumente</param>
    private void ProcesTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        Debug.WriteLine(DateTime.Now.ToLongTimeString());
        moveNext();
        if (_currentMove >= _movesCount) procesTimer.Stop();
    }
    /// <summary>
    /// Macht den neuen Zug:
    /// - Ermitelt die neue Blickrichtung 
    /// - Ändert die Farbe der aktuellen Zelle
    /// - Erhöht die Anzahl der Züge
    /// - Setzt die neue Position der Ameise
    /// - Aktualisiert die Ausgabe
    /// </summary>
    private void moveNext()
    {
        CurrentMovesCount++;
        if(_currentGridIndex >= 0 && _currentGridIndex < _grid.Length)
        {
            // Farbe ändern
            var currentCell = _grid[_currentGridIndex];
            _grid[_currentGridIndex] = !currentCell;
            // Position ermitteln
            switch (_antCurrentOutlook)
            {
                case Outlook.North:
                    AntCurrentOutlook = currentCell ? Outlook.West : Outlook.East;
                    _currentAntPosition.Column += currentCell ? -1 : 1;
                    break;
                case Outlook.East:
                    AntCurrentOutlook = currentCell ? Outlook.North : Outlook.South;
                    _currentAntPosition.Row += currentCell ? -1 : 1;
                    break;
                case Outlook.South:
                    AntCurrentOutlook = currentCell ? Outlook.East : Outlook.West;
                    _currentAntPosition.Column += currentCell ? 1 : -1;
                    break;
                case Outlook.West:
                    AntCurrentOutlook = currentCell ? Outlook.South : Outlook.North;
                    _currentAntPosition.Row += currentCell ? 1 : -1;
                    break;
                default:
                    break;
            }
            // Schutz für die Grenz-Fälle
            _currentAntPosition.Row = Math.Max(0, _currentAntPosition.Row);
            _currentAntPosition.Column = Math.Max(0, _currentAntPosition.Column);
            // Ausgabe aktualisieren
            var toString = string.Join(",", _grid.Cast<bool>().Select(b => b ? "s" : "w"));
            CurrentResult = toString.Insert(_currentGridIndex * 2 + 1, GetAntOutlookAsChar());
        }
    }
    /// <summary>
    /// Hilfsmethode. Konwertiert die aktuelle Blickrichtung in Char (n, o, s, w) DE-Lokalization
    /// </summary>
    /// <returns>Blickrichtung als Char</returns>
    private string GetAntOutlookAsChar()
    {
        return Enum.GetName(typeof(Outlook), _antCurrentOutlook).ToLower().Replace("east", "ost").FirstOrDefault().ToString();
    }
    #endregion
}
