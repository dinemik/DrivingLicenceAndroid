using System;
using System.Collections.Generic;
using DrivingLicenceAndroidPCL.Interface.Json;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using static Android.Icu.Util.ULocale;

namespace DrivingLicenceAndroidPCL.Class.Json
{
    [Table("Ticket")]
    public class Ticket : ITicket
    {
        [JsonProperty("Id"), PrimaryKey]
        public int Id { get; set; }

        [JsonProperty("Answers"), ManyToMany(typeof(string))]
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
        public Uri Filename { get; set; }

        [JsonProperty("Question")]
        public string Question { get; set; }

        [JsonProperty("Timestamp")]
        public string Timestamp { get; set; }

        //[JsonProperty("Topic")]
        //public int Topic { get; set; }
    }
}