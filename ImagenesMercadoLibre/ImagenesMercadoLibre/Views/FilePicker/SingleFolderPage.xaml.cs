using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ImagenesMercadoLibre.Views.FilePicker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingleFolderPage : ContentPage
    {
        public SingleFolderPage()
        {
            InitializeComponent();
        }

        string _OutputTextBlock;
        public string OutputTextBlock
        {
            get => _OutputTextBlock;
            set
            {
                _OutputTextBlock = value;
                //RaisePropertyChanged();
            }
        }
//        async Task PickFolderButton_Click(object sender, EventArgs e)
//        {
//            OutputTextBlock = "";

//            // UWP-specific code
//            if (Device.RuntimePlatform == Device.UWP)
//            {
//                // Clear previous returned folder name, if it exists, between iterations of this scenario
//                //OutputTextBlock.Text = "";            

//                Windows.Storage.Pickers.FolderPicker folderPicker = new Windows.Storage.Pickers.FolderPicker();
//                folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
//                folderPicker.FileTypeFilter.Add(".docx");
//                folderPicker.FileTypeFilter.Add(".xlsx");
//                folderPicker.FileTypeFilter.Add(".pptx");
//                Windows.Storage.StorageFolder folder = null;
//#if WINDOWS_UWP
//                folder = await folderPicker.PickSingleFolderAsync();
//#endif
//                if (folder != null)
//                {
//                    // Application now has read/write access to all contents in the picked folder (including other sub-folder contents)
//                    Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);
//                    //OutputTextBlock.Text 
//                    OutputTextBlock = "Picked folder: " + folder.Name;
//                }
//                else
//                {
//                    //OutputTextBlock.Text 
//                    OutputTextBlock = "Operation cancelled.";
//                }
//            }
//        }
    }
}