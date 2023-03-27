using LangtonAntKataApp.Pages;

namespace LangtonAntKataApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("langtonAntsFieldPage", typeof(LangtonAntsFieldPage));
    }
}
