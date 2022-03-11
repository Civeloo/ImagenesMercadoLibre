using SQLite;

namespace ImagenesMercadoLibre.Models
{
    [Table("Token")]
    public class TokenModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Auth { get; set; }
        public string Access { get; set; }
        public string Refresh { get; set; }
    }
}
