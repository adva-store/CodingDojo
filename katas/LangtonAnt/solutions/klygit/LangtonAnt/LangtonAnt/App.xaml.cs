using Microsoft.Extensions.DependencyInjection;
using System;
using Xamarin.Forms;

namespace LangtonAnt
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Init();
            ServiceProvider.GetService<IPageManager>().Init(this, new BackendIn());
        }


        public static IServiceProvider ServiceProvider { get; set; }

        public static IServiceProvider Init()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IPageManager, PageManager>()
                .AddSingleton<IFileManager, FileManager>()
                .AddSingleton<IGameDataManager, GameDataManager>()
                .AddSingleton<IImgResManager, ImgResManager>()
                .AddTransient<BackendInViewModel>()
                .AddTransient<BackendOutViewModel>()
                .AddTransient<FrontendViewModel>()
                .BuildServiceProvider();
            ServiceProvider = serviceProvider;

            return serviceProvider;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
