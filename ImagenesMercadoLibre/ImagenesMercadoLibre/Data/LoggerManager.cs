using ImagenesMercadoLibre.Models;
using ImagenesMercadoLibre.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ImagenesMercadoLibre.Data
{
    public class LoggerManager
    {
        IPathPickerService log;
        public LoggerManager(IPathPickerService service)//ILoggerService service)
        {
            log = service;
        }
        public async Task<string> GetFolderPathAsync()
        {
            return await log.GetFolderPathAsync();
        }        
        public async Task DeleteAllFoldersAsync()
        {
            await log.DeleteAllFoldersAsync();
        }
        public async Task StartDownload(string url, string fileName, string path)
        {
            await log.StartDownload(url, fileName, path);
        }
        public async Task<List<PictureModel>> PicturesPrepareUpload(string url, string itemId, string path)
        {
            return await log.PicturesPrepareUpload(url, itemId, path);
        }        
        public void Ex(Exception ex)
        {
            log.Ex(ex);
        }
        public void WebEx(WebException webEx)
        {
            log.WebEx(webEx);
        }
    }
}
