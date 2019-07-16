using Android.Support.V7.Widget;
using Android.Views;
using DrivingLicenceAndroidPCL.Model.Interface.DataBase;
using DrivingLicenceAndroidPCL.Model.Interface.DataBaseIncorrect;
using DrivingLicenceApp.Holder;
using System.Collections.Generic;
using System.Linq;

namespace DrivingLicenceApp.Adapter
{
    public class AnswerEndAdapter : RecyclerView.Adapter
    {
        public IEnumerable<IAnswerIncorrectDb> Answers { get; set; }
        public ITicketIncorrectDb Ticket { get; set; }
        public override int ItemCount => Answers.Count();


        public AnswerEndAdapter(ITicketIncorrectDb ticket)
        {
            Answers = ticket.Answers;
            Ticket = ticket;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var vh = holder as AnswerEndHolder;
            vh.Answer.Text = Answers.ElementAt(position).Ans;

            if (Answers.ElementAt(position).Ans == Ticket.UserAnswer)
                vh.Answer.SetBackgroundColor(Android.Graphics.Color.Red);

            if (Answers.ElementAt(position).Correct)
                vh.Answer.SetBackgroundColor(Android.Graphics.Color.Green);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) =>
            new AnswerEndHolder(LayoutInflater.From(parent.Context).Inflate(Resource.Layout.answer_Item, parent, false));
    }
}