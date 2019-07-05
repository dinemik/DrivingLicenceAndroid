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
        /*
        * Filtring Tickets 
        * 1. Get selected topics
        * 2. Taking All Tickets
        * 3. Shuffle
        * 4. Taking --int count-- questions
        */
        public async Task<IEnumerable<ITicketDb>> GetTicketsByTopicNamesAsync(IEnumerable<string> Names, int count)
        {
            var topics = await OfflineSaveService.DownloadTicketsAsync();
            var tickets = topics.Where(o => Names.Any(i => i == o.Name)).SelectMany(o => o.TicketsDb).ToList().Shuffle();
            return tickets.Count() >= count ? tickets.Take(count) : tickets;
        }

        /*
         * Get All Topics
         */
        public async Task<IEnumerable<ITopicDb>> GetAllTopicAsync() =>
             await OfflineSaveService.DownloadTicketsAsync();
    }
}