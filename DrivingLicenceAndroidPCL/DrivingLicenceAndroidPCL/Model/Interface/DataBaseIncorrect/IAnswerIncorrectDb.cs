using DrivingLicenceAndroidPCL.Model.Interface.DataBase;

namespace DrivingLicenceAndroidPCL.Model.Interface.DataBaseIncorrect
{
    public interface IAnswerIncorrectDb : IAnswerDb
    {
        int AnsId { get; set; }
    }
}