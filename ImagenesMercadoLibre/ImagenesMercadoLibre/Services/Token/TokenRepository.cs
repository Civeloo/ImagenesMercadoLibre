using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ImagenesMercadoLibre.Models;

namespace ImagenesMercadoLibre.Services.Token
{
    class TokenRepository : ITokenRepository
    {
        public void TokenDelete(TokenModel model)
        {
            throw new NotImplementedException();
        }

        public void TokenDeleteAll()
        {
            throw new NotImplementedException();
        }

        public async Task<List<TokenModel>> TokenGetAllAsync()
        {
            return await App.Database.GetTokensAsync();
        }

        public Task<TokenModel> TokenGetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public void TokenSave(TokenModel model)
        {
            throw new NotImplementedException();
        }
    }
}
