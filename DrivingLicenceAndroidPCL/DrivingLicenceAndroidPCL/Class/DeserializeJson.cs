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
        public static async Task<IEnumerable<ITopicJson>> GetTopicsAsync()
        {
            using (HttpClient web = new HttpClient())
            {
                var json = await web.GetStringAsync("https://drivinglicens-93fe9.firebaseio.com/.json");
                return await Task.Run<IEnumerable<ITopicJson>>(() => JsonConvert.DeserializeObject<List<TopicJson>>(json));
            }
        }
    }
}