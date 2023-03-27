namespace StrangeChessboard;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("strangeChessboardPage", typeof(StrangeChessboardPage));
    }
}
