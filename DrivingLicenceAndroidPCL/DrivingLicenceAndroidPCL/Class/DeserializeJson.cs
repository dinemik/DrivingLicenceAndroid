using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using DrivingLicenceAndroidPCL.Model.Interface.Json;
using DrivingLicenceAndroidPCL.Model.Class.Json;

namespace DrivingLicenceAndroidPCL.Class
{
    public static class DeserializeJson
    {
        /*
         * 1. Download string 'json'
         * 2. Deserialize 
         */
        public static async Task<IEnumerable<ICategoryJson>> GetTopicsAsync()
        {
            using (HttpClient web = new HttpClient())
            {
                var json = await web.GetStringAsync("https://drivinglicencenew.firebaseio.com/.json");
                return await Task.Run<IEnumerable<ICategoryJson>>(() => JsonConvert.DeserializeObject<List<CategoryJson>>(json));
            }
        }
    }
}