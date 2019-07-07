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
using FFImageLoading.Views;

namespace DrivingLicenceApp
{
    [Activity(Label = "ResizeImageActivity", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ResizeImageActivity : Activity
    {
        private ImageViewAsync ImageImg { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_resizeImage);

            ImageImg = FindViewById<ImageViewAsync>(Resource.Id.ImageImg);

            var ImgUrl = Intent.Extras.Get("TicketImage").ToString();
            ImageImg.LoadImage(ImgUrl);
        }
    }
}