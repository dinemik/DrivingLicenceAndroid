using DrivingLicenceAndroidPCL.Model.Interface.All;

namespace DrivingLicenceAndroidPCL.Model.Interface.DataBaseIncorrect
{
    public interface IAnswerIncorrectDb : IAnswer
    {
        int Id { get; set; }
        int AnsId { get; set; }
        int TicketId { get; set; }
    }
}