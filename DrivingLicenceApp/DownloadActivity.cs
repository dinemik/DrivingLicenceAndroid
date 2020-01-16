using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Widget;
using DrivingLicenceAndroidPCL.Class.PublicServices;
using DrivingLicenceApp.Adapter;
using DrivingLicenceApp.Class;
using DrivingLicenceApp.Models.Class;

namespace DrivingLicenceApp
{
    [Activity(Label = "DownloadActivity")]
    public class DownloadActivity : Activity
    {
        #region UI
        private RecyclerView CategoryesDownload { get; set; }
        private Button DownloadBtn { get; set; } 
        private CheckBox WithImage { get; set; }
        #endregion

        #region Other
        private List<string> Categoryes { get; set; }
        private AndroidAnimations Animations { get; set; }
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
            Animations = new AndroidAnimations(this);

            LoadInfo();
        }

        public async void LoadInfo()
        {
            var tmp = await new GetTopicService(Animations).GetAllOnlineCategoryAsync();
            CategoryesDownload.SetAdapter(new CategoryDownloadinAdapter(tmp.Select(o => new CategoryAndroid { Img = o.Img, Name = o.Name, Selected = false }), SelectCategory));
        }

        private void SelectCategory(object sender, EventArgs args, string category)
        {
            if (Categoryes.Any(o => o == category))
                Categoryes.Remove(category);
            else
                Categoryes.Add(category);
        }

        private async void Download(object sender, EventArgs args)
        {
            await new GetTopicService(Animations).DownloadByCategoryesAsync(Categoryes, WithImage.Checked);
        }
    }
}
