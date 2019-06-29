using DrivingLicenceAndroidPCL.Class;
using System.Collections.Generic;
using Android.Support.V7.App;
using Android.Content;
using Android.Widget;
using Android.App;
using Android.OS;
using System.Linq;
using DrivingLicenceAndroidPCL.Model.Interface.DataBase;
using Android.Graphics;
using System;
using Android.Media;
using Android.Util;
using FFImageLoading.Views;
using Android.Support.V7.Widget;
using DrivingLicenceApp.Adapter;

namespace DrivingLicenceApp
{
    [Activity]
    public class TestingActivity : AppCompatActivity
    {
        private IEnumerable<ITicketDb> Topics { get; set; }

        #region UI
        private TextView TimerTxt { get; set; }
        private ImageView HelpImg { get; set; }
        private ImageViewAsync QuestionPic { get; set; }

        private TextView QuestionTxt { get; set; }

        private RecyclerView QuestionsRecView { get; set; }

        private ImageView NextImg { get; set; }
        #endregion

        private int Position { get; set; }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_testing);
            Topics = await new TopicService().GetTopicsByNamesAsync(Intent.GetStringArrayListExtra("Tickets"), 30);

            TimerTxt = FindViewById<TextView>(Resource.Id.TimeTxt);
            HelpImg = FindViewById<ImageView>(Resource.Id.HelpImg);
            QuestionPic = FindViewById<ImageViewAsync>(Resource.Id.QuestionImg);

            QuestionTxt = FindViewById<TextView>(Resource.Id.QuestionTxt);
            QuestionsRecView = FindViewById<RecyclerView>(Resource.Id.QuestionsRecView);

            var manager = new LinearLayoutManager(this)
            { Orientation = OrientationHelper.Vertical };
            QuestionsRecView.SetLayoutManager(manager);

            NextImg = FindViewById<ImageView>(Resource.Id.NextQuestImg);
            NextImg.Click += Next;

            Position = 0;
        }

        private void Answer(object sender, EventArgs args)
        {
            var send = (sender as TextView);
            var elem = Topics.ElementAt(Position);

            send.SetBackgroundColor(elem.Answers.FirstOrDefault(o => o.Answ == send.Text).Correct ? Color.Green : Color.Red);
            QuestionsRecView.GetChildAt(elem.Answers.IndexOf(elem.Answers.First(o => o.Correct))).FindViewById<TextView>(Resource.Id.AnsTxt).SetBackgroundColor(Color.Green);

            NextImg.Enabled = true;
        }
        
        private void Next(object sender, EventArgs args)
        {
            var elem = Topics.ElementAt(Position);

            try
            {
                QuestionPic.LoadImage(elem.Filename);

                if(elem.Filename != null)
                    QuestionPic.SetPadding(20, 20, 20, 20);
                else
                    QuestionPic.SetPadding(0, 0, 0, 0);
            }
            catch (Exception) { }

            QuestionTxt.Text = elem.Question;

            QuestionsRecView.SetAdapter(new AnswerAdapter(elem.Answers, Answer));

            NextImg.Enabled = false;
        }
    }
}