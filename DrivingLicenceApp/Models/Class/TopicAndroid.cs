using DrivingLicenceApp.Models.Interface;

namespace DrivingLicenceApp.Models.Class
{
    public class TopicAndroid : ITopicAndroid
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isChecked { get; set; }
        public int TicketsCount { get; set; }
    }
}