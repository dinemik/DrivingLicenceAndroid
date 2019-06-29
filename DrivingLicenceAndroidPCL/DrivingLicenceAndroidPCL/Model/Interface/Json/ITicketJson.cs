using DrivingLicenceAndroidPCL.Model.Interface.All;
using System.Collections.Generic;

namespace DrivingLicenceAndroidPCL.Model.Interface.Json
{
    public interface ITicketJson : ITicket
    {
        List<string> Answers { get; set; }
    }
}