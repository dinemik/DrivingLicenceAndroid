using DrivingLicenceAndroidPCL.Model.Interface.DataBase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrivingLicenceAndroidPCL.Interface
{
    public interface ITopicService
    {
        Task<IEnumerable<ITicketDb>> GetTicketsByTopicNamesAsync(IEnumerable<string> Names, int count, Action<int> load = null);
        Task<IEnumerable<ITicketDb>> GetTicketsByCount(int count, Action<int> load = null);
        Task<IEnumerable<ITopicDb>> GetAllTopicAsync(Action<int> load = null);
    }
}