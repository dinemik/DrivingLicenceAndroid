using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using DrivingLicenceAndroidPCL.Class.PublicServices;
using DrivingLicenceAndroidPCL.Model.Interface.All;
using DrivingLicenceApp.Adapter;
using DrivingLicenceApp.Class;
using System;
using System.Collections.Generic;

namespace DrivingLicenceApp
{
    [Activity(Label = "CategoryActivity")]
    public class CategoryActivity : Activity
    {
        #region UI
        private RecyclerView Recycler { get; set; }
        #endregion

        #region Other
        private bool Online { get; set; }
        private AndroidAnimations Animations { get; set; }
        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_category);

            Online = Intent.GetBooleanExtra("Online", false);

            Recycler = FindViewById<RecyclerView>(Resource.Id.categoryRecycler);

            Recycler.SetLayoutManager(new GridLayoutManager(this, 3));
            Animations = new AndroidAnimations(this);

            Load();
        }

        private async void Load()
        {
            IEnumerable<ICategory> categories = null;

            if (Online)
                categories = await new GetTopicService(Animations).GetAllOnlineCategoryAsync();
            else
                categories = await new GetTopicService(Animations).GetAllOfflineCategoryAsync();

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