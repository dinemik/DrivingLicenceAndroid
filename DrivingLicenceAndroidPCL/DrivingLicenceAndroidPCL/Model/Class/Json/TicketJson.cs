using System.Collections.Generic;
using DrivingLicenceAndroidPCL.Model.Interface.Json;

namespace DrivingLicenceAndroidPCL.Model.Class.Json
{
    public class TicketJson : ITicketJson
    {
        public int Id { get; set; }
        public List<string> Answers { get; set; }
        public string Coeficient { get; set; }
        public int CorrectAnswer { get; set; }
        public int Cutoff { get; set; }
        public string Desc { get; set; }
        public int FileParent { get; set; }
        public string Filename { get; set; }
        public string Question { get; set; }
        public string Timestamp { get; set; }
        public int TopicId { get; set; }
    }
}