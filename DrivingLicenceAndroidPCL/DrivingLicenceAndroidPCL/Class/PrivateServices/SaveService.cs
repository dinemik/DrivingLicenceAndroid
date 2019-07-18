using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrivingLicenceAndroidPCL.Interface;
using DrivingLicenceAndroidPCL.Interface.PrivateServices;
using DrivingLicenceAndroidPCL.Model.Class.DataBase;
using DrivingLicenceAndroidPCL.Model.Interface.DataBase;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace DrivingLicenceAndroidPCL.Class.PrivateServices
{
    internal class SaveService : ISaveService
    {
        private int ImgCount { get; set; }
        private IAnimationService Animation { get; set; }

        public SaveService() { }

        public SaveService(IAnimationService animation) =>
            Animation = animation;


        public async Task<bool> SaveCategoryesIntoDatabaseAsync(IEnumerable<ICategoryDb> categories, bool downloadWithImg = false)
        {
            var conStr = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DrivingLicenceDatabase.db3");

            using (var db = new SQLiteConnection(conStr))
            {
                try
                {
                    List<CategoryDb> category = categories.Cast<CategoryDb>().ToList();

                    db.CreateTable<CategoryDb>();
                    db.CreateTable<TopicDb>();
                    db.CreateTable<TicketDb>();
                    db.CreateTable<AnswerDb>();

                    //if downloaded  TODO
                    var downloaded = db.GetAllWithChildren<CategoryDb>();
                    category.RemoveAll(o => downloaded.Any(i => i.Name == o.Name));
                    //.....

                    /*TODO*/
                    //tmp code.....
                    category.ForEach(o => o.Topics = o.Topics.Take(1).ToList());
                    category.ForEach(o => o.Topics.ForEach(i => i.TicketsDb = i.TicketsDb.Take(10).ToList()));
                    //....

                    var download = new DownloadImageService(Animation);
                    ImgCount = category.Count();

                    /*animation started*/
                    Animation?.StartImageDownloadAnimation(ImgCount);
                    foreach (var item in category)
                    {
                        item.Img = await download.DownloadImagesAsync(item.Img);
                    }
                    /*animation ended*/
                    Animation?.EndImageDownloadAnimation();

                    if (downloadWithImg)
                    {
                        ImgCount = category.SelectMany(o => o.Topics.SelectMany(i => i.TicketsDb)).Where(o => o.Image != null).Count();

                        /*animation started*/
                        Animation?.StartImageDownloadAnimation(ImgCount);
                        foreach (var item in category)
                        {
                            foreach (var topic in item.Topics)
                            {
                                foreach (var ticket in topic.TicketsDb)
                                {
                                    ticket.Image = await download.DownloadImagesAsync(ticket.Image);
                                }
                            }
                        }
                        /*animation ended*/
                        Animation?.EndImageDownloadAnimation();
                    }

                    await Task.Run(() =>
                    {
                        db.InsertAllWithChildren(category, recursive: true);
                    });

                    db.Commit();
                }
                catch (SQLiteException)
                {
                    db.DropTable<CategoryDb>();
                    db.DropTable<TopicDb>();
                    db.DropTable<TicketDb>();
                    db.DropTable<AnswerDb>();

                    await SaveCategoryesIntoDatabaseAsync(categories, downloadWithImg);
                }
            }

            return true;
        }                                               
    }
}