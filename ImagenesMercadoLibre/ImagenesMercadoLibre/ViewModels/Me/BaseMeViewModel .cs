using ImagenesMercadoLibre.Models;
using ImagenesMercadoLibre.Services.Me;
using ImagenesMercadoLibre.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace ImagenesMercadoLibre.ViewModels.Me
{
    public class BaseMeViewModel : ViewModelBase//INotifyPropertyChanged
    {
        public MeModel _me = new MeModel();
        //public INavigation _navigationService;
        //public IValidator _meValidator;
        public IMeRepository _meRepository;
        public string nickname
        {
            get => _me.nickname;
            set
            {
                _me.nickname = value;
                RaisePropertyChanged();//NotifyPropertyChanged("nickname");
            }
        }
        public string first_name
        {
            get => _me.first_name;
            set
            {
                _me.first_name = value;
                RaisePropertyChanged();//NotifyPropertyChanged("first_name");
            }
        }
        public string last_name
        {
            get => _me.last_name;
            set
            {
                _me.last_name = value;
                RaisePropertyChanged();//NotifyPropertyChanged("last_name");
            }
        }
        public string gender
        {
            get => _me.gender;
            set
            {
                _me.gender = value;
                RaisePropertyChanged();//NotifyPropertyChanged("gender");
            }
        }
        public DateTime registration_date
        {
            get => _me.registration_date;
            set
            {
                _me.registration_date = value;
                RaisePropertyChanged();//NotifyPropertyChanged("registration_date");
            }
        }
        public string Address
        {
            get => _me.email;
            set
            {
                _me.email = value;
                RaisePropertyChanged();//NotifyPropertyChanged("email");
            }
        }
        List<MeModel> _meList;
        public List<MeModel> MeList
        {
            get => _meList;
            set
            {
                _meList = value;
                RaisePropertyChanged();//NotifyPropertyChanged("MeList");
            }
        }
        //#region INotifyPropertyChanged      
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
        //#endregion
    }
}
