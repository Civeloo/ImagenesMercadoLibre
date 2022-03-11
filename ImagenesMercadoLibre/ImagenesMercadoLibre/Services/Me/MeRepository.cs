using ImagenesMercadoLibre.Models;
using System.Collections.Generic;
using ImagenesMercadoLibre.Data;
using System.Threading.Tasks;

namespace ImagenesMercadoLibre.Services.Me
{
    public class MeRepository : IMeRepository
    {
        MyDatabase _database;
        public MeRepository()
        {
            _database = App.Database;
        }        
        public void DeleteAllMe()
        {
            //_database.DeleteAllAsync("Me").Wait();
            _database.DeleteAllAsync<MeModel>().Wait();
        }
        public void DeleteMe(MeModel model)
        {
            _database.DeleteAsync(model);
        }
        public List<MeModel> GetAllMe()
        {
            return _database.GetAllMe();
        }
        public MeModel GetMe(int id)
        {
            return _database.GetMe(id);
        }
        public async Task SaveMe(MeModel model)
        {
            await _database.SaveAsync(model);
        }
    }
}
