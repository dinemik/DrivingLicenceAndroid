using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using DrivingLicenceAndroidPCL.Model.Class.DataBase;
using DrivingLicenceApp.Holder;

namespace DrivingLicenceApp.Adapter
{
    class AnswerAdapter : RecyclerView.Adapter
    {
        public List<AnswerDb> Answers { get; set; }
        Action<object, EventArgs> Click { get; set; }

        public override int ItemCount => Answers.Count;

        public AnswerAdapter(List<AnswerDb> answers, Action<object, EventArgs> click)
        {
            Answers = answers;
            Click = click;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var hold = holder as AnswerHolder;
            hold.AnswerTxt.Text = Answers[position].Answ;
            hold.AnswerTxt.Click += (s, e) => Click(s, e);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) =>
            new AnswerHolder(LayoutInflater.From(parent.Context).Inflate(Resource.Layout.answer_Item, parent, false));
    }
}