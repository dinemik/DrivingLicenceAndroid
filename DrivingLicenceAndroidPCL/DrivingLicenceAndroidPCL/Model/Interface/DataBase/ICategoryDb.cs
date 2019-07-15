using DrivingLicenceAndroidPCL.Model.Class.DataBase;
using DrivingLicenceAndroidPCL.Model.Interface.All;
using System.Collections.Generic;

namespace DrivingLicenceAndroidPCL.Model.Interface.DataBase
{
    public interface ICategoryDb : ICategory
    {
        int Id { get; set; }
        List<TopicDb> Topics { get; set; }
    }
}