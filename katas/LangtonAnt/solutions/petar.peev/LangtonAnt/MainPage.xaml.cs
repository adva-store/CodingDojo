namespace LangtonAnt;
public partial class MainPage : ContentPage
{
	DashboardViewModel viewModel = new DashboardViewModel();
    public MainPage()
	{
        viewModel.Initialize();
		BindingContext = viewModel;
        AntDrawable.Matchfield = viewModel.Data;
        InitializeComponent();

		Loaded += (sender, args) => {
            viewModel.Data.PropertyChanged += (sender, args) => { if (args.PropertyName == "CurrentResult") antGraphicsView.Invalidate(); };
            viewModel.LoadDataCommand.Execute(null);
		};
	}
}

