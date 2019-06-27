using System.Collections.Generic;
using DrivingLicenceAndroidPCL.Interface.Json;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace DrivingLicenceAndroidPCL.Class.Json
{
    [Table("Topic")]
    public class Topic : ITopic
    {
        [JsonProperty("Id"), PrimaryKey]
        public long Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Tickets"), ManyToMany(typeof(Ticket))]
        public List<Ticket> Tickets { get; set; }
    }
}