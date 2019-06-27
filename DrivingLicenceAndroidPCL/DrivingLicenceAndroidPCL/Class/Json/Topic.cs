using DrivingLicenceAndroidPCL.Interface.Json;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite;

namespace DrivingLicenceAndroidPCL.Class.Json
{
    [Table("Topic")]
    public class Topic : ITopic
    {
        [JsonProperty("Id"), PrimaryKey]
        public long Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Tickets"), OneToMany]
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}