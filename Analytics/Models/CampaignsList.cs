using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Analytics.Models
{
    public class CampaignsList1
    {
        public int id { get; set; }
       // public int? cid { get; set; }
        public string CampaignName { get; set; }
        public string rid { get; set; }
        public DateTime? createdOn { get; set; }
        public DateTime? endDate { get; set; }
    }
    public class CampaignsList
    {
        public int id { get; set; }
        //public int? cid { get; set; }
        public string CampaignName { get; set; }

        public string rid { get; set; }
        public string createdOn { get; set; }
        public string endDate { get; set; }
    }
}
