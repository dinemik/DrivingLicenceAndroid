using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FFImageLoading.Views;

namespace DrivingLicenceApp.Holder
{
    public class CategoryHolder : RecyclerView.ViewHolder
    {
        public ImageViewAsync CategoryImg { get; set; }
        public CheckBox Box { get; set; }

        public CategoryHolder(View view) : base(view)
        {
            CategoryImg = view.FindViewById<ImageViewAsync>(Resource.Id.CategoryImg);
            Box = view.FindViewById<CheckBox>(Resource.Id.CategoryNameCb);
        }
    }
}