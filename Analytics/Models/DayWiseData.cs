using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Analytics.Models
{
    public class DayWiseData1
    {
        public DateTime? RequestedDate { get; set; }
        //public int Hour { get; set; }
        public int RequestCount { get; set; }

       // public Array daywisearray;
    }
    public class DayWiseData
    {
        public string RequestedDate { get; set; }
        public int RequestCount { get; set; }

    }
    
}