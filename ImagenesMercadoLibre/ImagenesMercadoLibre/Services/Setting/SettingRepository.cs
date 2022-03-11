using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ImagenesMercadoLibre.Data;
using ImagenesMercadoLibre.Models;

namespace ImagenesMercadoLibre.Services.Setting
{
    class SettingRepository : ISettingRepository
    {
        MyDatabase _database;
        public SettingRepository()
        {
            _database = App.Database;
        }
        public SettingModel SettingGet()
        {
            return _database.GetSetting();
        }

        public Task<int> SettingSet(SettingModel model)
        {
            return _database.SaveAsync(model);
        }
    }
}
