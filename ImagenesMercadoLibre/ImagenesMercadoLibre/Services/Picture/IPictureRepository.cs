using ImagenesMercadoLibre.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImagenesMercadoLibre.Services.Picture
{
    interface IPictureRepository
    {
        Task<List<PictureModel>> GetAllPictureAsync();
        //Get Specific data  
        Task<PictureModel> GetPictureAsync(string id);
        Task<List<PictureModel>> GetPicturesItemAsync(string item_id);
        // Delete all Data  
        void DeleteAllPicture();
        // Delete Specific  
        void DeletePicture(PictureModel model);
        void DeleteListPicture(List<PictureModel> models);
        // Insert new to DB   
        void SavePicture(PictureModel model);
    }
}
