using DrivingLicenceAndroidPCL.Class.Json;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
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

        [JsonProperty("Tickets"), ManyToMany(typeof(Ticket))]
        List<Ticket> Tickets { get; set; }
    }
}