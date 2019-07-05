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
        public string Coeficient { get; set; }
        public int CorrectAnswer { get; set; }
        public int Cutoff { get; set; }
        public string Desc { get; set; }
        public int FileParent { get; set; }
        public string Filename { get; set; }
        public string Question { get; set; }
        public string Timestamp { get; set; }
        [ForeignKey(typeof(TopicDb))]
        public int TopicId { get; set; }
        [ManyToOne]
        public TopicDb Topic { get; set; }
    }
}