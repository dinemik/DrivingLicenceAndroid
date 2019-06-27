using DrivingLicenceAndroidPCL.Interface.Json;
using DrivingLicenceAndroidPCL.Class.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace DrivingLicenceAndroidPCL.Class
{
    public static class DeserializeJson
    {
        public static async Task<IEnumerable<ITopic>> GetTopicsAsync()
        {
            using (HttpClient web = new HttpClient())
            {
                var json = await web.GetStringAsync("https://drivinglicens-93fe9.firebaseio.com/.json");
                return await Task.Run<IEnumerable<ITopic>>(() => JsonConvert.DeserializeObject<List<Topic>>(json));
            }
        }
    }
}