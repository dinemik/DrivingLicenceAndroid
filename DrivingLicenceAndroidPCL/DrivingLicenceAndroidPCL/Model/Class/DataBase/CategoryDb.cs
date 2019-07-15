using System;
using System.Collections.Generic;
using DrivingLicenceAndroidPCL.Model.Interface.DataBase;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace DrivingLicenceAndroidPCL.Model.Class.DataBase
{

    public class CategoryDb : ICategoryDb
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<TopicDb> Topics { get; set; }
    }
}