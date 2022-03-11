using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using ImagenesMercadoLibre.Models;
using System;
using SQLiteNetExtensionsAsync.Extensions;

namespace ImagenesMercadoLibre.Data
{
    public class MyDatabase
    {
        SQLiteAsyncConnection _database;
        public MyDatabase(string dbPath)
        {
            //var _database = DependencyService.Get<ISQLite>().GetConnectionAsync(dbPath);
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<TokenModel>().Wait();
            _database.CreateTableAsync<VariationModel>().Wait();
            _database.CreateTableAsync<ItemModel>().Wait();
            _database.CreateTableAsync<PictureModel>().Wait();
            _database.CreateTableAsync<MeModel>().Wait();
            _database.CreateTableAsync<SettingModel>().Wait();
        }

        public Task<List<TokenModel>> GetTokensAsync()
        {
            return _database.Table<TokenModel>().ToListAsync();
        }

        public Task<TokenModel> GetTokenAsync()//int id)
        {
            return _database.Table<TokenModel>()
                            //.Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }
        //public Task<int> SaveTokenAsync(Token token)
        //{
        //    if (token.ID > 0)
        //    {
        //        return _database.UpdateAsync(token);
        //    }
        //    else
        //    {
        //        return _database.InsertAsync(token);
        //    }
        //}
        //public Task<int> DeleteTokenAsync(Token token)
        //{
        //    return _database.DeleteAsync(token);
        //}
        public Task<List<object>> GetAsync(int id)
        {
            return _database.Table<List<object>>()
                            //.Where(i => i.id == id)
                            .FirstOrDefaultAsync();
        }
        public async Task<int> SaveAsync(Object model)
        {
            var rowsAffected = await _database.UpdateAsync(model);
            if (rowsAffected == 0)
            {
                return await _database.InsertAsync(model);
            }
            else return rowsAffected;
        }
        public async Task SaveWithChildrenAsync(Object model)
        {
            //var rowsAffected = 
            //await _database.UpdateWithChildrenAsync(model);
            //if (rowsAffected == 0)
            //{            
            await _database.InsertOrReplaceWithChildrenAsync(model, recursive: true);
            //}
        }
        public void Save(Object model)
        {
            var rowsAffected = _database.UpdateAsync(model).Result;
            if (rowsAffected == 0)
            {
                _database.InsertAsync(model);
            }
        }
        public Task<int> DeleteAsync(Object model)
        {
            return _database.DeleteAsync(model);
        }
        public async Task DeleteListAsync(List<Object> models)
        {
            foreach(var model in models)
                await _database.DeleteAsync(model);
        }
        public Task<int> DeleteAllAsync<T>()
        {
            return _database.DeleteAllAsync<T>();
        }

        public List<ItemModel> GetAllItem()
        {
            //return (from data in _database.Table<ItemModel>() select data).ToList();
            return _database.Table<ItemModel>().ToListAsync().Result;
        }
        public ItemModel GetItem(string id)
        {
            return _database.Table<ItemModel>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync().Result;
        }
        public ItemModel GetWithChildrenItem(string id)
        {
            return _database.GetWithChildrenAsync<ItemModel>(id).Result;
        }
        //public IEnumerable<T> GetByGuid(string guid)
        //{
        //    return _database.GetWithChildrenAsync<T>(guid).Result;
        //}
        //public T GetByGuid(string guid)
        //{
        //    return _database.GetWithChildrenAsync<T>(guid)
        //               .FirstOrDefault(i => i.EmployeeGuid == guid).Result;
        //}
        public List<ItemModel> ItemGetSearchResults(string q)
        {
            //var normalizedQuery = q?.ToLower() ?? "";
            return _database.Table<ItemModel>().Where(f => f.title
            //.ToLowerInvariant()
            .Contains(q)).ToListAsync().Result;
        }
        public Task<List<PictureModel>> GetPicturesAsync()
        {
            return _database.Table<PictureModel>().ToListAsync();
        }
        public Task<PictureModel> GetPictureAsync(string id)
        {
            return _database.Table<PictureModel>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }
        public async Task<List<PictureModel>> GetPicturesItemAsync(string item_id)
        {
            return await _database.Table<PictureModel>()
                            .Where(i => i.item_id == item_id)
                            .ToListAsync();
        }
        public SettingModel GetSetting()
        {
            return _database.Table<SettingModel>()
                    .FirstOrDefaultAsync()
                    .Result;
        }
        //Get All data
        public List<MeModel> GetAllMe()
        {
            return _database.Table<MeModel>().ToListAsync().Result;
            //return (from data in _database.Table<MeModel>()
            //        select data)
            //        .ToList();
        }
        //Get Specific data  
        public MeModel GetMe(int id)
        {
            return _database.Table<MeModel>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync().Result;
        }
    }
}
