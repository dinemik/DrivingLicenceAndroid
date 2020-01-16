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
    public class EndActivity : AppCompatActivity
    {
        private RecyclerView Recycler { get; set; }
        private IEnumerable<ITicketIncorrectDb> Tickets { get; set; }
        //private Button AgainBtn { get; set; }
        private Button MainMenu { get; set; }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_end);

            Recycler = FindViewById<RecyclerView>(Resource.Id.TicketsEndRV);
            //AgainBtn = FindViewById<Button>(Resource.Id.AgainBtn);
            MainMenu = FindViewById<Button>(Resource.Id.MainMenuBtn);

            MainMenu.Click += (s, e) =>
            {
                StartActivity(typeof(MainActivity));
                Finish();
            };

            if (Intent.Extras.Get("Statistic") == null)
                Load();
            else
                LoadAll();
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

        private async void LoadAll()
        {
            try
            {
                //FindViewById<CardView>(Resource.Id.ReloadTest).Visibility = Android.Views.ViewStates.Gone;
                Tickets = await new AnsweredService().GetIncorrectTicketAsync(GetTicketBy.All);

                var meneger = new LinearLayoutManager(this)
                { Orientation = LinearLayoutManager.Vertical };
                Recycler.SetLayoutManager(meneger);

                Recycler.SetAdapter(new EndUiAdapter(Tickets));
            }
            catch (SQLite.SQLiteException)
            {
                Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                alert.SetTitle("სტატისტიკა");
                alert.SetMessage("ჯერ გაიარე ტესტი");

                Dialog dialog = alert.Create();
                dialog.Show();
            }
        }
    }
}