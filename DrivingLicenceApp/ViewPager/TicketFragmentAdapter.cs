using Android.Support.V4.App;
using Android.Support.V7.Widget;
using DrivingLicenceAndroidPCL.Model.Class.DataBase;
using DrivingLicenceAndroidPCL.Model.Interface.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DrivingLicenceApp.ViewPager
{
    public class TicketFragmentAdapter : FragmentPagerAdapter
    {
        private List<TicketDb> Tickets { get; set; }
        private Action<object, EventArgs, RecyclerView> Answer { get; set; }

        public override int Count => Tickets.Count();


        public TicketFragmentAdapter(List<TicketDb> tickets, Action<object, EventArgs, RecyclerView> answer, FragmentManager manager) : base(manager)
        {
            Tickets = tickets;
            Answer = answer;
        }

        public override Fragment GetItem(int position) =>
            new TicketFragment(Tickets.ElementAt(position), Answer);
    }
}