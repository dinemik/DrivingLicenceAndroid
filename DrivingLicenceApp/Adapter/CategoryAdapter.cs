using System.Collections.Generic;
using Android.Support.V7.Widget;
using DrivingLicenceApp.Holder;
using Android.Views;
using System.Linq;
using System;
using DrivingLicenceApp.Models.Interface;
using DrivingLicenceApp.Models.Class;
using Android.Widget;
using DrivingLicenceAndroidPCL.Model.Interface.DataBase;

namespace DrivingLicenceApp.Adapter
{
    public class CategoryAdapter : RecyclerView.Adapter
    {
        private IEnumerable<ITopicAndroid> CategoriesAll { get; set; }

        private Action<object, EventArgs> UnChecked { get; set; } = null;

        public override int ItemCount => CategoriesAll.Count();

        public CategoryAdapter(IEnumerable<ITopicDb> categories, Action<object, EventArgs> unChecked, bool check)
        {
            UnChecked = unChecked;

            CategoriesAll = categories.Select(o => new TopicAndroid
            {
                Id = o.Id,
                isChecked = check,
                Name = o.Name,
                TicketsCount = o.TicketsDb.Count()
            }).ToList();
        }
        
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var vh = holder as CategoryHolder;
        
            
            vh.Category.Text = CategoriesAll.ElementAt(position).Name;
            vh.Category.Checked = CategoriesAll.ElementAt(position).isChecked;
        
            vh.QuestionsCount.Text = $"{CategoriesAll.ElementAt(position)?.TicketsCount} შეკითხვების რაოდენობა კატეგორიაში";
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
           var holder= new CategoryHolder(LayoutInflater.From(parent.Context).Inflate(Resource.Layout.category_Item, parent, false));
           
           holder.Category.Click += new EventHandler(UnChecked);
           holder.Category.Click += Check;

            return holder;
        }
        private void Check(object sender, EventArgs args)
        {
            var obj = CategoriesAll.First(o => o.Name == (sender as TextView).Text);
            obj.isChecked = !obj.isChecked;
        }
    }
}