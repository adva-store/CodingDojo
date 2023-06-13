using System.ComponentModel;
using PropertyChanged;
using StrangeChessboard;

namespace MauiPlaygroundApp;

[AddINotifyPropertyChangedInterface]
public partial class ChessboardConfigPage : ContentPage, INotifyPropertyChanged
{
	public ChessboardConfigPage()
	{
		InitializeComponent();
		BindingContext = this;
		UpdateCalc();
	}

	StrangeChessboardCalc _calc;

	/// <summary>
	/// Total (calculated) white area.
	/// </summary>
	public long TotalWhiteArea { get; set; }

	/// <summary>
	/// Total (calculated) black area.
	/// </summary>
	public long TotalBlackArea { get; set; }

	/// <summary>
	/// Error message in case something goes wrong.
	/// </summary>
	public string ErrorMessage { get; set; }

	/// <summary>
	/// Column definitions entered by user.
	/// </summary>
	public string ColumnSizes { get; set; } = "3, 1, 2, 7, 1";

	/// <summary>
	/// Row definitions entered by user.
	/// </summary>
	public string RowSizes { get; set; } = "1, 8, 4, 5, 2";

	public void OnColumnSizesChanged()
	{
		UpdateCalc();
	}

	public void OnRowSizesChanged()
	{
		UpdateCalc();
	}

	/// <summary>
	/// Helper method to update the UI using the chessboard calculator model.
	/// </summary>
	void UpdateCalc()
	{
		ErrorMessage = "";
		try
		{
			// Parse the input fields which are expected to be in format "1, 2, 3, 2".
			var colValues = ColumnSizes.Split(",", StringSplitOptions.RemoveEmptyEntries);
			var rowValues = RowSizes.Split(",", StringSplitOptions.RemoveEmptyEntries);

			long[] colSizes = colValues.Select(v => Convert.ToInt64(v)).ToArray();
			long[] rowSizes = rowValues.Select(v => Convert.ToInt64(v)).ToArray();

			_calc = new StrangeChessboardCalc(colSizes, rowSizes);
			(long whiteArea, long blackArea) = _calc.CalculateTotalAreaSize();

			TotalWhiteArea = whiteArea;
			TotalBlackArea = blackArea;
		}
		catch (Exception ex)
		{
			ErrorMessage = ex.Message;
		}
	}

	/// <summary>
	/// Navigate to the page visualizing the chessboard.
	/// </summary>
	async void OnViewButtonClicked(System.Object sender, System.EventArgs e)
	{
		await Shell.Current.GoToAsync("chessboard", new Dictionary<string, object>
		{
			{ "calc", _calc }
		});
	}
}
