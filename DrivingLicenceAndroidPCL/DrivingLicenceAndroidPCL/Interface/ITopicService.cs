using DrivingLicenceAndroidPCL.Interface.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrivingLicenceAndroidPCL.Interface
{
    public interface ITopicService
    {
        Task<IEnumerable<ITicket>> GetTopicsByNamesAsync(IEnumerable<string> names, int count);
        Task<IEnumerable<ITopic>> GetAllTopicAsync();
    }
}