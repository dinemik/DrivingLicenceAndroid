using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite;

namespace DrivingLicenceAndroidPCL.Interface.Json
{
    public interface ITicket
    {
        [JsonProperty("Answers"), PrimaryKey]
        List<string> Answers { get; set; }

        [JsonProperty("Coeficient")]
        string Coeficient { get; set; }

        [JsonProperty("CorrectAnswer")]
        long CorrectAnswer { get; set; }

        [JsonProperty("Cutoff")]
        long Cutoff { get; set; }

        [JsonProperty("Desc")]
        string Desc { get; set; }

        [JsonProperty("FileParent")]
        long FileParent { get; set; }

        [JsonProperty("Filename")]
        Uri Filename { get; set; }

        [JsonProperty("Id")]
        int Id { get; set; }

        [JsonProperty("Question")]
        string Question { get; set; }

        [JsonProperty("Timestamp")]
        string Timestamp { get; set; }

        //[JsonProperty("Topic")]
        //int Topic { get; set; }
    }
}