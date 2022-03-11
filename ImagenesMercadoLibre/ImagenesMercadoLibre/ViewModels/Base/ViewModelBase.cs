using ImagenesMercadoLibre.Services.Navigation;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace ImagenesMercadoLibre.ViewModels.Base
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        //Services
        public INavigationService _navigationService;
        // Variables
        private string _info;
        public string Info
        {
            get { return _info; }
            set
            {
                _info = value;
                RaisePropertyChanged();
            }
        }
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisePropertyChanged();
            }
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }
        private bool _isLoading = false;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                RaisePropertyChanged();// () => IsLoading);
            }
        }

        private int _loadingProgress = 0;
        public int LoadingProgress
        {
            get { return _loadingProgress; }
            set
            {
                _loadingProgress = value;
                RaisePropertyChanged();// () => LoadingProgress);
            }
        }

        private int _totalProgress = 0;
        public int TotalProgress
        {
            get { return _totalProgress; }
            set
            {
                _totalProgress = value;
                RaisePropertyChanged();//() => TotalProgress);
            }
        }
        //private bool runGetItems;
        //public bool RunGetItems
        //{ 
        //    get 
        //    {
        //        MessagingCenter.Subscribe<string, bool>("MyApp", "NotifyMsg", (sender, arg) =>
        //        {
        //            //Debug.WriteLine($"Recieved Msg | {arg}");
        //            runGetItems = arg;
        //        });
        //        return runGetItems; 
        //    }
        //    set
        //    {
        //        runGetItems = value;
        //        RaisePropertyChanged();
        //    }
        //}
        //public virtual void OnAppearing(object navigationContext)      
        //{
        //    //base.OnAppearing(navigationContext);
        //}
        public virtual void OnDisappearing()
        {
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}