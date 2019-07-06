using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DrivingLicenceAndroidPCL.Model.Interface.DataBase;

namespace DrivingLicenceAndroidPCL.Interface
{
    public interface IAnsweredService
    {
        Task<bool> SaveUserAnswersAsync(IEnumerable<ITicketDb> tickets, IEnumerable<int> answersIDs);
    }
}