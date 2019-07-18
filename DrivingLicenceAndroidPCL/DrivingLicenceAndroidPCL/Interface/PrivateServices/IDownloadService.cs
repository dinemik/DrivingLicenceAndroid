using System.Collections.Generic;
using System.Threading.Tasks;
using DrivingLicenceAndroidPCL.Model.Interface.DataBase;

namespace DrivingLicenceAndroidPCL.Interface.PrivateServices
{
    public interface IDownloadService
    {
        Task<IEnumerable<ICategoryDb>> AllDownloadCategoryesAsync();
    }
}