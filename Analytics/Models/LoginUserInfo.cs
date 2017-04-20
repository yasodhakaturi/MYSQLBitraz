using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Analytics.Models
{
    public class LoginUserInfo
    {
        public string redirect_url { get; set; }
        public UserInfo user_info { get; set; }
    }
}