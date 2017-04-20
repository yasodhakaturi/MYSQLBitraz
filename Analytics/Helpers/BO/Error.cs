using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Analytics.Helpers.BO
{
    public class Error
    {
        public Errormessage error { get; set; }
    }
    public class Errormessage
    {
        public string message { get; set; }
    }
}