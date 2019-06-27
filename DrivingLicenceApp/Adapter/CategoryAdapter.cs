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
using DrivingLicenceAndroidPCL.Interface.Json;
using DrivingLicenceApp.Class;
using DrivingLicenceApp.Holder;

namespace DrivingLicenceApp.Adapter
{
    public class CategoryAdapter : RecyclerView.Adapter
    {
        private List<ITopic> Categories { get; set; }
        private Action<object, EventArgs> UnChecked { get; set; } = null;

        private bool Checked { get; set; }

        public override int ItemCount => Categories.Count;

        public CategoryAdapter(IEnumerable<ITopic> categories, Action<object, EventArgs> unChecked, bool check)
        {
            Categories = categories.ToList();
            UnChecked = unChecked;
            Checked = check;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var vh = holder as CategoryHolder;
            vh.Category.Checked = Checked;
            vh.Category.Text = Categories[position].Name;
            vh.Category.Click += (s, e) => UnChecked.Invoke(s, e);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) =>
            new CategoryHolder(LayoutInflater.From(parent.Context).Inflate(Resource.Layout.category_Item, parent, false));

    }
}