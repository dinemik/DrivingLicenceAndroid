using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrivingLicenceAndroidPCL.Class;
using DrivingLicenceAndroidPCL.Model.Interface.DataBaseIncorrect;
using DrivingLicenceApp.Models.Class;
using DrivingLicenceApp.Models.Interface;

namespace DrivingLicenceApp.Class
{
    public static class GetStatistics
    {
        public static async Task<IEnumerable<ITicketAnsweredStatistic>> GetStatisticAsync()
        {
            return await Task.Run(async () => {
                var tickets = await new AnsweredService().GetIncorrectTicketAsync(DrivingLicenceAndroidPCL.Enums.GetTicketBy.All);
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
    }
}