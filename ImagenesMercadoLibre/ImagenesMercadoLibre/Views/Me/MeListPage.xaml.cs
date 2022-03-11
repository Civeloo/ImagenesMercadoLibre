using ImagenesMercadoLibre.ViewModels.Me;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ImagenesMercadoLibre.Views.Me
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MeListPage : ContentPage
    {
        private object Parameter { get; set; }
        public MeListPage(object parameter)
        {
            InitializeComponent();
            Parameter = parameter;
            BindingContext = App.Locator.MeListViewModel;
        }
    }
}