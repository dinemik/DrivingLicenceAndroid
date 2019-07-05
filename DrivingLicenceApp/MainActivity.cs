using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Android.Support.Design.Widget;
using System;
using Android.Content;

namespace DrivingLicenceApp
{
    /*
     * TODO
     * MainLauncher = false...
     * this is main UI
     */
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button CategoryBtn { get; set; }
        private Button RandomBtn { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            CategoryBtn = FindViewById<Button>(Resource.Id.CategoryStart);
            RandomBtn = FindViewById<Button>(Resource.Id.RandomStart);
            ///
            CategoryBtn.Click += Category;
            RandomBtn.Click += Random;
        }

        private void Category(object sender, EventArgs args)
        {
            Intent Category = new Intent(this, typeof(CategoryActivity));
            StartActivity(Category);
        }

        private void Random(object sender, EventArgs args)
        {

        }
    }
}