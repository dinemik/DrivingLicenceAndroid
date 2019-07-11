using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;

namespace DrivingLicenceApp
{
    [Activity(Label = "ResizeImageActivity", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ResizeImageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_resizeImage);

            ImageView ImageImg = FindViewById<ImageView>(Resource.Id.ImageImg);

            ImageImg.SetImageBitmap(BitmapFactory.DecodeFile(Intent.Extras.Get("TicketImage").ToString()));
            ImageImg.Click += (s, e) => Finish();
        }
    }
}