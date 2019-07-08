﻿using DrivingLicenceAndroidPCL.Class;
using System.Collections.Generic;
using DrivingLicenceApp.Adapter;
using Android.Support.V7.Widget;
using Android.Support.V7.App;
using Android.Content;
using Android.Widget;
using System.Linq;
using Android.App;
using Android.OS;
using System.Threading.Tasks;
using Felipecsl.GifImageViewLibrary;
using System.Net.Http;
using System;
using Android.Graphics;
using System.Threading;

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

        private List<string> Category { get; set; } = new List<string>(); 
        private bool Checked { get; set; } = false;


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

            /*
             ProgressBar.StartAnimation(ViewAnimator.);
             ProgressBar.Max = 100;
             ProgressBar.Progress = 100;
             ProgressBar.SecondaryProgress = 100;
             
             int progressStatus = 0, progressStatus1 = 100;
             new System.Threading.Thread(new ThreadStart(delegate {
                 while (progressStatus < 100)
                 {
                     progressStatus += 1;
                     progressStatus1 -= 1;
                     ProgressBar.Progress = progressStatus1;
                     System.Threading.Thread.Sleep(100);
                 }
             })).Start();
            */

            try
            {
                Recycler.SetAdapter(new CategoryAdapter(await new TopicService().GetAllTopicAsync(), CategoryChecked, Checked));
            }
            catch
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
    }
}