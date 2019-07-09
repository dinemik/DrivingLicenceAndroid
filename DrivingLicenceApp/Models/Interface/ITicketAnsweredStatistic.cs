using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DrivingLicenceApp.Models.Interface
{
    public interface ITicketAnsweredStatistic
    {
        int Id { get; set; }
        int Incorrect { get; set; }
        int Correct { get; set; }
        DateTime Time { get; set; }
    }
}