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
        //tickets for testing.
        private IEnumerable<ITicketDb> Tickets { get; set; }

        #region UI
        private TextView TimerTxt { get; set; }
        private ImageView HelpImg { get; set; }
        private ImageViewAsync QuestionPic { get; set; }

        private TextView QuestionTxt { get; set; }

        private RecyclerView QuestionsRecView { get; set; }

        private ImageView NextImg { get; set; }
        #endregion

        //tickets count
        private int TicketsCount { get; set; } = 30;
        //ticket id
        private int Position { get; set; } = 0;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_testing);

            //getting tickets.
            Tickets = await new TopicService().GetTopicsByNamesAsync(Intent.GetStringArrayListExtra("Tickets"), TicketsCount);
            //getting taked tickets count.
            TicketsCount = Tickets.Count();

            //UI.
            TimerTxt = FindViewById<TextView>(Resource.Id.TimeTxt);
            HelpImg = FindViewById<ImageView>(Resource.Id.HelpImg);
            QuestionPic = FindViewById<ImageViewAsync>(Resource.Id.QuestionImg);

            QuestionTxt = FindViewById<TextView>(Resource.Id.QuestionTxt);
            QuestionsRecView = FindViewById<RecyclerView>(Resource.Id.QuestionsRecView);

            NextImg = FindViewById<ImageView>(Resource.Id.NextQuestImg);
            NextImg.Click += NextBtn;

            //Recycler View Config.
            var manager = new LinearLayoutManager(this)
            { Orientation = OrientationHelper.Vertical };
            QuestionsRecView.SetLayoutManager(manager);

            NextImg.Enabled = false;
            Next();
        }

        private void Answer(object sender, EventArgs args)
        {
            //if correct
            (sender as TextView).SetBackgroundColor(Tickets.ElementAt(Position).Answers.FirstOrDefault(o => o.Answ == (sender as TextView).Text).Correct ? Color.Green : Color.Red);
            //if not correct
            QuestionsRecView.GetChildAt(Tickets.ElementAt(Position).Answers.IndexOf(Tickets.ElementAt(Position).Answers.First(o => o.Correct))).FindViewById<TextView>(Resource.Id.AnsTxt).SetBackgroundColor(Color.Green);

            //disable all answers
            for (int i = 0; i < QuestionsRecView.ChildCount; i++)
                QuestionsRecView.GetChildAt(i).FindViewById<TextView>(Resource.Id.AnsTxt).Enabled = false;

            NextImg.Enabled = true;
        }
        
        private void NextBtn(object sender, EventArgs args)
        {
            //new ticket id.
            Position++;

            Next();

            NextImg.Enabled = false;
        }

        private void Next()
        {
            //current ticket
            var elem = Tickets.ElementAt(Position);

            //set picture
            QuestionPic.LoadImage(elem.Filename, false);
            //picture desing
            int padding = elem.Filename != null ? 20 : 0;
            QuestionPic.SetPadding(padding, padding, padding, padding);
            //unload image
            if(padding == 0)
                QuestionPic.SetImageBitmap(null);

            QuestionTxt.Text = elem.Question;
            
            //recycler view adapter
            QuestionsRecView.SetAdapter(new AnswerAdapter(elem.Answers, Answer));
        }
    }
}