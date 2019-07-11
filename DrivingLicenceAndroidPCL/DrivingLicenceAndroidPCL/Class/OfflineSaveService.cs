using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System;
using DrivingLicenceAndroidPCL.Model.Interface.DataBase;
using DrivingLicenceAndroidPCL.Model.Class.DataBase;
using SQLite;
using SQLiteNetExtensions.Extensions;
using DrivingLicenceAndroidPCL.Model.Class.Json;
using System.Linq;
using System.Net.Http;
using Android.Graphics;

namespace DrivingLicenceAndroidPCL.Class
{
    public class OfflineSaveService
    {
        public static IEnumerable<ITopicDb> Topics { get; set; } = null;
        private static int ImgCount { get; set; }
        
        public static async Task<IEnumerable<ITopicDb>> DownloadTicketsAsync(Action<int> action)
        {
            if (Topics == null)
            {
                Topics = await Task.Run<IEnumerable<ITopicDb>>(async () => 
                {
                    var conStr = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DrivingLicenceDatabase.db3");
                    
                    using (var db = new SQLiteConnection(conStr))
                    {
                        bool download = false;
                        List<TopicJson> topics = null;

                        //Tmp Code !!!!! .....
                            db.DropTable<TopicDb>();
                            db.DropTable<TicketDb>();
                            db.DropTable<AnswerDb>();
                        //>>>>>>>>........

                        try
                        {
                            topics = (await DeserializeJson.GetTopicsAsync()).Cast<TopicJson>().ToList();
                            try
                            {
                                download = topics.SelectMany(o => o.Tickets).Count() != db.Table<TicketDb>().Count();
                            }
                            catch (SQLiteException)
                            {
                                download = true;
                            }
                        }
                        catch (Java.Net.UnknownHostException)
                        {
                            return db.GetAllWithChildren<TopicDb>(recursive: true);
                        }
        
                        if (download)
                        {
                            db.DropTable<TopicDb>();
                            db.DropTable<TicketDb>();
                            db.DropTable<AnswerDb>();
        
                            db.CreateTable<TopicDb>();
                            db.CreateTable<TicketDb>();
                            db.CreateTable<AnswerDb>();

                            var jsonObjDb = topics.Select(o =>
                            new TopicDb
                            {
                                Name = o.Name,
                                TicketsDb = o.Tickets.Select(i =>
                                new TicketDb
                                {
                                    Coeficient = i.Coeficient,
                                    Desc = i.Desc,
                                    Filename = i.Filename,
                                    Question = i.Question,
                                    Answers = i.Answers.Select(ans =>
                                    new AnswerDb
                                    {
                                        Answ = ans,
                                        Correct = ans == i.Answers[i.CorrectAnswer - 1],
                                        TicketId = i.Id
                                    }).ToList()
                                }).ToList()
                            }).ToList()
                            //Tmp Code !!!!! .....
                                .Where(o => o.TicketsDb.Any(i => i.Filename != null/*tmp code !!!*/)).Take(1).ToList();
                                jsonObjDb.ForEach(o => o.TicketsDb = o.TicketsDb.Where(i => i.Filename != null).Take(10).ToList());

                                var tst = jsonObjDb.SelectMany(o => o.TicketsDb).Where(o => o.Filename != null);
                                ImgCount = tst.Count();
                            //>>>>>>>>>>>>>>>>>>>............


                            //ImgCount = jsonObjDb.SelectMany(o => o.TicketsDb).Where(o => o.Filename != null).Concat();
                            foreach (var item in jsonObjDb)
                            {
                                foreach (var tick in item.TicketsDb)
                                {
                                    tick.Filename = await ImageSaver(tick.Filename, action);
                                }
                            }

                            db.InsertAllWithChildren(jsonObjDb, true);
        
                            db.Commit();
                        }
        
                        return db.GetAllWithChildren<TopicDb>(recursive: true);
                    }
                });
            }
            return Topics;
        }
        
        private async static Task<string> ImageSaver(string url, Action<int> action)
        {
            if (url == null)
                return null;
            else
                action?.Invoke(ImgCount);
        
            var filename = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Guid.NewGuid().ToString() + ".png");
        
            using (HttpClient http = new HttpClient())
            {
                FileStream file = new FileStream(filename, FileMode.Create);
        
                var imgByte = await http.GetByteArrayAsync(url);
                var bmap = BitmapFactory.DecodeByteArray(imgByte, 0, imgByte.Length).Copy(Bitmap.Config.Rgb565, true);
        
                bmap.Compress(Bitmap.CompressFormat.Png, 100, file);
                file.Close();
            }
        
            return filename;
        }
    }
}