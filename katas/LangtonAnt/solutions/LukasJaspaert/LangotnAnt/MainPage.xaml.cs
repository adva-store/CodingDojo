using LangotnAnt.ViewModel;

namespace LangotnAnt;

public partial class MainPage : ContentPage
{

	public MainPage(MainSettingsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

}

