
using System.Diagnostics;

namespace StrangeChessboard.StrangeChessboard;
/// <summary>
/// Das seltsame Schachbrett Implementationsklasse
/// Projekt: Langton's Ant Assessment
/// Autor: Petar Peev i.A. von Oliver Arentzen, advastore SE
/// </summary>
internal class Solution
{
    #region private members
    private const int _maxRange = 3000;
    private int[] _columnItems, _rowItems;
    #endregion

    /// <summary>
    /// Initializiert neue Reihenhöhen und Spaltenbreiten für das Schachbrett
    /// </summary>
    /// <param name="rangeSize">Spalten/Reihen- Anzahl des Tests</param>
    /// <exception cref="ArgumentOutOfRangeException">Es wird ein OutOfRange generiert falls die Spaltenanzahl größer als 3000 ins</exception>
    public void Initialize(int rangeSize)
    {
        if (rangeSize <= 0 || rangeSize > _maxRange) throw new ArgumentOutOfRangeException(nameof(rangeSize));
        _columnItems = new int[rangeSize];
        _rowItems = new int[rangeSize];
        var random = new Random();
        for (int i = 0; i < _columnItems.Length; i++) { _columnItems[i] = random.Next(1, 10); _rowItems[i] = random.Next(1, 10); }
    }
    /// <summary>
    /// Initializiert das Beispiel aus der Aufgabe-Seite zur Kontrolle
    /// </summary>
    public void Initialize()
    {
        _columnItems = new int[] { 3, 1, 2, 7, 1 };
        _rowItems = new int[] { 1, 8, 4, 5, 2 };
    }
    /// <summary>
    /// Kalkuliert die weiße und die schwarze Flächen
    /// </summary>
    /// <returns>Kalkulationsresult als Tupel</returns>
    public (int,int)  Calculate()
    {
        var whiteArea = AccumulateMultiple(_columnItems.GetRegularItems(), _rowItems.GetRegularItems()) +
                        AccumulateMultiple(_columnItems.GetOddItems(), _rowItems.GetOddItems());

        var blackArea = AccumulateMultiple(_columnItems.GetRegularItems(), _rowItems.GetOddItems()) +
                        AccumulateMultiple(_columnItems.GetOddItems(), _rowItems.GetRegularItems());

        // Denbug-Check ToDo: Löschen
        Debug.Assert(whiteArea + blackArea == _columnItems.ToArray().Sum() * _rowItems.ToArray().Sum(), "Calculation is Ok");

        return (whiteArea, blackArea);
    }
    public int[] ColumnItems => _columnItems;
    public int[] RowItems => _rowItems;
    /// <summary>
    /// Kalkuliert die Fläche aus der eingegebenen Breiten/Höhen (volle Schleife)
    /// </summary>
    /// <param name="columns"></param>
    /// <param name="rows"></param>
    /// <returns></returns>
    private int AccumulateMultiple(IEnumerable<int> columns, IEnumerable<int> rows)
    {
        var sum = 0;
        foreach (var column in columns)
            foreach (var row in rows)
                sum += column * row;
        return sum;
    }
}
