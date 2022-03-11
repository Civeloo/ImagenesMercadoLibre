using ImagenesMercadoLibre.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImagenesMercadoLibre.Services.Me
{
    public interface IMeRepository
    {
        List<MeModel> GetAllMe();
        //Get Specific data  
        MeModel GetMe(int ID);
        // Delete all Data  
        void DeleteAllMe();
        // Delete Specific  
        void DeleteMe(MeModel model);
        // Insert new to DB   
        Task SaveMe(MeModel model);
    }
}
