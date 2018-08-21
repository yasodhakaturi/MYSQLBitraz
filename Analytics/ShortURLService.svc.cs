using Analytics.Helpers.BO;
using Analytics.Helpers.Utility;
using Analytics.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Analytics
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ShortURLService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ShortURLService.svc or ShortURLService.svc.cs at the Solution Explorer and start debugging.
    public class ShortURLService : IShortURLService
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
            public string CampaignNumber { get; set; }
        }
        public class ShortUrl1
        {
            public string shortUrl { get; set; }
            public string CampaignId { get; set; }
            public string ShortUrlId { get; set; }
            public string ClientId { get; set; }
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

                    client cl_obj = new OperationsBO().CheckclientEmail(Email, Password);
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
                ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
                error errobj = new error();
                errobj.message = "Exception" + ex.Message;
                return JsonConvert.SerializeObject(errobj);
            }
        }

        public string AuthenticateUser(string Email, string Password,string Api_Key)
        {
             client cl_obj = new OperationsBO().CheckclientEmailApi_key(Email, Password,Api_Key);
             
            //if (Membership.ValidateUser(UserName, Password))
            // {
                 // Not sure if this is actually needed, but reading some documentation I think it's a safe bet to do here anyway.
                // Membership.GetAllUsers()[UserName].LastLoginDate = DateTime.Now;
             if (cl_obj != null)
             {
                 // Send back a token!
                 Guid token = Guid.NewGuid();

                 // Store a token for this username.
                 InMemoryInstances instance = InMemoryInstances.Instance;
                 instance.removeTokenUserPair(cl_obj.PK_ClientID.ToString()); //Because we don't implement a "Logout" method.
                 instance.addTokenUserPair(cl_obj.PK_ClientID.ToString(), token.ToString());

                 
                 System.ServiceModel.Web.WebOperationContext ctx = System.ServiceModel.Web.WebOperationContext.Current;
                 ctx.OutgoingResponse.Headers.Add("token", token.ToString());
                 return token.ToString();
             }
             else
             {
                 error errobj = new error();
                 errobj.message = "Authentication Failed.Please provide valid user inputs.";
                 return JsonConvert.SerializeObject(errobj);
             }
             //}
            
        }
        public class InMemoryInstances
        {
            private static volatile InMemoryInstances instance;
            private static object syncRoot = new Object();

            private Dictionary<string, string> usersAndTokens = null;

            private InMemoryInstances()
            {
                usersAndTokens = new Dictionary<string, string>();
            }

            public static InMemoryInstances Instance
            {
                get
                {
                    if (instance == null)
                    {
                        lock (syncRoot)
                        {
                            if (instance == null)
                                instance = new InMemoryInstances();
                        }
                    }

                    return instance;
                }
            }

            public void addTokenUserPair(string username, string token)
            {
                usersAndTokens.Add(username, token);
            }

            public bool checkTokenUserPair(string username, string token)
            {
                if (usersAndTokens.ContainsKey(username))
                {
                    string value = usersAndTokens[username];
                    if (value.Equals(token))
                        return true;
                }

                return false;
            }
            public string GetClientIdFromToken(string token)
            {
                string value = "";
                if (usersAndTokens.ContainsValue(token))
                {
                    value = usersAndTokens.Where(x => x.Value == token).Select(y => y.Key).SingleOrDefault();
                     //value = usersAndTokens[token];
                    return value;
                }

                return value;
            }
            public void removeTokenUserPair(string username)
            {
                usersAndTokens.Remove(username);
            }
        }
        public string RegisterCampaign(string CampaignName, string Password)
        {
            try
            {
                InMemoryInstances instance = InMemoryInstances.Instance;
                IncomingWebRequestContext woc = WebOperationContext.Current.IncomingRequest;
                 string ReferenceNumber = "";
                ReferenceNumber1 refnum = new ReferenceNumber1(); error err_obj = new error();
                string token = woc.Headers["token"];
                
                //if (pkclientid == "")
                //    return "User Unauthorized - not a valid token!";

               
                if (token != "" && token != null)
                {
                    string pkclientid = instance.GetClientIdFromToken(token);
                    int pk_clientid = Convert.ToInt32(pkclientid);
                    client cl_obj = (from c in dc.clients
                                     where c.PK_ClientID == pk_clientid
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
                            if (CampaignName.Trim() != "" && Password.Trim() != "" && CampaignName != null && Password != null)
                            {

                                new DataInsertionBO().Insertriddata(CampaignName, ReferenceNumber, Password, cl_obj.PK_ClientID);
                                //refnum.CampaignNumber = ReferenceNumber;

                            }
                            else if (CampaignName.Trim() != "" && (Password.Trim() == "" || Password==null))
                            {

                                new DataInsertionBO().Insertriddata(CampaignName, ReferenceNumber, "", cl_obj.PK_ClientID);
                                //refnum.CampaignNumber = ReferenceNumber;

                            }


                            System.ServiceModel.Web.WebOperationContext ctx = System.ServiceModel.Web.WebOperationContext.Current;
                            ctx.OutgoingResponse.Headers.Add("token", token);
                            int CampaignNumber = dc.riddatas.Where(x => x.ReferenceNumber == ReferenceNumber).Select(y => y.PK_Rid).SingleOrDefault();
                            // return "ReferenceNumber :" + ReferenceNumber;

                            //refnum.ReferenceNumber = ReferenceNumber;
                            return JsonConvert.SerializeObject("Campaign Registered Successfully.CampaignId is " + CampaignNumber);
                        }
                        else
                        {
                            err_obj.message = "CampaignName Already exists.";
                            return JsonConvert.SerializeObject(err_obj);
                        }
                        //else
                        //{
                        //    System.ServiceModel.Web.WebOperationContext ctx = System.ServiceModel.Web.WebOperationContext.Current;
                        //    ctx.OutgoingResponse.Headers.Add("token", token);
                        //    refnum.ReferenceNumber = Campaignexistance.ReferenceNumber;
                        //    return JsonConvert.SerializeObject(refnum);
                        //}

                    }
                    else
                    {
                        err_obj.message = "Please Pass valid token";
                        return JsonConvert.SerializeObject(err_obj);
                    }
                }
                else
                {
                    err_obj.message = "Please Pass token.";
                    return JsonConvert.SerializeObject(err_obj);
                }
            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
                error errobj = new error();
                //RID_UIDRIID = "NULL";
                errobj.message = "Exception" + ex.Message;
                return JsonConvert.SerializeObject(errobj);
            }
        }
        //public string GetCampaignReferenceNumber(string CampaignName, string Password)
        //{
        //    try
        //    {
        //        InMemoryInstances instance = InMemoryInstances.Instance;
        //        int clientid = 0; int Uni_RID = 0; string ReferenceNumber = "";
        //        ReferenceNumber1 refnum = new ReferenceNumber1(); error err_obj = new error();
        //        IncomingWebRequestContext woc = WebOperationContext.Current.IncomingRequest;
        //        string token = woc.Headers["token"];
        //        string pkclientid = instance.GetClientIdFromToken(token);
        //        int pk_clientid = Convert.ToInt32(pkclientid);
        //        if (token != "" && token != null)
        //        {
        //            client cl_obj = (from c in dc.clients
        //                             where c.PK_ClientID == pk_clientid
        //                             select c).SingleOrDefault();
        //            // string ReferenceNumber = string.Format("{0}_{1:N}", cl_obj.Email, Guid.NewGuid());

        //            if (cl_obj != null)
        //            {
        //                riddata Campaignexistance = new OperationsBO().CheckCampaignNameExistance(cl_obj, CampaignName);
        //                if (Password != null && Password != "" && Campaignexistance != null)
        //                {
        //                    if (Campaignexistance.Pwd != Password)
        //                        Campaignexistance = null;
        //                }
        //                if (Campaignexistance != null)
        //                {
        //                    System.ServiceModel.Web.WebOperationContext ctx = System.ServiceModel.Web.WebOperationContext.Current;
        //                    ctx.OutgoingResponse.Headers.Add("token", token);
        //                    refnum.CampaignNumber = Campaignexistance.ReferenceNumber;
        //                    return JsonConvert.SerializeObject(refnum);
        //                }
        //                else
        //                {
        //                    err_obj.message = "Invalid Campaign";
        //                    return JsonConvert.SerializeObject(err_obj);
        //                }
        //            }
        //            else
        //            {
        //                err_obj.message = "Please Pass valid token";
        //                return JsonConvert.SerializeObject(err_obj);
        //            }
        //        }
        //        else
        //        {
        //            err_obj.message = "Please Pass valid token";
        //            return JsonConvert.SerializeObject(err_obj);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
        //        error errobj = new error();
        //        //RID_UIDRIID = "NULL";
        //        errobj.message = "Exception" + ex.Message;
        //        return JsonConvert.SerializeObject(errobj);
        //    }
        //}



        public string GetShortUrl(string CampaignId, string Type,string longurlorMessage, string mobilenumber)
        {
            try
            {
                InMemoryInstances instance = InMemoryInstances.Instance;
                IncomingWebRequestContext woc = WebOperationContext.Current.IncomingRequest;
                string token = woc.Headers["token"];
                int rid;
                string Hashid = ""; int pk_uid = 0;
                if (token != "" && token != null)
                {
                    string pkclientid = instance.GetClientIdFromToken(token);
                    int pk_clientid = Convert.ToInt32(pkclientid);
                    client cl_obj = (from c in dc.clients
                                     where c.PK_ClientID == pk_clientid
                                     select c).SingleOrDefault();
                    if (CampaignId.Trim() != "" && longurlorMessage.Trim() != "" && Type.Trim() != "" && mobilenumber.Trim() != "" && cl_obj != null)
                    {
                        rid = Convert.ToInt32(CampaignId);
                        //check reference number in RID table
                        riddata objrid = (from registree in dc.riddatas
                                          where registree.PK_Rid == rid && registree.FK_ClientId == pk_clientid
                                          select registree).SingleOrDefault();
                        
                        if (objrid != null)
                        {
                            //check data in UID table
                            Hashid = (from registree in dc.uiddatas
                                      where registree.ReferenceNumber.Trim() == objrid.ReferenceNumber &&
                                      registree.LongurlorMessage.Trim() == longurlorMessage.Trim() &&
                                      registree.MobileNumber.Trim() == mobilenumber.Trim()
                                      select registree.UniqueNumber).SingleOrDefault();
                            //if data found in uiddata insert data into uiddata 
                            if (Hashid == null)
                            {
                                //Uniqueid = Helper.GetRandomAlphanumericString(5);
                                new DataInsertionBO().Insertuiddata(objrid.PK_Rid, objrid.FK_ClientId, objrid.ReferenceNumber,Type,longurlorMessage, mobilenumber);

                                pk_uid = (from registree in dc.uiddatas
                                          where registree.ReferenceNumber.Trim() == objrid.ReferenceNumber.Trim() &&
                                          registree.LongurlorMessage.Trim() == longurlorMessage.Trim() &&
                                          registree.MobileNumber.Trim() == mobilenumber.Trim()
                                          select registree.PK_Uid).SingleOrDefault();
                                //Hashid = Helper.GetHashID(pk_uid);
                                Hashid = dc.hashidlists.Where(x => x.PK_Hash_ID == pk_uid).Select(y => y.HashID).SingleOrDefault();
                                new OperationsBO().UpdateHashid(pk_uid, Hashid);
                            }


                            System.ServiceModel.Web.WebOperationContext ctx = System.ServiceModel.Web.WebOperationContext.Current;
                            ctx.OutgoingResponse.Headers.Add("token", token);
                            // return "http://g0.pe/" + Hashid;
                            //string ShortUrl = "https://g0.pe/" + Hashid;
                            string ShortUrl = ConfigurationManager.AppSettings["ShortenurlHost"].ToString() + Hashid;
                            uiddata uidrec=dc.uiddatas.Where(x=>x.FK_RID==rid).Select(y=>y).SingleOrDefault();
                            ShortUrl1 sobj = new ShortUrl1();
                            sobj.shortUrl = ShortUrl;
                            sobj.ShortUrlId = uidrec.PK_Uid.ToString();
                            sobj.CampaignId = CampaignId;
                            sobj.ClientId = uidrec.FK_ClientID.ToString();
                            return JsonConvert.SerializeObject(sobj);

                        }
                        else
                        {
                            error errobj = new error();
                            errobj.message = "CamapignName not registered.";
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
                    errobj.message = "Please Pass valid token";
                    return JsonConvert.SerializeObject(errobj);
                    //return "Please Pass valid APiKey";
                }
            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
                error errobj = new error();
                //UID_UIDRID = "NULL";
                errobj.message = "Exception" + ex.Message;
                return JsonConvert.SerializeObject(errobj);
            }
        }

        public string GetAllCampaigns()
        {
            try
            {
                InMemoryInstances instance = InMemoryInstances.Instance;
               
                error err_obj = new error();
                IncomingWebRequestContext woc = WebOperationContext.Current.IncomingRequest;
                string token = woc.Headers["token"];
                
                if (token != "" && token != null)
                {
                    string pkclientid = instance.GetClientIdFromToken(token);
                    int pk_clientid = Convert.ToInt32(pkclientid);
                    client cl_obj = (from c in dc.clients
                                     where c.PK_ClientID == pk_clientid
                                     select c).SingleOrDefault();
                    // string ReferenceNumber = string.Format("{0}_{1:N}", cl_obj.Email, Guid.NewGuid());

                    if (cl_obj != null)
                    {
                        List<CampaignsList> Campaigns = dc.riddatas.Where(x => x.FK_ClientId == pk_clientid).Select(y => new CampaignsList() { CampaignName = y.CampaignName }).ToList();

                        if (Campaigns.Count != 0)
                        {
                            System.ServiceModel.Web.WebOperationContext ctx = System.ServiceModel.Web.WebOperationContext.Current;
                            ctx.OutgoingResponse.Headers.Add("token", token);
                            //refnum.CampaignNumber = Campaignexistance.ReferenceNumber;
                            return JsonConvert.SerializeObject(Campaigns);
                        }
                        else
                        {
                            err_obj.message = "No Registered Campaigns";
                            return JsonConvert.SerializeObject(err_obj);
                        }
                    }
                        else
                        {
                            err_obj.message = "Please pass valid Email.";
                            return JsonConvert.SerializeObject(err_obj);
                        }
                }
                    else
                    {
                        err_obj.message = "Please Pass valid token";
                        return JsonConvert.SerializeObject(err_obj);
                    }
                
            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
                error errobj = new error();
                //RID_UIDRIID = "NULL";
                errobj.message = "Exception" + ex.Message;
                return JsonConvert.SerializeObject(errobj);
            }
        }

        public string GetCampaignAnalyticsData(string CampaignId)
        {
            try
            {
                InMemoryInstances instance = InMemoryInstances.Instance;
               
                error err_obj = new error();
                IncomingWebRequestContext woc = WebOperationContext.Current.IncomingRequest;
                string token = woc.Headers["token"];
                error errobj = new error();
                if (token != "" && token != null)
                {
                    string pkclientid = instance.GetClientIdFromToken(token);
                    int pk_clientid = Convert.ToInt32(pkclientid);
                    client cl_obj = (from c in dc.clients
                                     where c.PK_ClientID == pk_clientid
                                     select c).SingleOrDefault();
                    if (CampaignId != null)
                    {
                        int? cid = Convert.ToInt32(CampaignId);
                        riddata objr = dc.riddatas.Where(r => r.PK_Rid ==cid).SingleOrDefault();
                        if (objr != null && cl_obj != null)
                        {
                            List<ExportAnalyticsData> objexport = (from s in dc.shorturldatas
                                                                   join u in dc.uiddatas on s.FK_Uid equals u.PK_Uid
                                                                   join r in dc.riddatas on u.FK_RID equals r.PK_Rid
                                                                   join c in dc.clients on r.FK_ClientId equals c.PK_ClientID
                                                                   where s.FK_RID == objr.PK_Rid && s.FK_ClientID == objr.FK_ClientId
                                                                   select new ExportAnalyticsData()
                                                                   {
                                                                       CampaignId=s.FK_RID.ToString(),
                                                                       ClientId=s.FK_ClientID.ToString(),
                                                                       HitId=s.PK_Shorturl.ToString(),
                                                                       ShorturlId=s.FK_Uid.ToString(),
                                                                       CampaignName = r.CampaignName,
                                                                       Mobilenumber = u.MobileNumber,
                                                                       ShortURL = s.Req_url,
                                                                       LongUrl = u.LongurlorMessage,
                                                                       GoogleMapUrl = "https://www.google.com/maps?q=loc:" + s.Latitude + "," + s.Longitude,
                                                                       IPAddress = s.Ipv4,
                                                                       Browser = s.Browser,
                                                                       BrowserVersion = s.Browser_version,
                                                                       City = s.City,
                                                                       Region = s.Region,
                                                                       Country = s.Country,
                                                                       CountryCode = s.CountryCode,
                                                                       PostalCode = s.PostalCode,
                                                                       Lattitude = s.Latitude,
                                                                       Longitude = s.Longitude,
                                                                       MetroCode = s.MetroCode,
                                                                       DeviceName = s.DeviceName,
                                                                       DeviceBrand = s.DeviceBrand,
                                                                       OS_Name = s.OS_Name,
                                                                       OS_Version = s.OS_Version,
                                                                       IsMobileDevice = s.IsMobileDevice,
                                                                       CreatedDate = s.CreatedDate.ToString(),
                                                                       clientName = c.UserName


                                                                   }
                                                                     ).ToList();
                            if (objexport.Count>0)
                            {
                                System.ServiceModel.Web.WebOperationContext ctx = System.ServiceModel.Web.WebOperationContext.Current;
                                ctx.OutgoingResponse.Headers.Add("token", token);
                                //refnum.CampaignNumber = Campaignexistance.ReferenceNumber;
                                return JsonConvert.SerializeObject(objexport);
                            }
                            else
                            {
                                err_obj.message = "No Analytics Data for this Campaign";
                                return JsonConvert.SerializeObject(err_obj);
                            }
                        }
                        else
                        {
                            err_obj.message = "Campaign doesnot exist.";
                            return JsonConvert.SerializeObject(err_obj);
                        }
                    }
                    else
                    {
                        err_obj.message = "Please Pass valid Reference Number";
                        return JsonConvert.SerializeObject(err_obj);
                    }
                }

               else
                    {
                        err_obj.message = "Please Pass valid token";
                        return JsonConvert.SerializeObject(err_obj);
                    }
                

            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
                error errobj = new error();
                //RID_UIDRIID = "NULL";
                errobj.message = "Exception" + ex.Message;
                return JsonConvert.SerializeObject(errobj);
            }
        }
        public class CampaignsList
        {
            public string CampaignName { get; set; }
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
