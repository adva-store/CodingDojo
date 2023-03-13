using System.Globalization;
using System;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace LangtonAnt
{
    public partial class BackendIn : ContentPage
    {
        public BackendIn()
        {
            InitializeComponent();

            var viewModel = App.ServiceProvider.GetService<BackendInViewModel>();
            BindingContext = viewModel;

            Title = viewModel.PageTitle;
        }
    }

    public class BoolToColorConverter : IValueConverter
    {
        public Color TrueObject { set; get; }
        public Color FalseObject { set; get; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? TrueObject : FalseObject;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Color)value).Equals(TrueObject);
        }
    }
}
