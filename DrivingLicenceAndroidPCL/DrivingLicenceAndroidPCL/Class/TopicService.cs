using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DrivingLicenceAndroidPCL.Class.Json;
using DrivingLicenceAndroidPCL.Interface;
using DrivingLicenceAndroidPCL.Interface.Json;
using SQLite;
using SQLitePCL;

namespace DrivingLicenceAndroidPCL.Class
{
    public class TopicService : ITopicService
    {
        public async Task<ITopic> GetOneTopic(string name)
        {
            var topics = await DownloadService.DownloadTicketsAsync();
            return topics.FirstOrDefault(o => o.Name == name);
        }

        public async Task<IEnumerable<ITopic>> GetByIDs(IEnumerable<string> names)
        {
            var topics = await DownloadService.DownloadTicketsAsync();
            return topics.Where(o => names.Any(i => i == o.Name));
        }

        public async Task<IEnumerable<ITopic>> GetAllTopicAsync() =>
             await DownloadService.DownloadTicketsAsync();
    }
}