using DrivingLicenceAndroidPCL.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DrivingLicenceAndroidPCL.Linq;
using DrivingLicenceAndroidPCL.Model.Interface.DataBase;

namespace DrivingLicenceAndroidPCL.Class
{
    public class TopicService : ITopicService
    {
        public async Task<IEnumerable<ITicketDb>> GetTopicsByNamesAsync(IEnumerable<string> names, int count)
        {
            var topics = await DownloadService.DownloadTicketsAsync();
            var tickets = topics.Where(o => names.Any(i => i == o.Name)).SelectMany(o => o.TicketsDb).ToList().Shuffle();
            return tickets.Count() >= count ? tickets.Take(count) : tickets;
        }

        public async Task<IEnumerable<ITopicDb>> GetAllTopicAsync() =>
             await DownloadService.DownloadTicketsAsync();
    }
}