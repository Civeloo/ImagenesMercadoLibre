using System.Collections.Generic;
using System.Threading.Tasks;
using ImagenesMercadoLibre.Models;

namespace ImagenesMercadoLibre.Services.ML
{
    public interface IMercadolibreService
    {
        Task<List<MeModel>> RefreshMeAsync();
        Task SaveMeAsync(MeModel me, bool isNewMe);
        //Task DeleteMeAsync(string id);
        Task<List<ItemModel>> ItemRefreshAsync();
        //Task<PictureModel> PictureUpload(string itemPath, string fileName);
        Task ItemPictureUpdateAsync(string itemId);
        Task PictureAllDownloadAsync(List<ItemModel> items);
        void GetAuthCode();
        Task GetAccessTokenAsync(TokenModel token);
        Task UploadItems();
    }
}
