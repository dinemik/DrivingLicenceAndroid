using System;
using System.Collections.Generic;
using DrivingLicenceAndroidPCL.Model.Interface.Json;

namespace DrivingLicenceAndroidPCL.Model.Class.Json
{
    public class CategoryJson : ICategoryJson
    {
        public List<TopicJson> Topics { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
    }
}