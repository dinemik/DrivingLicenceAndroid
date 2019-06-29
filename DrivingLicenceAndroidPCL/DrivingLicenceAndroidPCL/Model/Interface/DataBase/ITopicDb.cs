using DrivingLicenceAndroidPCL.Model.Class.DataBase;
using DrivingLicenceAndroidPCL.Model.Interface.All;
using System.Collections.Generic;

namespace DrivingLicenceAndroidPCL.Model.Interface.DataBase
{
    public interface ITopicDb : ITopic
    {
        List<TicketDb> TicketsDb { get; set; }
    }
}