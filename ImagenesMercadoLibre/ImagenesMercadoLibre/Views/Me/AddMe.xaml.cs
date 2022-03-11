using ImagenesMercadoLibre.ViewModels.Me;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ImagenesMercadoLibre.Views.Me
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddMe : ContentPage
    {
        private object Parameter { get; set; }
        public AddMe(object parameter)
        {
            InitializeComponent();
            Parameter = parameter;
            BindingContext = App.Locator.AddMeViewModel;
        }
    }
}