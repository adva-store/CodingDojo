using LangtonAntFrontend.Pages;

namespace LangtonAntFrontend;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnButtonClicked(object sender, EventArgs e)
	{
        try
        {
            var result = await FilePicker.Default.PickAsync();
            if (result != null)
            {
                if (result.FileName.EndsWith("json", StringComparison.OrdinalIgnoreCase))
                {
                    using var stream = await result.OpenReadAsync();
                    StreamReader reader = new StreamReader(stream);
                    string json = reader.ReadToEnd();
                    await Navigation.PushModalAsync(new LangtonPage(json,Duration.Text));
                }
            }

         
        }
        catch (Exception)
        {
            await DisplayAlert("Alert", "Something went wrong, try again", "OK");
        }

    }
}

