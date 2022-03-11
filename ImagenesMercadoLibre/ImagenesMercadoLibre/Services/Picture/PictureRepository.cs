using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ImagenesMercadoLibre.Data;
using ImagenesMercadoLibre.Models;

namespace ImagenesMercadoLibre.Services.Picture
{    
    public class PictureRepository : IPictureRepository
    {
        MyDatabase _database;        
        public PictureRepository() 
        { 
            _database = App.Database; 
        }
        public void DeleteAllPicture()
        {
            _database.DeleteAllAsync<PictureModel>();
        }
        public void DeletePicture(PictureModel model)
        {            
            _database.DeleteAsync(model);
        }
        public void DeleteListPicture(List<PictureModel> models)
        {
            foreach (var model in models)
                DeletePicture(model);
        }
        public Task<List<PictureModel>> GetAllPictureAsync()
        {
            return _database.GetPicturesAsync();
        }
        public async Task<PictureModel> GetPictureAsync(string id)
        {
            return await _database.GetPictureAsync(id);
        }
        public async Task<List<PictureModel>> GetPicturesItemAsync(string item_id)
        {
            return await _database.GetPicturesItemAsync(item_id);
        }

        public void SavePicture(PictureModel model)
        {
            _database.Save(model);
        }
    }
}
