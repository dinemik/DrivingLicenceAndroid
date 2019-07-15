using DrivingLicenceAndroidPCL.Model.Interface.DataBase;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace DrivingLicenceAndroidPCL.Model.Class.DataBase
{
    [Table("Answer")]
    public class AnswerDb : IAnswerDb
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public string Ans { get; set; }
        [NotNull]
        public bool Correct { get; set; }
        [ForeignKey(typeof(TicketDb))]
        public int TicketId { get; set; }
    }
}
