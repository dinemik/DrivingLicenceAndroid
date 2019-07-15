using System.Collections.Generic;
using DrivingLicenceAndroidPCL.Model.Interface.DataBase;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace DrivingLicenceAndroidPCL.Model.Class.DataBase
{
    [Table("Topic")]
    public class TopicDb : ITopicDb
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<TicketDb> TicketsDb { get; set; }
        [ForeignKey(typeof(CategoryDb))]
        public int CategoryId { get; set; }
    }
}
