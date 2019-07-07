using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Widget;
using DrivingLicenceAndroidPCL.Class;
using DrivingLicenceAndroidPCL.Enums;
using DrivingLicenceAndroidPCL.Model.Interface.DataBaseIncorrect;
using DrivingLicenceApp.Adapter;

namespace DrivingLicenceApp
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    class EndActivity : AppCompatActivity
    {
        private RecyclerView Recycler { get; set; }
        private IEnumerable<ITicketIncorrectDb> Tickets { get; set; }
        private Button AgainBtn { get; set; }
        private Button MainMenu { get; set; }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_end);

            Recycler = FindViewById<RecyclerView>(Resource.Id.TicketsEndRV);
            AgainBtn = FindViewById<Button>(Resource.Id.AgainBtn);
            MainMenu = FindViewById<Button>(Resource.Id.MainMenuBtn);

            AgainBtn.Click += (s, e) => {
                Finish();
            };

            MainMenu.Click += (s, e) => {
                StartActivity(typeof(MainActivity));
                Finish();
            };

            Load();
        }

        private async void Load()
        {
            var TicketsCountStr = Intent.Extras.Get("TicketsCount").ToString();
            int.TryParse(TicketsCountStr, out int TicketsCountInt);
            var ticketsLstHrs = await new AnsweredService().GetIncorrectTicketAsync(GetTicketBy.Hrs);
            Tickets = ticketsLstHrs.Where(o => o.Id >= ticketsLstHrs.ElementAt(ticketsLstHrs.Count() - TicketsCountInt).Id);

            var meneger = new LinearLayoutManager(this)
            { Orientation = LinearLayoutManager.Vertical };
            Recycler.SetLayoutManager(meneger);

            Recycler.SetAdapter(new EndUiAdapter(Tickets));
        }
    }
}