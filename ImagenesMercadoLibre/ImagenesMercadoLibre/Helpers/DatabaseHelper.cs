using ImagenesMercadoLibre.Models;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace ImagenesMercadoLibre.Helpers
{
    public class DatabaseHelper
    {
        static SQLiteConnection sqliteconnection;
        //public const string DbFileName = "Contacts.db";
        public DatabaseHelper(string dbPath)//()
        {
            //sqliteconnection = DependencyService.Get<ISQLite>().GetConnection();
            sqliteconnection = new SQLiteConnection(dbPath);
            sqliteconnection.CreateTable<MeModel>();            
        }
        // Get All data      
        public List<MeModel> GetAllMe()
        {
            return (from data in sqliteconnection.Table<MeModel>()
                    select data).ToList();
        }
        //Get Specific data  
        public MeModel GetMe(int i)
        {
            return sqliteconnection.Table<MeModel>().FirstOrDefault(t => t.id == i);
        }
        // Delete all Data  
        public void DeleteAllMe()
        {
            sqliteconnection.DeleteAll<MeModel>();
        }
        // Delete Specific   
        public void DeleteMe(int id)
        {
            sqliteconnection.Delete<MeModel>(id);
        }
        // Insert new to DB   
        public void InsertMe(MeModel me)
        {
            sqliteconnection.Insert(me);
        }
        // Update Data  
        public void UpdateMe(MeModel me)
        {
            sqliteconnection.Update(me);
        }
    }
}
