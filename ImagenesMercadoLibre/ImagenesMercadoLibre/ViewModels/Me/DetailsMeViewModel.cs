using ImagenesMercadoLibre.Models;
using ImagenesMercadoLibre.Services.Me;
using ImagenesMercadoLibre.Services.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ImagenesMercadoLibre.ViewModels.Me
{
    public class DetailsMeViewModel : BaseMeViewModel
    {
        public ICommand UpdateMeCommand { get; private set; }
        public ICommand DeleteMeCommand { get; private set; }
        public DetailsMeViewModel(INavigationService navigationService, int selectedMeID)
        {
            _navigationService = navigationService;
            //_meValidator = new MeValidator();
            _me = new MeModel();
            _me.ID = selectedMeID;
            _meRepository = new MeRepository();
            UpdateMeCommand = new Command(async () => await UpdateMe());
            DeleteMeCommand = new Command(async () => await DeleteMe());
            FetchMeDetails();
        }
        void FetchMeDetails()
        {
            _me = _meRepository.GetMe(_me.ID);
        }
        async Task UpdateMe()
        {
            //var validationResults = _meValidator.Validate(_me);
            //if (validationResults.IsValid)
            //{
                bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Me Details", "Update Me Details", "OK", "Cancel");
                if (isUserAccept)
                {
                    await _meRepository.SaveMe(_me);
                    //await _navigationService.PopModalAsync();
                }
            //}
            //else
            //{
            //    await Application.Current.MainPage.DisplayAlert("Add Me", validationResults.Errors[0].ErrorMessage, "Ok");
            //}
        }
        async Task DeleteMe()
        {
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Me Details", "Delete Me Details", "OK", "Cancel");
            if (isUserAccept)
            {
                _meRepository.DeleteMe(_me);
                //await _navigationService.PopModalAsync();
            }
        }
    }
}
