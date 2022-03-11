using ImagenesMercadoLibre.Services.Item;
using ImagenesMercadoLibre.Services.Navigation;
using ImagenesMercadoLibre.ViewModels.Item;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace ImagenesMercadoLibre.ViewModels.Progress
{
    public class ProgressViewModel : ProgressBaseViewModel
    {
        public ProgressViewModel(INavigationService navigationService)
        {            
            _navigationService = navigationService;
            Title = "Procesando..";
            //IsBusy = true;
            MessagingCenter.Subscribe<string, string>("Progress", "Title", (sender, arg) => { Title = arg; });
            MessagingCenter.Subscribe<string, int>("Progress", "LoadingProgress", (sender, arg) => { LoadingProgress = arg; });
            MessagingCenter.Subscribe<string, int>("Progress", "TotalProgress", (sender, arg) => { TotalProgress = arg; });           
            OnAppering();
        }
        async void OnAppering()
        {
            
            IsBusy = true;
            if (App.RunGetItems)
            {
                App.RunGetItems = false;
                Title = "Descargando Articulos";
                await App.Manager.ItemRefreshAsync();
            }
            else 
            if (App.RunGetImages)
            {
                App.RunGetImages = false;                
                var itemR = new ItemRepository();
                var items = itemR.GetAllItem();
                Title = "Borrando Imágenes";
                await App.LoggerManager.DeleteAllFoldersAsync();
                Title = "Descargando Imágenes";
                await App.Manager.PictureAllDownloadAsync(items);
            }
            else
            if (App.RunUploadImages)
            {
                App.RunUploadImages = false;
                Title = "Subiendo Imágenes";
                await App.Manager.UploadItems();
            }
            IsBusy = false;
            await Application.Current.MainPage.DisplayAlert("Proceso", "Terminado", "Ok");
            _navigationService.NavigateBack();
        }
    }
}
