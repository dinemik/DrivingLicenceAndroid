using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System;
using Android.Support.V7.Widget;
using Android.Graphics;
using Android.Content;

namespace DrivingLicenceApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity
    {
        #region UI

        private Button DownloadOtherCategoryesBtn { get; set; }
        private Button OfflienTests { get; set; }
        private Button OnlineTests { get; set; }
        private Button Statistic { get; set; }

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            DownloadOtherCategoryesBtn = FindViewById<Button>(Resource.Id.DownloadOtherCategoryesBtn);
            OnlineTests = FindViewById<Button>(Resource.Id.OnlineTestsBtn);
            OfflienTests = FindViewById<Button>(Resource.Id.DowloadedTestsBtn);
            Statistic = FindViewById<Button>(Resource.Id.StatisticBtn);


            DownloadOtherCategoryesBtn.Click += (s, e) => {
                Intent downloadUi = new Intent(this, typeof(DownloadActivity));
                StartActivity(downloadUi);
            };

            OnlineTests.Click += (s, e) => {
                Intent OnlineCategory = new Intent(this, typeof(CategoryActivity));
                OnlineCategory.PutExtra("Online", true);
                StartActivity(OnlineCategory);
            };

            OfflienTests.Click += (s, e) => {
                Intent OfflineCategory = new Intent(this, typeof(CategoryActivity));
                OfflineCategory.PutExtra("Online", false);
                StartActivity(OfflineCategory);
            };

            Statistic.Click += (s, e) => {
                Intent Statistic = new Intent(this, typeof(StatisticActivity));
                StartActivity(Statistic);
            };
        }
    }
}