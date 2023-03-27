using LangtonAntKataApp.ViewModels;

namespace LangtonAntKataApp;

public partial class MainPage : ContentPage
{
	private MainPageViewModel pageVM;
	public MainPage()
	{
		InitializeComponent();
		pageVM = new MainPageViewModel();
		this.BindingContext = pageVM;
	}
}

