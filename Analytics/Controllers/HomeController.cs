using Analytics.Helpers.BO;
using Analytics.Helpers.Utility;
using Analytics.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Analytics.Controllers
{
    public class HomeController : Controller
    {
        shortenurlEntities dc = new shortenurlEntities();

        public ActionResult Index()
        
       
        {
            //var rnd = new Random();
            //string unsuffled = "0123456789ABCDEFGHIJKLMOPQRSTUVWXYZabcdefghijklmopqrstuvwxyz-.!~*'_";
            //string shuffled = new string(unsuffled.OrderBy(r => rnd.Next()).ToArray());
            UserViewModel obj = new UserViewModel();
            string url = Request.Url.ToString();
            obj = new OperationsBO().GetViewConfigDetails(url);

            //try
            //{
            //    HttpWebRequest webRequest; HttpWebResponse WebResp; Stream response; StreamReader data;
                
                
            //    webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/ShortURLService.svc/AuthenticateUser?Email=testuser@gmail.com&Password=testuser1&Api_Key=WFsAnOaJXENAkUERUGLrVXh3kzEFps1zF9f2N7ZSUQ");
            //    //webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/ShortURLService.svc/AuthenticateUser?Email=testuser@gmail.com&Password=testuser1&Api_Key=WFsAnOaJXENAkUERUGLrVXh3kzEFps1zF9f2N7ZSUQ");

            //    //webRequest = (HttpWebRequest)WebRequest.Create("http://test.mobilytics.ae/ShortURLService.svc/AuthenticateUser?Email=testuser@gmail.com&Password=testpassword&Api_Key=6ZuD3zVvSFGFWDWd0254dwW03ZoBiNF4DgzomnxPhZ8");

                
            //   // webRequest = (HttpWebRequest)WebRequest.Create("http://mobilytics.ae/ShortURLService.svc/AuthenticateUser?Email=testuser@gmail.com&Password=testuser&Api_Key=6gLqU53pOxhcXghYEBxjnKtT/wqKnXugfWz24f7yQ");

                
            //    webRequest.Method = "POST";
            //    webRequest.KeepAlive = true;
            //    //webRequest.Timeout = 100000;
            //    webRequest.ContentType = "application/x-www-form-urlencoded";
            //    //Stream os = null;
            //    //parameters p = new parameters();
            //    //p.UserName = "testuser1";
            //    //p.Email = "testuser@gmail.com";
            //    //p.Password = "testuser1";
            //    //string val = JsonConvert.SerializeObject(p);
            //    //byte[] bytes = Encoding.ASCII.GetBytes(val);
            //    //webRequest.ContentLength = bytes.Length;
            //    //os = webRequest.GetRequestStream();
            //    //os.Write(bytes, 0, bytes.Length);
            //    //os.Close();
            //    webRequest.ContentLength = 0;
            //    WebResp = (HttpWebResponse)webRequest.GetResponse();
            //    //string api_key = WebResp.Headers["Api_key"];
            //    string token = WebResp.Headers["token"];


               // webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/ShortURLService.svc/RegisterCampaign?CampaignName=testcampaign10&Password=teastcampaign5");
                //webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/ShortURLService.svc/RegisterCampaign?CampaignName=testcampaign8&Password=testpassword");

                //webRequest = (HttpWebRequest)WebRequest.Create("http://test.mobilytics.ae/ShortURLService.svc/RegisterCampaign?CampaignName=testcampaign5&Password=testpassword");
                //webRequest = (HttpWebRequest)WebRequest.Create("http://mobilytics.ae/ShortURLService.svc/RegisterCampaign?CampaignName=testcampaign11&Password=testpassword11");

                //webRequest.Method = "GET";
                ////webRequest.Timeout = 12000;
                //webRequest.ContentType = "application/json";
                ////webRequest.Headers.Add("Api_key", api_key);
                //webRequest.Headers.Add("token", token);
                //WebResp = (HttpWebResponse)webRequest.GetResponse();
                ////api_key = WebResp.Headers["Api_key"];
                //token = WebResp.Headers["token"];
                //response = WebResp.GetResponseStream();
                //data = new StreamReader(response);
                //string strres = data.ReadToEnd();
                //var json = JObject.Parse(strres);
                //string ReferenceNumberResult = (string)json["RegisterOrGetCampaignResult"];
                //string RegisterCampaignResult = (string)json["RegisterCampaignResult"];
                //string ReferenceNumber = (string)json["ReferenceNumber"];
               // ReferenceNumber1 ReferenceNumberjson = JsonConvert.DeserializeObject<ReferenceNumber1>(ReferenceNumberResult);
                //string ReferenceNumber = ReferenceNumberjson.ReferenceNumber;


                //webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/ShortURLService.svc/GetShortUrl?CampaignId=2&Type=message&longurlorMessage=https://google.com&mobilenumber=853185468");
                //webRequest = (HttpWebRequest)WebRequest.Create("http://test.mobilytics.ae/ShortURLService.svc/GetShortUrl?CampaignName=testcampaign5&Type=url&longurlorMessage=https://google.com&mobilenumber=8331878564");
                //webRequest = (HttpWebRequest)WebRequest.Create("http://mobilytics.ae/ShortURLService.svc/GetShortUrl?CampaignId=3&Type=url&longurlorMessage=https://google.com&mobilenumber=8331870564");
                //webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/ShortURLService.svc/GetShortUrl?CampaignName=testcampaign8&Type=message&longurlorMessage=smsbm.ae&mobilenumber=0");

                //webRequest.Method = "GET";
                ////webRequest.Timeout = 12000;
                //webRequest.ContentType = "application/json";
                ////webRequest.Headers.Add("Api_key", api_key);
                //webRequest.Headers.Add("token", token);
                //WebResp = (HttpWebResponse)webRequest.GetResponse();
                ////api_key = WebResp.Headers["Api_key"];
                //token = WebResp.Headers["token"];
                //Stream response1 = WebResp.GetResponseStream();
                //data = new StreamReader(response1);
                //string strres1 = data.ReadToEnd();
                //var json1 = JObject.Parse(strres1);

                //string GetShortUrlResult = (string)json1["GetShortUrlResult"];

                //ShortUrl ShortUrljson = JsonConvert.DeserializeObject<ShortUrl>(GetShortUrlResult);
                //string ShortUrl = ShortUrljson.shortUrl;

                // webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/ShortURLService.svc/GetALLCampaigns");

                //webRequest = (HttpWebRequest)WebRequest.Create("http://test.bitraz.com/ShortURLService.svc/GetALLCampaigns");
                //webRequest = (HttpWebRequest)WebRequest.Create("http://mobilytics.ae/ShortURLService.svc/GetALLCampaigns");

                //webRequest.Method = "GET";
                ////webRequest.Timeout = 12000;
                //webRequest.ContentType = "application/json";
                ////webRequest.Headers.Add("Api_key", api_key);
                //webRequest.Headers.Add("token", token);
                //WebResp = (HttpWebResponse)webRequest.GetResponse();
                ////api_key = WebResp.Headers["Api_key"];
                //token = WebResp.Headers["token"];
                //response = WebResp.GetResponseStream();
                //data = new StreamReader(response);
                //strres = data.ReadToEnd();
                //json = JObject.Parse(strres);
                ////string ReferenceNumberResult = (string)json["RegisterOrGetCampaignResult"];
                //string GetALLCampaignsResult = (string)json["GetAllCampaignsResult"];
                ////string ReferenceNumber = (string)json["ReferenceNumber"];
                //List<CampaignsList> campaigns = JsonConvert.DeserializeObject<List<CampaignsList>>(GetALLCampaignsResult);

                //webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/ShortURLService.svc/GetCampaignReferenceNumber?CampaignName=testcampaign5&Password=testpassword");
                //webRequest = (HttpWebRequest)WebRequest.Create("http://mobilytics.ae/ShortURLService.svc/GetCampaignReferenceNumber?CampaignName=testcampaign5&Password=testpassword5");


                //webRequest.Method = "GET";
                ////webRequest.Timeout = 12000;
                //webRequest.ContentType = "application/json";
                ////webRequest.Headers.Add("Api_key", api_key);
                //webRequest.Headers.Add("token", token);
                //WebResp = (HttpWebResponse)webRequest.GetResponse();
                ////api_key = WebResp.Headers["Api_key"];
                //token = WebResp.Headers["token"];
                //response = WebResp.GetResponseStream();
                //data = new StreamReader(response);
                //string strres = data.ReadToEnd();
                //JObject json = JObject.Parse(strres);
                ////string ReferenceNumberResult = (string)json["RegisterOrGetCampaignResult"];
                //string CampaignReferenceNumber = (string)json["GetCampaignReferenceNumberResult"];


                //webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/ShortURLService.svc/GetCampaignAnalyticsData?CampaignReferenceNumber=6");
                //webRequest = (HttpWebRequest)WebRequest.Create("http://mobilytics.ae/ShortURLService.svc/GetCampaignAnalyticsData?CampaignReferenceNumber=05993");

                //webRequest.Method = "GET";
                ////webRequest.Timeout = 12000;
                //webRequest.ContentType = "application/json";
                ////webRequest.Headers.Add("Api_key", api_key);
                //webRequest.Headers.Add("token", token);
                //WebResp = (HttpWebResponse)webRequest.GetResponse();
                ////api_key = WebResp.Headers["Api_key"];
                //token = WebResp.Headers["token"];
                //response = WebResp.GetResponseStream();
                //data = new StreamReader(response);
                //strres = data.ReadToEnd();
                //json = JObject.Parse(strres);
                ////string ReferenceNumberResult = (string)json["RegisterOrGetCampaignResult"];
                //string CampaignAnalyticsDataResult = (string)json["GetCampaignAnalyticsDataResult"];
                ////string ReferenceNumber = (string)json["ReferenceNumber"];
                //List<CampaignsList> CampaignAnalyticsData = JsonConvert.DeserializeObject<List<CampaignsList>>(CampaignAnalyticsDataResult);


            //}
            //catch (WebException webex)
            //{
            //    WebResponse errResp = webex.Response;
            //    using (Stream respStream = errResp.GetResponseStream())
            //    {
            //        StreamReader reader = new StreamReader(respStream);
            //        string text = reader.ReadToEnd();
            //    }
            //}
            //int t = 0;
            //for (int r = 0; r < 100000; r++)
            //{

            //    Helper.GenerateUniqueIDs();
            //    t = r;
            //}
            return View(obj);
            //return View();
        }
        public class CampaignsList
        {
            public string CampaignName { get; set; }
        }
        public class ReferenceNumber1
        {
            public string ReferenceNumber { get; set; }
        }
        public class ShortUrl
        {
            public string shortUrl { get; set; }
        }
        public class parameters
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
        //private static readonly char[] BaseChars =
        //"0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz./,".ToCharArray();
        //private static readonly Dictionary<char, int> CharValues = BaseChars
        //           .Select((c, i) => new { Char = c, Index = i })
        //           .ToDictionary(c => c.Char, c => c.Index);
        //public static long BaseToLong(string number)
        //{
        //    char[] chrs = number.ToCharArray();
        //    int m = chrs.Length - 1;
        //    int n = BaseChars.Length, x;
        //    long result = 0;
        //    for (int i = 0; i < chrs.Length; i++)
        //    {
        //        x = CharValues[chrs[i]];
        //        result += x * (long)Math.Pow(n, m--);
        //    }
        //    return result;
        //}

        public ActionResult Login()
        {
            return View();
        }

        public string testpost(string ClientId, string HitId)
        {
            HttpWebRequest webRequest; HttpWebResponse WebResp=null; Stream response; StreamReader data;
            //webRequest = (HttpWebRequest)WebRequest.Create("");
            //webRequest.Method = "POST";
            ////webRequest.Timeout = 12000;
            //webRequest.ContentType = "application/json";
            ////webRequest.Headers.Add("Api_key", api_key);
            ////webRequest.Headers.Add("token", token);
            //WebResp = (HttpWebResponse)webRequest.GetResponse();
            //api_key = WebResp.Headers["Api_key"];
            //token = WebResp.Headers["token"];
            //JsonTextReader
            //response = WebResp.GetResponseStream();
            //data = new StreamReader(response);
            //string strres = data.ReadToEnd();
            //var json = JObject.Parse(strres);
            ////var json;
            //string ReferenceNumberResult = (string)json["CampaignId"];
            //AnalyticsData obj = JsonConvert.DeserializeObject<AnalyticsData>(dataString);
           //string c=JObject["CampaignId"];

            HitId = "0";
            return HitId;
        }
        public class AnalyticsData
        {
            public int? CampaignId { get; set; }
            public int? ClientId { get; set; }
            public int? HitId { get; set; }
            public int? ShorturlId { get; set; }
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
            public DateTime? CreatedDate { get; set; }
            public string clientName { get; set; }

        }
    }
}