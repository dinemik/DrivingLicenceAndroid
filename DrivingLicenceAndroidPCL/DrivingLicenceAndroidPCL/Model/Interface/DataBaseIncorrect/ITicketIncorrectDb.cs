using DrivingLicenceAndroidPCL.Model.Class.DataBaseIncorrect;
using DrivingLicenceAndroidPCL.Model.Interface.All;
using System;
using System.Collections.Generic;

namespace DrivingLicenceAndroidPCL.Model.Interface.DataBaseIncorrect
{
    public interface ITicketIncorrectDb : ITicket
    {
        List<AnswerIncorrectDb> Answers { get; set; }
        DateTime Time { get; set; }
        int UserAnswerId { get; set; }
    }
}