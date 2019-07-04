using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System;
using DrivingLicenceAndroidPCL.Model.Class.Json;
using Newtonsoft.Json;
using DrivingLicenceAndroidPCL.Model.Interface.Json;

namespace DrivingLicenceAndroidPCL.Class
{
    public class OfflineSaveService
    {
        public static IEnumerable<ITopicJson> Topics { get; set; } = null;

        public static async Task<IEnumerable<ITopicJson>> DownloadTicketsAsync()
        {
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DrivingLicenceDatabase.Json");

            if (!File.Exists(filePath))
            {
                Topics = (await DeserializeJson.GetTopicsAsync()).Cast<TopicJson>().ToList();
                var json = JsonConvert.SerializeObject(Topics);
                StreamWriter file = new StreamWriter(filePath, true);
                await file.WriteLineAsync(json);
                file.Close();

                return Topics;
            }

            if (Topics == null)
            {
                var readedJson = File.ReadAllText(filePath);
                Topics = JsonConvert.DeserializeObject<List<TopicJson>>(readedJson);
            }

            return Topics;
        }
    }
}