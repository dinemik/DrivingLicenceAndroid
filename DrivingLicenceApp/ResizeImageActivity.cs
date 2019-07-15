using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using FFImageLoading.Views;

namespace DrivingLicenceApp
{
    [Activity(Label = "ResizeImageActivity", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ResizeImageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_resizeImage);
            ImageViewAsync ImageImg = FindViewById<ImageViewAsync>(Resource.Id.ImageImg);

            if (BitmapFactory.DecodeFile(Intent.Extras.Get("TicketImage").ToString()) != null)
                ImageImg.SetImageBitmap(BitmapFactory.DecodeFile(Intent.Extras.Get("TicketImage").ToString()));
            else
                ImageImg.LoadImage(Intent.Extras.Get("TicketImage").ToString());

            ImageImg.Click += (s, e) => Finish();
        }
    }
}