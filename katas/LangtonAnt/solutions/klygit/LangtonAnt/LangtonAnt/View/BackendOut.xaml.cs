using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LangtonAnt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BackendOut : ContentPage
    {
        public BackendOut()
        {
            InitializeComponent();

            BindingContext = App.ServiceProvider.GetService<BackendOutViewModel>();
            Title = "Generated Code";
        }
    }
}