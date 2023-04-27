using CommunityToolkit.Maui;
using LangotnAnt.ViewModel;
using LangotnAnt.Views;
using Microsoft.Extensions.Logging;

namespace LangotnAnt;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
		builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainSettingsViewModel>();
		builder.Services.AddSingletonWithShellRoute<AntFieldPage, AntFieldViewModel>("AntField");


		return builder.Build();
	}
}
