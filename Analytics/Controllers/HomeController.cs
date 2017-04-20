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
            //    // webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/Service1.svc/GetApiKey?UserName=testcampaign1&Email=testcampaign1@yahoo.co.in&Password=teastcampaign1");
            //    //webRequest = (HttpWebRequest)WebRequest.Create("http://test.bitraz.com/Service1.svc/GetApiKey?UserName=testcampaign1&Email=testcampaign@gmail.com&Password=teastcampaign1");

            //    webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/Service1.svc/GetApiKey");
            //    webRequest.Method = "POST";
            //    webRequest.KeepAlive = true;
            //    webRequest.Timeout = 100000;
            //    webRequest.ContentType = "application/x-www-form-urlencoded";
            //    Stream os = null;
            //    parameters p = new parameters();
            //    p.UserName = "testcampaign";
            //    p.Email = "testcampaign@gmail.com";
            //    p.Password = "testcampaign";
            //    string val = JsonConvert.SerializeObject(p);
            //    byte[] bytes = Encoding.ASCII.GetBytes(val);
            //    webRequest.ContentLength = bytes.Length;
            //    os = webRequest.GetRequestStream();
            //    os.Write(bytes, 0, bytes.Length);
            //    os.Close();
            //    WebResp = (HttpWebResponse)webRequest.GetResponse();
            //    string api_key = WebResp.Headers["API_KEY"];


            //    //webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/Service1.svc/RegisterCampaign?CampaignName=testcampaign5&Password=teastcampaign1");
            //    webRequest = (HttpWebRequest)WebRequest.Create("http://test.bitraz.com/Service1.svc/RegisterCampaign?CampaignName=testcampaign5&Password=teastcampaign1");
            //    webRequest.Method = "GET";
            //    //webRequest.Timeout = 12000;
            //    webRequest.ContentType = "application/json";
            //    webRequest.Headers.Add("API_KEY", api_key);
            //    WebResp = (HttpWebResponse)webRequest.GetResponse();
            //    api_key = WebResp.Headers["API_KEY"];
            //    response = WebResp.GetResponseStream();
            //    data = new StreamReader(response);
            //    string strres = data.ReadToEnd();
            //    var json = JObject.Parse(strres);
            //    string ReferenceNumberResult = (string)json["RegisterCampaignResult"];
            //    //string ReferenceNumber = (string)json["ReferenceNumber"];
            //    ReferenceNumber1 ReferenceNumberjson = JsonConvert.DeserializeObject<ReferenceNumber1>(ReferenceNumberResult);
            //    string ReferenceNumber = ReferenceNumberjson.ReferenceNumber;


            //    //webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/Service1.svc/GetShortUrl?referencenumber=" + ReferenceNumber + "&longurl=google.com&mobilenumber=8331877564");
            //    webRequest = (HttpWebRequest)WebRequest.Create("http://test.bitraz.com/Service1.svc/GetShortUrl?ReferenceNumber=" + ReferenceNumber + "&Longurl=google.com&MobileNumber=8331877564");
            //    webRequest.Method = "GET";
            //    //webRequest.Timeout = 12000;
            //    webRequest.ContentType = "application/json";
            //    webRequest.Headers.Add("API_KEY", api_key);
            //    WebResp = (HttpWebResponse)webRequest.GetResponse();
            //    api_key = WebResp.Headers["API_KEY"];
            //    Stream response1 = WebResp.GetResponseStream();
            //    data = new StreamReader(response1);
            //    string strres1 = data.ReadToEnd();
            //    var json1 = JObject.Parse(strres1);

            //    string GetShortUrlResult = (string)json1["GetShortUrlResult"];

            //    ShortUrl ShortUrljson = JsonConvert.DeserializeObject<ShortUrl>(GetShortUrlResult);
            //    string ShortUrl = ShortUrljson.shortUrl;

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
     


    }
}