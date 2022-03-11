using HttpParamsUtility;
using ImagenesMercadoLibre.Models;
using MercadoLibre.SDK;
using MercadoLibre.SDK.Meta;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;
using ImagenesMercadoLibre.Services.Me;
using MercadoLibre.SDK.Models;
using ImagenesMercadoLibre.Services.Item;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.IO;
using ImagenesMercadoLibre.Services.Picture;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using RestSharp;

namespace ImagenesMercadoLibre.Services.ML
{
    public class MercadolibreService : IMercadolibreService
    {
        //private static readonly HttpClient httpclient = new HttpClient();

        MeliApiService meli;
        //MeliCredentials Credentials;
        long clientId = 5480711421985209;
        string clientSecret = "jXCjaf3TmYxUaA84GRVFEMDBgvTPLLFZ";
        string callBackUrl = "https://www.mercadolibre.com";
        string redirectUrl;
        TokenModel token;
        //public List<Me> me { get; private set; }
        public MercadolibreService()
        {
            
            OnCreate();
        }
        public async void OnCreate()
        {
            try
            {
                token = await App.Database.GetTokenAsync();
                if (token is null)
                {
                    meli = new MeliApiService { Credentials = new MeliCredentials(MeliSite.Argentina, clientId, clientSecret) };
                }
                else
                {
                    meli = new MeliApiService { Credentials = new MeliCredentials(MeliSite.Argentina, clientId, clientSecret, token.Access, token.Refresh) };
                }
                await GetRefreshTokenAsync(token);
            }
            catch (Exception ex)
            {
                //App.LoggerManager.Ex(ex);
            }
        }
        public void OnTokenChanged()
        {
            meli.Credentials.TokensChanged += (sender, args) => { doSomethingWithNewTokenValues(args.Info); };
        }
        public void GetAuthCode()
        {
            redirectUrl = meli.GetAuthUrl(clientId, MeliSite.Argentina, callBackUrl);
            Device.OpenUri(new Uri(redirectUrl));
        }
        //public async Task<bool> GetAccessTokenAsync(string code) => await m.AuthorizeAsync(code, callBackUrl);
        public async Task GetAccessTokenAsync1(TokenModel token)
        {
            try
            {
                var success = await meli.AuthorizeAsync(token.Auth, callBackUrl);
                if (success)
                {
                    token.Access = meli.Credentials.AccessToken;
                    //token.Auth = code;
                    token.Refresh = meli.Credentials.RefreshToken;
                    await App.Database.SaveAsync(token);
                    await RefreshMeAsync();
                }
            }
            catch (Exception ex)
            {
                App.LoggerManager.Ex(ex);
            }
        }
        public void GetAccessTokenAsync2(TokenModel token)
        {
            //var client = new RestClient("https://api.mercadolibre.com/items/" + itemId + "?access_token=" + meli.Credentials.AccessToken);
            var client = new RestClient("https://api.mercadolibre.com/oauth/token");
            var request = new RestRequest(Method.POST);
            //request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("Connection", "keep-alive");
            //request.AddHeader("Content-Length", "118");
            //request.AddHeader("Accept-Encoding", "gzip, deflate");
            //request.AddHeader("Host", "api.mercadolibre.com");
            //request.AddHeader("Postman-Token", "7ddce0d9-2f2f-411c-aed2-20aec9dc363c,0dd53a8e-d8c6-4201-b10d-1efd5a607c99");
            //request.AddHeader("Cache-Control", "no-cache");
            //request.AddHeader("Accept", "*/*");
            //request.AddHeader("User-Agent", "PostmanRuntime/7.19.0");
            //request.AddHeader("Content-Type", "application/json");
            //request.AddParameter("undefined", "{\r\n  \"pictures\":[\r\n    {\"source\":\"http://mla-s1-p.mlstatic.com/789712-MLA32829006190_112019-F.jpg\"}\r\n  ]\r\n}", ParameterType.RequestBody);            
            //request.AddParameter("undefined", json, ParameterType.RequestBody);
            request.AddParameter("grant_type", "authorization_code", ParameterType.UrlSegment);
            request.AddParameter("client_id", meli.Credentials.ClientId, ParameterType.UrlSegment);
            request.AddParameter("client_secret", meli.Credentials.ClientSecret, ParameterType.UrlSegment);
            request.AddParameter("code", token.Auth, ParameterType.UrlSegment);
            request.AddParameter("redirect_uri", callBackUrl, ParameterType.UrlSegment);
        }
        public async Task GetAccessTokenAsync(TokenModel token)
        {
            AuthorizeAsync(token);            
        }
        public async Task AuthorizeAsync(TokenModel token)
        {
            HttpClient httpclient = new HttpClient();
            try
            {                
                var url = "https://api.mercadolibre.com/oauth/token";
                var parameters = new Dictionary<string, string> {
                    { "grant_type", "authorization_code" },
                    { "client_id", meli.Credentials.ClientId.ToString() },
                    { "client_secret", meli.Credentials.ClientSecret },
                    { "code", token.Auth },
                    { "redirect_uri", callBackUrl }
                };                
                var encodedContent = new FormUrlEncodedContent(parameters);
                var response = await httpclient.PostAsync(url, encodedContent).ConfigureAwait(false);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // Do something with response. Example get content:
                    //var responseContent = await response.Content.ReadAsStringAsync ().ConfigureAwait (false);
                    var content = Encoding.ASCII.GetString(await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false));
                    if (!(content is null))
                    {
                        JObject rss = JObject.Parse(content);
                        token.Access = (string)rss["access_token"];
                        token.Refresh = (string)rss["refresh_token"];
                        App.Database.Save(token);
                        RefreshMeAsync().Wait();
                    }                    
                }
                //using (var client = new HttpClient())
                //{
                //    var encodedContent = new FormUrlEncodedContent(parameters);
                //    var response = await client.PostAsync(url,
                //                  encodedContent).ConfigureAwait(false);
                //    if (response.StatusCode == HttpStatusCode.OK)
                //    {
                //        // Do something with response. Example get content:
                //        // var responseContent = await response.Content.ReadAsStringAsync ().ConfigureAwait (false);
                //    }
                //    HttpContent responseContent = response.Content;
                //    using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
                //    {
                //        Console.WriteLine(await reader.ReadToEndAsync());
                //    }
                //    if (responseContent != null)
                //    {
                //        var content = Encoding.ASCII.GetString(await responseContent.ReadAsByteArrayAsync());
                //        if (!(content is null))
                //        {
                //            JObject rss = JObject.Parse(content);
                //            token.Access = (string)rss["access_token"];
                //            token.Refresh = (string)rss["refresh_token"];
                //            App.Database.Save(token);
                //            await RefreshMeAsync();
                //        }
                //    }
                //}
                httpclient.CancelPendingRequests();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }           
        }
        public async Task<bool> GetRefreshTokenAsync(TokenModel token)
        {
            //How do I refresh the access token?         
            //var credentials = new MeliCredentials(MeliSite.Argentina, 123456, "secret");
            //credentials.TokensChanged += (sender, args) => { doSomethingWithNewTokenValues(args.Info); };
            //var success = await MercadoLibreApiService.AuthorizeAsync(credentials, code, callBackUrl);
            if (!(meli.Credentials.RefreshToken == null))
            {
                //https://api.mercadolibre.com/oauth/token?grant_type=refresh_token&client_id=APP_ID&client_secret=SECRET_KEY&refresh_token=REFRESH_TOKEN
                HttpParams pRT = new HttpParams()
                    .Add("grant_type", "refresh_token")
                    .Add("client_id", meli.Credentials.ClientId)
                    .Add("client_secret", meli.Credentials.ClientSecret)
                    .Add("refresh_token", meli.Credentials.RefreshToken)//token.Refresh)
                    ;
                try
                {
                    var rRT = await meli.PostAsync("/oauth/token", pRT);
                    if (rRT.IsSuccessStatusCode)
                    {
                        var cRT = await rRT.Content.ReadAsStringAsync();
                        if (!(cRT is null))
                        {
                            var t = JsonConvert.DeserializeAnonymousType(cRT, new { refresh_token = "", access_token = "" });
                            token.Access = t.access_token;
                            token.Refresh = t.refresh_token;
                            await App.Database.SaveAsync(token);
                            //OnTokenChanged();
                        }
                    }
                }
                catch (Exception ex)
                {
                    //await Application.Current.MainPage.DisplayAlert(ex.Message, ex.StackTrace, "Ok");
                     App.LoggerManager.Ex(ex);
                }
            }
            return true;
        }
        void doSomethingWithNewTokenValues(TokenResponse info)
        {
            token.Access = info.AccessToken;
            token.Refresh = info.RefreshToken;
            App.Database.SaveAsync(token).Wait();
        }
        //public IRestResponse GetRest(string text)
        public async Task<string> GetRest(string resource, string param, string value)//, int offset)//, Token token)
        {
            string txtParam = "";

            token = await App.Database.GetTokenAsync();
            //if (meli.Credentials.AccessToken == null)
            if (token.Access==null)
            {
                //if (!await GetRefreshTokenAsync(token))
                //{
                await GetAccessTokenAsync(token);
        //}                
    }
    //Making GET calls
    //HttpParams p = new HttpParams().Add("access_token", m.Credentials.AccessToken);
    HttpParams p = new HttpParams()
        .Add("access_token", token.Access);

            //if (offset > 0)
            //{
            //    p.Add("offset", offset);
            //    txtOffset = "offset=" + offset + "&";
            //}
            if (param != "")
            {
                p.Add(param, value);
                txtParam = param + "=" + value + "&";
            }

            //HttpResponseMessage response = new ;            
            try
            {
                var response = await meli.GetAsync(resource + txtParam + "access_token=" + token.Access);
                OnTokenChanged();
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                //else
                //{
                //    HttpParams p1 = new HttpParams().Add("access_token", meli.Credentials.AccessToken);
                //    response = await meli.GetAsync(resource, p1);
                //    if (response.IsSuccessStatusCode)
                //    {                                        
                //        return await response.Content.ReadAsStringAsync();
                //    }
                //else return null;
                //}            
            }
            catch (Exception ex)
            {
                //await Application.Current.MainPage.DisplayAlert(ex.Message, ex.StackTrace, "Ok");
                 App.LoggerManager.Ex(ex);
            }
            return null;
        }
        public async Task<List<MeModel>> RefreshMeAsync()
        {
            var _meRepository = new MeRepository();
            try 
            {
                MeModel m = new MeModel();
                token = await App.Database.GetTokenAsync();
                var content = await GetRest("/users/me?", "", "");
                if (!(content is null))
                {
                    m = JsonConvert.DeserializeObject<MeModel>(content, Constants.JsnSerializerSettings);
                    await App.Database.SaveAsync(m);
                }
            }
            catch (Exception ex)
            {
                App.LoggerManager.Ex(ex);
            }            
            return _meRepository.GetAllMe();//App.Database.GetListAsync(new MeModel);
        }
        //public async Task<List<ItemModel>> ItemRefreshAsync()
        //{
        //    string content;
        //    int page = 0;
        //    int tpage = 0;
        //    string paging_total;

        //    int it; 
        //    try
        //    {
        //        do
        //        {
        //            page += 1;
        //            //ItemProgressBar = (page / 100);
        //            content = await GetRest("/users/" + App.UserId + "/items/search?status=active&", page);
        //            if (content != null)
        //            {
        //                JObject itemSearch = JObject.Parse(content);
        //                paging_total = itemSearch["paging"]["total"].ToString();
        //                if (paging_total != null)
        //                {
        //                    tpage = Int32.Parse(paging_total);
        //                }
        //                // get JSON result objects into a list
        //                IList<JToken> results = itemSearch["results"].Children().ToList();
        //                it = 0;
        //                foreach (JToken result in results)
        //                {
        //                    it += 1;
        //                    //Debug.WriteLine("pag: " + page + " de " + tpage + ", item: " + it.ToString());
        //                    await ItemPictureRefreshAsync(result.ToString());
        //                }
        //            }
        //        } while ((page < tpage));
        //    }
        //    catch (Exception ex)
        //    {
        //        //Debug.WriteLine(ex.Message+","+ex.StackTrace);
        //    }
        //    ItemRepository _itemRepository = new ItemRepository();
        //    return _itemRepository.GetAllItem();
        //}
        public async Task<List<ItemModel>> ItemRefreshAsync()
        {
            var itemR = new ItemRepository();
            itemR.DeleteAllItem();
            var pictureR = new PictureRepository();
            pictureR.DeleteAllPicture();
            string content;
            int loadingProgress = 0;
            int totalProgress = 0;
            string paging_total;            
            string param="";
            string value="";
            string url;
            IList<JToken> results = null;
            int it;            
            try
            {
                MessagingCenter.Send<string, string>("Progress", "Title", "Descargando Items");
                do
                {
                    //page += 1;
                    //ItemProgressBar = (page / 100);
                    //content = await GetRest("/users/" + App.UserId + "/items/search?status=active&", page);   
                    if (value != "") param = "scroll_id";
                    url = "/users/" + App.UserId + "/items/search?search_type=scan&status=active&";
                    content = await GetRest(url, param, value);
                    if (content != null)
                    {
                        JObject itemSearch = JObject.Parse(content);
                        value = itemSearch["scroll_id"].ToString();
                        paging_total = itemSearch["paging"]["total"].ToString();
                        if (paging_total != null)
                        {
                            totalProgress = Int32.Parse(paging_total);
                        }
                        MessagingCenter.Send<string, int>("Progress", "TotalProgress", totalProgress);
                        // get JSON result objects into a list
                        results = itemSearch["results"].Children().ToList();
                        it = 0;                        
                        if ((results != null) & (results.Count() > 0))
                            foreach (JToken result in results)
                            {
                                it += 1;
                                ////Debug.WriteLine("pag: " + page + " de " + tpage + ", item: " + it.ToString());                                
                                var item = new ItemModel();
                                item.ID = result.ToString();
                                await itemR.SaveItem(item);
                                loadingProgress += 1;
                                MessagingCenter.Send<string, int>("Progress", "LoadingProgress", loadingProgress);
                            }
                            ////Debug.WriteLine("pag: " + page + " de " + tpage);
                    }
                } while ((content != null) & (results != null) & (results.Count() > 0)); //((page < tpage));
                await ItemDetailRefreshAsync(itemR.GetAllItem());
            }
            catch (Exception ex)
            {
                ////Debug.WriteLine(ex.Message + "," + ex.StackTrace);
                 App.LoggerManager.Ex(ex);
            }            
            //await PictureAllDownloadAsync(items);
            return itemR.GetAllItem();
        }

        public async Task PictureAllDownloadAsync(List<ItemModel> items)
        {
            int loadingProgress = 0;
            int totalProgress = 0;
            try 
            {
                MessagingCenter.Send<string, string>("Progress", "Title", "Descargando Imágenes de Items");
                totalProgress = items.Count();
                MessagingCenter.Send<string, int>("Progress", "TotalProgress", totalProgress);
                var pictureR = new PictureRepository();
                foreach (ItemModel item in items)
                {
                    loadingProgress += 1;
                    MessagingCenter.Send<string, int>("Progress", "LoadingProgress", loadingProgress);
                    var sku = item.seller_custom_field;
                    if (sku == null) sku = item.seller_sku;
                    var pictures = await pictureR.GetPicturesItemAsync(item.ID);
                    foreach (PictureModel picture in pictures)
                    {
                        //Debug.WriteLine("[PictureDownload]: "+sku+" "+item.title);
                        picture.date = DateTime.Now.ToString();
                        await PictureDownload(picture, pathGenerated(sku, item.title));
                        pictureR.SavePicture(picture);
                    }
                }
            }
            catch (Exception ex)
            {
                 App.LoggerManager.Ex(ex);
            }
        }
        //public async Task ItemPictureRefreshAsync(string result)
        //{
        //    var item = new ItemModel();

        //    //string sku;
        //    // serialize JSON results into .NET objects              
        //    IList<JToken> pictureResults;
        //    JObject pictureSearch;
        //    //IList<JToken> attributeResults;
        //    JObject attributeSearch;
        //    JToken attribute;

        //    // JToken.ToObject is a helper method that uses JsonSerializer internally                    
        //    var content = await GetRest("/items/" + result + "?", 0);
        //    item = JsonConvert.DeserializeObject<ItemModel>(content, Constants.JsnSerializerSettings);

        //    attributeSearch = JObject.Parse(content);
        //    attribute = attributeSearch.SelectToken("$.attributes[?(@.id == 'SELLER_SKU')]");
        //    if (attribute != null)
        //    {
        //        item.seller_sku = (string)attribute.SelectToken("value_name");
        //    }
        //    //else {item.seller_sku = item.seller_custom_field};

        //    await App.Database.SaveAsync(item);

        //    pictureSearch = JObject.Parse(content);
        //    pictureResults = pictureSearch["pictures"].Children().ToList();
        //    foreach (JToken pictureResult in pictureResults)
        //    {
        //       await PictureRefresh(pictureResult.ToString(), item);
        //    }
        //}
        public async Task ItemDetailRefreshAsync(List<ItemModel> items)//(string result)
        {
            //string sku;
            // serialize JSON results into .NET objects              
            IList<JToken> pictureResults;
            JObject pictureSearch;
            //IList<JToken> attributeResults;
            JObject attributeSearch;
            JToken attribute;
            int loadingProgress = 0;            
            // JToken.ToObject is a helper method that uses JsonSerializer internally              
            try
            {
                MessagingCenter.Send<string, string>("Progress", "Title", "Descargando Detalle de Items");
                MessagingCenter.Send<string, int>("Progress", "LoadingProgress", loadingProgress);
                foreach (ItemModel i in items)
                {
                    var content = await GetRest("/items/" + i.ID + "?", "", "");
                    var item = JsonConvert.DeserializeObject<ItemModel>(content, Constants.JsnSerializerSettings);
                    attributeSearch = JObject.Parse(content);
                    attribute = attributeSearch.SelectToken("$.attributes[?(@.id == 'SELLER_SKU')]");
                    if (attribute != null)                    
                        item.seller_sku = (string)attribute.SelectToken("value_name");                    
                    else 
                        item.seller_sku = item.seller_custom_field ;
                    if (item.variations.Count > 0)
                        await App.Database.SaveWithChildrenAsync(item);
                    else
                        await App.Database.SaveAsync(item);
                    int order = 0;
                    pictureSearch = JObject.Parse(content);
                    pictureResults = pictureSearch["pictures"].Children().ToList();
                    foreach (JToken pictureResult in pictureResults)
                    {
                        order += 1;
                        await PictureRefresh(pictureResult.ToString(), item, order);
                    }
                    loadingProgress += 1;
                    MessagingCenter.Send<string, int>("Progress", "LoadingProgress", loadingProgress);
                }                
            } catch (Exception ex) { App.LoggerManager.Ex(ex); }
        }
        public async Task PictureRefresh(string pictureResult, ItemModel item, int order)
        {
            var picture = new PictureModel();
            var secureUrl = "";
            var url = "";
            try
            {
                picture = JsonConvert.DeserializeObject<PictureModel>(pictureResult, Constants.JsnSerializerSettings);
                url = picture.url;
                picture.url = url.Replace("-O", "-F");
                secureUrl = picture.secure_url;
                picture.secure_url = secureUrl.Replace("-O", "-F");
                picture.item_id = item.ID;
                picture.order = order;
                picture.date = DateTime.Now.ToString();
                var sku = item.seller_custom_field;
                if (sku == null)
                {
                    sku = item.seller_sku;
                }
                await App.Database.SaveAsync(picture);
            }
            catch (Exception ex)
            {
                ////Debug.WriteLine(ex.Message + "," + ex.StackTrace);
                 App.LoggerManager.Ex(ex);
            }            
        }
        public string pathGenerated(string sku, string title)
        {
            return "SKU"+sku + " " + RemoveInvalidChars(title);
        }

        public async Task SaveMeAsync(MeModel me, bool isNewMe = false)
        {
            await App.Database.SaveAsync(me);
        }
        //public async Task DownloadImageAsync(string imageUrl, string fileName, string path)
        //{
        //    var _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(15) };
        //    try
        //    {
        //        using (var httpResponse = await _httpClient.GetAsync(imageUrl))
        //        {
        //            if (httpResponse.StatusCode == HttpStatusCode.OK)
        //            {
        //                var pathImgSku = Path.Combine(App.PathImg, path);
        //                if (Directory.Exists(pathImgSku) == false) Directory.CreateDirectory(pathImgSku);
        //                //Thread.Sleep(1000);
        //                var fileWithPath = pathImgSku + "//" + fileName + ".jpg";
        //                using (FileStream fs = File.Create(fileWithPath))
        //                {
        //                    await httpResponse.Content.CopyToAsync(fs);
        //                    await fs.FlushAsync();
        //                    fs.Close();
        //                }
        //            }
        //            else
        //            {
        //                //Url is Invalid
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await Application.Current.MainPage.DisplayAlert(ex.Message, ex.StackTrace, "Ok");
        //    }
        //}
        //public async static void DownLoadFileInBackground(string imageUrl, string fileName, string path)
        //{
        //    try
        //    {
        //        WebClient client = new WebClient();
        //        Uri uri = new Uri(imageUrl);

        //        var pathImgSku = Path.Combine(App.PathImg, path);
        //        if (!(Directory.Exists(pathImgSku)))
        //        {
        //            //Directory.CreateDirectory(pathImgSku);
        //            Windows.Storage.StorageFolder storageFolder = await App.LoggerManager.CreatePath(path);//DependencyService.Get<IPathPickerService>().CreatePath(path);
        //            pathImgSku = storageFolder.Path;
        //        }
                    
        //        var fileWithPath = pathImgSku + "//" + fileName + ".jpg";

        //        // Specify that the DownloadFileCallback method gets called
        //        // when the download completes.
        //        client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DownloadFileCallback);
        //        // Specify a progress notification handler.
        //        client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);
        //        client.DownloadFileAsync(uri, fileWithPath);
        //    }
        //    catch (Exception ex)
        //    {
        //        //await Application.Current.MainPage.DisplayAlert(ex.Message, ex.StackTrace, "Ok");
        //         App.LoggerManager.Ex(ex);
        //    }
        //}

        private static void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private static void DownloadFileCallback(object sender, AsyncCompletedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public string RemoveInvalidChars(string filename)
        {
            return string.Concat(filename.Split(Path.GetInvalidFileNameChars()));
        }
        public async Task PictureDownload(PictureModel picture, string path)
        {
            try
            {                
                //Debug.WriteLine("picture: " + picture.ID + " de " + path + ", url: " + picture.secure_url+"/n");                
                //intenta descargar 2 veces cada 2 archivos
                if (picture.url != null)
                {                    
                    await App.LoggerManager.StartDownload(picture.url, "["+picture.order+"]"+picture.ID, path);                                
                }
            } catch (Exception ex) { App.LoggerManager.Ex(ex); }            
        }
        //public async Task<PictureModel> PictureUpload(string itemPath, string fileName)
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

        //    //string pictureId = "";
        //    //string pictureUrl = "";
        //    PictureModel picture = new PictureModel();
        //    var filePath = Path.Combine(itemPath, fileName); //itemPath + "//" + fileName;

        //    var oWeb = new WebClient();
        //    //oWeb.QueryString = reqparm;
        //    var baseUrl = "https://api.mercadolibre.com/pictures?access_token=" + meli.Credentials.AccessToken;
        //    var responseBytes = oWeb.UploadFile(baseUrl, @"" + filePath + "");
        //    if (!(responseBytes == null))
        //    //if (responseBytes.IsSuccessStatusCode)
        //    {
        //        //var content = await response.ReadAsStringAsync();
        //        var content = Encoding.ASCII.GetString(responseBytes);
        //        if (!(content is null))
        //        {
        //            //var t = JsonConvert.DeserializeAnonymousType(content, new { id = "" });
        //            //pictureId = t.id;
        //            JObject rss = JObject.Parse(content);
        //            picture.ID = (string)rss["id"];
        //            picture.url = (string)rss["variations"][0]["url"];
        //            File.Move(filePath, Path.Combine(itemPath, picture.ID + "-F.jpg"));
        //            await App.Database.SaveAsync(picture);
        //        }
        //    }
        //    return picture;
        //}

        async Task ItemPictureSet(string itemId, string pictureId)
        {
            //Vincula una imagen a tu artículo
            //Con el picture_id antes obtenido, puedes vincular la imagen a tu artículo, así:
            //  curl - X POST - H "Content-Type: application/json" - H "Accept: application/json" - d
            //'{
            //   "id":"MLA430387888_032012"
            //}'
            //https://api.mercadolibre.com/items/MLA421101451/pictures?access_token=$ACCESS_TOKEN
            //¡Eso es todo! Dirígete a la página de descripción de tu artículo(utilizando el campo permalink) y controla cómo se muestra tu imagen.
            var values = new Dictionary<string, string>
                {
                    { "id", pictureId }
                };
            var p = new HttpParams()
                .Add("access_token", meli.Credentials.AccessToken);
            try
            {
                var response = await meli.PostAsync("/items/" + itemId + "/pictures", p, values);
            }
            catch (Exception ex)
            {
                //await Application.Current.MainPage.DisplayAlert(ex.Message, ex.StackTrace, "Ok");
                 App.LoggerManager.Ex(ex);
            }
        }
        public async Task ItemPictureReplace(List<PictureModel> pictures, List<VariationModel> variations)//string itemId, string pictureId, string filePath)
        {
            //Reemplaza imágenes
            //Si necesitas reemplazar las imágenes actuales de un artículo, debes realizar una solicitud PUT incluyendo el ID del Artículo y el url de la imagen, con tu access_token como en el siguiente ejemplo:
            //  curl - X PUT - H "Content-Type: application/json" - H "Accept: application/json" - d
            //'{
            //  "pictures":[
            //    {"source":"http://www.apertura.com/export/sites/revistaap/img/Tecnologia/Logo_ML_NUEVO.jpg_33442984.jpg"},
            //    {"source":"http://appsuser.net/www/wp-content/uploads/2012/10/logo-mercadolibre.jpg"}
            //  ]
            //}' 
            //https://api.mercadolibre.com/items/{item_id}?access_token=$ACCESS_TOKEN
            //¡A tener en cuenta!
            //En caso que se desee reemplazar una imagen se deberá crear un nuevo source(darle otro nombre a la imagen) de lo contrario, al re utilizar el mismo que ya existía con diferente contenido no se actualizará la imagen.
            //En caso de tener un grupo de imágenes y deseas realizar las siguientes acciones: Agregar imagen: deberás mandar los IDs de las imágenes cargadas que deseas conservar más los source (URL) de las nuevas imágenes.Además, puedes modificar el orden enviando el body del PUT de la forma en que quieras verlas.
            //Para borrar la imagen, deberás mandar sólo los IDs de las imágenes cargadas que deseas conservar.
            string filePath = pictures[0].url;
            var itemId = pictures[0].item_id;
            var pictureR = new PictureRepository();
            JObject jO = new JObject(
                new JProperty("pictures",
                    new JArray(
                        from picture in pictures
                        select new JObject(
                            new JProperty("source", (string)picture.url)
                        )
                    )
                )
            );
            if (variations.Count() > 0)
            {
                jO.Add(
                    new JProperty("variations",// "variations": [{
                        new JArray(
                            from variation in variations
                            select new JObject(
                                new JProperty("id", (string)variation.ID),//"id": "16787985187",
                                //new JProperty("picture_ids", (List<string>)variation.pictures)//"picture_ids": ["http://SOURCE_IMAGEN_NUEVA.jpg","111111 - IMAGEN_EXISTENTE_111111"]
                                new JProperty("picture_ids",
                                    new JArray(
                                        from picture in pictures
                                        select new JValue((string)picture.url)                                        
                                    )
                                )
                            )
                        )
                    )
                );
            }
            string json = jO.ToString(Formatting.Indented);
            //var p = new HttpParams().Add("access_token", meli.Credentials.AccessToken);
            //var response = await meli.PutAsync("/items/" + itemId, p, json);
            var client = new RestClient("https://api.mercadolibre.com/items/" + itemId + "?access_token=" + meli.Credentials.AccessToken);
            var request = new RestRequest(Method.PUT);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("Connection", "keep-alive");
            //request.AddHeader("Content-Length", "118");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", "api.mercadolibre.com");
            //request.AddHeader("Postman-Token", "7ddce0d9-2f2f-411c-aed2-20aec9dc363c,0dd53a8e-d8c6-4201-b10d-1efd5a607c99");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            //request.AddHeader("User-Agent", "PostmanRuntime/7.19.0");
            request.AddHeader("Content-Type", "application/json");
            //request.AddParameter("undefined", "{\r\n  \"pictures\":[\r\n    {\"source\":\"http://mla-s1-p.mlstatic.com/789712-MLA32829006190_112019-F.jpg\"}\r\n  ]\r\n}", ParameterType.RequestBody);            
            request.AddParameter("undefined", json, ParameterType.RequestBody);
            try
            {
                var restResponse = client.Execute(request);
                //restResponse = client.Execute(request);
                if (restResponse.Content != "[]")
                {
                    var p = await pictureR.GetPicturesItemAsync(itemId);
                    pictureR.DeleteListPicture(p);
                    foreach (var picture in pictures)
                    {                        
                        pictureR.SavePicture(picture);
                    }
                }
            }
            catch (Exception ex)
            {
                //await Application.Current.MainPage.DisplayAlert(ex.Message, ex.StackTrace, "Ok");
                 App.LoggerManager.Ex(ex);
            }
        }        
        public async Task ItemPictureUpdateAsync(String itemId)
        {
            var url = "https://api.mercadolibre.com/pictures?access_token=" + meli.Credentials.AccessToken;
            var _itemR = new ItemRepository();                   
            try
            {
                //var _item = _itemR.GetItem(itemId);
                var _item = _itemR.GetWithChildrenItem(itemId);
                var sku = _item.seller_custom_field;
                if (sku == null) sku = _item.seller_sku;
                var itemPath = pathGenerated(sku, _item.title);
                var pictureNew = await App.LoggerManager.PicturesPrepareUpload(url, itemId, itemPath);
                if ( (pictureNew != null) && (pictureNew.Count>0) )
                    await ItemPictureReplace(pictureNew, _item.variations);
            }
            catch (Exception ex)
            {             
                App.LoggerManager.Ex(ex);
            }
        }
        public async Task UploadItems()
        {
            var itemsR = new ItemRepository();
            var items = itemsR.GetAllItem();
            int loadingProgress = 0;
            int totalProgress = 0;
            MessagingCenter.Send<string, string>("Progress", "Title", "Subiendo Imágenes de Items");
            totalProgress = items.Count();
            MessagingCenter.Send<string, int>("Progress", "TotalProgress", totalProgress);
            foreach (var item in items)
            {
                loadingProgress += 1;
                MessagingCenter.Send<string, int>("Progress", "LoadingProgress", loadingProgress);
                await ItemPictureUpdateAsync(item.ID);
            }
        }
    }
}