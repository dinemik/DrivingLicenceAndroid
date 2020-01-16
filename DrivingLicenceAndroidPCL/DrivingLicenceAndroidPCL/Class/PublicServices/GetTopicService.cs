using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrivingLicenceAndroidPCL.Class.PrivateServices;
using DrivingLicenceAndroidPCL.Interface;
using DrivingLicenceAndroidPCL.Model.Interface.DataBase;

namespace DrivingLicenceAndroidPCL.Class.PublicServices
{
    public class GetTopicService
    {
        private IAnimationService Animation { get; set; }

        public GetTopicService() { }

        public GetTopicService(IAnimationService animation) =>
            Animation = animation;


        public async Task<IEnumerable<ICategoryDb>> GetAllOfflineCategoryAsync() =>
            await Task.Run(async () => await GetOfflineCategoryesService.Instance.GetAllOfflineCategoryesAsync());

        public async Task<IEnumerable<ICategoryDb>> GetAllOnlineCategoryAsync()
        {
            Animation?.StartJsonDownloadAnimation();
            var downloadedJson = await Task.Run(async () => await DownloadService.Instance.AllDownloadCategoryesAsync());
            Animation?.EndJsonDownloadAnimation();
            return downloadedJson;
        }

        public async Task<IEnumerable<ICategoryDb>> DownloadCategoryesAsync(bool downloadWithImages = false)
        {
            Animation?.StartJsonDownloadAnimation();
            var down = await DownloadService.Instance.AllDownloadCategoryesAsync();
            Animation?.EndJsonDownloadAnimation();
            await new SaveService(Animation).SaveCategoryesIntoDatabaseAsync(down, downloadWithImages);
            return await GetOfflineCategoryesService.Instance.GetAllOfflineCategoryesAsync();
        }

        public async Task DownloadByCategoryesAsync(IEnumerable<string> categories, bool downloadWithImages = false)
        {
            Animation?.StartJsonDownloadAnimation();
            var down = (await DownloadService.Instance.AllDownloadCategoryesAsync()).Where(o => categories.Any(i => o.Name == i));
            Animation?.EndJsonDownloadAnimation();
            await new SaveService(Animation).SaveCategoryesIntoDatabaseAsync(down, downloadWithImages);
        }
    }
}