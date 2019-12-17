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
            
            //MSYNC();
            //try
            //{
            //    HttpWebRequest webRequest; HttpWebResponse WebResp; Stream response; StreamReader data;


            //    //webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/ShortURLService.svc/AuthenticateUser?Email=testuser@gmail.com&Password=testuser1&Api_Key=WFsAnOaJXENAkUERUGLrVXh3kzEFps1zF9f2N7ZSUQ");
            //    //webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/ShortURLService.svc/AuthenticateUser?Email=testuser@gmail.com&Password=testuser1&Api_Key=WFsAnOaJXENAkUERUGLrVXh3kzEFps1zF9f2N7ZSUQ");

            //    //webRequest = (HttpWebRequest)WebRequest.Create("http://test.mobilytics.ae/ShortURLService.svc/AuthenticateUser?Email=testuser@gmail.com&Password=testpassword&Api_Key=6ZuD3zVvSFGFWDWd0254dwW03ZoBiNF4DgzomnxPhZ8");


            //     webRequest = (HttpWebRequest)WebRequest.Create("http://mobilytics.ae/ShortURLService.svc/AuthenticateUser?Email=testuser@gmail.com&Password=testuser&Api_Key=6gLqU53pOxhcXghYEBxjnKtT/wqKnXugfWz24f7yQ");


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


            //    //webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/ShortURLService.svc/RegisterCampaign?CampaignName=testcampaign10&Password=teastcampaign5");
            //    //webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/ShortURLService.svc/RegisterCampaign?CampaignName=testcampaign8&Password=testpassword");

            //    //webRequest = (HttpWebRequest)WebRequest.Create("http://test.mobilytics.ae/ShortURLService.svc/RegisterCampaign?CampaignName=testcampaign5&Password=testpassword");
            //    webRequest = (HttpWebRequest)WebRequest.Create("http://mobilytics.ae/ShortURLService.svc/RegisterCampaign?CampaignName=testcampaign25&Password=testpassword25&WebHookUrl=");

            //    webRequest.Method = "GET";
            //    //webRequest.Timeout = 12000;
            //    webRequest.ContentType = "application/json";
            //    //webRequest.Headers.Add("Api_key", api_key);
            //    webRequest.Headers.Add("token", token);
            //    WebResp = (HttpWebResponse)webRequest.GetResponse();
            //    //api_key = WebResp.Headers["Api_key"];
            //    token = WebResp.Headers["token"];
            //    response = WebResp.GetResponseStream();
            //    data = new StreamReader(response);
            //    string strres = data.ReadToEnd();
            //    var json = JObject.Parse(strres);
            //    //string ReferenceNumberResult = (string)json["RegisterOrGetCampaignResult"];
            //    string RegisterCampaignResult = (string)json["RegisterCampaignResult"];
            //    //string ReferenceNumber = (string)json["ReferenceNumber"];
            //    //ReferenceNumber1 ReferenceNumberjson = JsonConvert.DeserializeObject<ReferenceNumber1>(ReferenceNumberResult);
            //    //string ReferenceNumber = ReferenceNumberjson.ReferenceNumber;


            //    //webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/ShortURLService.svc/GetShortUrl?CampaignId=2&Type=message&longurlorMessage=https://google.com&mobilenumber=853185468");
            //    //webRequest = (HttpWebRequest)WebRequest.Create("http://test.mobilytics.ae/ShortURLService.svc/GetShortUrl?CampaignName=testcampaign5&Type=url&longurlorMessage=https://google.com&mobilenumber=8331878564");
            //    webRequest = (HttpWebRequest)WebRequest.Create("http://mobilytics.ae/ShortURLService.svc/GetShortUrl?CampaignId=3&Type=url&longurlorMessage=https://google.com&mobilenumber=8331870564");
            //    //webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/ShortURLService.svc/GetShortUrl?CampaignName=testcampaign8&Type=message&longurlorMessage=smsbm.ae&mobilenumber=0");

            //    webRequest.Method = "GET";
            //    //webRequest.Timeout = 12000;
            //    webRequest.ContentType = "application/json";
            //    //webRequest.Headers.Add("Api_key", api_key);
            //    webRequest.Headers.Add("token", token);
            //    WebResp = (HttpWebResponse)webRequest.GetResponse();
            //    //api_key = WebResp.Headers["Api_key"];
            //    token = WebResp.Headers["token"];
            //    Stream response1 = WebResp.GetResponseStream();
            //    data = new StreamReader(response1);
            //    string strres1 = data.ReadToEnd();
            //    var json1 = JObject.Parse(strres1);

            //    string GetShortUrlResult = (string)json1["GetShortUrlResult"];

            //    ShortUrl ShortUrljson = JsonConvert.DeserializeObject<ShortUrl>(GetShortUrlResult);
            //    string ShortUrl = ShortUrljson.shortUrl;

            //    //webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/ShortURLService.svc/GetALLCampaigns");

            //    //webRequest = (HttpWebRequest)WebRequest.Create("http://test.bitraz.com/ShortURLService.svc/GetALLCampaigns");
            //    webRequest = (HttpWebRequest)WebRequest.Create("http://mobilytics.ae/ShortURLService.svc/GetALLCampaigns");

            //    webRequest.Method = "GET";
            //    //webRequest.Timeout = 12000;
            //    webRequest.ContentType = "application/json";
            //    //webRequest.Headers.Add("Api_key", api_key);
            //    webRequest.Headers.Add("token", token);
            //    WebResp = (HttpWebResponse)webRequest.GetResponse();
            //    //api_key = WebResp.Headers["Api_key"];
            //    token = WebResp.Headers["token"];
            //    response = WebResp.GetResponseStream();
            //    data = new StreamReader(response);
            //    strres = data.ReadToEnd();
            //    json = JObject.Parse(strres);
            //    //string ReferenceNumberResult = (string)json["RegisterOrGetCampaignResult"];
            //    string GetALLCampaignsResult = (string)json["GetAllCampaignsResult"];
            //    //string ReferenceNumber = (string)json["ReferenceNumber"];
            //    List<CampaignsList> campaigns = JsonConvert.DeserializeObject<List<CampaignsList>>(GetALLCampaignsResult);




            //    //webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/ShortURLService.svc/GetCampaignAnalyticsData?CampaignId=6");
            //    webRequest = (HttpWebRequest)WebRequest.Create("http://mobilytics.ae/ShortURLService.svc/GetCampaignAnalyticsData?CampaignId=3");

            //    webRequest.Method = "GET";
            //    //webRequest.Timeout = 12000;
            //    webRequest.ContentType = "application/json";
            //    //webRequest.Headers.Add("Api_key", api_key);
            //    webRequest.Headers.Add("token", token);
            //    WebResp = (HttpWebResponse)webRequest.GetResponse();
            //    //api_key = WebResp.Headers["Api_key"];
            //    token = WebResp.Headers["token"];
            //    response = WebResp.GetResponseStream();
            //    data = new StreamReader(response);
            //    strres = data.ReadToEnd();
            //    json = JObject.Parse(strres);
            //    //string ReferenceNumberResult = (string)json["RegisterOrGetCampaignResult"];
            //    string CampaignAnalyticsDataResult = (string)json["GetCampaignAnalyticsDataResult"];
            //    //string ReferenceNumber = (string)json["ReferenceNumber"];
            //    List<CampaignsList> CampaignAnalyticsData = JsonConvert.DeserializeObject<List<CampaignsList>>(CampaignAnalyticsDataResult);


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
        //start tempfunc
        public void MSYNC()
        {
            try
            {
                //ErrorLogs.LogErrorData("test", "test");
                shorturlclickreference clickref = dc.shorturlclickreferences.Select(x => x).FirstOrDefault();
                shorturlclickreference shrt_clickref = new shorturlclickreference();
                int gettodayshorturlid = 0; int gettodayuserid = 0;
                if (clickref == null)
                {
                    gettodayshorturlid = dc.shorturldatas.AsEnumerable().Where(x => x.CreatedDate != null && x.CreatedDate.Value.Date == DateTime.UtcNow.Date).Min(x => x.PK_Shorturl);
                    gettodayuserid = dc.uiddatas.AsEnumerable().Where(x => x.CreatedDate != null && x.CreatedDate.Value.Date == DateTime.UtcNow.Date).Min(x => x.PK_Uid);

                    shrt_clickref.Ref_ShorturlClickID = gettodayshorturlid;
                    shrt_clickref.Ref_UsersID = gettodayuserid;
                    dc.shorturlclickreferences.Add(shrt_clickref);
                    dc.SaveChanges();
                    clickref = dc.shorturlclickreferences.Select(x => x).FirstOrDefault();
                }
                int cntvisits = dc.shorturldatas.Where(x => x.PK_Shorturl > clickref.Ref_ShorturlClickID).Count();
                int cntusers = dc.uiddatas.Where(x => x.PK_Uid > clickref.Ref_UsersID).Count();
                if (cntvisits > 0 || cntusers > 0)
                {
                    int getnextshorturlid = 0; int getnextuserid = 0;
                    List<StatsModel_visits> lstvisits = new List<StatsModel_visits>();
                    List<StatsModel_users> lstusers = new List<StatsModel_users>();
                    List<StatsModel_uniquevisits_Today> lstuniquevisits_tot_today = new List<StatsModel_uniquevisits_Today>();
                    List<StatsModel_uniquevisits> lstuniquevisits_tot = new List<StatsModel_uniquevisits>();
                    List<StatsModel_uniqueusers> lstuniqueusers_tot = new List<StatsModel_uniqueusers>();
                    List<StatsModel_uniqueusers_today> lstuniqueusers_tot_today = new List<StatsModel_uniqueusers_today>();
                    if (cntusers > 0)
                    {
                        lstusers = dc.uiddatas.AsEnumerable()
                            .Where(x => x.PK_Uid > clickref.Ref_UsersID && x.CreatedDate.Value.Date == DateTime.UtcNow.Date)
                            .GroupBy(x => x.FK_RID)
                            .Select(res => new StatsModel_users()
                            {
                                Users_Today = res.Select(x => x.MobileNumber).Count(),
                                UniqueUsers_Today = res.Select(x => x.MobileNumber).Distinct().Count(),
                                fk_rid = res.Select(x => x.FK_RID).FirstOrDefault()
                            }).ToList();

                        lstuniqueusers_tot = (from s in dc.uiddatas
                                                                    .AsEnumerable()
                                              join st in lstusers on s.FK_RID equals st.fk_rid
                                              //where s.CreatedDate.Value.Date == DateTime.UtcNow.Date
                                              group s by s.FK_RID into res
                                              select new StatsModel_uniqueusers()
                                              {
                                                  fk_rid = res.Select(x => x.FK_RID).FirstOrDefault(),
                                                  UniqueUsers = res.Select(x => x.MobileNumber).Distinct().Count()
                                              }).ToList();
                        foreach (StatsModel_users users in lstusers)
                        {
                            stat_counts st_count = new stat_counts(); int uniqueusers = 0;
                            uniqueusers = lstuniqueusers_tot.Where(x => x.fk_rid == users.fk_rid).Select(x => x.UniqueUsers).SingleOrDefault();
                            //uniqueusers_today = lstuniqueusers_tot_today.Where(x => x.fk_rid == users.fk_rid).Select(x => x.UniqueUsers_Today).SingleOrDefault();
                            st_count = dc.stat_counts.Where(x => x.FK_Rid == users.fk_rid).SingleOrDefault();
                            if (st_count != null)
                            {
                                st_count.UsersToday = (users.Users_Today > 0) ? (st_count.UsersToday + users.Users_Today) : st_count.UsersToday;
                                st_count.UrlTotal_Today = st_count.UsersToday;
                                //st_count.UniqueUsersToday = (users.UniqueUsers_Today > 0) ? (st_count.UniqueUsersToday + users.UniqueUsers_Today) : st_count.UniqueUsersToday;
                                st_count.UniqueUsers = (uniqueusers > 0) ? (uniqueusers) : st_count.UniqueUsers;
                                st_count.UniqueUsersToday = (users.UniqueUsers_Today > 0) ? users.UniqueUsers_Today : st_count.UniqueUsersToday;
                                st_count.TotalUsers = (users.Users_Today > 0) ? (st_count.TotalUsers + users.Users_Today) : st_count.TotalUsers;
                                dc.SaveChanges();
                                ErrorLogs.LogErrorData("StatsToday_Service visitis today st_count" + st_count.UniqueUsers, "UniqueUsersToday=" + st_count.UniqueUsersToday.ToString());

                            }
                        }
                    }
                    if (cntvisits > 0)
                    {
                        lstvisits = dc.shorturldatas.AsEnumerable()
                           .Where(x => x.PK_Shorturl > clickref.Ref_ShorturlClickID && x.CreatedDate.Value.Date == DateTime.UtcNow.Date)
                           .GroupBy(x => x.FK_RID)
                           .Select(res => new StatsModel_visits()
                           {
                               Visits_today = res.Select(x => x.FK_Uid).Count(),
                               uniqueVisits_today = res.Select(x => x.FK_Uid).Distinct().Count(),
                               Todays_ReVisitCount = (res.Select(x => x.FK_Uid).Count()) - (res.Select(x => x.FK_Uid).Distinct().Count()),
                               fk_rid = res.Select(x => x.FK_RID).FirstOrDefault(),
                               //fk_uid=res.Select(x=>x.FK_Uid).Distinct().ToList()
                           }).ToList();
                        //lstuniquevisits_tot_today = (from s in dc.shorturldatas
                        //                                           .AsEnumerable()
                        //                 join st in lstvisits on s.FK_RID equals st.fk_rid
                        //                 where s.CreatedDate.Value.Date == DateTime.UtcNow.Date
                        //                 group s by s.FK_RID into res
                        //                 select new StatsModel_uniquevisits_Today()
                        //                 {
                        //                     fk_rid = res.Select(x => x.FK_RID).FirstOrDefault(),
                        //                     uniqueVisits_today = res.Select(x => x.FK_Uid).Distinct().Count()
                        //                 }).ToList();
                        lstuniquevisits_tot = (from s in dc.shorturldatas
                                                                   .AsEnumerable()
                                               join st in lstvisits on s.FK_RID equals st.fk_rid
                                               //where s.CreatedDate.Value.Date == DateTime.UtcNow.Date
                                               group s by s.FK_RID into res
                                               select new StatsModel_uniquevisits()
                                               {
                                                   fk_rid = res.Select(x => x.FK_RID).FirstOrDefault(),
                                                   uniquevists = res.Select(x => x.FK_Uid).Distinct().Count()
                                               }).ToList();
                        foreach (StatsModel_visits vst in lstvisits)
                        {
                            stat_counts st_count = new stat_counts();
                            int uniquevists = 0;
                            uniquevists = lstuniquevisits_tot.Where(x => x.fk_rid == vst.fk_rid).Select(x => x.uniquevists).SingleOrDefault();
                            //uniquevistis_today = lstuniquevisits_tot_today.Where(x => x.fk_rid == vst.fk_rid).Select(x => x.uniqueVisits_today).SingleOrDefault();
                            //int novisits = lstnovisits_today.Where(x => x.fk_rid == vst.fk_rid).Select(x => x.Novists_today).SingleOrDefault();
                            st_count = dc.stat_counts.Where(x => x.FK_Rid == vst.fk_rid).SingleOrDefault();
                            if (st_count != null)
                            {
                                //ErrorLogs.LogErrorData("StatsToday_Service visitis today st_count" + st_count.VisitsToday, vst.Visits_today.ToString());

                                st_count.VisitsToday = st_count.VisitsToday + vst.Visits_today;
                                //st_count.UniqueVisitsToday = st_count.UniqueVisitsToday + vst.uniqueVisits_today;
                                st_count.VisitsTotal_Today = st_count.VisitsToday;
                                st_count.RevisitsTotal_Today = st_count.RevisitsTotal_Today + vst.Todays_ReVisitCount;

                                st_count.UniqueVisits = (uniquevists > 0) ? (uniquevists) : (st_count.UniqueVisits);
                                st_count.UniqueVisitsToday = (vst.uniqueVisits_today > 0) ? vst.uniqueVisits_today : (st_count.UniqueVisitsToday);
                                st_count.TotalVisits = st_count.TotalVisits + vst.Visits_today;
                                st_count.NoVisitsTotal_Today = st_count.TotalUsers - st_count.UniqueVisitsToday;
                                st_count.RevisitsPercent_Today = (st_count.RevisitsTotal_Yesterday > 0) ? ((st_count.RevisitsTotal_Today - st_count.RevisitsTotal_Yesterday) / (st_count.RevisitsTotal_Yesterday)) : 0;
                                st_count.NoVisitsPercent_Today = (st_count.NoVisitsTotal_Yesterday > 0) ? ((st_count.NoVisitsTotal_Today - st_count.NoVisitsTotal_Yesterday) / (st_count.NoVisitsTotal_Yesterday)) : 0;
                                st_count.UrlPercent_Today = (st_count.UsersYesterday > 0) ? ((st_count.UrlTotal_Today - st_count.UsersYesterday) / (st_count.UsersYesterday)) : 0;
                                //st_count.NoVisitsTotal_Today = (novisits == null) ? st_count.NoVisitsTotal_Today : novisits;
                                dc.SaveChanges();
                            }
                        }

                    }


                    stat_counts st_count_admin = dc.stat_counts.Where(x => x.FK_Rid == 0).SingleOrDefault();
                    if (st_count_admin != null)
                    {

                        st_count_admin.VisitsToday = (lstvisits != null) ? (st_count_admin.VisitsToday + lstvisits.Select(x => x.Visits_today).Sum()) : st_count_admin.VisitsToday;
                        st_count_admin.VisitsTotal_Today = st_count_admin.VisitsToday;
                        st_count_admin.RevisitsTotal_Today = (lstvisits != null) ? (st_count_admin.RevisitsTotal_Today + lstvisits.Select(x => x.Todays_ReVisitCount).Sum()) : st_count_admin.RevisitsTotal_Today;

                        st_count_admin.UniqueVisits = (lstuniquevisits_tot != null) ? (lstuniquevisits_tot.Select(x => x.uniquevists).Sum()) : st_count_admin.UniqueVisits;
                        st_count_admin.UniqueVisitsToday = (lstvisits != null) ? (lstvisits.Select(x => x.uniqueVisits_today).Sum()) : st_count_admin.UniqueVisitsToday;

                        st_count_admin.TotalVisits = (lstvisits != null) ? (lstvisits.Select(x => x.Visits_today).Sum()) : st_count_admin.TotalVisits;
                        //st_count.NoVisitsTotal_Today = (novisits == null) ? st_count.NoVisitsTotal_Today : novisits;
                        st_count_admin.UsersToday = (lstusers != null) ? (st_count_admin.UsersToday + lstusers.Select(x => x.Users_Today).Sum()) : st_count_admin.UsersToday;
                        st_count_admin.UniqueUsers = (lstuniqueusers_tot != null) ? (lstuniqueusers_tot.Select(x => x.UniqueUsers).Sum()) : st_count_admin.UniqueUsers;
                        st_count_admin.UniqueUsersToday = (lstusers != null) ? (lstusers.Select(x => x.UniqueUsers_Today).Sum()) : st_count_admin.UniqueUsersToday;
                        st_count_admin.TotalUsers = (lstusers != null) ? (st_count_admin.TotalUsers + lstusers.Select(x => x.Users_Today).Sum()) : st_count_admin.TotalUsers;
                        st_count_admin.UrlTotal_Today = st_count_admin.TotalUsers;
                        st_count_admin.NoVisitsTotal_Today = st_count_admin.TotalUsers - st_count_admin.UniqueVisitsToday;
                        st_count_admin.VisitsPercent_Today = (st_count_admin.VisitsYesterday > 0) ? ((st_count_admin.VisitsTotal_Today - st_count_admin.VisitsYesterday) / (st_count_admin.VisitsYesterday)) : 0;
                        st_count_admin.RevisitsPercent_Today = (st_count_admin.RevisitsTotal_Yesterday > 0) ? ((st_count_admin.RevisitsTotal_Today - st_count_admin.RevisitsTotal_Yesterday) / (st_count_admin.RevisitsTotal_Yesterday)) : 0;
                        st_count_admin.NoVisitsPercent_Today = (st_count_admin.NoVisitsTotal_Yesterday > 0) ? ((st_count_admin.NoVisitsTotal_Today - st_count_admin.NoVisitsTotal_Yesterday) / (st_count_admin.NoVisitsTotal_Yesterday)) : 0;
                        dc.SaveChanges();
                        //ErrorLogs.LogErrorData("StatsToday_Service visitis today st_count_admin " + st_count_admin.VisitsToday, lstvisits.Select(x => x.Visits_today).Sum().ToString());

                    }
                    getnextshorturlid = dc.shorturldatas.Any() ? dc.shorturldatas.Max(x => x.PK_Shorturl) : 0;
                    getnextuserid = dc.uiddatas.Any() ? dc.uiddatas.Max(x => x.PK_Uid) : 0;
                    if (cntvisits > 0)
                        clickref.Ref_ShorturlClickID = getnextshorturlid;
                    if (cntusers > 0)
                        clickref.Ref_UsersID = getnextuserid;
                    dc.SaveChanges();

                }

            }
            catch (Exception ex)
            {

                ErrorLogs.LogErrorData("StatsToday_Service" + ex.InnerException + ex.StackTrace, ex.Message);

            }
        }


        public class StatsModel_visits
        {
            public int Visits_today { get; set; }
            public int uniqueVisits_today { get; set; }
            public int Todays_ReVisitCount { get; set; }

            public int? fk_rid { get; set; }
            // public List<int?> fk_uid { get; set; }
        }
        public class StatsModel_users
        {
            public int Users_Today { get; set; }
            public int UniqueUsers_Today { get; set; }
            //public int UniqueUsers { get; set; }
            public int? fk_rid { get; set; }
        }
        public class StatsModel_uniquevisits
        {
            public int uniquevists { get; set; }
            public int? fk_rid { get; set; }
        }
        public class StatsModel_uniquevisits_Today
        {
            public int uniqueVisits_today { get; set; }
            public int? fk_rid { get; set; }
        }
        public class StatsModel_Novisits_Today
        {
            public List<int> Novists_today { get; set; }
            public int? fk_rid { get; set; }
        }
        public class StatsModel_uniqueusers_today
        {
            public int UniqueUsers_Today { get; set; }
            public int? fk_rid { get; set; }
        }
        public class StatsModel_uniqueusers
        {
            public int UniqueUsers { get; set; }
            public int? fk_rid { get; set; }
        }
        public class StatsCounts_Today
        {
            public int Visits_today { get; set; }
            public int uniqueVisits_today { get; set; }
            public int Todays_ReVisitCount { get; set; }
            public int uniquevists { get; set; }
            public int Users_Today { get; set; }
            public int UniqueUsers_Today { get; set; }
            public int UniqueUsers { get; set; }
            public int? fk_rid { get; set; }

        }
        //end tempfunc

        
    }
}