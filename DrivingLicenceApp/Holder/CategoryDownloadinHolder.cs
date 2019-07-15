using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FFImageLoading.Views;

namespace DrivingLicenceApp.Holder
{
    public class CategoryDownloadinHolder : RecyclerView.ViewHolder
    {
        public ImageViewAsync CategoryImg { get; set; }
        public CheckBox CategoryCb { get; set; }

        public CategoryDownloadinHolder(View view) : base(view)
        {
            CategoryImg = view.FindViewById<ImageViewAsync>(Resource.Id.CategoryImg);
            CategoryCb = view.FindViewById<CheckBox>(Resource.Id.CategoryNameCb);
        }
    }
}