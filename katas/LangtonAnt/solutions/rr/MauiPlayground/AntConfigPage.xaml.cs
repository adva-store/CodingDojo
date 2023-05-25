using System.ComponentModel;
using AntLogic;
using PropertyChanged;

namespace MauiPlaygroundApp;

[AddINotifyPropertyChangedInterface]
public partial class AntConfigPage : ContentPage, INotifyPropertyChanged
{
	public AntConfigPage()
	{
		InitializeComponent();

		// Initialize picker with enum values of directions ant can move into.
		AntDirections = Enum
			.GetValues(typeof(Ant.AntDirection))
			.Cast<Ant.AntDirection>()
			.ToArray();

		BindingContext = this;
		UpdateAntModel();
	}

	/// <summary>
	/// Possible moving directions.
	/// </summary>
	public Ant.AntDirection[] AntDirections { get; }

	/// <summary>
	/// Speed of ant. This is a delay in milliseconds between each step.
	/// </summary>
	public int MoveSpeed { get; set; } = 100;

	/// <summary>
	/// Size of the grid. Used fo width and height.
	/// </summary>
	public int GridSize { get; set; } = 11;

	/// <summary>
	/// Zero based column the ant starts on.
	/// </summary>
	public int StartColumn { get; set; } = 5;

	/// <summary>
	/// Zero based row the ant starts on.
	/// </summary>
	public int StartRow { get; set; } = 5;

	/// <summary>
	/// Number of steps the ant will move.
	/// </summary>
	public int NumberOfSteps { get; set; } = 200;

	/// <summary>
	/// Starting direction of the ant.
	/// </summary>
	public Ant.AntDirection StartDirection { get; set; } = Ant.AntDirection.West;

	public void OnPropertyChanged(string propertyName, object before, object after)
	{
		// Lazy implementation. If any property changes, update the ant model.
		Console.WriteLine("Updating grid and ant.");
		UpdateAntModel();
	}

	/// <summary>
	/// Helper to recreate the ant's grid and the ant itself when input paraeters have changed.
	/// </summary>
	void UpdateAntModel()
	{

		_grid = new AntGrid(GridSize);
		_ant = new Ant(_grid)
		{
			Position = (StartColumn, StartRow),
			Direction = StartDirection,
			StepsLeft = NumberOfSteps
		};
	}

	AntGrid _grid;
	Ant _ant;

	/// <summary>
	/// Navigates to the page visualizing the ant on the grid.
	/// Passed the ant model and the grid model as parameters.
	/// </summary>
	async void OnGoButtonClicked(System.Object sender, System.EventArgs e)
	{
		UpdateAntModel();
		await Shell.Current.GoToAsync("ant", new Dictionary<string, object>
		{
			{ "ant", _ant },
			{ "dimension", GridSize },
			{ "moveSpeed", MoveSpeed }
		});
	}
}
