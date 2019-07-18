namespace DrivingLicenceAndroidPCL.Interface
{
    public interface IAnimationService
    {
        void StartImageDownloadAnimation(int count);
        void NextImageDownloadAnimation(int downloadedImageCount);
        void EndImageDownloadAnimation();

        void StartJsonDownloadAnimation();
        void EndJsonDownloadAnimation();
    }
}