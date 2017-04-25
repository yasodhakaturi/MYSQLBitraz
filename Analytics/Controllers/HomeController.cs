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
            //     webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/Service1.svc/GetApiKey?UserName=testuser1&Email=testuser@gmail.com&Password=testuser1");
            //   // webRequest = (HttpWebRequest)WebRequest.Create("http://test.bitraz.com/Service1.svc/GetApiKey?UserName=testcampaign&Email=testcampaign@gmail.com&Password=teastcampaign");

            //    //webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/Service1.svc/GetApiKey");
                
                
            //    webRequest.Method = "POST";
            //    webRequest.KeepAlive = true;
            //    webRequest.Timeout = 100000;
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
            //    string api_key = WebResp.Headers["Api_key"];


            //    webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/Service1.svc/RegisterOrGetCampaign?CampaignName=testcampaign3&Password=teastcampaign3");
            //    //webRequest = (HttpWebRequest)WebRequest.Create("http://test.bitraz.com/Service1.svc/RegisterOrGetCampaign?CampaignName=testcampaign3&Password=teastcampaign3");
            //    webRequest.Method = "GET";
            //    //webRequest.Timeout = 12000;
            //    webRequest.ContentType = "application/json";
            //    webRequest.Headers.Add("Api_key", api_key);
            //    WebResp = (HttpWebResponse)webRequest.GetResponse();
            //    api_key = WebResp.Headers["Api_key"];
            //    response = WebResp.GetResponseStream();
            //    data = new StreamReader(response);
            //    string strres = data.ReadToEnd();
            //    var json = JObject.Parse(strres);
            //    string ReferenceNumberResult = (string)json["RegisterCampaignResult"];
            //    //string ReferenceNumber = (string)json["ReferenceNumber"];
            //    ReferenceNumber1 ReferenceNumberjson = JsonConvert.DeserializeObject<ReferenceNumber1>(ReferenceNumberResult);
            //    string ReferenceNumber = ReferenceNumberjson.ReferenceNumber;


            //    webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/Service1.svc/GetShortUrl?referencenumber=" + ReferenceNumber + "&longurl=https://google.com&mobilenumber=8331877564");
            //    //webRequest = (HttpWebRequest)WebRequest.Create("http://test.bitraz.com/Service1.svc/GetShortUrl?ReferenceNumber=" + ReferenceNumber + "&Longurl=https://google.com&MobileNumber=8331877564");
            //    webRequest.Method = "GET";
            //    //webRequest.Timeout = 12000;
            //    webRequest.ContentType = "application/json";
            //    webRequest.Headers.Add("Api_key", api_key);
            //    WebResp = (HttpWebResponse)webRequest.GetResponse();
            //    api_key = WebResp.Headers["Api_key"];
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