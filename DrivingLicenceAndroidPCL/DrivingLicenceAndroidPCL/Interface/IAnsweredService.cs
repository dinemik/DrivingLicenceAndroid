using System.Collections.Generic;
using System.Threading.Tasks;
using DrivingLicenceAndroidPCL.Enums;
using DrivingLicenceAndroidPCL.Model.Interface.DataBase;
using DrivingLicenceAndroidPCL.Model.Interface.DataBaseIncorrect;

namespace DrivingLicenceAndroidPCL.Interface
{
    public interface IAnsweredService
    {
        Task<bool> SaveUserAnswersAsync(IEnumerable<ITicketDb> tickets, IEnumerable<int> answersIDs);

        Task<IEnumerable<ITicketIncorrectDb>> GetIncorrectTicketAsync(GetTicketBy get);
    }
}