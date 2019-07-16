using System;

namespace DrivingLicenceAppPCL.Models.Interface
{
    public interface ITicketAnsweredStatistic
    {
        int Id { get; set; }
        int Incorrect { get; set; }
        int Correct { get; set; }
        DateTime Time { get; set; }
    }
} 
