using System.Collections.Generic;
using DrivingLicenceAndroidPCL.Model.Class.DataBase;
using DrivingLicenceAndroidPCL.Model.Interface.All;

namespace DrivingLicenceAndroidPCL.Model.Interface.DataBase
{
    public interface ITicketDb : ITicket
    {
        List<AnswerDb> Answers { get; set; }
    }
}