using Analytics.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Analytics.Models
{
    public class clientView:BaseEntity
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? IsActive { get; set; }
       // public List<clientView> clientsList { get; set; }
    }
    public class clientView1
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        //public string Password { get; set; }
        public bool? IsActive { get; set; }
        // public List<clientView> clientsList { get; set; }
    }
    public class client_API_KEY
    {
        public string API_KEY { get; set; }
    }
}