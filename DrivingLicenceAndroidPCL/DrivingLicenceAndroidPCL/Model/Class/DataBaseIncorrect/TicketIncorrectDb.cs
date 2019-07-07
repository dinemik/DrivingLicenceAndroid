using System;
using System.Collections.Generic;
using DrivingLicenceAndroidPCL.Model.Interface.DataBaseIncorrect;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace DrivingLicenceAndroidPCL.Model.Class.DataBaseIncorrect
{
    [Table("IncorrectTickets")]
    public class TicketIncorrectDb : ITicketIncorrectDb
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Coeficient { get; set; }
        public string Desc { get; set; }
        public string Filename { get; set; }
        public string Question { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<AnswerIncorrectDb> Answers { get; set; }
        public int UserAnswerId { get; set; }
        public DateTime Time { get; set; }

    }
}