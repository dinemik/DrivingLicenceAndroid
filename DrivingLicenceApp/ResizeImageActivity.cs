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

            var imagePath = Intent.Extras.Get("TicketImage").ToString();

            if (BitmapFactory.DecodeFile(imagePath) != null)
                ImageImg.SetImageBitmap(BitmapFactory.DecodeFile(Intent.Extras.Get("TicketImage").ToString()));
            else
                ImageImg.LoadImage($"https://firebasestorage.googleapis.com/v0/b/drivinglicencenew.appspot.com/o/Ticket_ImagesOne%2F{imagePath}?alt=media");

            ImageImg.Click += (s, e) => Finish();
        }
    }
}