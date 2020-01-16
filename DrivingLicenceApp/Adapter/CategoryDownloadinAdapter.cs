using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using DrivingLicenceAndroidPCL.Model.Interface.DataBase;
using DrivingLicenceApp.Holder;
using DrivingLicenceApp.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DrivingLicenceApp.Adapter
{
    public class CategoryDownloadinAdapter : RecyclerView.Adapter
    {
        private IEnumerable<ICategoryAndroid> Categories { get; set; }
        Action<object, EventArgs, string> Action { get; set; }
        public override int ItemCount => Categories.Count();

        public CategoryDownloadinAdapter(IEnumerable<ICategoryAndroid> categories, Action<object, EventArgs, string> action)
        {
            Categories = categories;
            Action = action;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var vh = (holder as CategoryDownloadinHolder);
            vh.CategoryImg.LoadImage($"https://firebasestorage.googleapis.com/v0/b/drivinglicencenew.appspot.com/o/Categories%2F{Categories.ElementAt(position).Img}?alt=media");
            vh.CategoryCb.Text = Categories.ElementAt(position).Name;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var vh = new CategoryDownloadinHolder(LayoutInflater.From(parent.Context).Inflate(Resource.Layout.categorydownloading_Item, parent, false));

            vh.CategoryCb.Click += (s, e) => Action.Invoke(s, e, Categories.First(o => o.Name == (s as CheckBox).Text).Name);
            vh.CategoryImg.Click += (s, e) => Categories.First(o => o.Name == (s as TextView).Text).Selected = !Categories.First(o => o.Name == (s as TextView).Text).Selected;

            return vh;
        }
    }
}