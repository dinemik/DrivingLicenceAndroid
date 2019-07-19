using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using DrivingLicenceAndroidPCL.Model.Class.DataBase;
using DrivingLicenceApp.Adapter;
using FFImageLoading.Views;
using System;

namespace DrivingLicenceApp.ViewPager
{
    public class TicketFragment : Fragment
    {
        private TicketDb Elem { get; set; }
        private Action<object, EventArgs, RecyclerView> Answer { get; set; }

        public TicketFragment(TicketDb elem, Action<object, EventArgs, RecyclerView> answer)
        {
            Elem = elem;
            Answer = answer;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.ticketFragment, container, false);

            var quest = view.FindViewById<TextView>(Resource.Id.QuestionTxt);
            var img = view.FindViewById<ImageViewAsync>(Resource.Id.QuestionImg);
            var rec = view.FindViewById<RecyclerView>(Resource.Id.AnswRecView);

            //set picture
            if (BitmapFactory.DecodeFile(Elem.Image) != null)
                img.SetImageBitmap(BitmapFactory.DecodeFile(Elem.Image));
            else
                img.LoadImage(Elem.Image);

            //picture desing
            int padding = Elem.Image != null ? 20 : 0;
            img.SetPadding(padding, padding, padding, padding);

            //set question.
            quest.Text = Elem.Question;

            img.Click += (s, e) =>
            {
                var resiz = new Intent(container.Context, typeof(ResizeImageActivity));
                resiz.PutExtra("TicketImage", Elem.Image);
                StartActivity(resiz);
            };

            var manager = new LinearLayoutManager(container.Context)
            { Orientation = OrientationHelper.Vertical };

            rec.SetLayoutManager(manager);
            rec.SetAdapter(new AnswerAdapter(Elem.Answers, (s, e) => Answer.Invoke(s, e, rec)));

            return view;
        }
    }
}