using ImagenesMercadoLibre.Models;
using ImagenesMercadoLibre.Services.Me;
using ImagenesMercadoLibre.Services.Navigation;
using ImagenesMercadoLibre.Views.Me;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ImagenesMercadoLibre.ViewModels.Me
{
    public class AddMeViewModel : BaseMeViewModel
    {
        //public ICommand AddMeCommand { get; private set; }
        //public ICommand ViewAllMesCommand { get; private set; }
        public AddMeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            //_meValidator = new MeValidator();
            _me = new MeModel();
            _meRepository = new MeRepository();

            //AddMeCommand = new Command(async () => await AddMe());
            //ViewAllMesCommand = new Command(async () => await ShowMeList());
        }
        //async Task AddMe()
        //{
        //    //var validationResults = _meValidator.Validate(_me);

        //    //if (validationResults.IsValid)
        //    //{
        //        bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Add Me", "Do you want to save Me details?", "OK", "Cancel");
        //        if (isUserAccept)
        //        {
        //            _meRepository.InsertMe(_me);
        //            //await _navigationService.PushModalAsync(new MeListPage());
        //        }
        //    //}
        //    //else
        //    //{
        //    //    await Application.Current.MainPage.DisplayAlert("Add Me", validationResults.Errors[0].ErrorMessage, "Ok");
        //    //}
        //}
        //async Task ShowMeList()
        //{
        //    //await _navigationService.PushModalAsync(new MeListPage());
        //}
        public bool IsViewAll => _meRepository.GetAllMe().Count > 0 ? true : false;
    }
}
