using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Analytics.Models
{
    public class CampaignSummary
    {
        public totalUrls totalUrls { get; set; }
        public users users { get; set; }
        public visits visits { get; set; }
       // public lastactivity lastactivity { get; set; }
        public activities activities { get; set; }


    }
    //public class totalUrls
    //{
    //    public int count { get; set; }
    //}
    //public class users
    //{
    //    public int total { get; set; }
    //    public int uniqueUsers { get; set; }
    //    public int uniqueUsersToday { get; set; }
    //    public int usersToday { get; set; }
    //    public int uniqueUsersYesterday { get; set; }
    //    public int usersYesterday { get; set; }
    //    public int uniqueUsersLast7days { get; set; }
    //    public int usersLast7days { get; set; }
    //}
    //public class visits
    //{
    //    public int total { get; set; }
    //    public int uniqueVisits { get; set; }
    //    public int visitsToday { get; set; }
    //    public int uniqueVisitsToday { get; set; }
    //    public int visitsYesterday { get; set; }
    //    public int uniqueVisitsYesterday { get; set; }
    //    public int uniqueVisitsLast7days { get; set; }
    //    public int visitsLast7days { get; set; }
    //}

    public class lastactivity
    {
        public int urlTotal { get; set; }
        public int visitsTotal { get; set; }
        public int revisitsTotal { get; set; }

    }
  
}