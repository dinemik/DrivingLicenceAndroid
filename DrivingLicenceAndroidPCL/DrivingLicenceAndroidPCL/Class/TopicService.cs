using DrivingLicenceAndroidPCL.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DrivingLicenceAndroidPCL.Linq;
using DrivingLicenceAndroidPCL.Model.Interface.Json;

namespace DrivingLicenceAndroidPCL.Class
{
    public class TopicService : ITopicService
    {
        /*
        * Filtring Tickets 
        * 1. Get selected topics
        * 2. Taking All Tickets
        * 3. Shuffle
        * 4. Taking --int count-- questions
        */
        public async Task<IEnumerable<ITicketJson>> GetTopicsByNamesAsync(IEnumerable<string> names, int count)
        {
            var topics = await OfflineSaveService.DownloadTicketsAsync();
            var tickets = topics.Where(o => names.Any(i => i == o.Name)).SelectMany(o => o.Tickets).ToList().Shuffle();
            return tickets.Count() >= count ? tickets.Take(count) : tickets;
        }

        /*
         * Get All Topics
         */
        public async Task<IEnumerable<ITopicJson>> GetAllTopicAsync() =>
             await OfflineSaveService.DownloadTicketsAsync();
    }
}