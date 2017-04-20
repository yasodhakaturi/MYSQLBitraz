using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Analytics.Models
{
    public class UserViewModel
    {
        public string env { get; set; }
        public appUrlModel apiUrl { get; set; }
        public UserInfo userInfo { get; set; }
        public string isAdmin { get; set; }
        public string isclient { get; set; }
        public string appUrl { get; set; }

    }
}