using ImagenesMercadoLibre.Services.ML;
using ImagenesMercadoLibre.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace ImagenesMercadoLibre.Data
{
    public class MercadolibreManager
    {
        IMercadolibreService mls;
        public MercadolibreManager(IMercadolibreService service)
        {
            mls = service;
        }
        public Task<List<MeModel>> GetMeAsync()
        {
            return mls.RefreshMeAsync();
        }
        public Task SaveMeAsync(MeModel me, bool isNewMe = false)
        {
            return mls.SaveMeAsync(me, isNewMe);
        }
        public Task<List<ItemModel>> ItemRefreshAsync()
        {
            return mls.ItemRefreshAsync();
        }
        public Task ItemPictureUpdateAsync(string itemId)
        {
            return mls.ItemPictureUpdateAsync(itemId);
        }
        public Task PictureAllDownloadAsync(List<ItemModel> items) 
        {
            return mls.PictureAllDownloadAsync(items);
        }
        public void GetAuthCode()
        {
            mls.GetAuthCode();
        }
        public Task GetAccessTokenAsync(TokenModel token)
        {
            return mls.GetAccessTokenAsync(token);
        }
        public async Task UploadItems()
        {
            await mls.UploadItems();
        }
    }    
}
