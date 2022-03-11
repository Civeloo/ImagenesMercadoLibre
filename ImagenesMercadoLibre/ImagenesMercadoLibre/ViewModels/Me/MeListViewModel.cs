using ImagenesMercadoLibre.Models;
using ImagenesMercadoLibre.Services.Me;
using ImagenesMercadoLibre.Services.Navigation;
using ImagenesMercadoLibre.Views.Me;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ImagenesMercadoLibre.ViewModels.Me
{
    public class MeListViewModel : BaseMeViewModel
    {
        public ICommand AddCommand { get; private set; }
        public ICommand DeleteAllMesCommand { get; private set; }

        public MeListViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _meRepository = new MeRepository();

            //AddCommand = new Command(async () => await ShowAddMe());
            //DeleteAllMesCommand = new Command(async () => await DeleteAllMes());

            FetchMes();
        }

        async void FetchMes()
        {
            MeList = await App.Manager.GetMeAsync();
        }

        //async Task ShowAddMe()
        //{
        //    //await _navigation.PushModalAsync(new AddMe());
        //}
        //async Task DeleteAllMes()
        //{
        //    bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Me List", "Delete All Me Details ?", "OK", "Cancel");
        //    if (isUserAccept)
        //    {
        //        _meRepository.DeleteAllMe();
        //        //await _navigation.PushModalAsync(new AddMe());
        //    }
        //}

        //async void ShowMeDetails(int selectedMeID)
        //{
        //    //await _navigation.PushModalAsync(new MeDetailsPage(selectedMeID));
        //}

        MeModel _selectedMeItem;
        public MeModel SelectedMeItem
        {
            get => _selectedMeItem;
            set
            {
                if (value != null)
                {
                    _selectedMeItem = value;
                    RaisePropertyChanged();//NotifyPropertyChanged("SelectedMeItem");
                    //ShowMeDetails(value.id);
                }
            }
        }
    }
}
