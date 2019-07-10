using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DrivingLicenceAndroidPCL.Enums;
using DrivingLicenceAndroidPCL.Interface;
using DrivingLicenceAndroidPCL.Model.Class.DataBaseIncorrect;
using DrivingLicenceAndroidPCL.Model.Interface.DataBase;
using DrivingLicenceAndroidPCL.Model.Interface.DataBaseIncorrect;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace DrivingLicenceAndroidPCL.Class
{
    public class AnsweredService : IAnsweredService
    {
        private static string ConStr { get; set; }

        public AnsweredService() =>
            ConStr = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DrivingLicenceDatabase.db3");


        public async Task<IEnumerable<ITicketIncorrectDb>> GetIncorrectTicketAsync(GetTicketBy get)
        {
            switch (get)
            {
                case GetTicketBy.All:
                    return await GetAllIncorrectTickets();

                case GetTicketBy.ToDay:
                    return (await GetAllIncorrectTickets()).Where(o => o.Time.Day == DateTime.Now.Day);

                case GetTicketBy.Yesterday:
                    return (await GetAllIncorrectTickets()).Where(o => o.Time.Day == DateTime.Now.AddDays(-1).Day);

                case GetTicketBy.Hrs:
                    return (await GetAllIncorrectTickets()).Where(o => o.Time.AddHours(-1).Hour == DateTime.Now.AddHours(-1).Hour || 
                                                                       o.Time.Hour == DateTime.Now.Hour);
                default:
                    throw new Exception("Incorrect command");
            }
        }

        private async Task<IEnumerable<ITicketIncorrectDb>> GetAllIncorrectTickets()
        {
            using (SQLiteConnection db = new SQLiteConnection(ConStr))
            {
                return await Task.Run<IEnumerable<ITicketIncorrectDb>>(() => db.GetAllWithChildren<TicketIncorrectDb>().ToList());
            }
        }

        public async Task<bool> SaveUserAnswersAsync(IEnumerable<ITicketDb> tickets, IEnumerable<int> answersIDs)
        {
            return await Task.Run(() =>
            {
                using (SQLiteConnection db = new SQLiteConnection(ConStr))
                {
                    db.CreateTable<AnswerIncorrectDb>();
                    db.CreateTable<TicketIncorrectDb>();

                    List<TicketIncorrectDb> ticketsIncorrect = new List<TicketIncorrectDb>();

                    for (int i = 0; i < tickets.Count(); i++)
                    {
                        var tick = tickets.ElementAt(i);
                        var ans = answersIDs.ElementAt(i);

                        ticketsIncorrect.Add(new TicketIncorrectDb
                        {
                            Answers = tick.Answers.Select(o => new AnswerIncorrectDb
                            {
                                AnsId = o.Id,
                                Answ = o.Answ,
                                Correct = o.Correct
                            }).ToList(),
                            UserAnswerId = ans,
                            Coeficient = tick.Coeficient,
                            Desc = tick.Desc,
                            Filename = tick.Filename,
                            Question = tick.Question,
                            Time = DateTime.Now
                        });
                    }

                    db.InsertAllWithChildren(ticketsIncorrect);

                    db.Commit();

                    return true;
                }
            });
        }

        public async Task<bool> DeleteStatistic()
        {
            return await Task.Run(() => {
                using (SQLiteConnection db = new SQLiteConnection(ConStr))
                {
                    db.DropTable<AnswerIncorrectDb>();
                    db.DropTable<TicketIncorrectDb>();

                    db.Commit();
                    return true;
                }
            });
        }
    }
}