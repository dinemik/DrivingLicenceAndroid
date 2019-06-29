using DrivingLicenceAndroidPCL.Model.Interface.Json;
using System.Collections.Generic;

namespace DrivingLicenceAndroidPCL.Model.Class.Json
{
    public class TopicJson : ITopicJson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TicketJson> Tickets { get; set; }
    }
}