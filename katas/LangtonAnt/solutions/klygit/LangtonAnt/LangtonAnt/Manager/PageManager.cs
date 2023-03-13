using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace LangtonAnt
{
    internal class PageManager : IPageManager
    {
        private NavigationPage NavigationPage { get; set; }

        public void Init(App app, Page page)
        {
            NavigationPage = new NavigationPage(page);
            app.MainPage = NavigationPage;
        }

        public async Task DisplayToastAsync(string message, int durationMilliseconds = 3000)
        {
            await NavigationPage?.DisplayToastAsync(message, durationMilliseconds);
        }

        public async Task PushPageAsync(Page page)
        {
            await NavigationPage?.PushAsync(page);
        }
    }
}
