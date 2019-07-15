using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using DrivingLicenceAndroidPCL.Class;
using DrivingLicenceAndroidPCL.Class.PublicServices;
using DrivingLicenceAndroidPCL.Model.Interface.All;
using DrivingLicenceAndroidPCL.Model.Interface.Json;
using DrivingLicenceApp.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DrivingLicenceApp
{
    [Activity(Label = "CategoryActivity")]
    public class CategoryActivity : Activity
    {
        #region UI
        private RecyclerView Recycler { get; set; }
        private ProgressDialog Progress { get; set; }
        #endregion

        #region Other
        private bool Online { get; set; }
        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_category);

            Online = Intent.GetBooleanExtra("Online", false);

            Recycler = FindViewById<RecyclerView>(Resource.Id.categoryRecycler);

            Recycler.SetLayoutManager(new GridLayoutManager(this, 3));

            Load();
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
            if (Progress != null)
                Progress.Cancel();
        }

        private async void Load()
        {
            IEnumerable<ICategory> categories = null;

            if (Online)
                categories = await new GetTopicService().GetAllOnlineCategoryAsync(() => RunOnUiThread(() => PogressbarRingload()), EndProgresBar);
            else
                categories = await new GetTopicService().GetAllOfflineCategoryAsync();

            Recycler.SetAdapter(new CategoryAdapter(categories, Click));
        }

        private void Click(object sender, EventArgs args, string category)
        {
            Intent topicUi = new Intent(this, typeof(TopicActivity));
            topicUi.PutExtra("Online", Online);
            topicUi.PutExtra("Category", category);
            StartActivity(topicUi);
        }
    }
}