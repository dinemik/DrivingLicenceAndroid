using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Widget;
using DrivingLicenceAndroidPCL.Class.PublicServices;
using DrivingLicenceApp.Adapter;
using DrivingLicenceApp.Models.Class;

namespace DrivingLicenceApp
{
    [Activity(Label = "DownloadActivity")]
    public class DownloadActivity : Activity
    {
        #region UI
        private ProgressDialog Progress { get; set; }
        private RecyclerView CategoryesDownload { get; set; }
        private Button DownloadBtn { get; set; } 
        private CheckBox WithImage { get; set; }
        #endregion

        #region Other
        private List<string> Categoryes { get; set; }
        private int ImageCount { get; set; }
        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_download);

            Categoryes = new List<string>();

            CategoryesDownload = FindViewById<RecyclerView>(Resource.Id.CategoryesDownload);
            DownloadBtn = FindViewById<Button>(Resource.Id.DownloadBtn);
            WithImage = FindViewById<CheckBox>(Resource.Id.WithImage);

            DownloadBtn.Click += Download;

            CategoryesDownload.SetLayoutManager(new GridLayoutManager(this, 3));

            LoadInfo();
        }

        public async void LoadInfo()
        {
            var tmp = await new GetTopicService().GetAllOnlineCategoryAsync(() => RunOnUiThread(() => PogressbarRingload()), () => RunOnUiThread(() => EndProgresBar()));
            CategoryesDownload.SetAdapter(new CategoryDownloadinAdapter(tmp.Select(o => new CategoryAndroid { Img = o.Img, Name = o.Name, Selected = false }), SelectCategory));
        }

        private void Progressbar()
        {
            Progress = new ProgressDialog(this);
            Progress.SetCancelable(false);
            Progress.SetMessage("ფაილების გადმოწერა !!!");
            Progress.SetProgressStyle(ProgressDialogStyle.Spinner);
            Progress.Show();
        }

        private void PogressbarRingload()
        {
            if (Progress == null)
                Progressbar();
        }

        private void EndProgresBar()
        {
            Progress.Cancel();
            Progress.Dispose();
            Progress = null;
        }



        private void SelectCategory(object sender, EventArgs args, string category)
        {
            if (Categoryes.Any(o => o == category))
                Categoryes.Remove(category);
            else
                Categoryes.Add(category);
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
            if (Progress == null)
                Progressbar(picCount);

            ImageCount++;
            Progress.Progress = ImageCount;

            if (ImageCount == picCount)
            {
                EndProgresBar();

                Toast.MakeText(this, "Downloaded", ToastLength.Long).Show();
            }
        }

        private async void Download(object sender, EventArgs args)
        {
            await new GetTopicService().DownloadByCategoryesAsync(Categoryes, () => RunOnUiThread(() => PogressbarRingload()), () => RunOnUiThread(() => EndProgresBar()), (count) => RunOnUiThread(() => ProgressBarLoad(count)), WithImage.Checked);
            Finish();
        }
    }
}
