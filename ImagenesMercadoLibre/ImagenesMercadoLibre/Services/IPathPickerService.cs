using ImagenesMercadoLibre.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ImagenesMercadoLibre.Services
{
    public interface IPathPickerService
    {
        Task<string> GetPathStreamAsync();
        Task DeleteAllFoldersAsync();
        Task<Windows.Storage.StorageFolder> CreatePath(string newPath);
        Task StartDownload(string url, string fileName, string path);
        Task<List<PictureModel>> PicturesPrepareUpload(string url, string itemId, string path);
        Task<string> GetFolderPathAsync();
        Task Ex(Exception ex);
        void WebEx(WebException webEx);
        Task WriteLog(string text);        
    }
}
