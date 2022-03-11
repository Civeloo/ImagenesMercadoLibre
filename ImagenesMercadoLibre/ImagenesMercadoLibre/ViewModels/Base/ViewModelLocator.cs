using ImagenesMercadoLibre.Services.Navigation;
using ImagenesMercadoLibre.ViewModels.Item;
using ImagenesMercadoLibre.ViewModels.Me;
using ImagenesMercadoLibre.ViewModels.Progress;
using ImagenesMercadoLibre.ViewModels.Token;
using ImagenesMercadoLibre.Views;
using Unity;

namespace ImagenesMercadoLibre.ViewModels.Base
{
    public class ViewModelLocator
    {
        readonly IUnityContainer _container;
        public ViewModelLocator()
        {
            _container = new UnityContainer();
            // ViewModels
            _container.RegisterType<MainPage>();
            _container.RegisterType<ItemSearchBarViewModel>();            
            _container.RegisterType<TokenViewModel>();
            _container.RegisterType<TokenEntryViewModel>();
            _container.RegisterType<ItemListViewModel>();
            _container.RegisterType<MeListViewModel>();
            _container.RegisterType<DetailsMeViewModel>();
            _container.RegisterType<AddMeViewModel>();
            _container.RegisterType<ProgressViewModel>();
            // Servicios
            _container.RegisterType<INavigationService, NavigationService>();
        }
        public MainPage MainPage
        {
            get { return _container.Resolve<MainPage>(); }
        }
        public DetailsMeViewModel DetailsMeViewModel
        {
            get { return _container.Resolve<DetailsMeViewModel>(); }
        }
        public TokenViewModel TokenViewModel
        {
            get { return _container.Resolve<TokenViewModel>(); }
        }        
        public TokenEntryViewModel TokenEntryViewModel
        {
            get { return _container.Resolve<TokenEntryViewModel>(); }
        }
        public ItemListViewModel ItemListViewModel
        {
            get { return _container.Resolve<ItemListViewModel>(); }
        }
        public ItemSearchBarViewModel ItemSearchBarViewModel
        {
            get { return _container.Resolve<ItemSearchBarViewModel>(); }
        }
        public MeListViewModel MeListViewModel
        {
            get { return _container.Resolve<MeListViewModel>(); }
        }
        public AddMeViewModel AddMeViewModel
        {
            get { return _container.Resolve<AddMeViewModel>(); }
        }
        public ProgressViewModel ProgressViewModel
        {
            get { return _container.Resolve<ProgressViewModel>(); }
        }
    }
}
