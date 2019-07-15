using DrivingLicenceAndroidPCL.Model.Interface.Json;

namespace DrivingLicenceAndroidPCL.Model.Class.Json
{
    public class AnswerJson : IAnswerJson
    {
        public string Ans { get; set; }
        public bool Correct { get; set; }
    }
}