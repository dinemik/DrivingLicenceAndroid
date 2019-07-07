using DrivingLicenceAndroidPCL.Model.Interface.DataBaseIncorrect;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace DrivingLicenceAndroidPCL.Model.Class.DataBaseIncorrect
{
    [Table("IncorrectAnswers")]
    public class AnswerIncorrectDb : IAnswerIncorrectDb
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Answ  { get; set; }
        public bool Correct { get; set; }
        [ForeignKey(typeof(TicketIncorrectDb))]
        public int TicketId { get; set; }
        public int AnsId { get; set; }
    }
}