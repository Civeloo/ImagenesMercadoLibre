using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ImagenesMercadoLibre.Views.Progress
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProgressPage : ContentPage
    {
        private object Parameter { get; set; }
        public ProgressPage(object parameter)
        {
            InitializeComponent();
            Parameter = parameter;
            BindingContext = App.Locator.ProgressViewModel;
        }
    }
}