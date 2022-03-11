using ImagenesMercadoLibre.Models;
using ImagenesMercadoLibre.Services.Token;
using ImagenesMercadoLibre.ViewModels.Base;
using System.Collections.Generic;

namespace ImagenesMercadoLibre.ViewModels.Token
{
    public class TokenBaseViewModel : ViewModelBase//INotifyPropertyChanged
    {
        private TokenModel _token = new TokenModel();    
        private ITokenRepository _tokenRepository = new TokenRepository();
        internal ITokenRepository TokenRepository 
        { 
            get => _tokenRepository; 
            //set => _tokenRepository = value; 
        }
        public TokenModel Token
        {
            get => _token;
            set
            {
                _token = value;
                RaisePropertyChanged();
            }
        }
        public int ID
        {
            get => Token.ID;
            set
            {
                Token.ID = value;
                RaisePropertyChanged();
            }
        }
        public string Auth
        {
            get => _token.Auth;
            set
            {
                _token.Auth = value;
                RaisePropertyChanged();
            }
        }
        public string Access
        {
            get => Token.Access;
            set
            {
                Token.Access = value;
                RaisePropertyChanged();
            }
        }
        public string Refresh
        {
            get => Token.Refresh;
            set
            {
                Token.Refresh = value;
                RaisePropertyChanged();
            }
        }
        List<TokenModel> _tokenList;
        public List<TokenModel> TokenList
        {
            get => _tokenList;
            set
            {
                _tokenList = value;
                RaisePropertyChanged();
            }
        }
        TokenModel _tokenSelected;
        public TokenModel TokenSelected
        {
            get => _tokenSelected;
            set
            {
                _tokenSelected = value;
                RaisePropertyChanged();
            }
        }
    }
}
