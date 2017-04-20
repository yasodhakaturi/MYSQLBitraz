using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Analytics.Models
{
    public class DashBoardSummary
    {
        public totalUrls totalUrls { get; set; }
        public users users { get; set; }
        public visits visits { get; set; }
        public campaigns campaigns { get; set; }
        public List<recentCampaigns> recentCampaigns { get; set; }
        public activities activities { get; set; }

    }
    public class totalUrls
    {
        public int count { get; set; }
    }
    public class users
    {
        public int total { get; set; }
        public int uniqueUsers { get; set; }
        public int uniqueUsersToday { get; set; }
        public int usersToday { get; set; }
        public int uniqueUsersYesterday { get; set; }
        public int usersYesterday { get; set; }
        public int uniqueUsersLast7days { get; set; }
        public int usersLast7days { get; set; }
    }
    public class visits
    {
        public int total { get; set; }
        public int uniqueVisits { get; set; }
        public int visitsToday { get; set; }
        public int uniqueVisitsToday { get; set; }
        public int visitsYesterday { get; set; }
        public int uniqueVisitsYesterday { get; set; }
        public int uniqueVisitsLast7days { get; set; }
        public int visitsLast7days { get; set; }
    }
    public class campaigns
    {
        public int total { get; set; }
        public int campaignsLast7days { get; set; }
        public int campaignsMonth { get; set; }

    }
    public class recentCampaigns
    {
        public Int64 id { get; set; }
        public string rid { get; set; }
        public string createdOn { get; set; }
        public string endDate { get; set; }
        //public DateTime? crd { get; set; }
        //public DateTime? endd { get; set; }
        public int visits { get; set; }
        public int users { get; set; }
        public bool status { get; set; }
    }
    public class recentCampaigns1
    {
        public Int64 id { get; set; }
        public string rid { get; set; }
        //public string createdOn { get; set; }
        //public string endDate { get; set; }
        public DateTime? crd { get; set; }
        public DateTime? endd { get; set; }
        public int visits { get; set; }
        public int users { get; set; }
        public bool status { get; set; }
    }
    public class activities 
    {
        public today today { get; set; }
        public last7days last7days { get; set; }
        public month month { get; set; }
    }
    public class today
    {
        public int urlTotal { get; set; }
        public double urlPercent { get; set; }
        public int visitsTotal { get; set; }
        public double visitsPercent { get; set; }
        public int revisitsTotal { get; set; }
        public double revisitsPercent { get; set; }
        public int noVisitsTotal { get; set; }
        public double noVisitsPercent { get; set; }

    }
    public class last7days:today
    {
        //public string urlTotal { get; set; }
        //public string urlPercent { get; set; }
        //public string visitsTotal { get; set; }
        //public string visitsPercent { get; set; }
        //public string revisitsTotal { get; set; }
        //public string revisitsPercent { get; set; }
        //public string noVisitsTotal { get; set; }
        //public string noVisitsPercent { get; set; }
    }
    public class month : today
    {
    }
}