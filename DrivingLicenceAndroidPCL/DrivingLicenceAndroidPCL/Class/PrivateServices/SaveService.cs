using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Android.Graphics;
using DrivingLicenceAndroidPCL.Model.Class.DataBase;
using DrivingLicenceAndroidPCL.Model.Interface.DataBase;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace DrivingLicenceAndroidPCL.Class.PrivateServices
{
    internal class SaveService
    {
        private int ImgCount { get; set; }

        public async Task<bool> SaveCategoryesIntoDatabaseAsync(IEnumerable<ICategoryDb> categories, Action<int> LoadingAnimation = null, bool downloadWithImg = false)
        {
            await Task.Run(async () => {
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

                        //if downloaded
                        var downloaded = db.GetAllWithChildren<CategoryDb>();
                        category.RemoveAll(o => downloaded.Any(i => i.Name == o.Name));
                        //.....

                        /*TODO*/
                        //tmp code.....
                        category.ForEach(o => o.Topics = o.Topics.Take(1).ToList());
                        category.ForEach(o => o.Topics.ForEach(i => i.TicketsDb = i.TicketsDb.Take(10).ToList()));
                        //....

                        ImgCount = category.Count();
                        foreach (var item in category)
                        {
                            item.Img = await DownloadImagesAsync(item.Img, LoadingAnimation);
                        }

                        if (downloadWithImg)
                        {
                            ImgCount = category.SelectMany(o => o.Topics.SelectMany(i => i.TicketsDb)).Where(o => o.Image != null).Count();

                            foreach (var item in category)
                            {
                                foreach (var topic in item.Topics)
                                {
                                    foreach (var ticket in topic.TicketsDb)
                                    {
                                        ticket.Image = await DownloadImagesAsync(ticket.Image, LoadingAnimation);
                                    }
                                }
                            }
                        }

                        db.InsertAllWithChildren(category, true);

                        db.Commit();
                    }
                    catch (SQLiteException)
                    {
                        db.DropTable<CategoryDb>();
                        db.DropTable<TopicDb>();
                        db.DropTable<TicketDb>();
                        db.DropTable<AnswerDb>();
                        await SaveCategoryesIntoDatabaseAsync(categories, LoadingAnimation, downloadWithImg);
                    }
                }
            });
            return true;
        }

        private async Task<string> DownloadImagesAsync(string url, Action<int> action)
        {
            if (url == null)
                return null;
            else
                action?.Invoke(ImgCount);

            var filename = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Guid.NewGuid().ToString() + ".png");

            using (HttpClient http = new HttpClient())
            {
                FileStream file = new FileStream(filename, FileMode.Create);

                var imgByte = await http.GetByteArrayAsync(url);
                var bmap = BitmapFactory.DecodeByteArray(imgByte, 0, imgByte.Length).Copy(Bitmap.Config.Rgb565, true);

                bmap.Compress(Bitmap.CompressFormat.Png, 50, file);
                file.Close();
            }

            return filename;
        }

        public async Task<IEnumerable<ICategoryDb>> GetAllOfflineCategoryesAsync()
        {
            return await Task.Run(() => {
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
    }
}