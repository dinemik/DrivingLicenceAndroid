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
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity
    {
        private Button CategoryBtn { get; set; }
        private Button RandomBtn { get; set; }
        private Button StatisticBtn { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            CategoryBtn = FindViewById<Button>(Resource.Id.CategoryStartBtn);
            RandomBtn = FindViewById<Button>(Resource.Id.RandomStartBtn);
            StatisticBtn = FindViewById<Button>(Resource.Id.StatisticBtn);
            ///
            CategoryBtn.Click += Category;
            RandomBtn.Click += Random;
            StatisticBtn.Click += Statistic;
        }

        private void Category(object sender, EventArgs args)
        {
            Intent Category = new Intent(this, typeof(CategoryActivity));
            StartActivity(Category);
        }

        private void Random(object sender, EventArgs args)
        {
            Intent Category = new Intent(this, typeof(TestingActivity));
            StartActivity(Category);
        }

        private void Statistic(object sender, EventArgs args)
        {
            Intent statistic = new Intent(this, typeof(StatisticActivity));
            StartActivity(statistic);
        }
    }
}