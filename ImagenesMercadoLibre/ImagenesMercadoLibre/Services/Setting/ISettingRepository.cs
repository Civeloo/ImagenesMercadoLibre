using ImagenesMercadoLibre.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImagenesMercadoLibre.Services.Setting
{
    interface ISettingRepository
    {
        SettingModel SettingGet();
        Task<int> SettingSet(SettingModel model);
    }
}
