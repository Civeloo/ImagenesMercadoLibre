using ImagenesItemrcadoLibre.ViewModels.Item;
using ImagenesMercadoLibre.Models;
using ImagenesMercadoLibre.Services.Item;
using ImagenesMercadoLibre.Services.Navigation;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ImagenesMercadoLibre.ViewModels.Item
{
    public class ItemSearchBarViewModel : ItemBaseViewModel
    {
        //Services
        //private readonly INavigationService _navigationService;
        // Commands
        //private ICommand _navigateCommand;
        //public ICommand NavigateCommand
        //{
        //    get { return _navigateCommand = _navigateCommand ?? new DelegateCommand(NavigateCommandExecute); }
        //}
        //public void NavigateCommandExecute()
        //{
        //    _navigationService.NavigateBack();
        //}
        private async void SelectedItem_PropertyChanged(
        object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedItem")
            {
                await _OnItemTapped();
            }
        }
        private async Task _OnItemTapped()
        {
            var result = await Application.Current.MainPage.DisplayAlert("Upload", "Desea subir las imágenes?", "Si", "No");
            if (result)
            {
                var e = SelectedItem;
                if (SelectedItem != null)
                {
                    var _item = SelectedItem as ItemModel;
                    await PictureUpdate(_item.ID);//await mls.ItemPictureUpdateAsync(_item.ID);
                }
            } else _navigationService.NavigateBack();
        }
        public ICommand PerformSearch => new Command<string>((string query) =>
        {
            ItemList = ItemRepository.ItemGetSearchResults(query);
            //MessagingCenter.Send(App.Locator.ItemSearchBarViewModel, "Change");
        });
        public ItemSearchBarViewModel(INavigationService navigationService)//(INavigation navigation)
        {       
            _navigationService = navigationService;
            //Info = "Enter a search term and press enter or click the magnifying glass to perform a search.";
            Info = "Ingresar el nombre y presionar enter  hacer click en lupa para realizar busqueda.";
            Title = "Seleccionar el artículo para subir sus imagenes...";
            //MessagingCenter.Subscribe<ItemSearchBarViewModel>(this, "Change", (sender) =>
            //{
            //    Info = "Changed from Messaging Center";
            //    Display();
            //});
            //_navigation = navigation;
            //_itemRepository = new ItemRepository();
            ItemList = ItemRepository.GetAllItem();
            // Register for changes to the selected customer.
            PropertyChanged +=
                new PropertyChangedEventHandler(SelectedItem_PropertyChanged);            
        }
        public async Task PictureUpdate(string id)
        {
            IsBusy = true;
            await App.Manager.ItemPictureUpdateAsync(id);
            await Application.Current.MainPage.DisplayAlert("Upload", "Terminado", "Ok");
            IsBusy = false;
            _navigationService.NavigateBack();
        }
    }
}

