using System.Threading.Tasks;

namespace DrivingLicenceAndroidPCL.Interface.PrivateServices
{
    public interface IDownloadImageService
    {
        Task<string> DownloadImagesAsync(string url);
    }
}