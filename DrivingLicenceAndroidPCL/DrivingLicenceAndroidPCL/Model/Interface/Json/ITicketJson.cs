using DrivingLicenceAndroidPCL.Model.Class.Json;
using DrivingLicenceAndroidPCL.Model.Interface.All;
using System.Collections.Generic;

namespace DrivingLicenceAndroidPCL.Model.Interface.Json
{
    public interface ITicketJson : ITicket
    {
        List<AnswerJson> Answers { get; set; }
    }
}