using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System;
using SQLite;
using DrivingLicenceAndroidPCL.Model.Class.Json;
using DrivingLicenceAndroidPCL.Model.Class.DataBase;
using DrivingLicenceAndroidPCL.Model.Interface.DataBase;

namespace DrivingLicenceAndroidPCL.Class
{
    public class DownloadService
    {
        public static IEnumerable<ITopicDb> Topics { get; set; } = null;

        public static async Task<IEnumerable<ITopicDb>> DownloadTicketsAsync()
        {
            List<TopicJson> topics = (await DeserializeJson.GetTopicsAsync()).Cast<TopicJson>().ToList();

            /*
            if(Topics == null)
            {
                var conStr = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DrivingLicenceDatabase.db3");
                
                using (var db = new SQLiteConnection(conStr))
                {
                    List<TopicJson> topics = (await DeserializeJson.GetTopicsAsync()).Cast<TopicJson>().ToList();

                    
                     
                    if (File.Exists(conStr))
                    {
                        db.CreateTable<TopicDb>();
                        db.CreateTable<TicketDb>();
                        db.CreateTable<AnswerDb>();

                    }

                    if (db.Table<TopicDb>().Count() == 0)
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
                        }).ToList();

                        db.InsertAllWithChildren(jsonObjDb);

                        db.Commit();

                        var tst = db.GetAllWithChildren<TopicDb>();
                        var t = jsonObjDb.Select(o => o.TicketsDb.Select(i => i.Answers)).ToList();
                        foreach (var tickets in tst.SelectMany(o => o.TicketsDb))
                        {
                            foreach (var ans in t.SelectMany(o => o))
                            {
                                tickets.Answers = ans;
                                break;
                            }
                        }

                        Topics = jsonObjDb;
                    }

                     
                     
                     
                }
            }
            */

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
                       }).ToList();

            Topics = jsonObjDb;


            return Topics;
        }
    }
}