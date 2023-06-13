namespace MauiPlaygroundApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		// The visualization pages for the ant and the chessboard are pushed onto
		// the navigation stack and therefore not in the initial view hierachy.
		// That's why we must register routes for them to be able to navigate to them.
		Routing.RegisterRoute("antconfig/ant", typeof(AntPage));
		Routing.RegisterRoute("chessboardconfig/chessboard", typeof(ChessboardPage));
	}
}

