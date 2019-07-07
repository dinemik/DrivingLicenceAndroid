using DrivingLicenceAndroidPCL.Class;
using System.Collections.Generic;
using Android.Support.V7.App;
using Android.Content;
using Android.Widget;
using Android.App;
using Android.OS;
using System.Linq;
using Android.Graphics;
using System;
using FFImageLoading.Views;
using Android.Support.V7.Widget;
using DrivingLicenceApp.Adapter;
using System.Timers;
using DrivingLicenceAndroidPCL.Model.Interface.DataBase;
using DrivingLicenceAndroidPCL.Enums;

namespace DrivingLicenceApp
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class TestingActivity : AppCompatActivity
    {
        //tickets for testing.
        private IEnumerable<ITicketDb> Tickets { get; set; }
        private List<int> AnswerIds { get; set; }

        #region UI
        private TextView TimerTxt { get; set; }
        private ImageView HelpImg { get; set; }
#pragma warning disable CS0618 // Type or member is obsolete
        private ImageViewAsync QuestionPic { get; set; }
#pragma warning restore CS0618 // Type or member is obsolete

        private TextView QuestionTxt { get; set; }


        private TextView CorAns { get; set; }
        private TextView FilAns { get; set; }

        private TextView QuestionCount { get; set; }
        private TextView NextQuestion { get; set; }

        private RecyclerView QuestionsRecView { get; set; }

        private ImageView NextImg { get; set; }
        #endregion

        //tickets count
        private int TicketsCount { get; set; }
        //ticket id
        private int Position { get; set; }

        //correct answers count.
        private int CorrectAns { get; set; }
        //filed answers count.
        private int FailedAns { get; set; } 
        //max incorrect answers count
        private int MaxIncorrectCount { get; set; }
        //timer obj...
        private Timer Timer { get; set; }
        //max time...
        private int Sec { get; set; }


        public TestingActivity()
        {
            Sec = 1800;

            TicketsCount = 4;
            Position = 0;
            CorrectAns = 0;
            FailedAns = 0;
            MaxIncorrectCount = 3;
            Timer = new Timer();
            AnswerIds = new List<int>();
        }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_testing);

            //getting tickets.
            if(Intent.GetStringArrayListExtra("Tickets") != null)
                Tickets = await new TopicService().GetTicketsByTopicNamesAsync(Intent.GetStringArrayListExtra("Tickets"), TicketsCount);
            else
                Tickets = await new TopicService().GetTicketsByCount(TicketsCount);

            //getting taked tickets count.
            TicketsCount = Tickets.Count();
            //tst.Dispose();

            //UI.
            TimerTxt = FindViewById<TextView>(Resource.Id.TimeTxt);
            HelpImg = FindViewById<ImageView>(Resource.Id.HelpImg);
#pragma warning disable CS0618 // Type or member is obsolete
            QuestionPic = FindViewById<ImageViewAsync>(Resource.Id.QuestionImg);
#pragma warning restore CS0618 // Type or member is obsolete

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

            //start timer.
            TimerStart();
        }

        private void Answer(object sender, EventArgs args)
        {
            //if correct
            var userAns = Tickets.ElementAt(Position).Answers.FirstOrDefault(o => o.Answ == (sender as TextView).Text);
            (sender as TextView).SetBackgroundColor(userAns.Correct ? Color.Green : Color.Red);
            //add user answer
            AnswerIds.Add(userAns.Id);

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
            if (FailedAns == MaxIncorrectCount)
            {
                Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                alert.SetTitle("არადა კაი იყო 😁");
                alert.SetMessage($"შენ ვერ ჩააბარე გამოცდა იმითომ რომ {MaxIncorrectCount} შეკითხვას გაეცი არასწორი პასუხი");

                Dialog dialog = alert.Create();
                dialog.Show();
            }

            NextImg.Enabled = true;
        }
        
        private async void NextBtn(object sender, EventArgs args)
        {
            //new ticket id.
            Position++;

            //save answers.
            if (Position == TicketsCount)
            {
                Position = 0;
                await new AnsweredService().SaveUserAnswersAsync(Tickets, AnswerIds);

                var endUi = new Intent(this, typeof(EndActivity));
                endUi.PutExtra("TicketsCount", TicketsCount);
                StartActivity(endUi);
            }

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
        private void HelpForAns(object sender, EventArgs args)
        {
            Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
            alert.SetTitle("დახმარება");
            alert.SetMessage(Tickets.ElementAt(Position).Desc);

            Dialog dialog = alert.Create();
            dialog.Show();
        }

        //start timer.
        private void TimerStart()
        {
            //seconds
            Timer.Interval = Sec;
            Timer.Elapsed += Timer_Elapsed;
            Timer.Start();
        }

        //ui change.
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //seconds to minutes and seconds mm:ss
            string formato = $"{Sec/60}:{Sec%60}";
            RunOnUiThread(() => { TimerTxt.Text = formato; });

            if (Sec == 0)
                Cancelar();

            Sec--;
        }

        //disable timer and notified
        private void Cancelar()
        {
            Timer.Enabled = false;
            Timer.Dispose();

            RunOnUiThread(() => {
                Toast.MakeText(Application.Context, "დრო გავიდა", ToastLength.Long).Show();
            });
        }
    }
}