using System;
using Android.App;
using Android.Content;
using DrivingLicenceAndroidPCL.Interface;

namespace DrivingLicenceApp.Class
{
    public class AndroidAnimations : IAnimationService
    {
        private ProgressDialog JsonProgress { get; set; }
        private ProgressDialog DownloadImageProgress { get; set; }
        private Context Context { get; set; }

        public AndroidAnimations(Context context)
        {
            Context = context;
        }


        public void StartJsonDownloadAnimation()
        {
            JsonProgress = new ProgressDialog(Context);
            JsonProgress.SetCancelable(false);
            JsonProgress.SetMessage("ფაილების გადმოწერა !!!");
            JsonProgress.SetProgressStyle(ProgressDialogStyle.Spinner);
            JsonProgress.Show();
        }

        public void EndJsonDownloadAnimation()
        {
            JsonProgress.Cancel();
            JsonProgress.Dispose();
            JsonProgress = null;
        }



        public void StartImageDownloadAnimation(int count)
        {
            DownloadImageProgress = new ProgressDialog(Context);
            DownloadImageProgress.SetCancelable(false);
            DownloadImageProgress.SetMessage("სურათების გადმოწერა !!!");
            DownloadImageProgress.SetProgressStyle(ProgressDialogStyle.Horizontal);
            DownloadImageProgress.Max = count;
            DownloadImageProgress.Show();
        }

        public void NextImageDownloadAnimation(int downloadedImageCount)
        {
            DownloadImageProgress.Progress = downloadedImageCount;
        }

        public void EndImageDownloadAnimation()
        {
            DownloadImageProgress.Cancel();
            DownloadImageProgress.Dispose();
            DownloadImageProgress = null;
        }
    }
}