using DrivingLicenceAndroidPCL.Model.Interface.All;

namespace DrivingLicenceApp.Models.Interface
{
    public interface ICategoryAndroid : ICategory
    {
        bool Selected { get; set; }
    }
}