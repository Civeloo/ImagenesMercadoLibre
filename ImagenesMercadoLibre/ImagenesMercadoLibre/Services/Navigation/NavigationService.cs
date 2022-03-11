using ImagenesMercadoLibre.ViewModels.Item;
using ImagenesMercadoLibre.ViewModels.Me;
using ImagenesMercadoLibre.ViewModels.Progress;
using ImagenesMercadoLibre.ViewModels.Token;
using ImagenesMercadoLibre.Views;
using ImagenesMercadoLibre.Views.Item;
using ImagenesMercadoLibre.Views.Me;
using ImagenesMercadoLibre.Views.Progress;
using ImagenesMercadoLibre.Views.Token;
using System;
using System.Collections.Generic;
//using ViewModels;
//using Views;
using Xamarin.Forms;

namespace ImagenesMercadoLibre.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        /// <summary>
        /// 
        /// </summary>
        private IDictionary<Type, Type> viewModelRouting = new Dictionary<Type, Type>()
        {
            { typeof(MainPage), typeof(MainPage) },
            { typeof(ItemSearchBarViewModel), typeof(ItemSearchBarPage) },
            { typeof(TokenViewModel), typeof(TokensPage) },
            { typeof(TokenEntryViewModel), typeof(TokenEntryPage) },
            { typeof(ItemListViewModel), typeof(ItemListPage) },
            { typeof(MeListViewModel), typeof(MeListPage) },
            { typeof(DetailsMeViewModel), typeof(MeDetailsPage) },
            { typeof(ProgressViewModel), typeof(ProgressPage) },
        };
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDestinationViewModel"></typeparam>
        /// <param name="navigationContext"></param>
        public void NavigateTo<TDestinationViewModel>(object navigationContext = null)
        {
            Type pageType = viewModelRouting[typeof(TDestinationViewModel)];
            var page = Activator.CreateInstance(pageType, navigationContext) as Page;
            Application.Current.MainPage.Navigation.PushAsync(page);
        }
        public void NavigateTo<TDestinationViewModel>(object navigationContext = null, object model = null)
        {
            Type pageType = viewModelRouting[typeof(TDestinationViewModel)];
            var page = Activator.CreateInstance(pageType, navigationContext) as Page;
            page.BindingContext = model;
            Application.Current.MainPage.Navigation.PushAsync(page);
        }
            /// <summary>
            /// 
            /// </summary>
            public void NavigateBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        public void NavigateBackToFirst()
        {
            Application.Current.MainPage.Navigation.PopToRootAsync();
        }
    }
}
