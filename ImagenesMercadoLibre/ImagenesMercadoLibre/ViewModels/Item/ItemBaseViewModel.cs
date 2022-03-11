using ImagenesMercadoLibre.Models;
using ImagenesMercadoLibre.Services.Item;
using ImagenesMercadoLibre.ViewModels.Base;
using System;
using System.Collections.Generic;

namespace ImagenesItemrcadoLibre.ViewModels.Item
{
    public class ItemBaseViewModel : ViewModelBase//INotifyPropertyChanged
    {
        public ItemModel _item = new ItemModel();
        //public INavigation _navigation;        
        //public IValidator _itemValidator;
        public IItemRepository _itemRepository = new ItemRepository();
        //private double _itemProgressBar;
        public IItemRepository ItemRepository
        {
            get => _itemRepository;
            set
            {
                _itemRepository = value;
                RaisePropertyChanged();
            }
        }
        public string ID
        {
            get => _item.ID;
            set
            {
                _item.ID = value;
                RaisePropertyChanged();//NotifyPropertyChanged("ID");
            }
        }
        public string title
        {
            get => _item.title;
            set
            {
                _item.title = value;
                RaisePropertyChanged();//NotifyPropertyChanged("title");
            }
        }
        public DateTime last_updated
        {
            get => _item.last_updated;
            set
            {
                _item.last_updated = value;
                RaisePropertyChanged();//NotifyPropertyChanged("last_updated");
            }
        }
        List<ItemModel> _itemList;
        public List<ItemModel> ItemList
        {
            get => _itemList;
            set
            {
                _itemList = value;
                RaisePropertyChanged();//NotifyPropertyChanged("ItemList");
            }
        }
        ItemModel selectedItem;
        public ItemModel SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                RaisePropertyChanged();//NotifyPropertyChanged("SelectedItem");
            }
        }
        //public double ItemProgressBar
        //{
        //    get => _itemProgressBar;
        //    set
        //    {
        //        _itemProgressBar = value;
        //        RaisePropertyChanged();//NotifyPropertyChanged("ItemProgressBar");
        //    }
        //}
        //public bool IsBusy
        //{
        //    get => _isBusy;
        //    set
        //    {
        //        _isBusy = value;
        //        RaisePropertyChanged();//NotifyPropertyChanged("IsBusy");
        //    }
        //}        
        //#region INotifyPropertyChanged      
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
        //#endregion
    }
}
