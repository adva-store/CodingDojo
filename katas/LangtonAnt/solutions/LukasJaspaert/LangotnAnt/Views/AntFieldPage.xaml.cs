using LangotnAnt.ViewModel;

namespace LangotnAnt.Views;

public partial class AntFieldPage : ContentPage
{
	public AntFieldPage(AntFieldViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}