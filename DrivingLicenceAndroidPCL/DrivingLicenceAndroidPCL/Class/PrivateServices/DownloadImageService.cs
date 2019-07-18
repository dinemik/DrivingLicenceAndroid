using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Android.Graphics;
using DrivingLicenceAndroidPCL.Interface;
using DrivingLicenceAndroidPCL.Interface.PrivateServices;

namespace DrivingLicenceAndroidPCL.Class.PrivateServices
{
    internal class DownloadImageService : IDownloadImageService
    {
        private IAnimationService Animation { get; set; }
        private static int ImgCount { get; set; }
        public DownloadImageService()
        { }

        public DownloadImageService(IAnimationService animation)
        {
            Animation = animation;
            ImgCount = 0;
        }

        public async Task<string> DownloadImagesAsync(string url)
        {
            if (url == null)
                return null;
            else
                Animation?.NextImageDownloadAnimation(++ImgCount);

            var filename = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Guid.NewGuid().ToString() + ".png");

            using (HttpClient http = new HttpClient())
            {
                FileStream file = new FileStream(filename, FileMode.Create);

                var imgByte = await http.GetByteArrayAsync(url);
                var bmap = BitmapFactory.DecodeByteArray(imgByte, 0, imgByte.Length).Copy(Bitmap.Config.Rgb565, true);

                bmap.Compress(Bitmap.CompressFormat.Png, 50, file);
                file.Close();
            }

            return filename;
        }
    }
}