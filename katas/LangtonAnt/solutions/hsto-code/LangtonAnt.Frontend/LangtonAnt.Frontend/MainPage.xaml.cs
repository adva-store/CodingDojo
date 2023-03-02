using LangtonAnt.Frontend.ViewModels;

namespace LangtonAnt.Frontend;

public partial class MainPage : ContentPage
{

    private MainPageViewModel viewModel;

    public MainPage()
    {
        InitializeComponent();
        viewModel = new MainPageViewModel(this.chessGrid);
        BindingContext = viewModel;
    }
}


