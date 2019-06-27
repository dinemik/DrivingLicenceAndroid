using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DrivingLicenceAndroidPCL.Class.Json;
using DrivingLicenceAndroidPCL.Interface;
using DrivingLicenceAndroidPCL.Interface.Json;
using SQLite;

namespace DrivingLicenceAndroidPCL.Class
{
    public class DownloadService
    {
        public static List<Topic> Topics { get; set; } = null;

        public static async Task<List<Topic>> DownloadTicketsAsync()
        {
            if(Topics == null)
            {
                var conStr = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DrivingLicenceDatabase.db3");
                
                using (var db = new SQLiteConnection(conStr))
                {
                    if(File.Exists(conStr))
                    {
                        db.CreateTable<Topic>();
                        db.CreateTable<Ticket>();
                    }

                    if (db.Table<Topic>().Count() == 0)
                    {
                        db.DropTable<Topic>();
                        db.DropTable<Ticket>();

                        db.CreateTable<Topic>();
                        db.CreateTable<Ticket>();

                        var topics = await DeserializeJson.GetTopicsAsync();
                        foreach (var item in topics)
                        {
                            db.Insert(item);
                        }
                    }
                
                    return db.Table<Topic>().ToList();
                }
            }

            return Topics;
        }
    }
}