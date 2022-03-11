using ImagenesMercadoLibre.ViewModels.Item;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ImagenesMercadoLibre.Views.Item
{
    [XamlCompilation(XamlCompilationOptions.Compile)]   
    public partial class ItemListPage : ContentPage
    {
        private object Parameter { get; set; }
        public ItemListPage(object parameter)
        {
            InitializeComponent();
            Parameter = parameter;
            BindingContext = App.Locator.ItemListViewModel;
        }         
    }
}