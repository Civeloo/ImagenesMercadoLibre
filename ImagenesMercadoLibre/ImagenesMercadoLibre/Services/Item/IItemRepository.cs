using System.Collections.Generic;
using System.Threading.Tasks;
using ImagenesMercadoLibre.Models;

namespace ImagenesMercadoLibre.Services.Item
{
    public interface IItemRepository
    {
        List<ItemModel> GetAllItem();
        //Get Specific data  
        ItemModel GetItem(string id);
        // Delete all Data  
        void DeleteAllItem();
        // Delete Specific  
        void DeleteItem(ItemModel model);
        // Insert new to DB   
        Task<int> SaveItem(ItemModel model);
        List<ItemModel> ItemGetSearchResults(string query);
    }
}
