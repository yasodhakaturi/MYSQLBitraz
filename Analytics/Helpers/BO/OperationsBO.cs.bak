using Analytics.Helpers.Utility;
using Analytics.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.ServiceModel.Web;
using System.Text.RegularExpressions;
using System.Web;

namespace Analytics.Helpers.BO
{
    public class OperationsBO
    {
        shortenURLEntities dc = new shortenURLEntities();
        string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;
        SqlConnection lSQLConn = null;
        SqlCommand lSQLCmd = new SqlCommand();


        public UIDDATA CheckUniqueid(string Uniqueid_UIDRID)
        {
            try
            {
               // bool check;
                UIDDATA un_UID = new UIDDATA();
                un_UID = (from uniid in dc.UIDDATAs
                             where uniid.UniqueNumber == Uniqueid_UIDRID
                             select uniid).SingleOrDefault();
                if (un_UID != null)
                {
                    //check = true;
                    return un_UID;
                }
                else
                {
                    //check = false;
                    return un_UID;
                }
            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException.ToString());
                return null;
            }
        }
        public bool CheckPassword_RIDDATA(int? rid, string pwd)
        {
            try
            {
                int row_rid = 0; bool check;
                row_rid = (from r in dc.RIDDATAs
                           where r.PK_Rid == rid && r.Pwd == pwd
                           select r.PK_Rid).SingleOrDefault();
                if (row_rid != 0 && row_rid != null)
                    check = true;
                else
                    check = false;
                return check;
            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException.ToString());
                return false;
            }
        }
        //public int GetUniqueid(int Uniqueid_UIDRID, string type)
        //{
        //    try
        //    {
        //        int un_UIDRID = 0;
        //        un_UIDRID = (from uniid in dc.UIDandRIDDatas
        //                     where uniid.UIDorRID == Uniqueid_UIDRID && uniid.TypeDiff == type
        //                     select uniid.PK_UniqueId).SingleOrDefault();
        //        if (un_UIDRID != 0)
        //            return un_UIDRID;
        //        else
        //            return un_UIDRID;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException.ToString());
        //        return 0;
        //    }
        //}
        public string GetLongURL(string Uniqueid_ShortURL)
        {
            try
            {
                string LongURL = "";
                LongURL = (from uniid in dc.UIDDATAs
                           where uniid.UniqueNumber == Uniqueid_ShortURL
                           select uniid.Longurl).SingleOrDefault();
                if (LongURL != "")
                    return LongURL;
                else
                    return LongURL;
            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException.ToString());
                return "";
            }

        }
        //public PWDDataBO GetUIDRIDDATA(int Uniqueid_UIDRID)
        //{
        //    try
        //    {
        //        string pwd = null;
                
        //        PWDDataBO obj = (from uniid in dc.UIDandRIDDatas
        //                       where uniid.PK_UniqueId == Uniqueid_UIDRID  
        //                       select new PWDDataBO
        //                     {
        //                         typediff=uniid.TypeDiff,
        //                         UIDorRID=uniid.UIDorRID
        //                     }).SingleOrDefault();
        //        if (obj != null)
        //        {
        //            if (obj.typediff == "2")
        //            {
        //                pwd = (from r in dc.RIDDATAs
        //                       where r.PK_Rid == obj.UIDorRID
        //                       select r.Pwd).SingleOrDefault();
        //                obj.pwd = pwd;
        //            }
        //            else
        //            {
        //                obj.pwd = pwd;
        //            }
        //            return obj;
        //        }
        //        else
        //            return null;
                
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException.ToString());
        //        return null;
        //    }
        //}
        //public int? GetUIDRID(int Uniqueid_UIDRID, string type)
        //{
        //    try
        //    {
                
        //        int? un_UIDRID = 0;
        //        un_UIDRID = (from uniid in dc.UIDandRIDDatas
        //                     where uniid.PK_UniqueId == Uniqueid_UIDRID && uniid.TypeDiff == type
        //                     select uniid.UIDorRID).SingleOrDefault();
        //        if (un_UIDRID != 0)
        //        {

        //            return un_UIDRID;
        //        }
        //        else
        //        {

        //            return un_UIDRID;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException.ToString());
        //        return 0;
        //    }
        //}

        public UserViewModel GetViewConfigDetails(string url)
        {
            UserViewModel obj = new UserViewModel();
            string env = ""; string appurl = "";

            // if (url.Contains(".com") || url.Contains("www."))
            //  env = "prod";
            // else
            // env = "dev"; 

            //obj.env = env;
            //if (url.Contains(".com") || url.Contains("www."))
            //    appurl = url;
            //else
            //    appurl = "http://localhost:3000";

            env = ConfigurationManager.AppSettings["env"].ToString();
            appurl = ConfigurationManager.AppSettings["appurl"].ToString();


            obj.appUrl = appurl;
            UserInfo user_obj = new UserInfo();

            if (HttpContext.Current.Session["userdata"] != null)
            {
                user_obj.user_id = Helper.CurrentUserId;
                user_obj.user_name = Helper.CurrentUserName;
                //user_obj.user_role = Helper.CurrentUserRole;
                if (Helper.CurrentUserRole.ToLower() == "admin")
                { obj.isAdmin = "true"; obj.isClient = "false"; }
                else if (Helper.CurrentUserRole.ToLower() == "client")
                { obj.isClient = "true"; obj.isAdmin = "false"; }
            }
            else
            {
                user_obj.user_id = 0;
                user_obj.user_name = "null";
                obj.isAdmin = "false";
                obj.isClient = "false";
            }
            
            obj.userInfo = user_obj;
            appUrlModel appobj = new appUrlModel();
            appobj.admin = "/Admin";
            appobj.analytics = "/Analytics";
            appobj.landing = "/Home";
            obj.apiUrl = appobj;
            obj.env = env;
            return obj;
        }
        public string IpAddress()
        {
            string strIpAddress;
            strIpAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (strIpAddress == null)
            {
                strIpAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return strIpAddress;
        }
        public void Monitize(string Shorturl)
        
        {
            try
            {
                string longurl = "";
                //long decodedvalue = new ConvertionBO().BaseToLong(Shorturl);
                //int Uniqueid_SHORTURLDATA = Convert.ToInt32(decodedvalue);
                int Fk_UID = 0;
                UIDDATA uid_obj = new UIDDATA();
                uid_obj = new OperationsBO().CheckUniqueid(Shorturl);
                //if (new OperationsBO().CheckUniqueid(Shorturl))
                if (uid_obj != null)
                {
                    //int? Fk_UID = (from u in dc.UIDandRIDDatas
                    //               where u.PK_UniqueId == Uniqueid_SHORTURLDATA && u.TypeDiff == "1"
                    //               select u.UIDorRID).SingleOrDefault();
                    Fk_UID = uid_obj.PK_Uid;
                    int? FK_RID = (from u in dc.UIDDATAs
                                   where u.PK_Uid == Fk_UID
                                   select u.FK_RID).SingleOrDefault();
                    int? FK_clientid = (from r in dc.RIDDATAs
                                        where r.PK_Rid == FK_RID
                                        select r.FK_ClientId).SingleOrDefault();
                    //retrive ipaddress and browser
                    //string ipv4 = new ConvertionBO().GetIP4Address();
                    string ipv4 = IpAddress();
                    string ipv6 = HttpContext.Current.Request.UserHostAddress;
                    string browser = HttpContext.Current.Request.Browser.Browser;
                    string browserversion = HttpContext.Current.Request.Browser.Version;
                    string req_url = HttpContext.Current.Request.Url.ToString();
                    //string[] header_array = HttpContext.Current.Request.Headers.AllKeys;
                    string useragent = HttpContext.Current.Request.UserAgent;
                    string hostname = HttpContext.Current.Request.UserHostName;
                    string devicetype = HttpContext.Current.Request.Browser.Platform;
                    string ismobiledevice = HttpContext.Current.Request.Browser.IsMobileDevice.ToString();
                    //retrieve longurl from uid
                    longurl = uid_obj.Longurl;
                    //longurl = new OperationsBO().GetLongURL(Uniqueid_SHORTURLDATA);
                    //if(longurl!=null)
                    //    HttpContext.Current.Response.Redirect(longurl);

                    //retrive city,country
                    var City = ""; var Region = ""; var Country = ""; var CountryCode = ""; var url = "";
                    //url = "http://freegeoip.net/json/" + "99.25.39.48";
                    url = "http://freegeoip.net/json/" + ipv4;
                    var request = System.Net.WebRequest.Create(url);
                    using (WebResponse wrs = request.GetResponse())
                    using (Stream stream = wrs.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string json = reader.ReadToEnd();
                        var obj = JObject.Parse(json);
                        City = (string)obj["city"];
                        Region = (string)obj["region_name"];
                        Country = (string)obj["country_name"];
                        CountryCode = (string)obj["country_code"];
                    }
                    //retrive city,country if city country not found with ipv4
                    if (City == "" && Country == "")
                    {
                        url = "http://freegeoip.net/json/" + ipv6;
                        var request1 = System.Net.WebRequest.Create(url);
                        using (WebResponse wrs = request1.GetResponse())
                        using (Stream stream = wrs.GetResponseStream())
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string json = reader.ReadToEnd();
                            var obj = JObject.Parse(json);
                            City = (string)obj["city"];
                            Region = (string)obj["region_name"];
                            Country = (string)obj["country_name"];
                            CountryCode = (string)obj["country_code"];
                        }
                    }
                    new DataInsertionBO().InsertShortUrldata(ipv4, ipv6, browser, browserversion, City, Region, Country, CountryCode, req_url, useragent, hostname, devicetype, ismobiledevice,Fk_UID,FK_RID,FK_clientid);
                }
                //WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.Redirect;
                //if (!longurl.StartsWith("http://") && !longurl.StartsWith("https://"))
                //    WebOperationContext.Current.OutgoingResponse.Headers.Add("Location", "http://" + longurl);
                //else
                //    WebOperationContext.Current.OutgoingResponse.Headers.Add("Location", longurl);
                if (longurl != null && !longurl.StartsWith("http://") && !longurl.StartsWith("https://"))
                    HttpContext.Current.Response.Redirect("http://" + longurl);
                else
                    HttpContext.Current.Response.Redirect(longurl);


            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException.ToString());

            }
        }


        public string GetApiKey()
        {
            string APIKey="";
            using (var cryptoProvider = new RNGCryptoServiceProvider())
            {
                byte[] secretKeyByteArray = new byte[32]; //256 bit
                cryptoProvider.GetBytes(secretKeyByteArray);
                APIKey = Convert.ToBase64String(secretKeyByteArray);
                APIKey=APIKey.Replace("+", "") .Replace("#", "").Replace("&","").TrimEnd('=');
            }
            return APIKey;
        }
        public RIDDATA CheckCampaignNameExistance(Client obj,string CamapignName)
        {
            RIDDATA checkCampaign = new RIDDATA(); 
             checkCampaign = dc.RIDDATAs.Where(r => r.FK_ClientId == obj.PK_ClientID && r.CampaignName == CamapignName).Select(s => s).SingleOrDefault();
           
       return checkCampaign;

        }
        public Client CheckClientEmail(string email)
        {
            Client obj = new Client();
            obj = dc.Clients.Where(c => c.Email == email).Select(x => x).SingleOrDefault();
            //if (obj != null)
            //    check = true;
            //else
            //    check = false;
            return obj;

        }
        public bool CheckClientEmail1(string email)
        {
            Client obj = new Client(); bool check = false;
            obj = dc.Clients.Where(c => c.Email == email).Select(x => x).SingleOrDefault();
            if (obj != null)
                check = true;
            //else
            //    check = false;
            return check;

        }
        public bool CheckClientId(int id)
        {
            Client obj = new Client(); bool check = false;
            obj = dc.Clients.Where(c => c.PK_ClientID == id).Select(x => x).SingleOrDefault();
            if (obj != null)
                check = true;
            //else
            //    check = false;
            return check;
        }
       
        public void UpdateClient(string username,string email,bool? isactive)
        {
            try
            {
                //string strQuery = "Update MMPersonMessage set Status = 'R' where FKMessageId = (" + messageid + ") and FKToPersonId = (" + personid + ")";
                DateTime utcdt = DateTime.UtcNow;
                string strQuery = "Update Client set UserName = '" + username + "' ,IsActive='" + isactive + "',UpdatedDate='" + utcdt + "' where Email ='" + email + "'";
                SqlHelper.ExecuteNonQuery(Helper.ConnectionString, CommandType.Text, strQuery);
            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException.ToString());
            }
        }
        //public void InsertUIDRIDData(string referencenumber)
        //{
        //    try
        //    {
        //    int rid = dc.RIDDATAs.Where(r => r.ReferenceNumber == referencenumber).Select(x => x.PK_Rid).SingleOrDefault();
        //    lSQLConn = new SqlConnection(connStr);
        //    SqlDataReader myReader;
        //    lSQLConn.Open();
        //    lSQLCmd.CommandType = CommandType.StoredProcedure;
        //    lSQLCmd.CommandText = "InsertintoUIDRID";
        //    //lSQLCmd.Parameters.Add(new SqlParameter("@Fk_Uniqueid", Uniqueid_SHORTURLDATA));
        //    lSQLCmd.Parameters.Add(new SqlParameter("@typediff", "2"));
        //    lSQLCmd.Parameters.Add(new SqlParameter("@uidorrid", rid));
        //    lSQLCmd.Connection = lSQLConn;
        //    myReader = lSQLCmd.ExecuteReader();
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException.ToString());
        //    }
        //}


        public bool CheckReferenceNumber(string referencenumber)
        {
            RIDDATA obj = new RIDDATA(); bool check = false;
            obj = dc.RIDDATAs.Where(c => c.ReferenceNumber == referencenumber).Select(x => x).SingleOrDefault();
            if (obj != null)
                check = true;
            //else
            //    check = false;
            return check;

        }
        public RIDDATA CheckReferenceNumber1(string referencenumber)
        {
            RIDDATA obj = new RIDDATA(); bool check = false;
            obj = dc.RIDDATAs.Where(c => c.ReferenceNumber == referencenumber).Select(x => x).SingleOrDefault();
            if (obj != null)
                return obj;
                //else
            //    check = false;
            return obj;

        }

        public void UpdateCampaign(string referencenumber,string CampaignName, string password, bool? isactive)
        {
            try
            {
                string strQuery="";
                DateTime dt = DateTime.UtcNow;
                if (password != null && CampaignName!=null)
                    strQuery = "Update RIDDATA set CampaignName='" + CampaignName + "',Pwd='" + password + "',IsActive='" + isactive + "',UpdatedDate='" + dt + "' where ReferenceNumber ='" + referencenumber + "'";
                else if(CampaignName!=null && password==null)
                    strQuery = "Update RIDDATA set CampaignName='" + CampaignName + "',IsActive='" + isactive + "',UpdatedDate='" + dt + "' where ReferenceNumber ='" + referencenumber + "'";
                else if (CampaignName == null && password != null)
                    strQuery = "Update RIDDATA set Pwd='" + password + "',IsActive='" + isactive + "',UpdatedDate='" + dt + "' where ReferenceNumber ='" + referencenumber + "'";
                SqlHelper.ExecuteNonQuery(Helper.ConnectionString, CommandType.Text, strQuery);
            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException.ToString());
            }
        }
        public void UpdateHashid(int pk_uid, string Hashid)
        {
            try
            {
                //string strQuery = "Update MMPersonMessage set Status = 'R' where FKMessageId = (" + messageid + ") and FKToPersonId = (" + personid + ")";
                DateTime utcdt = DateTime.UtcNow;
                string strQuery = "Update UIDDATA set UniqueNumber = '" + Hashid + "' where PK_Uid ='" + pk_uid + "'";
                SqlHelper.ExecuteNonQuery(Helper.ConnectionString, CommandType.Text, strQuery);
            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException.ToString());
            }
        }

        public bool ValidateEmail(string emailstr)
        {
              string MatchEmailPattern = 
			@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
     + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
     + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
     + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";
           
        if(emailstr!="") 
        return Regex.IsMatch(emailstr, MatchEmailPattern);
     else 
        return false;
        }
        //public CountsData GetCountsData(SqlDataReader myReader,string filterBy,string DateFrom,string DateTo)
        //{

        //    CountsData countobj = new CountsData();
        //    if (filterBy != "" && DateFrom == "" && DateTo == "")
        //    {
        //        if (filterBy == "Year")
        //        {
        //            List<YearData> YearsDataObj = ((IObjectContextAdapter)dc)
        //                                .ObjectContext
        //                                .Translate<YearData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();
        //            //countobj.YearsData = YearsDataObj;
                   
        //        }
        //        if (filterBy == "Month")
        //        {
        //            List<MonthData> MonthDataObj = ((IObjectContextAdapter)dc)
        //                                .ObjectContext
        //                                .Translate<MonthData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();
        //            //countobj.MonthsData = MonthDataObj;
        //        }
        //        if (filterBy == "CurrentMonth")
        //        {
        //            List<CurrentMonthData> CurrentMonthDataObj = ((IObjectContextAdapter)dc)
        //             .ObjectContext
        //             .Translate<CurrentMonthData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();

        //            //countobj.CurrentMonthData = CurrentMonthDataObj;
        //        }
        //        if (filterBy == "Today")
        //        {
        //            List<TodayData> TodayDataObj = ((IObjectContextAdapter)dc)
        //            .ObjectContext
        //            .Translate<TodayData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();
        //            //countobj.TodaysData = TodayDataObj;
        //        }
        //    }

        //    else if (filterBy == "" && DateFrom == "" && DateTo == "")
        //    {
        //        List<YearData> YearsDataObj = ((IObjectContextAdapter)dc)
        //            .ObjectContext
        //            .Translate<YearData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();


        //        // Move to Month result 
        //        myReader.NextResult();
        //        List<MonthData> MonthDataObj = ((IObjectContextAdapter)dc)
        //            .ObjectContext
        //            .Translate<MonthData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();

        //        // Move to CurrentMonth result 
        //        myReader.NextResult();
        //        List<CurrentMonthData> CurrentMonthDataObj = ((IObjectContextAdapter)dc)
        //            .ObjectContext
        //            .Translate<CurrentMonthData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();

        //        // Move to TodayData result 
        //        myReader.NextResult();
        //        List<TodayData> TodayDataObj = ((IObjectContextAdapter)dc)
        //            .ObjectContext
        //            .Translate<TodayData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();

        //        // Move to YesterDayData result 
        //        myReader.NextResult();
        //        List<YesterDayData> YesterDayDataObj = ((IObjectContextAdapter)dc)
        //            .ObjectContext
        //            .Translate<YesterDayData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();

        //        // Move to Last7DaysData result 
        //        myReader.NextResult();
        //        List<Last7DaysData> Last7DaysDataObj = ((IObjectContextAdapter)dc)
        //            .ObjectContext
        //            .Translate<Last7DaysData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();

        //        // Move to BrowsersData result 
        //        myReader.NextResult();
        //        List<BrowsersData> BrowsersDataObj = ((IObjectContextAdapter)dc)
        //            .ObjectContext
        //            .Translate<BrowsersData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();

        //        // Move to CountryData result 
        //        myReader.NextResult();
        //        List<CountryData> CountryDataObj = ((IObjectContextAdapter)dc)
        //            .ObjectContext
        //            .Translate<CountryData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();

        //        // Move to CityData result 
        //        myReader.NextResult();
        //        List<CityData> CityDataObj = ((IObjectContextAdapter)dc)
        //            .ObjectContext
        //            .Translate<CityData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();

        //        // Move to RegionData result 
        //        myReader.NextResult();
        //        List<RegionData> RegionDataObj = ((IObjectContextAdapter)dc)
        //            .ObjectContext
        //            .Translate<RegionData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();

        //        // Move to DeviceTypeData result 
        //        myReader.NextResult();
        //        List<DeviceTypeData> DeviceTypeDataObj = ((IObjectContextAdapter)dc)
        //            .ObjectContext
        //            .Translate<DeviceTypeData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();


        //        //countobj.YearsData = YearsDataObj;
        //        //countobj.MonthsData = MonthDataObj;
        //        //countobj.CurrentMonthData = CurrentMonthDataObj;
        //        //countobj.TodaysData = TodayDataObj;
        //        //countobj.YesterdaysData = YesterDayDataObj;
        //        //countobj.Last7DaysData = Last7DaysDataObj;
        //        //countobj.BrowsersData = BrowsersDataObj;
        //        //countobj.CountriesData = CountryDataObj;
        //        //countobj.CitiesData = CityDataObj;
        //        //countobj.RegionsData = RegionDataObj;
        //        //countobj.DevicesData = DeviceTypeDataObj;
        //    }
        //    return countobj;
        //}

    
    }
}