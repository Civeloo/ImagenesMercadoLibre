using System;
using System.IO;
using Xamarin.Forms;
using ImagenesMercadoLibre.Data;
using ImagenesMercadoLibre.Views;
using ImagenesMercadoLibre.Services.ML;
using ImagenesMercadoLibre.ViewModels.Base;
using ImagenesMercadoLibre.Services.Me;
using ImagenesMercadoLibre.Services;
using System.Net.Http;

namespace ImagenesMercadoLibre
{
    public partial class App : Application
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        public readonly HttpClient httpclient;   
        private static ViewModelLocator _locator;
        public static ViewModelLocator Locator
        {
            get
            {
                return _locator = _locator ?? new ViewModelLocator();
            }
        }
        public static MercadolibreManager Manager { get; private set; }
        public static LoggerManager LoggerManager { get; private set; }
        static int _userId;
        public static int UserId
        {
            get
            {
                if (_userId == 0)
                {
                    var _meRepository = new MeRepository();
                    var _me = _meRepository.GetAllMe();
                    _userId = _me[0].ID;
                }
                return _userId;
            }            
        }
        static string myPath;
        public static string MyPath
            {
            get
            {
                if (myPath == null) myPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);                
                return myPath;
            }
        }
        static string pathImg;
        public static string PathImg
        {
            get
            {
                if (pathImg == null)
                {
                    //var settingR = new SettingRepository();
                    //var setting = settingR.SettingGet();
                    //if (setting != null) pathImg = setting.PathImage;
                    pathImg = App.LoggerManager.GetFolderPathAsync().Result;//DependencyService.Get<IPathPickerService>().GetFolderPathAsync().Result;
                }
                return pathImg;
            }
            //set
            //{
            //    if (pathImg == value) return;
            //    pathImg = value;

            //    var settingR = new SettingRepository();
            //    var setting = new SettingModel();
            //    setting.PathImage = pathImg;
            //    settingR.SettingSet(setting);
            //    //OnPropertyChanged("PathImg");
            //}
        }
        static MyDatabase database;

        public static MyDatabase Database
        {
            get
            {
                if (database == null) database = new MyDatabase(Path.Combine(MyPath, "ImagenesMercadoLibre.db3")); 
                return database;
            }
        }
        private static bool runGetItems;
        public static bool RunGetItems
        {
            get
            {
                return runGetItems;
            }
            set
            {
                runGetItems = value;
            }
        }
        private static bool runGetImages;
        public static bool RunGetImages
        {
            get
            {
                return runGetImages;
            }
            set
            {
                runGetImages = value;
            }
        }        
        private static bool runUploadImages;
        public static bool RunUploadImages
        {
            get
            {
                return runUploadImages;
            }
            set
            {
                runUploadImages = value;
            }
        }

        //#region StartupProgress (decimal)
        //private decimal _StartupProgress;
        ///// <summary>Decimal values of 0 - 1
        ///// 
        ///// </summary>
        //public decimal StartupProgress
        //{
        //    get
        //    {
        //        return _StartupProgress;
        //    }

        //    set
        //    {
        //        if (_StartupProgress == value) return;
        //        _StartupProgress = value;
        //        OnPropertyChanged("StartupProgress");
        //    }
        //}
        //#endregion StartupProgress  (decimal)

        public App()
        {
            InitializeComponent();
            Manager = new MercadolibreManager(new MercadolibreService());
            LoggerManager = new LoggerManager(DependencyService.Get<IPathPickerService>());//LoggerService());
            //MainPage = new MainPage();    
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

    }
}
