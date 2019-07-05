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

namespace DrivingLicenceApp
{
    [Activity]
    class EndActivity : AppCompatActivity
    {
        private TextView Correct { get; set; }
        private TextView Incorrect { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_end);

            Correct = FindViewById<TextView>(Resource.Id.CorrectTxt);
            Incorrect = FindViewById<TextView>(Resource.Id.IncorectTxt);

            var intent = new Intent(this, typeof(EndActivity));
            intent.PutExtra("MyData", "Data from Activity1");
            StartActivity(intent);


            Correct.Text = "";
            Incorrect.Text = "";
        }
    }
}