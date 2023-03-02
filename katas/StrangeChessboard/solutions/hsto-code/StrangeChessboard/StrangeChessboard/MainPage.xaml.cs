using StrangeChessboard.Models;
using StrangeChessboard.ViewModels;

namespace StrangeChessboard;

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


