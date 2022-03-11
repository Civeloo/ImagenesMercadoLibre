using System.Collections.Generic;
using System.Threading.Tasks;
using ImagenesMercadoLibre.Data;
using ImagenesMercadoLibre.Models;

namespace ImagenesMercadoLibre.Services.Item
{
    public class ItemRepository : IItemRepository
    {
        MyDatabase _database;
        public ItemRepository()
        {            
            _database = App.Database;
        }
        public void DeleteAllItem()
        {
            _database.DeleteAllAsync<ItemModel>();
        }
        public void DeleteItem(ItemModel model)
        {
            _database.DeleteAsync(model);
        }
        public List<ItemModel> GetAllItem()
        {
            return _database.GetAllItem();
        }
        public ItemModel GetItem(string id)
        {
            return _database.GetItem(id);
        }
        public ItemModel GetWithChildrenItem(string id)
        {
            return _database.GetWithChildrenItem(id);
        }
        public Task<int> SaveItem(ItemModel model)
        {
            return _database.SaveAsync(model);
        }
        public List<ItemModel> ItemGetSearchResults(string query) 
        {
            return _database.ItemGetSearchResults(query);                        
        }
        public List<string> ItemListToString(List<ItemModel> _itemModel)
        {
            List<string> stringList = new List<string>();
            _itemModel.ForEach(x => {
                stringList.Add(x.title.ToString());
            });
            return stringList;
        }
    }
}
