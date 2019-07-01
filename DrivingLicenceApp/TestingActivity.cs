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
using FFImageLoading.Views;
using Android.Support.V7.Widget;
using DrivingLicenceApp.Adapter;
using System.Threading.Tasks;
using Felipecsl.GifImageViewLibrary;

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


        private TextView CorAns { get; set; }
        private TextView FilAns { get; set; }

        private TextView QuestionCount { get; set; }
        private TextView NextQuestion { get; set; }

        private RecyclerView QuestionsRecView { get; set; }

        private ImageView NextImg { get; set; }
        #endregion

        //tickets count
        private int TicketsCount { get; set; } = 30;
        //ticket id
        private int Position { get; set; } = 0;

        private int CorrectAns { get; set; } = 0;
        private int FailedAns { get; set; } = 0;
        private int MaxIncorectCount { get; set; } = 3;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_testing);

            //getting tickets.
            Tickets = await new TopicService().GetTopicsByNamesAsync(Intent.GetStringArrayListExtra("Tickets"), TicketsCount);
            //getting taked tickets count.
            TicketsCount = Tickets.Count();
            //tst.Dispose();

            //UI.
            TimerTxt = FindViewById<TextView>(Resource.Id.TimeTxt);
            HelpImg = FindViewById<ImageView>(Resource.Id.HelpImg);
            QuestionPic = FindViewById<ImageViewAsync>(Resource.Id.QuestionImg);

            QuestionTxt = FindViewById<TextView>(Resource.Id.QuestionTxt);
            QuestionsRecView = FindViewById<RecyclerView>(Resource.Id.QuestionsRecView);

            CorAns = FindViewById<TextView>(Resource.Id.CorrectAnswers);
            FilAns = FindViewById<TextView>(Resource.Id.FiledAnswers);

            QuestionCount = FindViewById<TextView>(Resource.Id.AllQuestions);
            NextQuestion = FindViewById<TextView>(Resource.Id.NextQuest);


            NextImg = FindViewById<ImageView>(Resource.Id.NextQuestImg); 
            NextImg.Click += NextBtn;

            //_________
            QuestionCount.Text = Tickets.Count().ToString();
            NextQuestion.Text = (Position + 1).ToString();

            //Recycler View Config.
            var manager = new LinearLayoutManager(this)
            { Orientation = OrientationHelper.Vertical };
            QuestionsRecView.SetLayoutManager(manager);
             
            NextImg.Enabled = false;
            Next();

            HelpImg.Click += HelpForAns;
        }

        private void Answer(object sender, EventArgs args)
        {
            //if correct
            (sender as TextView).SetBackgroundColor(Tickets.ElementAt(Position).Answers.FirstOrDefault(o => o.Answ == (sender as TextView).Text).Correct ? Color.Green : Color.Red);
            //if not correct
            QuestionsRecView.GetChildAt(Tickets.ElementAt(Position).Answers.IndexOf(Tickets.ElementAt(Position).Answers.First(o => o.Correct))).FindViewById<TextView>(Resource.Id.AnsTxt).SetBackgroundColor(Color.Green);
            //correct or incorect count detect
            _ = Tickets.ElementAt(Position).Answers.FirstOrDefault(o => o.Answ == (sender as TextView).Text).Correct ? CorrectAns++ : FailedAns++;

            //disable all answers
            for (int i = 0; i < QuestionsRecView.ChildCount; i++)
                QuestionsRecView.GetChildAt(i).FindViewById<TextView>(Resource.Id.AnsTxt).Enabled = false;

            //cor ans
            CorAns.Text = CorrectAns.ToString();
            //incor ans
            FilAns.Text = FailedAns.ToString();

            // if incorect answers limit set limit -> int MaxIncorectCount = 3 default
            if (FailedAns == MaxIncorectCount) 
                Toast.MakeText(Application.Context, $"შენ ვერ ჩააბარე გამოცდა იმითომ რომ {MaxIncorectCount} შეკითხვას გაეცი არასწორი პასუხი", ToastLength.Long).Show();

            NextImg.Enabled = true;
        }
        
        private void NextBtn(object sender, EventArgs args)
        {
            //new ticket id.
            Position++;
            //question count.
            NextQuestion.Text = (Position + 1).ToString();
            //disable this btn.
            NextImg.Enabled = false;
            //next question.
            Next();
        }

        private void Next()
        {
            //current ticket
            var elem = Tickets.ElementAt(Position);

            //unload image
            QuestionPic.SetImageBitmap(null);

            //set picture
            QuestionPic.LoadImage(elem.Filename, false);
            //picture desing
            int padding = elem.Filename != null ? 20 : 0;
            QuestionPic.SetPadding(padding, padding, padding, padding);

            //set question.
            QuestionTxt.Text = elem.Question;
            
            //recycler view adapter
            QuestionsRecView.SetAdapter(new AnswerAdapter(elem.Answers, Answer));
        }

        //help method
        private void HelpForAns(object sender, EventArgs args) =>
            Toast.MakeText(Application.Context, Tickets.ElementAt(Position).Desc, ToastLength.Long).Show();
    }
}