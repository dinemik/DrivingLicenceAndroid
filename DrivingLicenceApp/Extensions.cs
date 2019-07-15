using FFImageLoading;
using FFImageLoading.Views;

namespace DrivingLicenceApp
{
    public static class Extensions
    {
        public static void LoadImage(this ImageViewAsync imageView, string url, bool smallPlaceholder = true)
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