using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using DrivingLicenceAndroidPCL.Class;
using DrivingLicenceAndroidPCL.Enums;
using Microcharts;
using Microcharts.Droid;
using SkiaSharp;

namespace DrivingLicenceApp
{
    [Activity(Label = "StatisticActivity")]
    public class StatisticActivity : Activity
    {
        public List<Entry> Statistic { get; set; }
        private ChartView Chart { get; set; }

        private Button ByHrs { get; set; }
        private Button ByMin { get; set; }
        private Button ByMonth { get; set; }
        private Button ByDay { get; set; }

        private Button ViewTicket { get; set; }
        private Button DeleteStatistic { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_statistic);

            Chart = FindViewById<ChartView>(Resource.Id.StatisticView);

            ByHrs = FindViewById<Button>(Resource.Id.ByHrsBtn);
            ByMin = FindViewById<Button>(Resource.Id.ByMinBtn);
            ByMonth = FindViewById<Button>(Resource.Id.ByMonthBtn);
            ByDay = FindViewById<Button>(Resource.Id.ByDayBtn);

            ViewTicket = FindViewById<Button>(Resource.Id.ViewTicketsBtn);
            DeleteStatistic = FindViewById<Button>(Resource.Id.DeleteStatisticBtn);

            ByHrs.Click += GetByHrs;
            ByMin.Click += GetByMin;
            ByMonth.Click += GetByMonth;
            ByDay.Click += GetByDay;

            ViewTicket.Click += ViewTickets;
            DeleteStatistic.Click += DeleteStat;

            Load(GetStatisticsBy.ByHrs);
        }

        private async void Load(GetStatisticsBy by)
        {
            try
            {
                var statistic = await GetStatisticsService.GetStatisticAsync(by);

                Statistic = statistic.Select(o => new Entry(o.Correct)
                {
                    Color = SKColor.Parse("#FF0000"),
                    Label = string.Format("{0}.{1}.{2} {3}:{4}", o.Time.Day, o.Time.Month, o.Time.Year, o.Time.Hour, o.Time.Minute),
                    ValueLabel = (o.Correct + o.Incorrect).ToString()
                }).ToList();

                Chart.Chart = new LineChart { Entries = Statistic, BackgroundColor = SKColor.Parse("#000000") };
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


        private void GetByHrs(object sender, EventArgs args)
        {
            Load(GetStatisticsBy.ByHrs);
        }

        private void GetByMin(object sender, EventArgs args)
        {
            Load(GetStatisticsBy.ByMin);
        }

        private void GetByMonth(object sender, EventArgs args)
        {
            Load(GetStatisticsBy.ByMonth);
        }

        private void GetByDay(object sender, EventArgs args)
        {
            Load(GetStatisticsBy.ByDay);
        }


        private void ViewTickets(object sender, EventArgs args)
        {
            Intent intent = new Intent(this, typeof(EndActivity));
            intent.PutExtra("Statistic", " ");
            StartActivity(intent);
        }

        private async void DeleteStat(object sender, EventArgs args)
        {
            await GetStatisticsService.DeleteStatistic();
        }
    }
} 