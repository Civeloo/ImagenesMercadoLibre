using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ImagenesMercadoLibre.Views.Token
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TokensPage : ContentPage
    {
        private object Parameter { get; set; }
        public TokensPage(object parameter)
        {
            InitializeComponent();
            Parameter = parameter;
            BindingContext = App.Locator.TokenViewModel;
        }
    }
}