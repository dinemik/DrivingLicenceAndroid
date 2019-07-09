using System;
using DrivingLicenceApp.Models.Interface;

namespace DrivingLicenceApp.Models.Class
{
    public class TicketAnsweredStatistic : ITicketAnsweredStatistic
    {
        public int Id { get; set; }
        public int Incorrect { get; set; }
        public int Correct { get; set; }
        public DateTime Time { get; set; }
    }
}