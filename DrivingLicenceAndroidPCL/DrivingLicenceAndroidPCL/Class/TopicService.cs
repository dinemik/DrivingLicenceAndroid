using DrivingLicenceAndroidPCL.Interface.Json;
using DrivingLicenceAndroidPCL.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DrivingLicenceAndroidPCL.Linq;
using DrivingLicenceAndroidPCL.Class.Json;

namespace DrivingLicenceAndroidPCL.Class
{
    public class TopicService : ITopicService
    {
        public async Task<IEnumerable<ITicket>> GetOneTicketAsync(string name)
        {
            var topics = await DownloadService.DownloadTicketsAsync();
            return topics.FirstOrDefault(o => o.Name == name).Tickets;
        }

        public async Task<IEnumerable<ITicket>> GetByNamesAsync(IEnumerable<string> names, int count)
        {
            var topics = await DownloadService.DownloadTicketsAsync();
            var topicsFiltred = topics.Where(o => names.Any(i => i == o.Name)).ToList();

            List<Ticket> randomTickets = new List<Ticket>();
            if (topicsFiltred.Count() == 1)
            {
                for (int i = 0; i < count; i++)
                {
                    if (i > topicsFiltred.Count())
                        i = 0;
                }

                topicsFiltred.First().Tickets.Shuffle();
                return topicsFiltred.First().Tickets.Take(30);
            }

            foreach (var item in topicsFiltred)
            {
                item.Tickets.Shuffle();
                randomTickets.Add(item.Tickets.First());
            }

            return randomTickets;
        }

        public async Task<IEnumerable<ITopic>> GetAllTopicAsync() =>
             await DownloadService.DownloadTicketsAsync();
    }
}