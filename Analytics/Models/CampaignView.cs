using Analytics.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Analytics.Models
{
    public class CampaignView : BaseEntity
    {
        public int id { get; set; }
        public string ReferenceNumber { get; set; }
        public string pwd { get; set; }
        public bool? isactive { get; set; }
        //public List<string> listcampaigns { get; set; }

    }
    public class CampaignView1
    {
        public int Id { get; set; }
        public string ReferenceNumber { get; set; }
        public string CampaignName { get; set; }
        public bool HasPassword { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public DateTime? CreatedDateStr { get; set; }
        public int CreatedUserId { get; set; }
        public string CreatedUserName { get; set; }
        public string CreatedUserEmail { get; set; }
        public bool? CreatedUserActiveState { get; set; }
        //public List<string> listcampaigns { get; set; }

    }

}