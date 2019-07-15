using System.Collections.Generic;
using DrivingLicenceAndroidPCL.Model.Interface.DataBase;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace DrivingLicenceAndroidPCL.Model.Class.DataBase
{
    [Table("Ticket")]
    public class TicketDb : ITicketDb
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<AnswerDb> Answers { get; set; }

        [NotNull]
        public string Question { get; set; }
        public string Image { get; set; }
        public string Help { get; set; }

        [ManyToOne]
        public TopicDb Topic { get; set; }
        [ForeignKey(typeof(TopicDb))]
        public int TopicId { get; set; }
    }
}
