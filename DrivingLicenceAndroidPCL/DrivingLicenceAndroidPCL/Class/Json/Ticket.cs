using DrivingLicenceAndroidPCL.Interface.Json;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using SQLite;

namespace DrivingLicenceAndroidPCL.Class.Json
{
    [Table("Ticket")]
    public class Ticket : ITicket
    {
        [JsonProperty("Id"), PrimaryKey]
        public int Id { get; set; }

        [JsonProperty("Answers"), OneToMany]
        public List<string> Answers { get; set; }

        [JsonProperty("Coeficient")]
        public string Coeficient { get; set; }

        [JsonProperty("CorrectAnswer")]
        public long CorrectAnswer { get; set; }

        [JsonProperty("Cutoff")]
        public long Cutoff { get; set; }

        [JsonProperty("Desc")]
        public string Desc { get; set; }

        [JsonProperty("FileParent")]
        public long FileParent { get; set; }

        [JsonProperty("Filename")]
        public string Filename { get; set; }

        [JsonProperty("Question")]
        public string Question { get; set; }

        [JsonProperty("Timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("Topic")]
        public int TopicId { get; set; }
    }
}