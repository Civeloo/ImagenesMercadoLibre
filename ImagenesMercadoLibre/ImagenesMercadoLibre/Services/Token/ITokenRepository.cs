using ImagenesMercadoLibre.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImagenesMercadoLibre.Services.Token
{
    interface ITokenRepository
    {
        Task<List<TokenModel>> TokenGetAllAsync();
        //Get Specific data  
        Task<TokenModel> TokenGetAsync(string id);
        // Delete all Data  
        void TokenDeleteAll();
        // Delete Specific  
        void TokenDelete(TokenModel model);
        // Insert new to DB   
        void TokenSave(TokenModel model);
    }
}
