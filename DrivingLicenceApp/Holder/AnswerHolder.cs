using System;
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
    class AnswerHolder : RecyclerView.ViewHolder
    {
        public TextView AnswerTxt { get; set; }

        public AnswerHolder(View view) : base(view)
        {
            AnswerTxt = view.FindViewById<TextView>(Resource.Id.AnsTxt);
        }
    }
}