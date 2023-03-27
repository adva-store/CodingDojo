namespace StrangeChessboard;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnGenerateBtnClicked(object sender, EventArgs e)
	{
        await Shell.Current.GoToAsync("strangeChessboardPage");
    }
}

