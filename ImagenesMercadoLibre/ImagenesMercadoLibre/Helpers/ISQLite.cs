using SQLite;

namespace ImagenesMercadoLibre.Helpers
{
    interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
