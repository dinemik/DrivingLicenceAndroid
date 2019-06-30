using DrivingLicenceAndroidPCL.Model.Interface.All;

namespace DrivingLicenceApp.Models.Interface
{
    public interface ITopicAndroid : ITopic
    {
        bool isChecked { get; set; }
        int TicketsCount { get; set; }
    }
}