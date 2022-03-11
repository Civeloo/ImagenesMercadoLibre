using ImagenesMercadoLibre.Models;
using ImagenesMercadoLibre.Services;
using ImagenesMercadoLibre.UWP.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using ImagenesMercadoLibre.Services.Picture;
using Xamarin.Forms;
using Windows.Storage.FileProperties;
using Windows.Storage.Search;
using System.Linq;

namespace ImagenesMercadoLibre.UWP.Services
{
    public class FileHelper
    {
        public async Task<string> GetPathStreamAsync()
        {
            string path = null;
            try
            {
                // Create and initialize the FolderOpenPicker
                var openPicker = new FolderPicker
                {
                    ViewMode = PickerViewMode.Thumbnail,
                    SuggestedStartLocation = PickerLocationId.Desktop,
                };
                openPicker.FileTypeFilter.Add("*");
                // Get a file and return a Stream 
                var storageFolder = await openPicker.PickSingleFolderAsync();
                if (storageFolder != null)
                {
                    //IRandomAccessStreamWithContentType raStream = await storageFolder.OpenReadAsync();
                    //return raStream.AsStreamForRead();
                    // Application now has read/write access to all contents in the picked folder
                    // (including other sub-folder contents)
                    //StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", storageFolder);
                    StorageApplicationPermissions.FutureAccessList.AddOrReplace("OutputFolder", storageFolder);
                    path = storageFolder.Path;
                }
                //else
                //{
                //    return null;
                //}
            }
            catch (Exception ex)
            {
                await Ex(ex);
            }
            return path;
        }
        public async Task DeleteAllFoldersAsync()
        {
            StorageFolder appFolder = await GetFolderAsync();
            // Get the files and folders in the current folder.
            IReadOnlyList<IStorageItem> itemsInFolder = await appFolder.GetItemsAsync();
            // Iterate over the results and print the list of items
            // to the Visual Studio Output window.
            foreach (IStorageItem item in itemsInFolder)
            {
                //if (item.IsOfType(StorageItemTypes.Folder))
                //    Debug.WriteLine("Folder: " + item.Name);
                //else
                //    Debug.WriteLine("File: " + item.Name + ", " + item.DateCreated);
                await item.DeleteAsync();
            }
        }
        public async Task<StorageFolder> CreatePath(string newPath)
        {
            StorageFolder storageFolder = null;
            try
            {
                //Debug.WriteLine("[CreatePath] GetFolder");
                StorageFolder folder = await GetFolderAsync();
                //Debug.WriteLine("[CreatePath] TryGetItemAsync :" + newPath);
                if (await folder.TryGetItemAsync(newPath) != null)
                    storageFolder = await folder.GetFolderAsync(newPath);                 
                else 
                    storageFolder = await folder.CreateFolderAsync(newPath);
            }
            catch (Exception ex)
            {
               await Ex(ex);
            }
            return storageFolder;
        }
        public async void CreateFile(IStorageFolder folder, string fileName)
        {
            try
            {
                await folder.CreateFileAsync(fileName);
            }
            catch (Exception ex)
            {
               await Ex(ex);
            }
        }

        //public string RememberFile(StorageFile file)
        //{
        //    string token = Guid.NewGuid().ToString();
        //    StorageApplicationPermissions.FutureAccessList.AddOrReplace(token, file);
        //    return token;
        //}
        //To retrieve the file the next time, you can use this:
        public async Task<StorageFile> GetFileForToken(string token)
        {
            Debug.WriteLine("[GetFileForToken]");
            StorageFile storageFile = null;
            try
            {
                if (StorageApplicationPermissions.FutureAccessList.ContainsItem(token))
                    storageFile = await StorageApplicationPermissions.FutureAccessList.GetFileAsync(token);
            }
            catch (Exception ex)
            {
               await Ex(ex);
            }
            return storageFile;
        }
        public async Task<string> GetFolderPathAsync()
        {
            string storageFolderPath = null;
            try
            {
                var storageFolder = await GetFolderAsync();
                storageFolderPath = storageFolder.Path;
            }
            catch (Exception ex)
            {
               await Ex(ex);
            }
            return storageFolderPath;
        }
        public async Task<StorageFolder> GetFolderAsync()
        {
            Debug.WriteLine("[GetFolderAsync]");
            StorageFolder storageFolder = null;
            try
            {
                storageFolder = await GetFolderForTokenAsync("OutputFolder");
            }
            catch (Exception ex)
            {
               await Ex(ex);
            }
            return storageFolder;
        }
        //public async Task<StorageFolder> GetStorageFolder(string subFolder)
        //{            
        //    return  await GetFolder().GetFolderAsync(subFolder);
        //}
        public async Task<StorageFolder> GetFolderForTokenAsync(string token)
        {
            Debug.WriteLine("[GetFolderForTokenAsync]");
            StorageFolder storageFolder = null;
            try
            {
                if (StorageApplicationPermissions.FutureAccessList.ContainsItem(token))
                    storageFolder = await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(token);
            }
            catch (Exception ex)
            {
               await Ex(ex);
            }
            return storageFolder;
        }
        //public async Task StartDownload1(string fileUrl, string fileName, string folderName)            
        //{
        //    try
        //    {
        //        fileName = fileName + ".jpg";
        //        StorageFolder storageFolder = await CreatePath(folderName);                        
        //        StorageFile sf = null;
        //        if (await storageFolder.TryGetItemAsync(fileName) != null)
        //        {
        //            sf = await storageFolder.GetFileAsync(fileName);
        //        }
        //        else
        //        {
        //            //if (sf == null) 
        //            sf = await storageFolder.CreateFileAsync(fileName);//, CreationCollisionOption.GenerateUniqueName);
        //        }                
        //        //downloadFolder = (await sf.GetParentAsync()).ToString();
        //        HttpClient client = new HttpClient();
        //        byte[] buffer = await client.GetByteArrayAsync(fileUrl);
        //        using (Stream stream = await sf.OpenStreamForWriteAsync())
        //        {
        //            stream.Write(buffer, 0, buffer.Length);
        //        }
        //        //path = sf.Path;
        //    }
        //    catch (Exception e)
        //    {
        //        MessageDialog dialog = new MessageDialog("Sorry, something went wrong...", "An error...");
        //        await dialog.ShowAsync();
        //    }
        //}
        public async Task StartDownload(string url, string fileName, string path)
        {
            try
            {
                fileName = fileName + ".jpg";
                Debug.WriteLine("[CreatePath]: " + path);
                StorageFolder storageFolder = await CreatePath(path);
                StorageFile sf = null;
                Debug.WriteLine("[TryGetItemAsync]: " + fileName);
                if (await storageFolder.TryGetItemAsync(fileName) != null)
                {
                    sf = await storageFolder.GetFileAsync(fileName);
                    //await sf.DeleteAsync();
                }
                else
                    sf = await storageFolder.CreateFileAsync(fileName);//, CreationCollisionOption.GenerateUniqueName);
                //WebClient client = new WebClient();
                MyWebClient client = new MyWebClient();
                Uri uri = new Uri(url);

                var fileWithPath = storageFolder.Path + "\\" + fileName;// + ".jpg";

                // Specify that the DownloadFileCallback method gets called
                // when the download completes.
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCallback);
                // Specify a progress notification handler.
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);

                //await Task.Delay(TimeSpan.FromSeconds(1));

                //client.DownloadFileAsync(uri, sf.Path);// fileWithPath);
                byte[] buffer = null;
                try
                {
                    Debug.WriteLine("[StartDownload] picture: " + fileName + " de " + path + ", url: " + url);
                    buffer = await client.DownloadDataTaskAsync(uri);
                    Debug.WriteLine("[FinishedDownload] picture: " + fileName + " de " + path + ", url: " + url);
                }
                catch (WebException webEx)
                {
                    WebEx(webEx);
                }
                if (buffer != null)
                {
                    using (Stream stream = await sf.OpenStreamForWriteAsync())
                    {
                        Debug.WriteLine("[StartCopy] picture: " + fileName + " de " + path + ", url: " + url);
                        stream.Write(buffer, 0, buffer.Length);
                        Debug.WriteLine("[FinishedCopy] picture: " + fileName + " de " + path + ", url: " + url);
                    }
                }
                //client.CancelAsync();
            }
            catch (Exception ex)
            {
               await Ex(ex);
            }            
        }

        private class MyWebClient : WebClient
        {
            protected override WebRequest GetWebRequest(Uri uri)
            {
                WebRequest w = base.GetWebRequest(uri);
                w.Timeout = 10000;//20 * 60 * 1000;  
                if (w is HttpWebRequest httpWebRequest)
                {
                    httpWebRequest.ReadWriteTimeout = w.Timeout;
                }
                return w;
            }
        }

        private void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void DownloadFileCallback(object sender, AsyncCompletedEventArgs e)
        {
            //throw new NotImplementedException();
        }
        //public async Task<PictureModel> PictureUpload(string url, string fileName, string path)
        //{
        //    //Carga una imagen
        //    //Ahora es momento de cargar tu primer archivo de imagen para almacenarlo en nuestros servidores. Es realmente muy fácil: solo anota la ruta exacta donde tienes guardada la imagen:
        //    //curl - F file =@/ home / user / picture.jpg         
        //    //https://api.mercadolibre.com/pictures?access_token=$ACCESS_TOKEN
        //    //Como respuesta recibirás un JSON con la descripción de los detalles de la imagen. Recuerda guardar el ID de la imagen.Los otros campos representan diferentes tamaños de imágenes.            
        //    //{
        //    //  "id":"MLA430387888_032012",
        //    //   "quality":"",
        //    //   "variations":[...]
        //    //}                       
        //    var storageFolder = await GetFolder().GetFolderAsync(path);
        //    StorageFile sf = null;
        //    //if (await storageFolder.TryGetItemAsync(fileName) != null)
        //        sf = await storageFolder.GetFileAsync(fileName);            

        //    PictureModel picture = new PictureModel();
        //    //var filePath = Path.Combine(itemPath, fileName); //itemPath + "//" + fileName;
        //    var oWeb = new WebClient();
        //    //            var baseUrl = "https://api.mercadolibre.com/pictures?access_token=" + accessToken;//meli.Credentials.AccessToken;

        //    Uri uri = new Uri(url);
        //    //var responseBytes = oWeb.UploadFile(url, @"" + sf.Path + "");
        //    try
        //    {
        //        BackgroundUploader uploader = new BackgroundUploader();
        //        uploader.SetRequestHeader("Filename", sf.Name);

        //        //UploadOperation upload = uploader.CreateUpload(uri, sf);
        //        var operation = await Task.Run(() => { return uploader.CreateUpload(uri, sf); });                
        //        var r = operation.GetResponseInformation();
        //        // Attach progress and completion handlers.
        //        //await HandleUploadAsync(upload, true);

        //        //var responseBytes = oWeb.UploadData(uri, await GetBytesAsync(sf));
        //        //if (!(responseBytes == null))
        //        {
        //            var content = Encoding.ASCII.GetString(responseBytes);
        //            if (!(content is null))
        //            {
        //                JObject rss = JObject.Parse(content);
        //                picture.ID = (string)rss["id"];
        //                picture.url = (string)rss["variations"][0]["url"];
        //                //File.Move(filePath, Path.Combine(itemPath, picture.ID + "-F.jpg"));
        //                await sf.RenameAsync(picture.ID + "-F.jpg");
        //                //await App.Database.SaveAsync(picture);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.ToString());
        //    }            
        //    return picture;
        //}
        public async Task<List<PictureModel>> PicturesPrepareUpload(string url, string itemId, string path)
        {
            var _pictureR = new PictureRepository();
            var _picture = new PictureModel();
            var picturesNew = new List<PictureModel>();
            var pictureId = "";
            var fileName = "";
            DateTimeOffset fileDate;
            PictureModel p = null;
            bool isUploadedNow = false;
            try
            {
                //String[] fileTypeFilter = new String[] { ".jpg", ".jpeg", ".png" };
                //// initialize queryOptions using a common query
                //QueryOptions queryOptions = new QueryOptions(CommonFileQuery.DefaultQuery, fileTypeFilter);
                //// clear all existing sorts
                //queryOptions.SortOrder.Clear();
                //// add descending sort by date
                //SortEntry se = new SortEntry();
                //se.PropertyName = "System.DateCreated";
                //se.AscendingOrder = true;
                //queryOptions.SortOrder.Add(se);
                var folder = await GetFolderAsync();
                if (await folder.TryGetItemAsync(path) != null)
                {
                    var di = await folder.GetFolderAsync(path);
                    //StorageFileQueryResult queryResult = di.CreateFileQueryWithOptions(queryOptions);
                    //if (queryResult != null)
                    {
                        //var files = await di.GetFilesAsync();
                        // Get the first 20 files in the current folder, sorted by date.
                        IReadOnlyList<StorageFile> files = await di.GetFilesAsync();// CommonFileQuery.OrderByDate, 0, 10);                        
                        int order = 0;
                        if (files != null)
                            foreach (var fi in files)
                            {
                                order += 1;
                                fileName = fi.Name;
                                fileDate = fi.DateCreated;
                                pictureId = fileName.Replace("-F", "");
                                for (int i = 0; i < 19; i++)                                
                                    pictureId = pictureId.Replace("["+i+"]", "");                              
                                pictureId = pictureId.Replace(".jpg", "");
                                _picture = await _pictureR.GetPictureAsync(pictureId);
                                if ((_picture is null) || (await isNow(_picture.date, fi)))
                                {
                                    p = await SendPicture(url, fileName, path, order);
                                    if (p != null) 
                                    {
                                        isUploadedNow = (p.url != null);
                                        p.item_id = itemId;
                                        p.date = fileDate.ToString();                                        
                                    }                                    
                                }
                                else
                                {
                                    p = _picture;
                                    p.date = fileDate.ToString();
                                };
                                if (p != null)
                                {
                                    p.order = order;
                                    picturesNew.Add(p);
                                }
                            }
                    }
                }                
            }
            catch (Exception ex)
            {
               await Ex(ex);
            }
            if (isUploadedNow)
                return picturesNew;//.OrderBy(x => x.date).ToList();//picturesNew;
            else
                return null;
        }
        public async Task<bool> isNow(string date, StorageFile file) 
        {
            if (date != null)
            {
                try
                {                    
                    DateTime oldDate = DateTime.Parse(date);
                    // Get file's basic properties.
                    BasicProperties basicProperties = await file.GetBasicPropertiesAsync();
                    //var fileSize = basicProperties.Size;
                    var dateModified = basicProperties.DateModified;
                    var difDate = dateModified - oldDate;
                    return (difDate.TotalMinutes > 1);
                }
                catch (Exception ex)
                {
                   await Ex(ex);
                    return true;
                }                
            } else return true;
        }
        public async Task<byte[]> GetBytesAsync(StorageFile file)
        {
            byte[] fileBytes = null;
            try
            {
                if (file == null) return null;
                using (var stream = await file.OpenReadAsync())
                {
                    fileBytes = new byte[stream.Size];
                    using (var reader = new DataReader(stream))
                    {
                        await reader.LoadAsync((uint)stream.Size);
                        reader.ReadBytes(fileBytes);
                    }
                }
            }
            catch (Exception ex)
            {
               await Ex(ex);
            }
            return fileBytes;
        }
        //public async void UploadData(byte[] imagen) //recibimos la imagen como parametro en el metodo
        //{
        //    var client = new HttpClient();
        //    using (client)
        //    {
        //        var objetoClase1 = new ObjetoClase()
        //        {
        //            ID = Guid.NewGuid(),
        //            MediaStream = imagen, //esta es la imagen representada en Byte[]
        //            Description = "dummy";
        //        };
        //        //La magia esta aqui
        //        var response = await client.PostAsync(new Uri("URL del WebAPI"), new HttpStringContent(JsonConvert.SerializeObject(objetoClase1), UnicodeEncoding.Utf8, "application/json"));

        //        if (response.IsSuccessStatusCode)
        //        {
        //            //Al entrar aqui es porque el POST hacia el webAPI fue exitoso (Codigo 200:OK)
        //        }
        //    }
        //}
        public async Task<PictureModel> SendPicture(string url, string fileName, string path, int order)
        {            
            StorageFolder storageFolder = null;
            StorageFile storageFile = null;
            Stream fileStream = null;
            PictureModel pictureUpload = null;
            try
            {
                //var storageFolder = await GetFolderAsync().GetFolderPathAsync(path);
                StorageFolder folder = await GetFolderAsync();
                //if (folder!=null)
                    storageFolder = await folder.GetFolderAsync(path);
                //if (storageFolder != null)
                {
                    storageFile = await storageFolder.GetFileAsync(fileName);                    
                    fileStream = await storageFolder.OpenStreamForReadAsync(fileName);
                    //if (fileStream != null)
                    {                        
                        MultipartFormDataContent dataContent = new MultipartFormDataContent();
                        using (HttpClient client = new HttpClient())
                        {
                            //using (var fileStream = File.OpenRead(@"c:\path\to\A3358.jpg"))
                            using (fileStream)
                            {
                                // create StreamContent from the file   
                                StreamContent fileValue = new StreamContent(fileStream);
                                // add the name and meta-data 
                                //dataContent.Add(fileValue, "file", "A3358.jpg");
                                dataContent.Add(fileValue, "file", storageFile.Name);
                                //string url = string.Format("https://api.mercadolibre.com/pictures?access_token={0}"
                                //    , txt_AT_ML.Text
                                //);
                                HttpResponseMessage response = await client.PostAsync(
                                          url,
                                          dataContent);
                                HttpContent responseContent = response.Content;
                                using (StreamReader reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
                                {
                                    Console.WriteLine(await reader.ReadToEndAsync());
                                }
                                if (responseContent != null)
                                {
                                    string content = Encoding.ASCII.GetString(await responseContent.ReadAsByteArrayAsync());
                                    if (!(content is null))
                                    {
                                        JObject rss = JObject.Parse(content);
                                        pictureUpload = new PictureModel();
                                        pictureUpload.ID = (string)rss["id"];
                                        pictureUpload.url = (string)rss["variations"][0]["url"];
                                        pictureUpload.date = DateTime.Now.ToString();
                                        pictureUpload.order = order;
                                        await storageFile.RenameAsync("[" + pictureUpload.order + "]" + pictureUpload.ID + "-F.jpg");                                        
                                    }
                                }
                            }
                        }
                    }
                }                                    
            }
            catch (Exception ex)
            {
               await Ex(ex);
            }
            return pictureUpload;
        }
        private async Task AddTextToFile(string fileName, String textToSave)
        {
            //var appFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var appFolder = await GetFolderAsync();
            var file = await appFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            await FileIO.AppendTextAsync(file, textToSave + Environment.NewLine);
            // Look in Output Window of Visual Studio for path to file
            Debug.WriteLine(String.Format("File is located at {0}", file.Path.ToString()));
        }
        public async Task WriteLog(string text)
        {
            await AddTextToFile("Log.txt", String.Format(text, DateTime.Now.Millisecond.ToString()));
        }
        public async Task Ex(Exception ex)
        {
            string text = ex.Message + "," + ex.StackTrace;
            await WriteLog(text);
            Debug.WriteLine(text);
            await Application.Current.MainPage.DisplayAlert(ex.Message, text, "Ok");
        }
        public async void WebEx(WebException webEx)
        {
            string text = webEx.ToString();
            await WriteLog(text);
            Console.WriteLine(text);
            if (webEx.Status == WebExceptionStatus.ConnectFailure)
            {
                Console.WriteLine("Are you behind a firewall?  If so, go through the proxy server.");
            }
            await Application.Current.MainPage.DisplayAlert(webEx.Message, text, "Ok");
        }
    }
}
