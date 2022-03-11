using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImagenesMercadoLibre.Models
{
    [Table("Setting")]
    public class SettingModel
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string PathImage { get; set; }
    }
}
