using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrivingLicenceAndroidPCL.Enums;
using DrivingLicenceAndroidPCL.Model.Interface.DataBaseIncorrect;
using DrivingLicenceAppPCL.Models.Class;
using DrivingLicenceAppPCL.Models.Interface;

namespace DrivingLicenceAndroidPCL.Class
{
    public static class GetStatisticsService
    {
        public static async Task<IEnumerable<ITicketAnsweredStatistic>> GetStatisticAsync(GetStatisticsBy by)
        {
            switch (by)
            {
                case GetStatisticsBy.ByDay:
                    return await GetByDayStatistic();

                case GetStatisticsBy.ByHrs:
                    return await GetByHrsStatistic();

                case GetStatisticsBy.ByMin:
                    return await GetByMinStatistic();

                case GetStatisticsBy.ByMonth:
                    return await GetByMonthStatistic();

                default:
                    throw new System.Exception("Incorrect Command");
            }
        }

        public static async Task<bool> DeleteStatistic() =>
            await new AnsweredService().DeleteStatistic();


        private static async Task<IEnumerable<ITicketAnsweredStatistic>> GetByDayStatistic()
        {
            return await Task.Run(async () => {
                var tickets = await new AnsweredService().GetIncorrectTicketAsync(GetTicketBy.All);
                var tickSortByTimt = new List<List<ITicketIncorrectDb>>();

                for (int i = 0; i < tickets.Count(); i++)
                {
                    var ct = tickets.Where(o => o.Time.Day == tickets.ElementAt(i).Time.Day).ToList();
                    tickSortByTimt.Add(ct);
                    i += ct.Count();
                }

                return tickSortByTimt.Select(ticketLst => new TicketAnsweredStatistic
                {
                    Correct = ticketLst.Where(ticket => ticket.Answers.First(ans => ans.AnsId == ticket.UserAnswerId).Correct).Count(),
                    Incorrect = ticketLst.Where(ticket => !ticket.Answers.First(ans => ans.AnsId == ticket.UserAnswerId).Correct).Count(),
                    Time = ticketLst.First().Time
                });
            });
        }

        private static async Task<IEnumerable<ITicketAnsweredStatistic>> GetByMinStatistic()
        {
            return await Task.Run(async () => {
                var tickets = await new AnsweredService().GetIncorrectTicketAsync(GetTicketBy.All);
                var tickSortByTimt = new List<List<ITicketIncorrectDb>>();

                for (int i = 0; i < tickets.Count(); i++)
                {
                    var ct = tickets.Where(o => o.Time.Minute == tickets.ElementAt(i).Time.Minute).ToList();
                    tickSortByTimt.Add(ct);
                    i += ct.Count();
                }

                return tickSortByTimt.Select(ticketLst => new TicketAnsweredStatistic
                {
                    Correct = ticketLst.Where(ticket => ticket.Answers.First(ans => ans.AnsId == ticket.UserAnswerId).Correct).Count(),
                    Incorrect = ticketLst.Where(ticket => !ticket.Answers.First(ans => ans.AnsId == ticket.UserAnswerId).Correct).Count(),
                    Time = ticketLst.First().Time
                });
            });
        }

        private static async Task<IEnumerable<ITicketAnsweredStatistic>> GetByHrsStatistic()
        {
            return await Task.Run(async () => {
                var tickets = await new AnsweredService().GetIncorrectTicketAsync(GetTicketBy.All);
                var tickSortByTimt = new List<List<ITicketIncorrectDb>>();

                for (int i = 0; i < tickets.Count(); i++)
                {
                    var ct = tickets.Where(o => o.Time.Hour == tickets.ElementAt(i).Time.Hour).ToList();
                    tickSortByTimt.Add(ct);
                    i += ct.Count();
                }

                return tickSortByTimt.Select(ticketLst => new TicketAnsweredStatistic
                {
                    Correct = ticketLst.Where(ticket => ticket.Answers.First(ans => ans.AnsId == ticket.UserAnswerId).Correct).Count(),
                    Incorrect = ticketLst.Where(ticket => !ticket.Answers.First(ans => ans.AnsId == ticket.UserAnswerId).Correct).Count(),
                    Time = ticketLst.First().Time
                });
            });
        }

        private static async Task<IEnumerable<ITicketAnsweredStatistic>> GetByMonthStatistic()
        {
            return await Task.Run(async () => {
                var tickets = await new AnsweredService().GetIncorrectTicketAsync(GetTicketBy.All);
                var tickSortByTimt = new List<List<ITicketIncorrectDb>>();

                for (int i = 0; i < tickets.Count(); i++)
                {
                    var ct = tickets.Where(o => o.Time.Month == tickets.ElementAt(i).Time.Month).ToList();
                    tickSortByTimt.Add(ct);
                    i += ct.Count();
                }

                return tickSortByTimt.Select(ticketLst => new TicketAnsweredStatistic
                {
                    Correct = ticketLst.Where(ticket => ticket.Answers.First(ans => ans.AnsId == ticket.UserAnswerId).Correct).Count(),
                    Incorrect = ticketLst.Where(ticket => !ticket.Answers.First(ans => ans.AnsId == ticket.UserAnswerId).Correct).Count(),
                    Time = ticketLst.First().Time
                });
            });
        }
    }
}