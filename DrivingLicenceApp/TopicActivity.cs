using DrivingLicenceAndroidPCL.Class;
using System.Collections.Generic;
using DrivingLicenceApp.Adapter;
using Android.Support.V7.Widget;
using Android.Support.V7.App;
using Android.Content;
using Android.Widget;
using System.Linq;
using Android.App;
using Android.OS;
using System;
using DrivingLicenceAndroidPCL.Class.PublicServices;
using DrivingLicenceApp.Models.Class;
using DrivingLicenceApp.Class;

namespace DrivingLicenceApp
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class TopicActivity : AppCompatActivity
    {
        #region UI
        private RecyclerView Recycler { get; set; }
        private ImageView Confirm { get; set; }
        private ProgressBar ProgressBar { get; set; }
        #endregion

        private List<string> Topics { get; set; } = new List<string>();
        private bool Checked { get; set; } = false;
        private AndroidAnimations Animations { get; set; }

        private bool Online { get; set; }
        private string Category { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_topic);

            Recycler = FindViewById<RecyclerView>(Resource.Id.TopicRecycler);
            Confirm = FindViewById<ImageView>(Resource.Id.StartTestImg);
            ProgressBar = FindViewById<ProgressBar>(Resource.Id.DownloadingProgress);

            var manager = new LinearLayoutManager(this)
            { Orientation = (int)Orientation.Vertical };
            Recycler.SetLayoutManager(manager);

            Online = Intent.GetBooleanExtra("Online", false);
            Category = Intent.GetStringExtra("Category");

            Animations = new AndroidAnimations(this);
            try
            {
                Load();
            }
            catch (Java.Net.UnknownHostException)
            {
                Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                alert.SetTitle("ინტერნეტის კავშირი");
                alert.SetMessage("ეს აპლიკაცია ირთვება პირვლად იმისთვის რომ ჩაირთოს საჭიროა ინტერნეტთან კავშირი შეკითხვების გადმოსაწერათ ერთჯერადი კავშირია.");

                Dialog dialog = alert.Create();
                dialog.Show();
            }

            // hide Loaging Animation
            ProgressBar.Visibility = Android.Views.ViewStates.Gone;
            Confirm.Click += StartTesting;
        }

        private async void Load()
        {
            if (Online)
                Recycler.SetAdapter(new TopicAdapter((await new GetTopicService(Animations).GetAllOnlineCategoryAsync()).FirstOrDefault(o => o.Name == Category).Topics.Select(i => new TopicAndroid { Name = i.Name, TicketsCount = i.TicketsDb.Count() }), CategoryChecked, Checked));
            else
                Recycler.SetAdapter(new TopicAdapter((await new GetTopicService(Animations).GetAllOfflineCategoryAsync()).FirstOrDefault(o => o.Name == Category).Topics.Select(i => new TopicAndroid { Name = i.Name, TicketsCount = i.TicketsDb.Count() }), CategoryChecked, Checked));
        }

        private void CategoryChecked(object sender, EventArgs args)
        {
            var txt = (sender as CheckBox).Text;

            if (Topics.Any(o => o == txt))
                Topics.Remove(txt);
            else
                Topics.Add(txt);
        }

        private void StartTesting(object sender, EventArgs args)
        {
            if (Topics.Count == 0)
            {
                Toast.MakeText(Application.Context, "ერთ კატეგორია მაინც უნდა გქონდეს არჩეული", ToastLength.Short).Show();
                return;
            }

            Intent testingUI = new Intent(this, typeof(TestingActivity));
            testingUI.PutExtra("Category", Category);
            testingUI.PutStringArrayListExtra("Topics", Topics);
            testingUI.PutExtra("Online", Online);
            StartActivity(testingUI);
        }
    }
}