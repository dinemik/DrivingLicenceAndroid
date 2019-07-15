using DrivingLicenceAndroidPCL.Model.Interface.All;

namespace DrivingLicenceAndroidPCL.Model.Interface.DataBase
{
    public interface IAnswerDb : IAnswer
    {
        int Id { get; set; }
        int TicketId { get; set; }
    }
}