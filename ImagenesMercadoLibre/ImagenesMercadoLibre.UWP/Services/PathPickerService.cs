using ImagenesMercadoLibre.Models;
using ImagenesMercadoLibre.Services;
using ImagenesMercadoLibre.UWP.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(PathPickerService))]
namespace ImagenesMercadoLibre.UWP.Services
{
    public class PathPickerService : IPathPickerService
    {
        FileHelper fh = new FileHelper();
        public async Task<StorageFolder> CreatePath(string newPath)
        {
            return await fh.CreatePath(newPath);
        }
        public async Task<string> GetFolderPathAsync()
        {
            return await fh.GetFolderPathAsync();
        }        
        public async Task DeleteAllFoldersAsync()
        {
            await fh.DeleteAllFoldersAsync();
        }
        public async Task<string> GetPathStreamAsync()
        {
            return await fh.GetPathStreamAsync();
        }
        public async Task<List<PictureModel>> PicturesPrepareUpload(string url, string itemId, string path)
        {
            return await fh.PicturesPrepareUpload(url, itemId, path);
        }
        public async Task StartDownload(string url, string fileName, string path)
        {
            await fh.StartDownload(url, fileName, path);
        }
        public async Task Ex(Exception ex)
        {
            await fh.Ex(ex);
        }
        public void WebEx(WebException webEx)
        {
            fh.WebEx(webEx);
        }
        public async Task WriteLog(string text)
        {
            await fh.WriteLog(text);
        }
    }
}