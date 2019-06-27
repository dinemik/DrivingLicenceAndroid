using System.Collections.Generic;
using DrivingLicenceAndroidPCL.Class.Json;
using Newtonsoft.Json;
using SQLite;

namespace DrivingLicenceAndroidPCL.Interface.Json
{
    public partial interface ITopic
    {
        [JsonProperty("Id"), PrimaryKey]
        long Id { get; set; }

        [JsonProperty("Name")]
        string Name { get; set; }

        [JsonProperty("Tickets")]
        List<Ticket> Tickets { get; set; }
    }
}