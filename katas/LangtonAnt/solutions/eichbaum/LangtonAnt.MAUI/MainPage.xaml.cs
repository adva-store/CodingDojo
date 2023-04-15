namespace LangtonAnt.MAUI;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	public string Size { get; set; } = "20";
	public string Speed { get; set; } = "50";
	public string StartRow { get; set; } = "10";
	public string StartColumn { get; set; } = "10";
	public string InitialMoves { get; set; } = "0";
	public Library.Direction Direction { get; set; } = Library.Direction.North;

	async void OnSubmitClicked(System.Object sender, System.EventArgs e)
	{
		if (!int.TryParse(Size, out int size) ||
			!int.TryParse(Speed, out int speed) ||
			!int.TryParse(StartRow, out int startRow) ||
			!int.TryParse(StartColumn, out int startColumn) ||
			!int.TryParse(InitialMoves, out int initialMoves))
		{
			await DisplayAlert("Invalid values", "Check values and try again.", "OK");
			return;
		}

		var antField = new Library.AntField(size, startRow, startColumn, Direction, initialMoves);
		await Navigation.PushAsync(new AntPage(speed, antField));
	}

	public List<Library.Direction> Directions => new()
	{
		Library.Direction.North,
		Library.Direction.East,
		Library.Direction.South,
		Library.Direction.West
	};
}