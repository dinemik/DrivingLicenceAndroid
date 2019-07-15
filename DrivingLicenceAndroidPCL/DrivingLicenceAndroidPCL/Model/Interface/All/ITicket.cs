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
        string Question { get; set; }
        string Image { get; set; }
        string Help { get; set; }
    }
}