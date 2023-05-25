using StrangeChessboard;

namespace MauiPlaygroundApp;

public partial class ChessboardPage : ContentPage, IQueryAttributable
{
	public ChessboardPage()
	{
		InitializeComponent();
	}

	StrangeChessboardCalc _calc;

	public void ApplyQueryAttributes(IDictionary<string, object> query)
	{
		_calc = query["calc"] as StrangeChessboardCalc;

		// Create column and row definitions using unit type "Star".
		// The column sizes and row sizes how many times "star" we want the column
		// to be wide repsectively, the row to be high.
		var colDefs = _calc.ColSizes
			.Select(cs => new ColumnDefinition
			{
				Width = new GridLength(cs, GridUnitType.Star)
			})
			.ToArray();

		var rowDefs = _calc.RowSizes
			.Select(rs => new RowDefinition
			{
				Height = new GridLength(rs, GridUnitType.Star)
			})
			.ToArray();

		ChessboardLayout.ColumnDefinitions = new ColumnDefinitionCollection(colDefs);
		ChessboardLayout.RowDefinitions = new RowDefinitionCollection(rowDefs);

		// Add some box views to visualize the board.
		for (int row = 0; row < _calc.NumRows; row++)
		{
			for (int col = 0; col < _calc.NumColumns; col++)
			{
				var box = new BoxView
				{
					Color = _calc.IsWhite(col, row) ? Colors.LightGray : Colors.DarkGray
				};
				ChessboardLayout.Add(box, col, row);
			}
		}
	}
}
