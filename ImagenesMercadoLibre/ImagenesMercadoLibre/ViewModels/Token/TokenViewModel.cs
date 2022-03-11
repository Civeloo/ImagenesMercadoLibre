using ImagenesMercadoLibre.Services.Navigation;
using ImagenesMercadoLibre.ViewModels.Base;
using ImagenesMercadoLibre.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ImagenesMercadoLibre.ViewModels.Token
{
    public class TokenViewModel : TokenBaseViewModel
    {
        ICommand _getTokenCommand;
        public ICommand GetTokenCommand
        {
            get { return _getTokenCommand = _getTokenCommand ?? new DelegateCommand(CommandExecute); }
        }
        public void CommandExecute()
        {
            GetToken();
        }
        public TokenViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Title = "Token";
            OnAppearing();
        }
        void GetToken()
        {
            App.Manager.GetAuthCode();
            //await Navigation.PushModalAsync(new TokenEntryPage
            //{
            //    BindingContext = new Token()
            //});
            _navigationService.NavigateTo<TokenEntryViewModel>(_navigationService);
            //OnAppearing();
        }
        protected async void OnAppearing()
        {
            //base.OnAppearing();
            TokenList = await TokenRepository.TokenGetAllAsync();
        }

        void OnListViewTokenSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                //await Navigation.PushModalAsync(new TokenEntryPage
                //{
                //    BindingContext = e.SelectedItem as Token
                //});
            }
        }       
    }
}
