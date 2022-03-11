using System;
using System.Collections.Generic;
using System.Text;

namespace ImagenesMercadoLibre.Services.Navigation
{
    public interface INavigationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDestinationViewModel"></typeparam>
        /// <param name="navigationContext"></param>
        void NavigateTo<TDestinationViewModel>(object navigationContext = null);
        void NavigateTo<TDestinationViewModel>(object navigationContext = null, object model = null);
        /// <summary>
        /// 
        /// </summary>
        void NavigateBack();

        /// <summary>
        /// 
        /// </summary>
        void NavigateBackToFirst();
    }
}
