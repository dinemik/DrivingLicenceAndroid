using DrivingLicenceAndroidPCL.Interface.Json;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using DrivingLicenceApp.Holder;
using Android.Views;
using System.Linq;
using System;

namespace DrivingLicenceApp.Adapter
{
    public class CategoryAdapter : RecyclerView.Adapter
    {
        private List<ITopic> CategoriesAll { get; set; }
        private List<ITopic> CategoriesChecked { get; set; }

        private Action<object, EventArgs> UnChecked { get; set; } = null;

        private bool Checked { get; set; }

        public override int ItemCount => CategoriesAll.Count;

        public CategoryAdapter(IEnumerable<ITopic> categories, Action<object, EventArgs> unChecked, bool check)
        {
            CategoriesAll = categories.ToList();
            UnChecked = unChecked;
            Checked = check;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var vh = holder as CategoryHolder;

            vh.Category.Click += (s, e) => UnChecked.Invoke(s, e);
            vh.Category.Text = CategoriesAll[position].Name;
            vh.Category.Checked = Checked;

            vh.QuestionsCount.Text = $"{CategoriesAll[position]?.Tickets?.Count} შეკითხვების რაოდენობა კატეგორიაში";
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) =>
            new CategoryHolder(LayoutInflater.From(parent.Context).Inflate(Resource.Layout.category_Item, parent, false));
    }
}