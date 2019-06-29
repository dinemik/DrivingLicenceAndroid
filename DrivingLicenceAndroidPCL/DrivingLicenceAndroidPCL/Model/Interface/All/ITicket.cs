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

namespace DrivingLicenceAndroidPCL.Model.Interface.All
{
    public interface ITicket
    {
        int Id { get; set; }
        string Coeficient { get; set; }
        int CorrectAnswer { get; set; }
        int Cutoff { get; set; }
        string Desc { get; set; }
        int FileParent { get; set; }
        string Filename { get; set; }
        string Question { get; set; }
        string Timestamp { get; set; }
        int TopicId { get; set; }
    }
}