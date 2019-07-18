using DrivingLicenceAndroidPCL.Model.Interface.DataBase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrivingLicenceAndroidPCL.Interface.PrivateServices
{
    public interface ISaveService
    {
        Task<bool> SaveCategoryesIntoDatabaseAsync(IEnumerable<ICategoryDb> categories, bool downloadWithImg = false);
    }
}