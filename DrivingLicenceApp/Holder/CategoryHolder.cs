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
    public class CategoryHolder : RecyclerView.ViewHolder
    {
        public CheckBox Category { get; set; }
        public TextView QuestionsCount { get; set; }
        
        public CategoryHolder(View view) : base(view)
        {
            Category = view.FindViewById<CheckBox>(Resource.Id.CategoryCard);
            QuestionsCount = view.FindViewById<TextView>(Resource.Id.QuestionsCountTxt);
        }
    }
}