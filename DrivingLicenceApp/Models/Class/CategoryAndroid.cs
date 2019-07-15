using DrivingLicenceApp.Models.Interface;

namespace DrivingLicenceApp.Models.Class
{
    public class CategoryAndroid : ICategoryAndroid
    {
        public bool Selected { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
    }
}