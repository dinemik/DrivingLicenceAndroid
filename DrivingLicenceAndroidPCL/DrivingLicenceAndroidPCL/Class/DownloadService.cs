using DrivingLicenceAndroidPCL.Class.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Linq;
using DrivingLicenceAndroidPCL.Interface.Json;

namespace DrivingLicenceAndroidPCL.Class
{
    public class DownloadService
    {
        public static IEnumerable<ITopic> Topics { get; set; } = null;

        public static async Task<IEnumerable<ITopic>> DownloadTicketsAsync()
        {
            if(Topics == null)
            {
                var conStr = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DrivingLicenceDatabase.db3");
                
                using (var db = new SQLiteConnection(conStr))
                {
                    if (File.Exists(conStr))
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
                            foreach (var tick in item.Tickets)
                            {
                                db.Insert(tick);
                            }
                        }
                    }

                    /* TODO */
                    /* This is f**k */
                    var tickets = db.Table<Ticket>().ToList();
                    var topic = db.Table<Topic>().ToList();
                    topic.ForEach(o => o.Tickets = tickets.Where(i => i.TopicId == o.Id).ToList());

                    Topics = topic;

                    return Topics;
                }
            }

            return Topics;
        }
    }
}