using DrivingLicenceAndroidPCL.Model.Interface.DataBase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrivingLicenceAndroidPCL.Interface
{
    public interface ITopicService
    {
        Task<IEnumerable<ITicketDb>> GetTicketsByTopicNamesAsync(IEnumerable<string> Names, int count);
        Task<IEnumerable<ITopicDb>> GetAllTopicAsync();
    }
}