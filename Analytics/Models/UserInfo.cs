using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Analytics.Models
{
    public class UserInfo
    {
        public int user_id { get; set; }
        public string user_role { get; set; }
        public string user_name { get; set; }
    }
    public class Analytics_Share
    {
        public string rid { get; set; }
        public bool has_authentication { get; set; }
        public bool is_authorized { get; set; }
    }
}