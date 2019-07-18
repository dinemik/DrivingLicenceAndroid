using DrivingLicenceAndroidPCL.Interface.PrivateServices;
using DrivingLicenceAndroidPCL.Model.Class.DataBase;
using DrivingLicenceAndroidPCL.Model.Interface.DataBase;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrivingLicenceAndroidPCL.Class.PrivateServices
{
    internal class GetOfflineCategoryesService : IGetOfflineCategoryesService
    {
        private IEnumerable<ICategoryDb> OfflineCategoryes { get; set; }

        private static GetOfflineCategoryesService instance;

        public static GetOfflineCategoryesService Instance {
            get {
                if (instance == null)
                    instance = new GetOfflineCategoryesService();
                return instance;
            }
        }

        private GetOfflineCategoryesService()
        { }

        public async Task<IEnumerable<ICategoryDb>> GetAllOfflineCategoryesAsync()
        {
            if (OfflineCategoryes == null)
            {
                return await Task.Run(() =>
                {
                    try
                    {
                        var conStr = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DrivingLicenceDatabase.db3");

                        using (var db = new SQLiteConnection(conStr))
                        {
                            return db.GetAllWithChildren<CategoryDb>(recursive: true);
                        }
                    }
                    catch (SQLiteException)
                    {
                        return new List<CategoryDb> { /**/ };
                    }
                });
            }
            else
            {
                return OfflineCategoryes;
            }
        }
    }
}