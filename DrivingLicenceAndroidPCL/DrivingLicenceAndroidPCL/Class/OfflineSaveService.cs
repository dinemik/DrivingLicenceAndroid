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

namespace DrivingLicenceAndroidPCL.Class
{
    public class OfflineSaveService
    {
        public static IEnumerable<ITopicDb> Topics { get; set; } = null;

        public static async Task<IEnumerable<ITopicDb>> DownloadTicketsAsync()
        {
            if (Topics == null)
            {
                Topics = await Task.Run<IEnumerable<ITopicDb>>(async () => 
                {
                    var conStr = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DrivingLicenceDatabase.db3");

                    using (var db = new SQLiteConnection(conStr))
                    {
                        if (File.Exists(conStr))
                        {
                            db.CreateTable<TopicDb>();
                            db.CreateTable<TicketDb>();
                            db.CreateTable<AnswerDb>();
                        }

                        if (db.Table<TopicDb>().Count() == 0)
                        {
                            List<TopicJson> topics = (await DeserializeJson.GetTopicsAsync()).Cast<TopicJson>().ToList();

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
                                    CorrectAnswer = i.CorrectAnswer,
                                    Cutoff = i.Cutoff,
                                    Desc = i.Desc,
                                    Filename = i.Filename,
                                    FileParent = i.FileParent,
                                    Question = i.Question,
                                    Timestamp = i.Timestamp,
                                    TopicId = i.TopicId,
                                    Answers = i.Answers.Select(ans =>
                                    new AnswerDb
                                    {
                                        Answ = ans,
                                        Correct = ans == i.Answers[i.CorrectAnswer - 1],
                                        TicketId = i.Id
                                    }).ToList()
                                }).ToList()
                            });

                            db.InsertAllWithChildren(jsonObjDb, true);

                            db.Commit();
                        }

                        return db.GetAllWithChildren<TopicDb>(recursive: true);
                    }
                });
            }
            return Topics;
        }
    }
}