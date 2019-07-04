using DrivingLicenceAndroidPCL.Model.Interface.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrivingLicenceAndroidPCL.Interface
{
    public interface ITopicService
    {
        Task<IEnumerable<ITicketJson>> GetTopicsByNamesAsync(IEnumerable<string> names, int count);
        Task<IEnumerable<ITopicJson>> GetAllTopicAsync();
    }
}