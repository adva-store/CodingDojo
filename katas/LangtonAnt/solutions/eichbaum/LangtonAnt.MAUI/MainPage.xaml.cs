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

	async void OnSubmitClicked(System.Object sender, System.EventArgs e)
	{
		if (!int.TryParse(Size, out int size) ||
			!int.TryParse(Speed, out int speed) ||
			!int.TryParse(StartRow, out int startRow) ||
			!int.TryParse(StartColumn, out int startColumn))
		{
			await DisplayAlert("Invalid values", "Check values and try again.", "OK");
			return;
		}

		var antField = new Library.AntField(size, startRow, startColumn, Library.Direction.North);
		await Navigation.PushAsync(new AntPage(speed, antField));
	}
}