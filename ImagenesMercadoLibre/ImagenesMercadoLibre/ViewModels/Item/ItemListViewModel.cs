using Acr.UserDialogs;
using ImagenesItemrcadoLibre.ViewModels.Item;
using ImagenesMercadoLibre.Models;
using ImagenesMercadoLibre.Services.Item;
using ImagenesMercadoLibre.Services.Navigation;
using ImagenesMercadoLibre.ViewModels.Progress;
using ImagenesMercadoLibre.Views;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ImagenesMercadoLibre.ViewModels.Item
{
    public class ItemListViewModel : ItemBaseViewModel
    {       
        public ItemListViewModel(INavigationService navigationService)
        { 
            _navigationService = navigationService;
            //_itemRepository = new ItemRepository();
            //ItemBackCommand = new Command(async () => await ItemBack());
            Title = "Descargando Imagenes...";
            FetchItems();
        }
        void OnCreate()
        {                        
            //FetchItems();          
            //ItemList = ItemRepository.GetAllItem();
        }        
        void FetchItems()
        {
            IsBusy = true;
            

            //MessagingCenter.Send<string, bool>("MyApp", "NotifyMsg", true);
            
//            _navigationService.NavigateTo<ProgressViewModel>(_navigationService);
            //ItemList = await App.Manager.GetItemAsync();
            //IsBusy = false;
            ItemList = ItemRepository.GetAllItem();
        }              
    }
}