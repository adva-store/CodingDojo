using System.ComponentModel;
using System.Diagnostics;
using AntLogic;
using PropertyChanged;

namespace MauiPlaygroundApp;

[AddINotifyPropertyChangedInterface]
public partial class AntPage : ContentPage, IQueryAttributable, INotifyPropertyChanged
{
	Ant _ant;
	int _dimension;
	int _moveSpeed;

	public AntPage()
	{
		InitializeComponent();
		BindingContext = this;
	}

	readonly Image _antImage = new()
	{
		Source = "ant.png",
		Aspect = Aspect.AspectFit
	};

	readonly Grid _antImageContainer = new();

	CancellationTokenSource _cancelSource = new CancellationTokenSource();

	protected async override void OnAppearing()
	{
		base.OnAppearing();

		// Subscribe to an event so we can resize the ant image if the grid changed its size.
		// By the way: Unfortunately, there's a (known) issue with the GridLayout not resizing on Mac Catalyst
		// when the window size changes. Workaround would be to customize grid and do our own
		// measurements.
		AntGridLayout.SizeChanged += AntGridLayout_SizeChanged;

		// Let the ant run while there are steps left or user navigates away.
		do
		{
			_antString = _ant.MoveSingleStep();
			await Task.Yield();
			UpdateView();
			await Task.Delay(_moveSpeed);
		}
		while (!_cancelSource.IsCancellationRequested && !string.IsNullOrWhiteSpace(_antString));

		Console.WriteLine("Exiting OnAppearing()");
	}

	string[] _previousGridData = new string[0];

	/// <summary>
	/// Helper to update the visuals.
	/// This places the ant at the correct position and updates the grid colors.
	/// </summary>
	void UpdateView()
	{
		// Use the string representation as the input, pretending we don't have direct access
		// to the ant and grid model.
		var gridData = _antString.Split(',', StringSplitOptions.RemoveEmptyEntries);

		for (int index = 0; index < gridData.Length; index++)
		{
			int col = index % _dimension;
			int row = index / _dimension;

			var cellData = gridData[index];

			// Optimize UI updates: skip if cell content has not changed.
			if (_previousGridData.Length == gridData.Length && _previousGridData[index] == cellData)
			{
				continue;
			}

			var cellColor = ' ';
			if (cellData.Length == 1)
			{
				// Cell without ant on it.
				cellColor = cellData[0];
			}
			else
			{
				// Cell with ant on it, the cell color is prefixed with the ant's direction.
				var facingDirection = cellData[0];
				cellColor = cellData[1];

				AntGridLayout.SetColumn(_antImageContainer, col);
				AntGridLayout.SetRow(_antImageContainer, row);

				_antImage.Rotation = facingDirection switch
				{
					'n' => 0,
					's' => 180,
					'w' => 270,
					'e' => 90,
					_ => 0
				};
			}

			// Update the box views in the grid layout to reflect the state of
			// the grid model.
			var boxView = AntGridLayout.Children.FirstOrDefault(
					view =>
						AntGridLayout.GetRow(view) == row
						&& AntGridLayout.GetColumn(view) == col
					) as BoxView;

			boxView.Color = cellColor == 's' ? Colors.DarkGray : Colors.White;
		}

		Status = $"Steps left: {_ant.StepsLeft}; Ant facing: {_ant.Direction}";
		_previousGridData = gridData;
	}

	protected override void OnDisappearing()
	{
		_cancelSource.Cancel();

		// Don't forget to unsubscribe, otherwise we'd introduce a memory leak.
		AntGridLayout.SizeChanged -= AntGridLayout_SizeChanged;

		base.OnDisappearing();
	}

	public void ApplyQueryAttributes(IDictionary<string, object> query)
	{
		try
		{
			_ant = query["ant"] as Ant;
			_dimension = Convert.ToInt32(query["dimension"]);
			_moveSpeed = Convert.ToInt32(query["moveSpeed"]);
		}
		catch (Exception ex)
		{
			// A bit anoying. If anything goes wrong while this method is being executed, the exception is silently
			// swallowed. The target page will not appear and it is unclear what went wrong.
			// Cost me endless hours of debugging.
			Console.WriteLine("Error getting query attributes: " + ex.Message);
			throw;
		}

		// Create a grid layout based on the ant's grid model.
		var colDefs = Enumerable
			.Repeat(new ColumnDefinition
			{
				Width = GridLength.Star
			}, _dimension)
			.ToArray();

		var rowDefs = Enumerable
			.Repeat(new RowDefinition
			{
				Height = GridLength.Star
			}, _dimension)
			.ToArray();

		AntGridLayout.ColumnDefinitions = new ColumnDefinitionCollection(colDefs);
		AntGridLayout.RowDefinitions = new RowDefinitionCollection(rowDefs);

		for (int row = 0; row < _dimension; row++)
		{
			for (int col = 0; col < _dimension; col++)
			{
				var box = new BoxView
				{
					Color = Colors.Gray
				};
				AntGridLayout.Add(box, col, row);
			}
		}

		// When putting an image into a grid layout cell, the image's width and height
		// requests are ignored by MAUI. What helps is wrapping the image in a container
		// (a simple 1x1 grid layout) which can then be resized just fine.
		_antImageContainer.Add(_antImage, 0, 0);
		AntGridLayout.Add(_antImageContainer, 0, 0);

		// Bind the dimensions of the ant image to the sizes we caculate in reaction to the grid
		// layout size changing. This is probably only useful on MacCatalyst where the window can be
		// resized.
		_antImageContainer.SetBinding(Image.WidthRequestProperty, new Binding("AntWidth"));
		_antImageContainer.SetBinding(Image.HeightRequestProperty, new Binding("AntHeight"));
	}

	private void AntGridLayout_SizeChanged(object sender, EventArgs e)
	{
		OnPropertyChanged(nameof(AntWidth));
		OnPropertyChanged(nameof(AntHeight));
	}

	/// <summary>
	/// Returns the ant's width depending on the current grid layout size.
	/// </summary>
	public double AntWidth => Width / AntGridLayout.ColumnDefinitions.Count * 0.75;

	/// <summary>
	/// Returns the ant's height depending on the current grid layout size.
	/// </summary>
	public double AntHeight => Height / AntGridLayout.RowDefinitions.Count * 0.75;

	/// <summary>
	/// The current ASCII representation.
	/// </summary>
	string _antString;

	public string Status { get; set; }
}
