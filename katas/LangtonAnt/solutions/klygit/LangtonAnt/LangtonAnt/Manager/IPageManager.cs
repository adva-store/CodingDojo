using System.Threading.Tasks;
using Xamarin.Forms;

namespace LangtonAnt
{
    public interface IPageManager
    {
        void Init(App app, Page page);
        Task DisplayToastAsync(string message, int durationMilliseconds = 3000);
        Task PushPageAsync(Page page);
    }
}