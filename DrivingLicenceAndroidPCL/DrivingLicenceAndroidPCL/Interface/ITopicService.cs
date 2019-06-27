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
using DrivingLicenceAndroidPCL.Interface.Json;

namespace DrivingLicenceAndroidPCL.Interface
{
    public interface ITopicService
    {
        Task<ITopic> GetOneTopic(string name);
        Task<IEnumerable<ITopic>> GetByIDs(IEnumerable<string> names);
        Task<IEnumerable<ITopic>> GetAllTopicAsync();
    }
}