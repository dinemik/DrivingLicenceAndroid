//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using DrivingLicenceAndroidPCL.Interface;
//using DrivingLicenceAndroidPCL.Model.Class.DataBase;
//using DrivingLicenceAndroidPCL.Model.Class.DataBaseAnswered;
//using DrivingLicenceAndroidPCL.Model.Interface.DataBase;
//using SQLite;
//using SQLiteNetExtensions.Extensions;
//using SQLitePCL;

//namespace DrivingLicenceAndroidPCL.Class
//{
//    public class AnsweredService : IAnsweredService
//    {
//        public async Task<bool> SaveUserAnswersAsync(IEnumerable<ITicketDb> tickets, IEnumerable<int> answersIDs)
//        {
//            return await Task.Run(() => {

//                var conStr = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DrivingLicenceAnsweredDatabase.db3");

//                using (SQLiteConnection db = new SQLiteConnection(conStr))
//                {
//                    //if (File.Exists(conStr))
//                    //{
//                    //    db.CreateTable<TopicAnsweredDb>();
//                    //    db.CreateTable<TicketAnsweredDb>();
//                    //    db.CreateTable<AnswerDb>();
//                    //}

//                    db.DropTable<TopicAnsweredDb>();
//                    db.DropTable<TicketAnsweredDb>();
//                    //db.DropTable<AnswerDb>();

//                    db.CreateTable<TopicAnsweredDb>();
//                    db.CreateTable<TicketAnsweredDb>();
//                    //db.CreateTable<AnswerDb>();

//                    var tick = tickets.Select(o => new TicketAnsweredDb
//                    {
//                        //Answers = o.Answers,
//                        Coeficient = o.Coeficient,
//                        CorrectAnswer = o.CorrectAnswer,
//                        Cutoff = o.Cutoff,
//                        Desc = o.Desc,
//                        Filename = o.Filename,
//                        FileParent = o.FileParent,
//                        Question = o.Question,
//                        Timestamp = o.Timestamp,
//                        Topic = new TopicAnsweredDb
//                        {
//                            Name = o.Topic.Name,
//                            Time = DateTime.Now,
//                        },
//                    }).Take(3).ToList();

//                    for (int i = 0; i < tick.Count(); i++)
//                    {
//                        tick[i].UserAnswerId = answersIDs.ElementAt(i);
//                    }

//                    db.InsertAll(tick);
//                    //db.InsertAllWithChildren(tick, false);

//                    db.Commit();

//                    var tst = db.Table<TicketAnsweredDb>().ToList();

//                    return true;
//                }
//            });
//        }
//    }
//}