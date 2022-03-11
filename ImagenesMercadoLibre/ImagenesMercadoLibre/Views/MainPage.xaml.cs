using System;
using System.ComponentModel;
using Xamarin.Forms;
using ImagenesMercadoLibre.Services.Navigation;
using ImagenesMercadoLibre.ViewModels.Item;
using ImagenesMercadoLibre.ViewModels.Me;
using ImagenesMercadoLibre.ViewModels.Token;
using ImagenesMercadoLibre.Views.FilePicker;
using ImagenesMercadoLibre.Services;
using ImagenesMercadoLibre.ViewModels.Progress;

namespace ImagenesMercadoLibre.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {      
        //Services
        private readonly INavigationService _navigationService = new NavigationService();

        public MainPage()
        {
            InitializeComponent();
        }
        void OnTokenButtonClicked(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new TokensPage());
            _navigationService.NavigateTo<TokenViewModel>(_navigationService);
        }
        async void OnDismissButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }
        void OnUploadButtonClicked(object sender, EventArgs e)
        {
            _navigationService.NavigateTo<ItemSearchBarViewModel>(_navigationService);
        }
        void OnDownloadButtonClicked(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new ItemListPage());
            App.RunGetItems = true;            
            _navigationService.NavigateTo<ProgressViewModel>(_navigationService);           
        }
        void OnUploadAllButtonClicked(object sender, EventArgs e)
        {
            App.RunUploadImages = true;
            _navigationService.NavigateTo<ProgressViewModel>(_navigationService);
        }
        void OnDownloadPicturesButtonClicked(object sender, EventArgs e)
        {
            App.RunGetImages = true;            
            _navigationService.NavigateTo<ProgressViewModel>(_navigationService);
        }
        void OnMeButtonClicked(object sender, EventArgs e)
        {
            _navigationService.NavigateTo<MeListViewModel>(_navigationService);
        }
        async void OnSingleFolderPageButtonClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;            

            //App.PathImg = 
            await DependencyService.Get<IPathPickerService>().GetPathStreamAsync();            

            (sender as Button).IsEnabled = true;
        }
    }

}
