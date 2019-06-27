using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DrivingLicenceAndroidPCL.Class.Json;
using DrivingLicenceAndroidPCL.Interface.Json;
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