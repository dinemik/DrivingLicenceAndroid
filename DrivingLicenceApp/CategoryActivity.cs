using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Widget;
using DrivingLicenceAndroidPCL.Class;
using DrivingLicenceAndroidPCL.Class.Json;
using DrivingLicenceAndroidPCL.Interface.Json;
using DrivingLicenceApp.Adapter;
using DrivingLicenceApp.Class;

namespace DrivingLicenceApp
{
    [Activity]
    public class CategoryActivity : AppCompatActivity
    {
        private RecyclerView Recycler { get; set; }
        
        private List<ITopic> Categories { get; set; } = new List<ITopic>
        {
            new Topic
            {
                Id = 1,
                Name = "Loading...."
            },
        };

        private bool Checked { get; set; } = false;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_category);

            Recycler = FindViewById<RecyclerView>(Resource.Id.CategoryRecycler);

            var meneger = new LinearLayoutManager(this);
            meneger.Orientation = (int)Orientation.Vertical;

            Recycler.SetLayoutManager(meneger);


            await Task.Run(() => {
                Recycler.SetAdapter(new CategoryAdapter(Categories, UnChecked, Checked));
            });

            /*TODO - optimize this shit*/
            var t = await new TopicService().GetAllTopicAsync();
            Recycler.SetAdapter(new CategoryAdapter(t, UnChecked, Checked));
        }

        /*TODO*/
        private void UnChecked(object sender, EventArgs args)
        {
            
        }
    }
}