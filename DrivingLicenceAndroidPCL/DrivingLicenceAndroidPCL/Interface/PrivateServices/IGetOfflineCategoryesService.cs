using DrivingLicenceAndroidPCL.Model.Interface.DataBase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrivingLicenceAndroidPCL.Interface.PrivateServices
{
    internal interface IGetOfflineCategoryesService
    {
        Task<IEnumerable<ICategoryDb>> GetAllOfflineCategoryesAsync();
    }
}