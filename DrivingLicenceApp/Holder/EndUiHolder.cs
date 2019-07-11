using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FFImageLoading.Views;

namespace DrivingLicenceApp.Holder
{
    public class EndUiHolder : RecyclerView.ViewHolder
    {
        public TextView QuestionTxt { get; set; }
        public ImageView ImageImg { get; set; }
        public TextView HelpText { get; set; }
        public RecyclerView AnswersRV { get; set; }

        public EndUiHolder(View view) : base(view)
        {
            QuestionTxt = view.FindViewById<TextView>(Resource.Id.QuestionEndTxt);
            ImageImg = view.FindViewById<ImageView>(Resource.Id.ImageEndImg);
            AnswersRV = view.FindViewById<RecyclerView>(Resource.Id.QuestionsRecView);
            HelpText = view.FindViewById<TextView>(Resource.Id.HelpEndTxt);
        }
    }
}