using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrivingLicenceAndroidPCL.Class.PrivateServices;
using DrivingLicenceAndroidPCL.Model.Interface.DataBase;

namespace DrivingLicenceAndroidPCL.Class.PublicServices
{
    public class GetTopicService
    {
        public async Task<IEnumerable<ICategoryDb>> GetAllOfflineCategoryAsync()
        {
            return await Task.Run(async () =>await new SaveService().GetAllOfflineCategoryesAsync());
        }

        public async Task<IEnumerable<ICategoryDb>> GetAllOnlineCategoryAsync(Action Start, Action End)
        {
            return await Task.Run(async () => await DownloadService.Instance.AllDownloadCategoryesAsync(Start, End));
        }

        public async Task<IEnumerable<ICategoryDb>> DownloadCategoryesAsync(Action Start = null, Action End = null, Action<int> ImageLoaging = null, bool downloadWithImages = false)
        {
            return await Task.Run(async () => {
                var down = await DownloadService.Instance.AllDownloadCategoryesAsync(Start, End);
                await new SaveService().SaveCategoryesIntoDatabaseAsync(down, ImageLoaging, downloadWithImages);
                return await new SaveService().GetAllOfflineCategoryesAsync();
            });
        }

        public async Task<IEnumerable<ICategoryDb>> DownloadByCategoryesAsync(IEnumerable<string> categories, Action Start = null, Action End = null, Action<int> ImageLoaging = null, bool downloadWithImages = false)
        {
            return await Task.Run(async () => {
                var down = (await DownloadService.Instance.AllDownloadCategoryesAsync(Start, End)).Where(o => categories.Any(i => o.Name == i));
                await new SaveService().SaveCategoryesIntoDatabaseAsync(down, ImageLoaging, downloadWithImages);
                return await new SaveService().GetAllOfflineCategoryesAsync();
            });
        }
    }
}