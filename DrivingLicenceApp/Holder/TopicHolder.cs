using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace DrivingLicenceApp.Holder
{
    public class TopicHolder : RecyclerView.ViewHolder
    {
        public CheckBox Category { get; set; }
        public TextView QuestionsCount { get; set; }
        
        public TopicHolder(View view) : base(view)
        {
            Category = view.FindViewById<CheckBox>(Resource.Id.CategoryCard);
            QuestionsCount = view.FindViewById<TextView>(Resource.Id.QuestionsCountTxt);
        }
    }
}