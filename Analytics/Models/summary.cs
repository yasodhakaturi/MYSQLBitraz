using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Analytics.Models
{
    public class Summary
    {
        public string url { get; set; }
        public int visits { get; set; }
        public int? unique_users { get; set; }
        public int total_users { get; set; }
    }
    public class GeoLocationsData1
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public string MobileNumber { get; set; }
        public DateTime? createdOn1 { get; set; }

        public string ShortUrl { get; set; }
    }
    public class GeoLocationsData
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public string MobileNumber { get; set; }
        public string createdOn { get; set; }

        public string ShortUrl { get; set; }
    }
   
}