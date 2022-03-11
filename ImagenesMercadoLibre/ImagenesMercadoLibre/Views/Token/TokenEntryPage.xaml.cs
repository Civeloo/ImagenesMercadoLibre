using ImagenesMercadoLibre.Models;
using System;
using ImagenesMercadoLibre.Services.ML;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ImagenesMercadoLibre.Services.Navigation;

namespace ImagenesMercadoLibre.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TokenEntryPage : ContentPage
    {
        private object Parameter { get; set; }
        public TokenEntryPage(object parameter)
        {
            InitializeComponent();
            Parameter = parameter;
            BindingContext = App.Locator.TokenEntryViewModel;
        }
    }
}