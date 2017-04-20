using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Analytics.Models
{
    public class MobileNumbersList
    {
        
            public List<string> MobileNumbers { get; set; }
        //public string name { get; set; }

          
    }

    public class BatchInput
    {
        public string ReferenceNumber { get; set; }
        public string MobileNumber { get; set; }
        public string LongUrl { get; set; }
        public string type { get; set; }
    }
    public class exportDataModel
    {
        public string Status { get; set; }
        public string MobileNumber { get; set; }
        public string ShortenUrl { get; set; }
        public int BatchID { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
    public class BatchStatus
    {
        public string Status { get; set; }
        public int BatchID { get; set; }

    }
    public class BatchIDList
    {
        public int BatchID { get; set; }
        public string BatchName { get; set; }
        public string CreatedDate{get;set;}
        public String Status{get;set;}
    }
    public class BatchDownload
    {
        public string Mobilenumber { get; set; }
        public string ShortUrl { get; set; }
    }
    public class ExportAnalyticsData
    {
        public string CampaignName { get; set; }
        public string Mobilenumber { get; set; }
        public string ShortURL { get; set; }
        public string LongUrl { get; set; }
        public string GoogleMapUrl { get; set; }
        public string IPAddress { get; set; }
        public string Browser { get; set; }
        public string BrowserVersion { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string PostalCode { get; set; }
        public string Lattitude { get; set; }
        public string Longitude { get; set; }
        public string MetroCode { get; set; }
        public string DeviceName { get; set; }
        public string DeviceBrand { get; set; }
        public string OS_Name { get; set; }
        public string OS_Version { get; set; }
        public string IsMobileDevice { get; set; }
        public string CreatedDate { get; set; }
        public string clientName { get; set; }
    }
}