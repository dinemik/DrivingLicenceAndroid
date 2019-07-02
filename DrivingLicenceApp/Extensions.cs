using FFImageLoading;
using FFImageLoading.Views;

namespace DrivingLicenceApp
{
    public static class Extensions
    {
#pragma warning disable CS0618 // Type or member is obsolete
        public static void LoadImage(this ImageViewAsync imageView, string url, bool smallPlaceholder = true)
#pragma warning restore CS0618 // Type or member is obsolete
        {
            ImageService.Instance
                .LoadUrl(url)
                .LoadingPlaceholder((!smallPlaceholder) ? "placeholderLarge" : "placeholderMinix", FFImageLoading.Work.ImageSource.CompiledResource)
                .ErrorPlaceholder((!smallPlaceholder) ? "placeholderLarge" : "placeholderMinix", FFImageLoading.Work.ImageSource.CompiledResource)
                .WithCache(FFImageLoading.Cache.CacheType.Disk)
                .DownSample(imageView.Width, imageView.Height, false)
                .DownSampleMode(FFImageLoading.Work.InterpolationMode.High)
                .CacheKey(url)
                .IntoAsync(imageView);
        }
    }
}