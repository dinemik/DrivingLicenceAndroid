using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using DrivingLicenceAndroidPCL.Model.Interface.All;
using DrivingLicenceApp.Holder;

namespace DrivingLicenceApp.Adapter
{
    public class CategoryAdapter : RecyclerView.Adapter
    {
        private IEnumerable<ICategory> Categories { get; set; }
        private Action<object, EventArgs, string> Action { get; set; }

        public override int ItemCount => Categories.Count();

        public CategoryAdapter(IEnumerable<ICategory> categories, Action<object, EventArgs, string> action)
        {
            Categories = categories;
            Action = action;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var vh = holder as CategoryHolder;
            WebClient web = new WebClient();
            var Bitmap = BitmapFactory.DecodeFile(Categories.ElementAt(position).Img);

            vh.Box.Text = Categories.ElementAt(position).Name;
            if (Bitmap != null)
                vh.CategoryImg.SetImageBitmap(Bitmap);
            else
                vh.CategoryImg.LoadImage(Categories.ElementAt(position).Img);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var vh = new CategoryHolder(LayoutInflater.From(parent.Context).Inflate(Resource.Layout.category_item, parent, false));
            vh.Box.Click += (s, e) => Action.Invoke(s, e, (s as CheckBox).Text);
            return vh;
        }
    }
}