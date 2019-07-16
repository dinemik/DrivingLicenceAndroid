using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using DrivingLicenceAndroidPCL.Model.Interface.DataBaseIncorrect;
using DrivingLicenceApp.Holder;

namespace DrivingLicenceApp.Adapter
{
    public class EndUiAdapter : RecyclerView.Adapter
    {
        private IEnumerable<ITicketIncorrectDb> Tickets { get; set; }
        public override int ItemCount => Tickets.Count();
        private Context Context { get; set; }

        public EndUiAdapter(IEnumerable<ITicketIncorrectDb> tickets) =>
            Tickets = tickets;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var vh = holder as EndUiHolder;

            vh.QuestionTxt.Text = Tickets.ElementAt(position).Question;

            if(BitmapFactory.DecodeFile(Tickets.ElementAt(position).Image) != null)
                vh.ImageImg.SetImageBitmap(BitmapFactory.DecodeFile(Tickets.ElementAt(position).Image));
            else
                vh.ImageImg.LoadImage(Tickets.ElementAt(position).Image);

            vh.HelpText.Text = Tickets.ElementAt(position).Help;

            var meneger = new LinearLayoutManager(Context)
            { Orientation = LinearLayoutManager.Vertical };
            vh.AnswersRV.SetLayoutManager(meneger);

            vh.AnswersRV.SetAdapter(new AnswerEndAdapter(Tickets.ElementAt(position)));
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            Context = parent.Context;
            return new EndUiHolder(LayoutInflater.From(parent.Context).Inflate(Resource.Layout.EndUI_Item, parent, false));
        }
    }
}