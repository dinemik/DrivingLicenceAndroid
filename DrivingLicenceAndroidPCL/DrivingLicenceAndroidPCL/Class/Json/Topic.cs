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
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Tickets"), OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Ticket> Tickets { get; set; }
    }
}