using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ImagenesMercadoLibre.Services.Logger
{
    internal class LoggerService : ILoggerService
    {
        public async void LogCatch(Exception ex)
        {
            Debug.WriteLine(ex.Message + "," + ex.StackTrace);
            await Application.Current.MainPage.DisplayAlert(ex.Message, ex.StackTrace, "Ok");
        }
    }
}
