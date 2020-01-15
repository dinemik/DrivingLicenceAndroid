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
using Android.Support.V7.Widget;
using System.Timers;
using DrivingLicenceAndroidPCL.Model.Class.DataBase;
using DrivingLicenceAndroidPCL.Class.PublicServices;
using DrivingLicenceAndroidPCL.Linq;
using DrivingLicenceApp.Class;
using DrivingLicenceApp.ViewPager;

namespace DrivingLicenceApp
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class TestingActivity : AppCompatActivity
    {
        //tickets for testing.
        private IEnumerable<TicketDb> Tickets { get; set; }
        private List<string> Answers { get; set; }

        #region UI
        private TextView TimerTxt { get; set; }
        private ImageView HelpImg { get; set; }
        private TextView CorAns { get; set; }
        private TextView FilAns { get; set; }
        private TextView QuestionCount { get; set; }
        private TextView NextQuestion { get; set; }
        private CheckBox AutoChange { get; set; }


        private Android.Support.V4.View.ViewPager TicketPager { get; set; }
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

        private AndroidAnimations Animations { get; set; }


        public TestingActivity()
        {
            Sec = 1800;

            TicketsCount = 30;
            Position = 0;
            CorrectAns = 0;
            FailedAns = 0;
            MaxIncorrectCount = 3;
            Timer = new Timer();
            Answers = new List<string>();
            Animations = new AndroidAnimations(this);
        }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_testing);

            try
            {
                //getting tickets.

                if(Intent.GetBooleanExtra("Online", false))
                {
                    var tmp = await new GetTopicService(Animations).GetAllOnlineCategoryAsync();
                    if (Intent.GetStringArrayListExtra("Topics") != null)
                        Tickets = tmp.First(o => o.Name == Intent.GetStringExtra("Category")).Topics.Where(o => Intent.GetStringArrayListExtra("Topics").Any(i => i == o.Name)).SelectMany(o => o.TicketsDb).ToList().Shuffle().Take(TicketsCount);
                    else
                        Tickets = tmp.First(o => o.Name == Intent.GetStringExtra("Category")).Topics.SelectMany(o => o.TicketsDb).ToList().Shuffle().Take(TicketsCount);
                }
                else
                {
                    var tmp = await new GetTopicService().GetAllOfflineCategoryAsync();
                    if (Intent.GetStringArrayListExtra("Topics") != null)
                        Tickets = tmp.First(o => o.Name == Intent.GetStringExtra("Category")).Topics.Where(o => Intent.GetStringArrayListExtra("Topics").Any(i => i == o.Name)).SelectMany(o => o.TicketsDb).ToList().Shuffle().Take(TicketsCount);
                    else
                        Tickets = tmp.First(o => o.Name == Intent.GetStringExtra("Category")).Topics.SelectMany(o => o.TicketsDb).ToList().Shuffle().Take(TicketsCount);
                }

                TicketsCount = Tickets.Count();
            }
            catch (Java.Net.UnknownHostException)
            {
                Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                alert.SetTitle("ინტერნეტის კავშირი");
                alert.SetMessage("ეს აპლიკაცია ირთვება პირვლად იმისთვის რომ ჩაირთოს საჭიროა ინტერნეტთან კავშირი შეკითხვების გადმოსაწერათ ერთჯერადი კავშირია.");

                Dialog dialog = alert.Create();
                dialog.Show();
            }


            //getting taked tickets count.
            TicketsCount = Tickets.Count();
            //tst.Dispose();

            //UI.
            TimerTxt = FindViewById<TextView>(Resource.Id.TimeTxt);
            HelpImg = FindViewById<ImageView>(Resource.Id.HelpImg);
          
            CorAns = FindViewById<TextView>(Resource.Id.CorrectAnswers);
            FilAns = FindViewById<TextView>(Resource.Id.FiledAnswers);

            QuestionCount = FindViewById<TextView>(Resource.Id.AllQuestions);
            NextQuestion = FindViewById<TextView>(Resource.Id.NextQuest);
            AutoChange = FindViewById<CheckBox>(Resource.Id.AutoChange);

            //_________
            QuestionCount.Text = Tickets.Count().ToString();
            NextQuestion.Text = (Position + 1).ToString();

            TicketPager = FindViewById<Android.Support.V4.View.ViewPager>(Resource.Id.TicketsPager);
            TicketPager.Adapter = new TicketFragmentAdapter(Tickets.ToList(), Answer, SupportFragmentManager);

            //Next();

            HelpImg.Click += HelpForAns;



            //start timer.
            TimerStart();
        }

        private async void Answer(object sender, EventArgs args, RecyclerView QuestionsRecView)
        {
            //if correct
            var userAns = Tickets.ElementAt(Position).Answers.FirstOrDefault(o => o.Ans == (sender as TextView).Text);
                (sender as TextView).SetBackgroundColor(userAns.Correct ? Color.Green : Color.Red);

            //if not correct
            QuestionsRecView.GetChildAt(Tickets.ElementAt(Position).Answers.IndexOf(Tickets.ElementAt(Position).Answers.First(o => o.Correct))).FindViewById<TextView>(Resource.Id.AnsTxt).SetBackgroundColor(Color.Green);

            //add user answer
            Answers.Add(userAns.Ans);

            //if not correct
            //correct or incorect count detect
            _ = Tickets.ElementAt(Position).Answers.FirstOrDefault(o => o.Ans == (sender as TextView).Text).Correct ? CorrectAns++ : FailedAns++;

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
               // alert.SetTitle("არადა კაი იყო 😁");
                alert.SetMessage($"შენ ვერ ჩააბარე გამოცდა იმითომ რომ {MaxIncorrectCount} შეკითხვას გაეცი არასწორი პასუხი");

                Dialog dialog = alert.Create();
                dialog.Show();
            }

            Position++;
            NextQuestion.Text = (Position + 1).ToString();

            if(AutoChange.Checked)
            {
                TicketPager.SetCurrentItem(Position, true);
            }

            if (Position == TicketsCount)
            {
                await new AnsweredService().SaveUserAnswersAsync(Tickets, Answers);

                var endUi = new Intent(this, typeof(EndActivity));
                endUi.PutExtra("TicketsCount", TicketsCount);
                StartActivity(endUi);

                ClearUi();
            }
        }

        //help method
        private void HelpForAns(object sender, EventArgs args)
        {
            Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
            alert.SetTitle("დახმარება");
            alert.SetMessage(Tickets.ElementAt(Position).Help);

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
            string formato = $"{Sec / 60}:{Sec % 60}";
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

            RunOnUiThread(() =>
            {
                Toast.MakeText(Application.Context, "დრო გავიდა", ToastLength.Long).Show();
            });
        }

        private void ClearUi()
        {
            Position = 0;
            CorrectAns = 0;
            FailedAns = 0;

            CorAns.Text = "0";
            FilAns.Text = "0";
            NextQuestion.Text = "0";

            Answers.Clear();
        }
    }
}