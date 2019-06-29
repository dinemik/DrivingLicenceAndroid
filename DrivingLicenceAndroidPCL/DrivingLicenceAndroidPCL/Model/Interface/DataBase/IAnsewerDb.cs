namespace DrivingLicenceAndroidPCL.Model.Interface.DataBase
{
    public interface IAnswerDb
    {
        int Id { get; set; }
        string Answ { get; set; }
        bool Correct { get; set; }
        int TicketId { get; set; }
    }
}