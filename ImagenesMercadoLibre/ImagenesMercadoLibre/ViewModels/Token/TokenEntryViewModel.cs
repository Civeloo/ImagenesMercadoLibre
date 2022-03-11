using ImagenesMercadoLibre.Services.Navigation;
using ImagenesMercadoLibre.ViewModels.Base;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImagenesMercadoLibre.ViewModels.Token
{
    public class TokenEntryViewModel : TokenBaseViewModel
    {
        ICommand _tokenEntrySave;
        public ICommand TokenEntrySave
        {
            get { return _tokenEntrySave = _tokenEntrySave ?? new DelegateCommand( CommandExecute); }
        }
        public void CommandExecute()
        {
            OnTokenEntrySave().Wait();
        }
        public TokenEntryViewModel(INavigationService navigationService)//(INavigation navigation)
        {
            _navigationService = navigationService;
            Title = "Ingresar Token Code";
            //if (TokenList != null) Token = TokenList[0];            
        }
        public async Task OnTokenEntrySave()
        {
            //var token = (TokenModel)BindingContext;   
            
            Token.Auth = Token.Auth.Replace("https://www.mercadolibre.com/?code=", string.Empty);            
            Token.Auth = Token.Auth.Trim();
            Token.ID = 1;
            await App.Manager.GetAccessTokenAsync(Token);
            //await Navigation.PopModalAsync();
            _navigationService.NavigateBackToFirst();
        }
        //async void OnDeleteButtonClicked(object sender, EventArgs e)
        //{
        //    var token = (TokenModel)BindingContext;
        //    await App.Database.DeleteAsync(token);
        //    await Navigation.PopModalAsync();
        //}
        //async void OnBackButtonClicked(object sender, EventArgs e)
        //{
        //    //await Navigation.PopModalAsync();           
        //}
    }
}
