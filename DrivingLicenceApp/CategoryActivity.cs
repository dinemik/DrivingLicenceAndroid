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

namespace DrivingLicenceApp
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class CategoryActivity : AppCompatActivity
    {
        #region UI
        private RecyclerView Recycler { get; set; }
        private ImageView Confirm { get; set; }
        private ProgressBar ProgressBar { get; set; }
        #endregion

        private ProgressDialog Progress { get; set; }
        private List<string> Category { get; set; } = new List<string>(); 
        private bool Checked { get; set; } = false;
        private int ImageCount { get; set; }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_category);

            Recycler = FindViewById<RecyclerView>(Resource.Id.CategoryRecycler);
            Confirm = FindViewById<ImageView>(Resource.Id.StartTestImg);
            ProgressBar = FindViewById<ProgressBar>(Resource.Id.DownloadingProgress);

            var manager = new LinearLayoutManager(this)
            { Orientation = (int)Orientation.Vertical };
            Recycler.SetLayoutManager(manager);

            try
            {
                Recycler.SetAdapter(new CategoryAdapter(await new TopicService().GetAllTopicAsync((count) => RunOnUiThread(() => ProgressBarLoad(count))), CategoryChecked, Checked));
            }
            catch(Java.Net.UnknownHostException)
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

        private void CategoryChecked(object sender, EventArgs args)
        {
            var txt = (sender as CheckBox).Text;

            if (Category.Any(o => o == txt)) 
                Category.Remove(txt);
            else
                Category.Add(txt);
        }
         
        private void StartTesting(object sender, EventArgs args)
        {
            if(Category.Count == 0)
            {
                Toast.MakeText(Application.Context, "ერთერთ კატეგორია აირჩიე", ToastLength.Short).Show();
                return;
            }
           
            Intent testingUI = new Intent(this, typeof(TestingActivity));
            testingUI.PutStringArrayListExtra("Tickets", Category);
            StartActivity(testingUI);
        }


        private void Progressbar(int picCount)
        {
            Progress = new ProgressDialog(this);
            Progress.SetCancelable(false);
            Progress.SetMessage("სურათების გადმოწერა !!!");
            Progress.SetProgressStyle(ProgressDialogStyle.Horizontal);
            Progress.Max = picCount;
            Progress.Show();
            ImageCount = 0;
        }

        private void ProgressBarLoad(int picCount)
        {
            if(Progress == null)
                Progressbar(picCount);

            ImageCount++;
            Progress.Progress = ImageCount;

            if (ImageCount == picCount)
                Progress.Cancel();
        }
    }
}