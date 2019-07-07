﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace DrivingLicenceApp.Holder
{
    public class AnswerEndHolder : RecyclerView.ViewHolder
    {
        public TextView Answer { get; set; }

        public AnswerEndHolder(View view) : base(view)
        {
            Answer = view.FindViewById<TextView>(Resource.Id.AnsTxt);
        }
    }
}