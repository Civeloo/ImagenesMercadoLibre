using ImagenesMercadoLibre.ViewModels.Me;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ImagenesMercadoLibre.Views.Me
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MeDetailsPage : ContentPage
    {
        private object Parameter { get; set; }
        public MeDetailsPage(object parameter)
        {
            InitializeComponent();
            Parameter = parameter;
            BindingContext = App.Locator.DetailsMeViewModel;
        }
    }
}