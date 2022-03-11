using System;
using System.Runtime.Serialization;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace ImagenesMercadoLibre.Models
{
    [Table("Picture")]
    [DataContract]
    public class PictureModel
    {
        [PrimaryKey]
        [DataMember(Name = "id")]
        public string ID { get; set; }
        public int order { get; set; }
        public string item_id { get; set; }
        [DataMember(Name = "url")]
        public string url { get; set; }
        [DataMember(Name = "secure_url")]
        public string secure_url { get; set; }
        [DataMember(Name = "size")]
        public string size { get; set; }
        [DataMember(Name = "max_size")]
        public string max_size { get; set; }
        [DataMember(Name = "quality")]
        public string quality { get; set; }
        public string date { get; set; }
    }
}
