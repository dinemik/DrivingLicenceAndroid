using DrivingLicenceAndroidPCL.Interface.PrivateServices;
using DrivingLicenceAndroidPCL.Model.Class.DataBase;
using DrivingLicenceAndroidPCL.Model.Interface.DataBase;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrivingLicenceAndroidPCL.Class.PrivateServices
{
    internal class DownloadService : IDownloadService
    {
        private static IEnumerable<ICategoryDb> Categories { get; set; }

        private static DownloadService instance = null;
        public static DownloadService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DownloadService();
                }
                return instance;
            }
        }
        private DownloadService()
        {
            Categories = null;
        }


        public async Task<IEnumerable<ICategoryDb>> AllDownloadCategoryesAsync()
        {
            if (Categories == null)
            {
                Categories = (await DeserializeJson.GetTopicsAsync()).Select(categoryes => new CategoryDb
                {

                    Img = categoryes.Img,
                    Name = categoryes.Name,
                    Topics = categoryes.Topics.Select(topic => new TopicDb
                    {
                        Name = topic.Name,
                        TicketsDb = topic.Tickets.Select(tickets => new TicketDb
                        {
                            Answers = tickets.Answers.Select(ans => new AnswerDb
                            {
                                Ans = ans.Ans,
                                Correct = ans.Correct
                            }).ToList(),
                            Help = tickets.Help,
                            Image = tickets.Image,
                            Question = tickets.Question,
                        }).ToList()
                    }).ToList()
                });
            }
            return Categories;
        }
    }
}