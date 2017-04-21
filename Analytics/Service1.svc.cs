using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Newtonsoft.Json.Linq;
//using System.Data.Sqlclient;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Web;
using System.ServiceModel.Channels;
using System.ServiceModel.Activation;
using System.Net.Sockets;
using Analytics;
using Analytics.Helpers.BO;
using Analytics.Helpers.Utility;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.Web.Hosting;
using System.Web.Configuration;
using System.Web.Security;
using MySql.Data.MySqlClient;

namespace Analytics
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Single)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    public class Service1 : IService1
    {
        shortenurlEntities dc = new shortenurlEntities();
        MySqlConnection lSQLConn = null;
        MySqlCommand lSQLCmd = new MySqlCommand();
        int Uniqueid_UID = 0; int Uniqueid_RID = 0; int Uniqueid_SHORTURLDATA = 0; int RID = 0; int UID = 0;

        public class error
        {

            public string message { get; set; }
        }
        public class ReferenceNumber1
        {
            public string ReferenceNumber { get; set; }
        }
        public class ShortUrl1
        {
            public string shortUrl { get; set; }
        }
        public string GetApiKey(string UserName, string Email, string Password)
        {
            try
            {
                client obj = new client(); bool isactive = true; int cid = 0;
                if (UserName.Trim() != "" && Email.Trim() != "" && Password.Trim() != "")
                {

                    //client isEmailExists = new OperationsBO().CheckclientEmail(Email);
                    //bool valideamil = new OperationsBO().ValidateEmail(Email);
                    //client obj1 = new client();
                    
                        //if (isEmailExists == null)
                        //{
                        //    if (valideamil)
                        //    {
                        //    //add client details
                        //    string ApiKey = new OperationsBO().GetApiKey();
                        //    obj.UserName = UserName;
                        //    obj.Email = Email;
                        //    obj.Password = Password;
                        //    obj.APIKey = ApiKey;
                        //    obj.IsActive = isactive;
                        //    obj.Role = "client";
                        //    obj.CreatedDate = DateTime.Today;
                        //    dc.clients.Add(obj);
                        //    dc.SaveChanges();
                        //}
                        //    else
                        //    {
                        //        error errobj = new error();
                        //        errobj.message = "Please Pass Valid email";
                        //        return JsonConvert.SerializeObject(errobj);
                        //    }
                        //}
                    
                    client cl_obj = new OperationsBO().CheckclientEmail(Email,Password);
                    //if (cl_obj != null)
                    //{
                    //    cid = cl_obj.PK_ClientID;
                    //}
                    if (cl_obj != null)
                    {
                        System.ServiceModel.Web.WebOperationContext ctx = System.ServiceModel.Web.WebOperationContext.Current;
                        ctx.OutgoingResponse.Headers.Add("Api_key", cl_obj.APIKey);
                    }
                    else
                    {
                        error errobj = new error();
                        errobj.message = "client doesnt exist.";
                        return JsonConvert.SerializeObject(errobj);
                    }
                    return "";

                }
                else
                {
                    error errobj = new error();
                    errobj.message = "Please Pass Username,Password&email";
                    return JsonConvert.SerializeObject(errobj);
                }

            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException.ToString());
                error errobj = new error();
                errobj.message = "Exception" + ex.Message;
                return JsonConvert.SerializeObject(errobj);
            }
        }



        public string GetShortUrl(string referencenumber, string longurl, string mobilenumber)
        {
            try
            {
                IncomingWebRequestContext woc = WebOperationContext.Current.IncomingRequest;
                string api_key = woc.Headers["Api_key"];

                client cl_obj = (from c in dc.clients
                                 where c.APIKey == api_key
                                 select c).SingleOrDefault();
                string Hashid = ""; int pk_uid = 0;
                if (cl_obj != null && api_key != "" && api_key != null)
                {
                    if (referencenumber.Trim() != "" && longurl.Trim() != "" && mobilenumber.Trim() != "")
                    {
                        //check reference number in RID table
                        riddata objrid = (from registree in dc.riddatas
                                          where registree.ReferenceNumber.Trim() == referencenumber.Trim()
                                          select registree).SingleOrDefault();
                        Uniqueid_RID = objrid.PK_Rid;
                        //if (Uniqueid_RID == 0)
                        //{
                        //    new DataInsertionBO().Insertriddata(referencenumber, "");
                        //    Uniqueid_RID = (from registree in dc.riddatas
                        //                    where registree.ReferenceNumber.Trim() == referencenumber.Trim()
                        //                    select registree.PK_Rid).SingleOrDefault();
                        //}
                        if (objrid != null)
                        {
                            //check data in UID table
                            Hashid = (from registree in dc.uiddatas
                                      where registree.ReferenceNumber.Trim() == referencenumber.Trim() &&
                                      registree.Longurl.Trim() == longurl.Trim() &&
                                      registree.MobileNumber.Trim() == mobilenumber.Trim()
                                      select registree.UniqueNumber).SingleOrDefault();
                            //if data found in uiddata insert data into uiddata 
                            if (Hashid == null)
                            {
                                //Uniqueid = Helper.GetRandomAlphanumericString(5);
                                new DataInsertionBO().Insertuiddata(Uniqueid_RID, objrid.FK_ClientId, referencenumber, longurl, mobilenumber);

                                pk_uid = (from registree in dc.uiddatas
                                          where registree.ReferenceNumber.Trim() == referencenumber.Trim() &&
                                          registree.Longurl.Trim() == longurl.Trim() &&
                                          registree.MobileNumber.Trim() == mobilenumber.Trim()
                                          select registree.PK_Uid).SingleOrDefault();
                                Hashid = Helper.GetHashID(pk_uid);
                                new OperationsBO().UpdateHashid(pk_uid, Hashid);
                            }

                            
                            System.ServiceModel.Web.WebOperationContext ctx = System.ServiceModel.Web.WebOperationContext.Current;
                            ctx.OutgoingResponse.Headers.Add("Api_key", api_key);
                            // return "http://g0.pe/" + Hashid;
                            string ShortUrl = "https://g0.pe/" + Hashid;
                            ShortUrl1 sobj = new ShortUrl1();
                            sobj.shortUrl = ShortUrl;
                            return JsonConvert.SerializeObject(sobj);

                        }
                        else
                        {
                            error errobj = new error();
                            errobj.message = "Referencenumber not valid";
                            return JsonConvert.SerializeObject(errobj);
                            //return "Referencenumber not valid";
                        }
                        //return "";
                    }
                    else
                    {
                        error errobj = new error();
                        errobj.message = "Please pass all values.";
                        return JsonConvert.SerializeObject(errobj);
                        //return "Please pass all values.";
                    }
                }
                else
                {
                    error errobj = new error();
                    errobj.message = "Please Pass valid APiKey";
                    return JsonConvert.SerializeObject(errobj);
                    //return "Please Pass valid APiKey";
                }
            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException.ToString());
                error errobj = new error();
                //UID_UIDRID = "NULL";
                errobj.message = "Exception" + ex.Message;
                return JsonConvert.SerializeObject(errobj);
            }
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
        public string GetCampaignNumber(string CampaignName, string Password)
        {
            try
            {

                int clientid = 0; int Uni_RID = 0; string ReferenceNumber = "";
                ReferenceNumber1 refnum = new ReferenceNumber1(); error err_obj = new error();
                IncomingWebRequestContext woc = WebOperationContext.Current.IncomingRequest;
                string api_key = woc.Headers["Api_key"];
                if (api_key != "" && api_key != null)
                {
                    client cl_obj = (from c in dc.clients
                                     where c.APIKey == api_key
                                     select c).SingleOrDefault();
                    // string ReferenceNumber = string.Format("{0}_{1:N}", cl_obj.Email, Guid.NewGuid());

                    if (cl_obj != null)
                    {
                        riddata Campaignexistance = new OperationsBO().CheckCampaignNameExistance(cl_obj, CampaignName);
                        if (Password != null && Password != "" && Campaignexistance != null)
                        {
                            if (Campaignexistance.Pwd != Password)
                                Campaignexistance = null;
                        }
                        if(Campaignexistance!=null)
                        {
                            System.ServiceModel.Web.WebOperationContext ctx = System.ServiceModel.Web.WebOperationContext.Current;
                            ctx.OutgoingResponse.Headers.Add("Api_key", api_key);
                            refnum.ReferenceNumber = Campaignexistance.ReferenceNumber;
                            return JsonConvert.SerializeObject(refnum);
                        }
                        else
                        {
                            err_obj.message = "Invalid Campaign";
                            return JsonConvert.SerializeObject(err_obj);
                        }
                    }
                    else
                    {
                        err_obj.message = "Please Pass valid APiKey";
                        return JsonConvert.SerializeObject(err_obj);
                    }
                }
                else
                {
                    err_obj.message = "Please Pass valid APiKey";
                    return JsonConvert.SerializeObject(err_obj);
                }
            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException.ToString());
                error errobj = new error();
                //RID_UIDRIID = "NULL";
                errobj.message = "Exception" + ex.Message;
                return JsonConvert.SerializeObject(errobj);
            }
        }
        public string RegisterCampaign(string CampaignName, string Password)
        {
            try
            {

                int clientid = 0; int Uni_RID = 0; string ReferenceNumber = "";
                ReferenceNumber1 refnum = new ReferenceNumber1(); error err_obj = new error();
                IncomingWebRequestContext woc = WebOperationContext.Current.IncomingRequest;
                string api_key = woc.Headers["Api_key"];
                if (api_key != "" && api_key != null)
                {
                    client cl_obj = (from c in dc.clients
                                     where c.APIKey == api_key
                                     select c).SingleOrDefault();
                    // string ReferenceNumber = string.Format("{0}_{1:N}", cl_obj.Email, Guid.NewGuid());

                    if (cl_obj != null)
                    {
                        riddata Campaignexistance = new OperationsBO().CheckCampaignNameExistance(cl_obj, CampaignName);
                        if (Campaignexistance == null)
                        {
                            Random randNum = new Random();
                            int r = randNum.Next(00000, 99999);
                            ReferenceNumber = r.ToString("D5");
                            //if (Uni_RID == 0)
                            //{
                            if (CampaignName.Trim() != "" && Password.Trim() != "" && CampaignName!=null && Password!=null)
                            {
                                
                                new DataInsertionBO().Insertriddata(CampaignName, ReferenceNumber, Password, cl_obj.PK_ClientID);
                                refnum.ReferenceNumber = ReferenceNumber;

                            }
                            else if (CampaignName.Trim() != "" && Password.Trim() == "")
                            {
                                
                                new DataInsertionBO().Insertriddata(CampaignName, ReferenceNumber, "", cl_obj.PK_ClientID);
                                refnum.ReferenceNumber = ReferenceNumber;

                            }


                            System.ServiceModel.Web.WebOperationContext ctx = System.ServiceModel.Web.WebOperationContext.Current;
                            ctx.OutgoingResponse.Headers.Add("Api_key", api_key);
                            // return "ReferenceNumber :" + ReferenceNumber;

                            //refnum.ReferenceNumber = ReferenceNumber;
                            return JsonConvert.SerializeObject(refnum);
                        }
                        else
                        {
                            System.ServiceModel.Web.WebOperationContext ctx = System.ServiceModel.Web.WebOperationContext.Current;
                            ctx.OutgoingResponse.Headers.Add("Api_key", api_key);
                            refnum.ReferenceNumber = Campaignexistance.ReferenceNumber;
                            return JsonConvert.SerializeObject(refnum);
                        }

                    }
                    else
                    {
                        err_obj.message = "Please Pass valid APiKey";
                        return JsonConvert.SerializeObject(err_obj);
                    }
                }
                else
                {
                    err_obj.message = "Please Pass Api Key.";
                    return JsonConvert.SerializeObject(err_obj);
                }
            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException.ToString());
                error errobj = new error();
                //RID_UIDRIID = "NULL";
                errobj.message = "Exception" + ex.Message;
                return JsonConvert.SerializeObject(errobj);
            }
        }

        //        public void Monitize(string Shorturl)

        //         {
        //             try
        //             {
        //                 string longurl = "";
        //                 long decodedvalue = new ConvertionBO().BaseToLong(Shorturl);
        //                 Uniqueid_SHORTURLDATA = Convert.ToInt32(decodedvalue);
        //                 //Uniqueid_SHORTURLDATA = Convert.ToInt32(Shorturl);
        //                 if (new OperationsBO().CheckUniqueid(Uniqueid_SHORTURLDATA, "1"))
        //                 {
        //                     int? Fk_UID = (from u in dc.UIDandriddatas
        //                                    where u.PK_UniqueId == Uniqueid_SHORTURLDATA && u.TypeDiff=="1"
        //                                    select u.UIDorRID).SingleOrDefault();
        //                     int? FK_RID = (from u in dc.uiddatas
        //                                   where u.PK_Uid == Fk_UID
        //                                   select u.FK_RID).SingleOrDefault();
        //                     int? FK_ClientID = (from r in dc.riddatas
        //                                     where r.PK_Rid == FK_RID
        //                                     select r.FK_ClientID).SingleOrDefault();
        //                     //retrive ipaddress and browser
        //                     string ipv4 = new ConvertionBO().GetIP4Address();
        //                     string ipv6 = HttpContext.Current.Request.UserHostAddress;
        //                     string browser = HttpContext.Current.Request.Browser.Browser;
        //                     string browserversion = HttpContext.Current.Request.Browser.Version;
        //                     string req_url = HttpContext.Current.Request.Url.ToString();
        //                     //string[] header_array = HttpContext.Current.Request.Headers.AllKeys;
        //                     string useragent = HttpContext.Current.Request.UserAgent;
        //                     System.Collections.ArrayList browsers = HttpContext.Current.Request.Browser.Browsers;
        //                     string hostname = HttpContext.Current.Request.UserHostName;
        //                     string devicetype = HttpContext.Current.Request.Browser.Platform;
        //                     string ismobiledevice = HttpContext.Current.Request.Browser.IsMobileDevice.ToString();
        //                     //retrieve longurl from uid
        //                     longurl = new OperationsBO().GetLongURL(Uniqueid_SHORTURLDATA);
        //                     //if(longurl!=null)
        //                     //    HttpContext.Current.Response.Redirect(longurl);

        //                     //retrive city,country
        //                     var City = ""; var Region = ""; var Country = ""; var CountryCode = ""; var url = "";
        //                     //url = "http://freegeoip.net/json/" + "99.25.39.48";
        //                     url = "http://freegeoip.net/json/" + ipv4;
        //                     var request = System.Net.WebRequest.Create(url);
        //                     using (WebResponse wrs = request.GetResponse())
        //                     using (Stream stream = wrs.GetResponseStream())
        //                     using (StreamReader reader = new StreamReader(stream))
        //                     {
        //                         string json = reader.ReadToEnd();
        //                         var obj = JObject.Parse(json);
        //                         City = (string)obj["city"];
        //                         Region = (string)obj["region_name"];
        //                         Country = (string)obj["country_name"];
        //                         CountryCode = (string)obj["country_code"];
        //                     }
        //                     //retrive city,country if city country not found with ipv4
        //                     if (City == "" && Country == "")
        //                     {
        //                         url = "http://freegeoip.net/json/" + ipv6;
        //                         var request1 = System.Net.WebRequest.Create(url);
        //                         using (WebResponse wrs = request1.GetResponse())
        //                         using (Stream stream = wrs.GetResponseStream())
        //                         using (StreamReader reader = new StreamReader(stream))
        //                         {
        //                             string json = reader.ReadToEnd();
        //                             var obj = JObject.Parse(json);
        //                             City = (string)obj["city"];
        //                             Region = (string)obj["region_name"];
        //                             Country = (string)obj["country_name"];
        //                             CountryCode = (string)obj["country_code"];
        //                         }
        //                     }
        //                     new DataInsertionBO().InsertShortUrldata(ipv4, ipv6, browser, browserversion, City, Region, Country, CountryCode, req_url, useragent, hostname, devicetype, ismobiledevice, Fk_UID, FK_RID, FK_ClientID, Uniqueid_SHORTURLDATA);
        //                 }
        //                 WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.Redirect;
        //                 if (!longurl.StartsWith("http://") && !longurl.StartsWith("https://"))
        //                     WebOperationContext.Current.OutgoingResponse.Headers.Add("Location", "http://" + longurl);
        //                 else
        //                     WebOperationContext.Current.OutgoingResponse.Headers.Add("Location", longurl);
        //             }
        //             catch (Exception ex)
        //             {
        //                 ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException.ToString());

        //             }
        //}
        //[DataContract]
        //public class CountsData
        //{
        //     [DataMember(EmitDefaultValue = false)]
        //    public List<DayWiseData> activity { get; set; }
        //    public List<CountryWiseData> locations { get; set; }
        //    public List<DeviceWiseData> devices { get; set; }
        //    public List<BrowserWiseData> platforms { get; set; }

        // }
        //[DataContract]
        //public class DayWiseData
        //{
        //   [DataMember(EmitDefaultValue = false, Name = "RequestedDate")]
        //    public string RequestedDateStr
        //    {
        //        get
        //        {
        //            if (this.RequestedDate.HasValue)
        //            {
        //               // return this.RequestedDate.Value.ToUniversalTime().ToString("s", CultureInfo.InvariantCulture);
        //                return this.RequestedDate.Value.ToString();
        //            }
        //            else
        //                return null;
        //        }
        //        set
        //        {
        //            // should implement this...
        //        }
        //    }

        //    //// this property is not transformed to JSon. Basically hidden
        //    [IgnoreDataMember]
        //    private DateTime? RequestedDate { get; set; }

        //   [DataMember(EmitDefaultValue = false)]
        //    public int RequestCount { get; set; }

        //}

        //public class CountryWiseData
        //{
        //    public string code { get; set; }
        //    public int value { get; set; }
        //    public string name { get; set; }
        //}
        //public class DeviceWiseData
        //{
        //    public string name { get; set; }
        //    public int y { get; set; }
        //}
        //public class BrowserWiseData
        //{
        //    public string name { get; set; }
        //    public int y { get; set; }
        //}
        //public class Summary
        //{
        //    public string url { get; set; }
        //    public int visits { get; set; }
        //    public int? unique_users { get; set; }
        //    public int total_users { get; set; }
        //}
        //public Stream GETAllCounts(string Fk_Uniqueid, string DateFrom, string DateTo)
        //{
        //    try 
        //    { 
        //    if (Fk_Uniqueid != "" && Fk_Uniqueid != null)
        //    {
        //        CountsData countobj = new CountsData();
        //        long decodedvalue = new ConvertionBO().BaseToLong(Fk_Uniqueid);
        //        Uniqueid_SHORTURLDATA = Convert.ToInt32(decodedvalue);
        //        int? rid = (from u in dc.UIDandriddatas
        //                    where u.PK_UniqueId == Uniqueid_SHORTURLDATA
        //                    select u.UIDorRID).SingleOrDefault();
        //        string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;

        //        // create and open a connection object
        //        lSQLConn = new MySqlConnection(connStr);
        //        SqlDataReader myReader;
        //        lSQLConn.Open();
        //        lSQLCmd.CommandType = CommandType.StoredProcedure;
        //        lSQLCmd.CommandText = "spGetALLCOUNTS1";
        //        //lSQLCmd.Parameters.Add(new MySqlParameter("@Fk_Uniqueid", Uniqueid_SHORTURLDATA));
        //        lSQLCmd.Parameters.Add(new MySqlParameter("@rid", rid));
        //        lSQLCmd.Parameters.Add(new MySqlParameter("@DateFrom", DateFrom));
        //        lSQLCmd.Parameters.Add(new MySqlParameter("@DateTo", DateTo));
        //        lSQLCmd.Connection = lSQLConn;
        //        myReader = lSQLCmd.ExecuteReader();


        //        List<DayWiseData> activity = ((IObjectContextAdapter)dc)
        //          .ObjectContext
        //          .Translate<DayWiseData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();


        //        // Move to locations result 
        //        myReader.NextResult();
        //        List<CountryWiseData> locations = ((IObjectContextAdapter)dc)
        //       .ObjectContext
        //       .Translate<CountryWiseData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();

        //        // Move to devices result 
        //        myReader.NextResult();
        //        List<DeviceWiseData> devices = ((IObjectContextAdapter)dc)
        //      .ObjectContext
        //      .Translate<DeviceWiseData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();

        //        // Move to platforms result 
        //        myReader.NextResult();
        //        List<BrowserWiseData> platforms = ((IObjectContextAdapter)dc)
        //      .ObjectContext
        //      .Translate<BrowserWiseData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();


        //        countobj.activity = activity;
        //        countobj.locations = locations;
        //        countobj.devices = devices;
        //        countobj.platforms = platforms;


        //        var s = new JavaScriptSerializer();
        //        string jsonclient = s.Serialize(countobj);

        //        WebOperationContext.Current.OutgoingResponse.ContentType =
        //            "application/json; charset=utf-8";
        //        return new MemoryStream(Encoding.UTF8.GetBytes(jsonclient));
        //    }
        //    else
        //    {
        //        string jsonclient = "please pass reference number";

        //        WebOperationContext.Current.OutgoingResponse.ContentType =
        //            "application/json; charset=utf-8";
        //        return new MemoryStream(Encoding.UTF8.GetBytes(jsonclient));
        //    }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException.ToString());
        //        string jsonclient = "Exception occurs";

        //        WebOperationContext.Current.OutgoingResponse.ContentType =
        //            "application/json; charset=utf-8";
        //        return new MemoryStream(Encoding.UTF8.GetBytes(jsonclient));
        //    }
        //}

        //public Stream GETSummary(string Fk_Uniqueid)
        //{
        //    try
        //    {
        //        Summary s_obj = new Summary();
        //        long decodedvalue = new ConvertionBO().BaseToLong(Fk_Uniqueid);
        //        Uniqueid_SHORTURLDATA = Convert.ToInt32(decodedvalue);
        //        //Uniqueid_SHORTURLDATA = Convert.ToInt32(Fk_Uniqueid);
        //        int? rid = (from u in dc.UIDandriddatas
        //                    where u.PK_UniqueId == Uniqueid_SHORTURLDATA && u.TypeDiff == "2"
        //                    select u.UIDorRID).SingleOrDefault();
        //        var surl = (from ss in dc.SHORTURLDATAs
        //                    where ss.FK_RID == rid
        //                    select new { ss.Req_url }).ToList().Take(1);
        //        int totalVisits = dc.SHORTURLDATAs.Where(sh => sh.FK_RID == rid).Count();
        //        //int totalUsers = dc.SHORTURLDATAs.Where(sh => sh.FK_RID == rid).Count();
        //        int totalUsers = dc.uiddatas.Where(sh => sh.FK_RID == rid).Count();
        //        int? totalUniqueUsers = (from sh in dc.SHORTURLDATAs
        //                                 where sh.FK_RID == rid
        //                                 select sh.FK_Uid).Distinct().Count();
        //        s_obj.url = surl.Select(x => x.Req_url).SingleOrDefault();
        //        s_obj.visits = totalVisits;
        //        s_obj.total_users = totalUsers;
        //        s_obj.unique_users = totalUniqueUsers;
        //        var s = new JavaScriptSerializer();
        //        string jsonclient = s.Serialize(s_obj);

        //        WebOperationContext.Current.OutgoingResponse.ContentType =
        //            "application/json; charset=utf-8";
        //        return new MemoryStream(Encoding.UTF8.GetBytes(jsonclient));
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException.ToString());
        //        string jsonclient = "Exception occurs";

        //        WebOperationContext.Current.OutgoingResponse.ContentType =
        //            "application/json; charset=utf-8";
        //        return new MemoryStream(Encoding.UTF8.GetBytes(jsonclient));
        //    }
        //}


        //    public string Authenticate_Token(Stream api_key)
        //    {
        //        string parameters = new StreamReader(api_key).ReadToEnd(); //email1;
        //        JObject jObject = JObject.Parse(parameters);
        //        string apikey = (string)jObject["_apikey"];
        //        bool check = ValidateAPIKey(apikey);
        //        //need to return token
        //        string token = "";
        //        return token;
        //    }


        //    bool ValidateAPIKey(string key)
        //    {

        //        //string key = HttpContext.Current.Request.QueryString["apikey"];
        //        //if (string.IsNullOrEmpty(key))
        //        //   key = HttpContext.Current.Request.Headers["apikey"];
        //        if (!string.IsNullOrEmpty(key))
        //        {
        //            Guid apiKey;
        //            Guid hardKey = new Guid("F5D14784-2D9E-4F57-A69E-50FB0551940A");
        //            // Convert the string into a Guid and validate it
        //            if (!Guid.TryParse(key, out apiKey) || !apiKey.Equals(hardKey)) //we are not validating yet just hard code one guid
        //            {
        //                //throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);
        //                return false;
        //            }
        //            else
        //                return false;
        //        }
        //        else
        //            return false;
        //            //throw new System.ServiceModel.Web.WebFaultException<string>("Please Provide  API Key", HttpStatusCode.Forbidden);

        //    }


        //    public string IsAuthorized(string username, string password, string apikey)
        //    {
        //        MembershipUser user = Membership.GetAllUsers()[username];

        //        Configuration config = ConfigurationManager.OpenExeConfiguration(HostingEnvironment.MapPath("~") + "\\web.config");

        //        SessionStateSection sessionStateConfig = (SessionStateSection)config.SectionGroups.Get("system.web").Sections.Get("sessionState");

        //        InMemoryInstances instance = InMemoryInstances.Instance;
        //        // Check for session state timeout (could use a constant here instead if you don't want to rely on the config).
        //        if (user.LastLoginDate.AddMinutes(sessionStateConfig.Timeout.TotalMinutes) < DateTime.Now)
        //        {
        //            // Remove token from the singleton in this instance, effectively a logout.                
        //            instance.removeTokenUserPair(username);
        //            return "User Unauthorized - login has expired!";
        //        }
        //        if (!instance.checkTokenUserPair(username, apikey))
        //            return "User Unauthorized - not a valid token!";

        //        return "Success - User is Authorized!";

        //    }
        //    public string AuthenticateUser(string username, string encryptedPassword)
        //    {
        //        //string pwd = Decrypt(encryptedPassword);
        //        string pwd = "";
        //        if (Membership.ValidateUser(username, pwd))
        //        {
        //            // Not sure if this is actually needed, but reading some documentation I think it's a safe bet to do here anyway.
        //            Membership.GetAllUsers()[username].LastLoginDate = DateTime.Now;

        //            // Send back a token!
        //            Guid token = Guid.NewGuid();

        //            // Store a token for this username.
        //            InMemoryInstances instance = InMemoryInstances.Instance;
        //            instance.removeTokenUserPair(username); //Because we don't implement a "Logout" method.
        //            instance.addTokenUserPair(username, token.ToString());

        //            return token.ToString();
        //        }

        //        return "Error - User was not able to be validated!";
        //    }



        //     public class InMemoryInstances
        //{
        //    private static volatile InMemoryInstances instance;
        //    private static object syncRoot = new Object();

        //    private Dictionary<string, string> usersAndTokens = null;

        //    private InMemoryInstances() 
        //    {
        //        usersAndTokens = new Dictionary<string, string>();
        //    }

        //    public static InMemoryInstances Instance
        //    {
        //        get 
        //        {
        //            if (instance == null) 
        //            {
        //                lock (syncRoot)                  
        //                {
        //                    if (instance == null) 
        //                        instance = new InMemoryInstances();
        //                }
        //            }

        //            return instance;
        //        }
        //    }

        //    public void addTokenUserPair(string username, string token)
        //    {
        //        usersAndTokens.Add(username, token);
        //    }

        //    public bool checkTokenUserPair(string username, string token)
        //    {
        //        if (usersAndTokens.ContainsKey(username)) {
        //            string value = usersAndTokens[username];
        //            if (value.Equals(token))
        //                return true;
        //        }

        //        return false;
        //    }

        //    public void removeTokenUserPair(string username)
        //    {
        //        usersAndTokens.Remove(username);
        //    }
        //}








        /// <summary>
        /// Initializes a new instance of the REST
        /// </summary>
        //public Service1()
        //{
        //    ValidateKey(); //since we have InstanceContextMode = InstanceContextMode.PerCall, this is called per every api call yay!!!

        //}

        //void ValidateKey()
        //{

        //    string key = HttpContext.Current.Request.QueryString["apikey"];
        //    if (string.IsNullOrEmpty(key))
        //        key = HttpContext.Current.Request.Headers["apikey"];
        //    if (!string.IsNullOrEmpty(key))
        //    {
        //        Guid apiKey;
        //        Guid hardKey = new Guid("F5D14784-2D9E-4F57-A69E-50FB0551940A");
        //        // Convert the string into a Guid and validate it
        //        if (!Guid.TryParse(key, out apiKey) || !apiKey.Equals(hardKey)) //we are not validating yet just hard code one guid
        //        {
        //            throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);
        //        }
        //    }
        //    else
        //        throw new System.ServiceModel.Web.WebFaultException<string>("Please Provide  API Key", HttpStatusCode.Forbidden);

        //}


    }
}
