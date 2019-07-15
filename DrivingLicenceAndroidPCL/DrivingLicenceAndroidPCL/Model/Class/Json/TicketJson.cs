using System.Collections.Generic;
using DrivingLicenceAndroidPCL.Model.Interface.Json;

namespace DrivingLicenceAndroidPCL.Model.Class.Json
{
    public class TicketJson : ITicketJson
    {
        public List<AnswerJson> Answers { get; set; }
        public string Question { get; set; }
        public string Image { get; set; }
        public string Help { get; set; }
    }
}