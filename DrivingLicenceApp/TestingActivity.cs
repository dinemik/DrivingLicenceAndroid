using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using DrivingLicenceAndroidPCL.Class;
using DrivingLicenceAndroidPCL.Class.Json;
using DrivingLicenceAndroidPCL.Interface.Json;

namespace DrivingLicenceApp
{
    [Activity]
    public class TestingActivity : AppCompatActivity
    {
        private IEnumerable<ITicket> Topics { get; set; }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_testing);
            Topics = await new TopicService().GetTopicsByNamesAsync(Intent.GetStringArrayListExtra("Tickets"), 30);
        }


    }
}