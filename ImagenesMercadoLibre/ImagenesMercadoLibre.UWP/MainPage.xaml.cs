using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ImagenesMercadoLibre.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new ImagenesMercadoLibre.App());
           // DefaultLaunch();
        }

        //async void DefaultLaunch()
        //{
        //    Path to the file in the app package to launch
        //    string imageFile = @"images    est.png";

        //    var file = await Windows.ApplicationModel.Package.Current.InstalledLocation;//GetFileAsync(imageFile);

        //    if (file != null)
        //    {
        //        Launch the retrieved file
        //    var p = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);//Windows.ApplicationModel.Package.Current.InstalledLocation;
        //        var success = await Windows.System.Launcher.LaunchFolderAsync(p);
        //        if (success)
        //        {
        //            // File launched
        //        }
        //        else
        //        {
        //            // File launch failed
        //        }
        //    }
        //    else
        //    {
        //        // Could not find file
        //    }
        //}
    }
}
