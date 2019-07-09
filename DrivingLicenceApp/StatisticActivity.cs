using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DrivingLicenceApp.Class;
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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_statistic);

            Chart = FindViewById<ChartView>(Resource.Id.StatisticView);

            Load();
        } 

        private async void Load()
        {
            var statistic = await GetStatistics.GetStatisticAsync();

            Statistic = statistic.Select(o => new Entry(o.Correct) {
                Color = SKColor.Parse("#FF0000"),
                Label = string.Format("{0}.{1}.{2} {3}:{4}", o.Time.Day, o.Time.Month, o.Time.Year, o.Time.Hour, o.Time.Minute),
                ValueLabel = (o.Correct + o.Incorrect).ToString()
            }).ToList();

            Chart.Chart = new BarChart { Entries = Statistic };
        }
    }
} 