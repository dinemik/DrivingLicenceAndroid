using DrivingLicenceAndroidPCL.Model.Class.DataBaseIncorrect;
using DrivingLicenceAndroidPCL.Model.Interface.All;
using System;
using System.Collections.Generic;

namespace DrivingLicenceAndroidPCL.Model.Interface.DataBaseIncorrect
{
    public interface ITicketIncorrectDb : ITicket
    {
        int Id { get; set; }
        List<AnswerIncorrectDb> Answers { get; set; }
        DateTime Time { get; set; }
        string UserAnswer { get; set; }
    }
}