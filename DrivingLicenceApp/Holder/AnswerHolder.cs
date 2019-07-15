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