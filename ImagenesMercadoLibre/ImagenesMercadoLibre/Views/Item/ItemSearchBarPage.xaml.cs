using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ImagenesMercadoLibre.Views.Item
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemSearchBarPage : ContentPage
    {
        private object Parameter { get; set; }
        public ItemSearchBarPage(object parameter)
        {
            InitializeComponent();
            Parameter = parameter;
            BindingContext = App.Locator.ItemSearchBarViewModel;
        }        
    }
}