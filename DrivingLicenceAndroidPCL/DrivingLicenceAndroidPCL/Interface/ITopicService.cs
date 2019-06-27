using DrivingLicenceAndroidPCL.Interface.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrivingLicenceAndroidPCL.Interface
{
    public interface ITopicService
    {
        Task<IEnumerable<ITicket>> GetOneTicketAsync(string name);
        Task<IEnumerable<ITicket>> GetByNamesAsync(IEnumerable<string> names, int count);
        Task<IEnumerable<ITopic>> GetAllTopicAsync();
    }
}