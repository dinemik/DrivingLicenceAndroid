using System.Collections.Generic;
using DrivingLicenceAndroidPCL.Model.Class.Json;
using DrivingLicenceAndroidPCL.Model.Interface.All;

namespace DrivingLicenceAndroidPCL.Model.Interface.Json
{
    public interface ITopicJson : ITopic
    {
        List<TicketJson> Tickets { get; set; }
    }
}