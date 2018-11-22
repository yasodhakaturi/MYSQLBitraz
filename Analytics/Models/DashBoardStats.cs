using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Analytics.Models
{
    public class DashBoardStats
    {
        public totalUrls_stat totalUrls { get; set; }
        public users_stat users { get; set; }
        public visits_stat visits { get; set; }
        public campaigns_stat campaigns { get; set; }
        public List<recentCampaigns_stat> recentCampaigns { get; set; }
        public activities_stat activities { get; set; }

    }
    public class totalUrls_stat
    {
        public int? count { get; set; }
    }
    public class users_stat
    {
        public int? total { get; set; }
        public int? uniqueUsers { get; set; }
        public int? uniqueUsersToday { get; set; }
        public int? usersToday { get; set; }
        public int? uniqueUsersYesterday { get; set; }
        public int? usersYesterday { get; set; }
        public int? uniqueUsersLast7days { get; set; }
        public int? usersLast7days { get; set; }
    }
    public class visits_stat
    {
        public int? total { get; set; }
        public int? uniqueVisits { get; set; }
        public int? visitsToday { get; set; }
        public int? uniqueVisitsToday { get; set; }
        public int? visitsYesterday { get; set; }
        public int? uniqueVisitsYesterday { get; set; }
        public int? uniqueVisitsLast7days { get; set; }
        public int? visitsLast7days { get; set; }
    }
    public class campaigns_stat
    {
        public int? total { get; set; }
        public int? campaignsLast7days { get; set; }
        public int? campaignsMonth { get; set; }

    }
    public class recentCampaigns_stat
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
    public class recentCampaigns1_stat
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
    public class activities_stat
    {
        public today_stat today { get; set; }
        public last7days_stat last7days { get; set; }
        public month_stat month { get; set; }
    }
    public class today1
    {
        //public int urlTotal { get; set; }
        //public double urlPercent { get; set; }
        //public int visitsTotal { get; set; }
        //public double visitsPercent { get; set; }
        public int? revisitsTotal { get; set; }
        public double? revisitsPercent { get; set; }
        public int? noVisitsTotal { get; set; }
        public double? noVisitsPercent { get; set; }

    }
    public class today2
    {
        public int? urlTotal { get; set; }
        public double? urlPercent { get; set; }
        public int? visitsTotal { get; set; }
        public double? visitsPercent { get; set; }
        //public int revisitsTotal { get; set; }
        //public double revisitsPercent { get; set; }
        //public int noVisitsTotal { get; set; }
        //public double noVisitsPercent { get; set; }

    }
    public class today_stat
    {
        public int? urlTotal { get; set; }
        public double? urlPercent { get; set; }
        public int? visitsTotal { get; set; }
        public double? visitsPercent { get; set; }
        public int? revisitsTotal { get; set; }
        public double? revisitsPercent { get; set; }
        public int? noVisitsTotal { get; set; }
        public double? noVisitsPercent { get; set; }

    }
    public class last7days_stat : today_stat
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
    public class month_stat : today_stat
    {
    }
public class uniqueUsersToday1
{
    public int? uniqueUsersToday { get; set; }
}
public class uniqueVisits1
{
    public int? uniqueVisits { get; set; }
}
public class uniqueVisitsToday1
{
    public int? uniqueVisitsToday { get; set; }

}
public class uniqueVisitsLast7day1
{
    public int? uniqueVisitsLast7day { get; set; }

}
public class visitsLast7days1
{
    public int? visitsLast7days { get; set; }

}
}