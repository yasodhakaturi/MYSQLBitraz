using Analytics.Helpers.BO;
using Analytics.Helpers.Utility;
using Analytics.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
//using System.Data.Sqlclient;
using System.Linq;  
using System.Web;
using System.Web.Mvc;

namespace Analytics.Controllers
{
    public class AnalyticsController : Controller
    {
        shortenurlEntities dc = new shortenurlEntities();
        MySqlConnection lSQLConn = null;
        MySqlCommand lSQLCmd = new MySqlCommand();
        // GET: Analytics
        public ActionResult Index()
        {
            UserViewModel obj = new UserViewModel();
            string url = Request.Url.ToString();
            obj = new OperationsBO().GetViewConfigDetails(url);

            return View(obj);
        }
        public JsonResult GETSummary1(int cid,string rid)
        {

            DashBoardSummary obj = new DashBoardSummary();

            try
            {
                if (Session["id"] != null)
                {
                    if (Session["id"] != null && rid == null)
                    {
                        //int c_id = (int)Session["id"];

                        //if (cid != "" && cid != null)
                        //{
                        string role = Helper.CurrentUserRole;
                        MySqlDataReader myReader;
                        if (role.ToLower() != "admin")
                        {
                            client obj_client = dc.clients.Where(x => x.PK_ClientID == cid).Select(y => y).SingleOrDefault();
                            if (obj_client != null)
                            {
                                string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;

                                // create and open a connection object
                                lSQLConn = new MySqlConnection(connStr);
                                lSQLConn.Open();
                                lSQLCmd.CommandType = CommandType.StoredProcedure;
                                lSQLCmd.CommandTimeout = 600;
                                lSQLCmd.CommandText = "spGetDashBoardSummary";
                                lSQLCmd.Parameters.Add(new MySqlParameter("@FkclientId", cid));
                                lSQLCmd.Connection = lSQLConn;
                                //myReader = lSQLCmd.ExecuteReader();
                            }
                        }
                        else
                        {
                            string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;

                            // create and open a connection object
                            lSQLConn = new MySqlConnection(connStr);
                            lSQLConn.Open();
                            lSQLCmd.CommandType = CommandType.StoredProcedure;
                            lSQLCmd.CommandTimeout = 600;
                            lSQLCmd.CommandText = "spGetDashBoardSummary";
                            lSQLCmd.Parameters.Add(new MySqlParameter("@FkclientId", "0"));
                            lSQLCmd.Connection = lSQLConn;
                        }
                        myReader = lSQLCmd.ExecuteReader();
                       
                        totalUrls totalUrls = ((IObjectContextAdapter)dc)
                              .ObjectContext
                              .Translate<totalUrls>(myReader, "shorturldatas", MergeOption.AppendOnly).SingleOrDefault();
                        
                         
                        // Move to Users result 
                            myReader.NextResult();
                            users users = ((IObjectContextAdapter)dc)
                           .ObjectContext
                           .Translate<users>(myReader, "shorturldatas", MergeOption.AppendOnly).SingleOrDefault();

                            // Move to Visits result 
                            myReader.NextResult();
                            visits visits = ((IObjectContextAdapter)dc)
                           .ObjectContext
                           .Translate<visits>(myReader, "shorturldatas", MergeOption.AppendOnly).SingleOrDefault();

                            // Move to Campaign result 
                            myReader.NextResult();
                            campaigns campaigns = ((IObjectContextAdapter)dc)
                           .ObjectContext
                           .Translate<campaigns>(myReader, "shorturldatas", MergeOption.AppendOnly).SingleOrDefault();

                            // Move to Recent Campaigns result 
                            myReader.NextResult();
                            List<recentCampaigns1> recentCampaigns = ((IObjectContextAdapter)dc)
                           .ObjectContext
                           .Translate<recentCampaigns1>(myReader, "shorturldatas", MergeOption.AppendOnly).ToList();

                            // Move to Today Campaigns result 
                            myReader.NextResult();
                            today today = ((IObjectContextAdapter)dc)
                           .ObjectContext
                           .Translate<today>(myReader, "shorturldatas", MergeOption.AppendOnly).SingleOrDefault();

                            // Move to Last 7 days Campaigns result 
                            myReader.NextResult();
                            last7days last7days = ((IObjectContextAdapter)dc)
                           .ObjectContext
                           .Translate<last7days>(myReader, "shorturldatas", MergeOption.AppendOnly).SingleOrDefault();

                            // Move to this month Campaigns result 
                            myReader.NextResult();
                            month month = ((IObjectContextAdapter)dc)
                           .ObjectContext
                           .Translate<month>(myReader, "shorturldatas", MergeOption.AppendOnly).SingleOrDefault();

                            List<recentCampaigns> objr = (from r in recentCampaigns
                                                          select new recentCampaigns()
                                                          {
                                                              id = r.id,
                                                              rid = r.rid,
                                                              visits = r.visits,
                                                              users = r.users,
                                                              status = r.status,
                                                              //crd = r.createdOn.Value.ToString("MM/dd/yyyyThh:mm:ss")
                                                              createdOn = r.crd.Value.ToString("yyyy-MM-ddThh:mm:ss"),
                                                              endDate = (r.endd == null) ? null : (r.endd.Value.ToString("yyyy-MM-ddThh:mm:ss"))

                                                          }).ToList();

                            activities obj_act = new activities();
                            obj.totalUrls = totalUrls;
                            obj.users = users;
                            obj.visits = visits;
                            obj.campaigns = campaigns;
                            obj.recentCampaigns = objr;
                            obj_act.today = today;
                            obj_act.last7days = last7days;
                            obj_act.month = month;
                            obj.activities = obj_act;
                        
                        return Json(obj, JsonRequestBehavior.AllowGet);
                        //}
                        //else
                        //{
                        //    return Json(obj);
                        //}
                    }
                    else if (Session["id"] != null && rid != null && rid != "")
                    {
                        //if (cid != "" && cid != null)
                        //{
                        //int c_id = (int)Session["id"];

                        CampaignSummary objc = new CampaignSummary();
                        string role = Helper.CurrentUserRole;
                        //client obj_client = dc.clients.Where(x => x.PK_ClientID == cid).Select(y => y).SingleOrDefault();
                        riddata objr=new riddata();
                        if(role.ToLower()=="admin")
                         objr = dc.riddatas.Where(x => x.ReferenceNumber == rid).Select(y => y).SingleOrDefault();
                        else
                            objr = dc.riddatas.Where(x => x.ReferenceNumber == rid && x.FK_ClientId == cid).Select(y => y).SingleOrDefault();


                        if (objr != null)
                        {
                            string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;

                            // create and open a connection object
                            lSQLConn = new MySqlConnection(connStr);
                            MySqlDataReader myReader;
                            lSQLConn.Open();
                            lSQLCmd.CommandType = CommandType.StoredProcedure;
                            lSQLCmd.CommandTimeout = 600;
                            lSQLCmd.CommandText = "spGetCampaignSummary";
                            lSQLCmd.Parameters.Add(new MySqlParameter("@rid", objr.PK_Rid));
                            lSQLCmd.Connection = lSQLConn;
                            myReader = lSQLCmd.ExecuteReader();


                            totalUrls totalUrls = ((IObjectContextAdapter)dc)
                              .ObjectContext
                              .Translate<totalUrls>(myReader, "shorturldatas", MergeOption.AppendOnly).SingleOrDefault();

                            // Move to locations result 
                            myReader.NextResult();
                            users users = ((IObjectContextAdapter)dc)
                           .ObjectContext
                           .Translate<users>(myReader, "shorturldatas", MergeOption.AppendOnly).SingleOrDefault();

                            // Move to locations result 
                            myReader.NextResult();
                            visits visits = ((IObjectContextAdapter)dc)
                           .ObjectContext
                           .Translate<visits>(myReader, "shorturldatas", MergeOption.AppendOnly).SingleOrDefault();

                            // // Move to locations result 
                            // myReader.NextResult();
                            // lastactivity lastactivity = ((IObjectContextAdapter)dc)
                            //.ObjectContext
                            //.Translate<lastactivity>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).SingleOrDefault();

                            // Move to locations result 
                            myReader.NextResult();
                            today today = ((IObjectContextAdapter)dc)
                           .ObjectContext
                           .Translate<today>(myReader, "shorturldatas", MergeOption.AppendOnly).SingleOrDefault();

                            // Move to locations result 
                            myReader.NextResult();
                            last7days last7days = ((IObjectContextAdapter)dc)
                           .ObjectContext
                           .Translate<last7days>(myReader, "shorturldatas", MergeOption.AppendOnly).SingleOrDefault();

                            // Move to locations result 
                            myReader.NextResult();
                            month month = ((IObjectContextAdapter)dc)
                           .ObjectContext
                           .Translate<month>(myReader, "shorturldatas", MergeOption.AppendOnly).SingleOrDefault();
                            activities obj_act = new activities();
                            objc.totalUrls = totalUrls;
                            objc.users = users;
                            objc.visits = visits;
                            //objc.lastactivity = lastactivity;
                            obj_act.today = today;
                            obj_act.last7days = last7days;
                            obj_act.month = month;
                            objc.activities = obj_act;

                        }
                        return Json(objc, JsonRequestBehavior.AllowGet);

                    }
                    //else
                    //{
                    //    Error obj_err = new Error();
                    //    Errormessage errmesobj = new Errormessage();
                    //    errmesobj.message = "unauthorized user.";
                    //    obj_err.error = errmesobj;

                    //    return Json(obj_err, JsonRequestBehavior.AllowGet);
                    //}
                }
                {
                    Error obj_err = new Error();
                    Errormessage errmesobj = new Errormessage();
                    errmesobj.message = "Session Expired.";
                    obj_err.error = errmesobj;
                    return Json(obj_err, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
                Error obj_err = new Error();
                Errormessage errmesobj = new Errormessage();
                errmesobj.message = "Exception Occured";
                obj_err.error = errmesobj;

                return Json(obj_err, JsonRequestBehavior.AllowGet);
            }
        }



        public JsonResult GETAllCampaigns()
        {
            List<CampaignsList> objc = new List<CampaignsList>();
            try
            {
                int c_id;
                if (Session["id"] != null&&Helper.CurrentUserRole.ToLower()=="client")
                {
                    //if (cid != "" && cid != null)
                    //{
                    //c_id = Convert.ToInt32(cid);
                    c_id = (int)Session["id"];
                    client obj_client = dc.clients.Where(x => x.PK_ClientID == c_id).Select(y => y).SingleOrDefault();
                    if (obj_client != null)
                    {
                        //objc = (from r in dc.riddatas
                        //        where r.FK_ClientID == c_id
                        //        select new CampaignsList()
                        //        {
                        //            id = r.PK_Rid,
                        //            rid = r.ReferenceNumber,
                        //            createdOn = r.CreatedDate,
                        //            endDate = r.EndDate

                        //        }).OrderByDescending(x=>x.createdOn).ToList();
                        List<CampaignsList1> objc1 = (from r in dc.riddatas
                                                      where r.FK_ClientId == c_id
                                                      select new CampaignsList1()
                                                      {
                                                          id = r.PK_Rid,
                                                          rid = r.ReferenceNumber,
                                                          createdOn = r.CreatedDate,
                                                          CampaignName = r.CampaignName,
                                                          //createdOn=dateformatt(r.CreatedDate),
                                                          endDate = r.EndDate

                                                      }).OrderByDescending(x => x.createdOn).ToList();
                        objc = (from r in objc1
                                select new CampaignsList()
                                {
                                    id = r.id,
                                    rid = r.rid,
                                    createdOn = r.createdOn.Value.ToString("yyyy-MM-ddThh:mm:ss"),
                                    CampaignName = r.CampaignName,
                                    //createdOn=dateformatt(r.CreatedDate),
                                    endDate = (r.endDate == null) ? null : (r.endDate.Value.ToString("yyyy-MM-ddThh:mm:ss"))

                                }).ToList();


                        //            objc = dc.riddatas
                        //                 .AsEnumerable()     // select all users from the database and bring them back to the client
                        //                 .Select((user, index) => new CampaignSummary()   // project in the index
                        //                 {
                        //                     id=index,
                        //                    cid= user.FK_ClientID,
                        //                    rid= user.ReferenceNumber,
                        //                    createdOn= user.CreatedDate,
                        //                    endDate=user.EndDate
                        //                 })
                        //.Where(user => user.cid == c_id).ToList(); 
                    }

                    return Json(objc, JsonRequestBehavior.AllowGet);
                    //}
                    //else
                    //    return Json(objc);
                }
                else if(Session["id"]!=null && Helper.CurrentUserRole.ToLower()=="admin")
                {
                    List<CampaignsList1> objc1 = (from r in dc.riddatas
                                                  select new CampaignsList1()
                                                  {
                                                      id = r.PK_Rid,
                                                      rid = r.ReferenceNumber,
                                                      createdOn = r.CreatedDate,
                                                      CampaignName = r.CampaignName,
                                                      //createdOn=dateformatt(r.CreatedDate),
                                                      endDate = r.EndDate

                                                  }).OrderByDescending(x => x.createdOn).ToList();
                    objc = (from r in objc1
                            select new CampaignsList()
                            {
                                id = r.id,
                                rid = r.rid,
                                createdOn = r.createdOn.Value.ToString("yyyy-MM-ddThh:mm:ss"),
                                CampaignName = r.CampaignName,
                                //createdOn=dateformatt(r.CreatedDate),
                                endDate = (r.endDate == null) ? null : (r.endDate.Value.ToString("yyyy-MM-ddThh:mm:ss"))

                            }).ToList();
                    return Json(objc, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    Error obj_err = new Error();
                    Errormessage errmesobj = new Errormessage();
                    errmesobj.message = "Session Expired.";
                    obj_err.error = errmesobj;

                    return Json(obj_err, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
                Error obj_err = new Error();
                Errormessage errmesobj = new Errormessage();
                errmesobj.message = "Exception Occured.";
                obj_err.error = errmesobj;

                return Json(obj_err, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GETGeoLocations(string rid, DateTime DateFrom, DateTime DateTo)
        {
            try
            {
                riddata objr=dc.riddatas.Where(r=>r.ReferenceNumber==rid).SingleOrDefault();
                List<GeoLocationsData> objg = new List<GeoLocationsData>();
                List<GeoLocationsData1> objg1=new List<GeoLocationsData1>();
                if ( objr != null && Session["id"] != null)
                {
                    
                    objg1 = (from s in dc.shorturldatas
                                                   join u in dc.uiddatas on s.FK_Uid equals u.PK_Uid
                                                   where s.FK_RID == objr.PK_Rid && u.FK_RID==objr.PK_Rid 
                                                   select new GeoLocationsData1()
                                                   {
                                                      Latitude=s.Latitude,
                                                      Longitude=s.Longitude,
                                                       MobileNumber=u.MobileNumber,
                                                       createdOn1 = s.CreatedDate,
                                                       ShortUrl=s.Req_url
                                                   }).ToList();
                    if(objg1!=null)
                    {
                        objg = (from g in objg1
                                where g.createdOn1.Value.Date>=DateFrom.Date && g.createdOn1.Value.Date<=DateTo.Date
                                select new GeoLocationsData()
                                {
                                    Latitude = g.Latitude,
                                    Longitude = g.Longitude,
                                    MobileNumber = g.MobileNumber,
                                    createdOn = g.createdOn1.Value.ToString("yyyy-MM-ddTHH:mm:ss"),
                                    ShortUrl = g.ShortUrl

                                }).ToList();
                    }

                }
                return Json(objg, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
                Error obj_err = new Error();
                Errormessage errmesobj = new Errormessage();
                errmesobj.message = "Exception Occured.";
                obj_err.error = errmesobj;

                return Json(obj_err, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GETAllCounts(string rid, string DateFrom, string DateTo)
        {
            try
            {
                if (rid != "" && rid != null && Session["id"] != null)
                {
                    CountsData countobj = new CountsData();
                    // long decodedvalue = new ConvertionBO().BaseToLong(Fk_Uniqueid);
                    //int Uniqueid_SHORTURLDATA = Convert.ToInt32(decodedvalue);
                    //int? rid = (from u in dc.UIDandriddatas
                    //            where u.PK_UniqueId == Uniqueid_SHORTURLDATA
                    //            select u.UIDorRID).SingleOrDefault();
                    riddata obj = dc.riddatas.Where(x => x.ReferenceNumber == rid).Select(y => y).SingleOrDefault();
                    if (obj != null)
                    {
                        string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;

                        // create and open a connection object
                        lSQLConn = new MySqlConnection(connStr);
                        MySqlDataReader myReader;
                        lSQLConn.Open();
                        lSQLCmd.CommandType = CommandType.StoredProcedure;
                        lSQLCmd.CommandText = "spGetALLCOUNTS1";
                        lSQLCmd.CommandTimeout = 600;
                        //lSQLCmd.Parameters.Add(new MySqlParameter("@Fk_Uniqueid", Uniqueid_SHORTURLDATA));
                        lSQLCmd.Parameters.Add(new MySqlParameter("@rid", obj.PK_Rid));
                        lSQLCmd.Parameters.Add(new MySqlParameter("@DateFrom", DateFrom));
                        lSQLCmd.Parameters.Add(new MySqlParameter("@DateTo", DateTo));
                        lSQLCmd.Connection = lSQLConn;
                        myReader = lSQLCmd.ExecuteReader();


                        List<DayWiseData1> activity = ((IObjectContextAdapter)dc)
                          .ObjectContext
                          .Translate<DayWiseData1>(myReader, "shorturldatas", MergeOption.AppendOnly).ToList();


                        // Move to locations result 
                        myReader.NextResult();
                        List<CountryWiseData> locations = ((IObjectContextAdapter)dc)
                       .ObjectContext
                       .Translate<CountryWiseData>(myReader, "shorturldatas", MergeOption.AppendOnly).ToList();

                        // Move to devices result 
                        myReader.NextResult();
                        List<DeviceWiseData> devices = ((IObjectContextAdapter)dc)
                      .ObjectContext
                      .Translate<DeviceWiseData>(myReader, "shorturldatas", MergeOption.AppendOnly).ToList();

                        // Move to platforms result 
                        myReader.NextResult();
                        List<BrowserWiseData> platforms = ((IObjectContextAdapter)dc)
                      .ObjectContext
                      .Translate<BrowserWiseData>(myReader, "shorturldatas", MergeOption.AppendOnly).ToList();

                        List<DayWiseData> objr = new List<DayWiseData>();
                        if (activity != null)
                        {
                            
                            objr = (from r in activity
                                    select new DayWiseData()
                                    {
                                        RequestedDate =(r.RequestedDate==null) ? null: r.RequestedDate.Value.ToString("yyyy-MM-ddTHH:mm:ss"),
                                        RequestCount = r.RequestCount
                                        //r.crd.Value.ToString("yyyy-MM-ddThh:mm:ss"),

                                    }).ToList();
                        }
                        //List<DayWiseData> objr = (from r in activity
                        //                              select new DayWiseData()
                        //                              {

                        //                                point=  r.RequestedDate.Value.ToString("yyyy-MM-ddThh:mm:ss")+','+r.RequestCount
                        //                                  //r.crd.Value.ToString("yyyy-MM-ddThh:mm:ss"),

                        //                              }).ToList();
                        countobj.activity = objr;
                        countobj.locations = locations;
                        countobj.devices = devices;
                        countobj.platforms = platforms;

                    }
                    return Json(countobj, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    Error obj_err = new Error();
                    Errormessage errmesobj = new Errormessage();
                    if (Session["id"] == null)
                        errmesobj.message = "Session Expired";
                    else
                        errmesobj.message = "please pass reference number";

                    obj_err.error = errmesobj;

                    return Json(obj_err, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
                Error obj_err = new Error();
                Errormessage errmesobj = new Errormessage();
                errmesobj.message = "Exception Occured";
                obj_err.error = errmesobj;

                return Json(obj_err, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GETSummary(int cid, string rid)
        {

            DashBoardStats obj = new DashBoardStats();

            try
            {
                if (Session["id"] != null)
                {
                    if (Session["id"] != null && rid == null)
                    {
                        //int c_id = (int)Session["id"];

                        //if (cid != "" && cid != null)
                        //{
                        string role = Helper.CurrentUserRole;
                        MySqlDataReader myReader;
                        if (role.ToLower() != "admin")
                        {
                            client obj_client = dc.clients.Where(x => x.PK_ClientID == cid).Select(y => y).SingleOrDefault();
                            if (obj_client != null)
                            {
                                string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;

                                // create and open a connection object
                                lSQLConn = new MySqlConnection(connStr);
                                lSQLConn.Open();
                                lSQLCmd.CommandType = CommandType.StoredProcedure;
                                lSQLCmd.CommandTimeout = 600;
                                lSQLCmd.CommandText = "spGetDashBoardStats";
                                lSQLCmd.Parameters.Add(new MySqlParameter("@FkclientId", cid));
                                lSQLCmd.Connection = lSQLConn;
                                //myReader = lSQLCmd.ExecuteReader();
                            }
                        }
                        else
                        {
                            string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;

                            // create and open a connection object
                            lSQLConn = new MySqlConnection(connStr);
                            lSQLConn.Open();
                            lSQLCmd.CommandType = CommandType.StoredProcedure;
                            lSQLCmd.CommandTimeout = 600;
                            lSQLCmd.CommandText = "spGetDashBoardStats";
                            lSQLCmd.Parameters.Add(new MySqlParameter("@FkClientId", "0"));
                            lSQLCmd.Connection = lSQLConn;
                        }
                        myReader = lSQLCmd.ExecuteReader();
                        
                        uniqueUsersToday1 uniqueUsersToday = ((IObjectContextAdapter)dc)
                              .ObjectContext
                              .Translate<uniqueUsersToday1>(myReader, "shorturldatas", MergeOption.AppendOnly).SingleOrDefault();
                        myReader.NextResult();
                        uniqueVisits1 uniqueVisits = ((IObjectContextAdapter)dc)
                             .ObjectContext
                             .Translate<uniqueVisits1>(myReader, "shorturldatas", MergeOption.AppendOnly).SingleOrDefault();
                        myReader.NextResult();
                        List<uniqueVisitsToday1> uniqueVisitsToday = ((IObjectContextAdapter)dc)
                             .ObjectContext
                             .Translate<uniqueVisitsToday1>(myReader, "shorturldatas", MergeOption.AppendOnly).ToList();
                        myReader.NextResult();
                        //List<uniqueVisitsLast7day1> uniqueVisitsLast7day = ((IObjectContextAdapter)dc)
                        //    .ObjectContext
                        //    .Translate<uniqueVisitsLast7day1>(myReader, "shorturldatas", MergeOption.AppendOnly).ToList();
                        //myReader.NextResult();
                        //List<visitsLast7days1> visitsLast7days = ((IObjectContextAdapter)dc)
                        //    .ObjectContext
                        //    .Translate<visitsLast7days1>(myReader, "shorturldatas", MergeOption.AppendOnly).ToList();
                        //myReader.NextResult();
                        List<recentCampaigns1> recentCampaigns = ((IObjectContextAdapter)dc)
                       .ObjectContext
                       .Translate<recentCampaigns1>(myReader, "shorturldatas", MergeOption.AppendOnly).ToList();
                        myReader.NextResult();
                        today1 today1 = ((IObjectContextAdapter)dc)
                       .ObjectContext
                       .Translate<today1>(myReader, "shorturldatas", MergeOption.AppendOnly).SingleOrDefault();
                       
                        stat_counts  st_obj_res=new stat_counts();
                        if(Helper.CurrentUserRole=="admin")
                        st_obj_res=dc.stat_counts.Where(x => x.FK_ClientID == cid && x.FK_Rid==0).SingleOrDefault();
                        else
                        {
                        List<stat_counts> st_obj = dc.stat_counts.Where(x => x.FK_ClientID == cid && x.FK_Rid!=0).ToList();
                        st_obj_res = (from s in st_obj
                                  group s by s.FK_ClientID into r
                                  select new stat_counts()
                                  {
TotalUsers=r.Sum(x=>x.TotalUsers),
UniqueUsers=r.Sum(x=>x.UniqueUsers),
//UniqueUsersToday=r.Sum(x=>x.UniqueUsersToday),
UsersToday=r.Sum(x=>x.UsersToday),
UniqueUsersYesterday=r.Sum(x=>x.UniqueUsersYesterday),
UsersYesterday=r.Sum(x=>x.UsersYesterday),
UniqueUsersLast7days=r.Sum(x=>x.UniqueUsersLast7days),
UsersLast7days=r.Sum(x=>x.UsersLast7days),

TotalVisits=r.Sum(x=>x.TotalVisits),
VisitsToday=r.Sum(x=>x.VisitsToday),
VisitsYesterday=r.Sum(x=>x.VisitsYesterday),
UniqueVisitsYesterday=r.Sum(x=>x.UniqueVisitsYesterday),
UniqueVisitsLast7day = r.Sum(x => x.UniqueVisitsLast7day) + r.Sum(x => x.UniqueVisitsYesterday) + uniqueVisitsToday.Sum(x => x.uniqueVisitsToday),
VisitsLast7days=r.Sum(x=>x.VisitsLast7days)+r.Sum(x=>x.VisitsYesterday)+r.Sum(x=>x.VisitsToday),
//UniqueVisitsLast7day=r.Sum(x=>x.UniqueVisitsLast7day),
//VisitsLast7days=r.Sum(x=>x.VisitsLast7days),
TotalCamapigns=r.Sum(x=>x.TotalCamapigns),
CampaignsLast7days=r.Sum(x=>x.CampaignsLast7days),
CampaignsMonth=r.Sum(x=>x.CampaignsMonth),
UrlTotal_Today=r.Sum(x=>x.UrlTotal_Today),
UrlPercent_Today=r.Sum(x=>x.UrlPercent_Today),
VisitsTotal_Today=r.Sum(x=>x.VisitsTotal_Today),
VisitsPercent_Today=r.Sum(x=>x.VisitsPercent_Today),

UrlTotal_Week = r.Sum(x => x.UrlTotal_Week),
UrlPercent_Week = r.Sum(x => x.UrlPercent_Week),
VisitsTotal_Week = r.Sum(x => x.VisitsTotal_Week),
VisitsPercent_Week = r.Sum(x => x.VisitsPercent_Week),
RevisitsTotal_Week=r.Sum(x=>x.RevisitsTotal_Week),
RevisitsPercent_Week=r.Sum(x=>x.RevisitsPercent_Week),
NoVisitsTotal_Week=r.Sum(x=>x.NoVisitsTotal_Week),
NoVisitsPercent_Week=r.Sum(x=>x.NoVisitsPercent_Week),

UrlTotal_Month = r.Sum(x => x.UrlTotal_Month),
UrlTotalPercent_Month = r.Sum(x => x.UrlTotalPercent_Month),
VisitsTotal_Month = r.Sum(x => x.VisitsTotal_Month),
VisitsPercent_Month = r.Sum(x => x.VisitsPercent_Month),
RevisitsTotal_Month = r.Sum(x => x.RevisitsTotal_Month),
RevisitsPercent_Month = r.Sum(x => x.RevisitsPercent_Month),
NoVisitsTotal_Month = r.Sum(x => x.NoVisitsTotal_Month),
NoVisitsPercent_Month = r.Sum(x => x.NoVisitsPercent_Month),
                                  }).SingleOrDefault();
                   }
                      totalUrls_stat totalUrls = new totalUrls_stat();
                      totalUrls.count = st_obj_res.TotalUsers;
                      users_stat users = new users_stat();
                      users.total = st_obj_res.TotalUsers;
                      users.uniqueUsers = st_obj_res.UniqueUsers;
                      users.uniqueUsersToday = uniqueUsersToday.uniqueUsersToday;
                      users.usersToday = st_obj_res.UsersToday;
                      users.uniqueUsersYesterday = st_obj_res.UniqueUsersYesterday;
                      users.usersYesterday = st_obj_res.UsersYesterday;
                      users.uniqueUsersLast7days = st_obj_res.UniqueUsersLast7days;
                      users.usersLast7days = st_obj_res.UsersLast7days;
                      visits_stat visits = new visits_stat();
                      visits.total = st_obj_res.TotalVisits;
                      visits.uniqueVisits = uniqueVisits.uniqueVisits;
                      visits.uniqueVisitsToday = uniqueVisitsToday.Sum(x => x.uniqueVisitsToday);
                      visits.visitsToday = st_obj_res.VisitsToday;
                      visits.uniqueVisitsYesterday = st_obj_res.UniqueVisitsYesterday;
                      visits.visitsYesterday = st_obj_res.VisitsYesterday;
                      //visits.uniqueVisitsLast7days =(st_obj_res.FK_Rid!=0)?st_obj_res.UniqueVisitsLast7day:(uniqueVisitsToday.Sum(x => x.uniqueVisitsToday)+st_obj_res.UniqueVisitsYesterday+st_obj_res.UniqueVisitsLast7day);
                      //visits.visitsLast7days = (st_obj_res.FK_Rid != 0) ? st_obj_res.VisitsLast7days : (st_obj_res.VisitsToday + st_obj_res.VisitsYesterday + st_obj_res.VisitsLast7days);
                      visits.uniqueVisitsLast7days = st_obj_res.UniqueVisitsLast7day;
                      visits.visitsLast7days = st_obj_res.VisitsLast7days;
                      campaigns_stat campaigns = new campaigns_stat();
                      campaigns.total = st_obj_res.TotalCamapigns;
                      campaigns.campaignsLast7days = st_obj_res.CampaignsLast7days;
                      campaigns.campaignsMonth = st_obj_res.CampaignsMonth;
                      today2 today2 = new today2();
                      today2.urlTotal = st_obj_res.UrlTotal_Today;
                      today2.urlPercent = st_obj_res.UrlPercent_Today;
                      today2.visitsTotal = st_obj_res.VisitsTotal_Today;
                      today2.visitsPercent = st_obj_res.VisitsPercent_Today;
                      today_stat today = new today_stat();
                      today.urlTotal = today2.urlTotal;
                      today.urlPercent = today2.urlPercent;
                      today.visitsTotal = today2.visitsTotal;
                      today.visitsPercent = today2.visitsPercent;
                      today.revisitsTotal = today1.revisitsTotal;
                      today.revisitsPercent = today1.revisitsPercent;
                      today.noVisitsTotal = today1.noVisitsTotal;
                      today.noVisitsPercent = today1.noVisitsPercent;
                      last7days_stat last7days = new last7days_stat();
                      last7days.urlTotal = st_obj_res.UrlTotal_Week;
                      last7days.urlPercent = st_obj_res.UrlPercent_Week;
                      last7days.visitsTotal = st_obj_res.VisitsTotal_Week;
                      last7days.visitsPercent = st_obj_res.VisitsPercent_Week;
                      last7days.revisitsTotal = st_obj_res.RevisitsTotal_Week;
                      last7days.revisitsPercent = st_obj_res.RevisitsPercent_Week;
                      last7days.noVisitsTotal = st_obj_res.NoVisitsTotal_Week;
                      last7days.noVisitsPercent = st_obj_res.NoVisitsPercent_Week;
                      month_stat month = new month_stat();
                      month.urlTotal = st_obj_res.UrlTotal_Month;
                      month.urlPercent = st_obj_res.UrlTotalPercent_Month;
                      month.visitsTotal = st_obj_res.VisitsTotal_Month;
                      month.visitsPercent = st_obj_res.VisitsPercent_Month;
                      month.revisitsTotal = st_obj_res.RevisitsTotal_Month;
                      month.revisitsPercent = st_obj_res.RevisitsPercent_Month;
                      month.noVisitsTotal = st_obj_res.NoVisitsTotal_Month;
                      month.noVisitsPercent = st_obj_res.NoVisitsPercent_Month;


                      List<recentCampaigns_stat> objr = (from r in recentCampaigns
                                                         select new recentCampaigns_stat()
                                                      {
                                                          id = r.id,
                                                          rid = r.rid,
                                                          visits = r.visits,
                                                          users = r.users,
                                                          status = r.status,
                                                          //crd = r.createdOn.Value.ToString("MM/dd/yyyyThh:mm:ss")
                                                          createdOn = r.crd.Value.ToString("yyyy-MM-ddThh:mm:ss"),
                                                          endDate = (r.endd == null) ? null : (r.endd.Value.ToString("yyyy-MM-ddThh:mm:ss"))

                                                      }).ToList();

                        activities_stat obj_act = new activities_stat();
                        obj_act.today = today;
                        obj_act.last7days = last7days;
                        obj_act.month = month;

                        obj.totalUrls = totalUrls;
                        obj.users = users;
                        obj.visits = visits;
                        obj.campaigns = campaigns;
                        obj.recentCampaigns = objr;
                        obj.activities = obj_act;

                        return Json(obj, JsonRequestBehavior.AllowGet);
                        //}
                        //else
                        //{
                        //    return Json(obj);
                        //}
                    }
                    else if (Session["id"] != null && rid != null && rid != "")
                    {
                        //if (cid != "" && cid != null)
                        //{
                        //int c_id = (int)Session["id"];

                        //CampaignSummary objc = new CampaignSummary();
                        string role = Helper.CurrentUserRole;
                        //client obj_client = dc.clients.Where(x => x.PK_ClientID == cid).Select(y => y).SingleOrDefault();
                        riddata objr = new riddata();
                        if (role.ToLower() == "admin")
                            objr = dc.riddatas.Where(x => x.ReferenceNumber == rid).Select(y => y).SingleOrDefault();
                        else
                            objr = dc.riddatas.Where(x => x.ReferenceNumber == rid && x.FK_ClientId == cid).Select(y => y).SingleOrDefault();


                        if (objr != null)
                        {
                            string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;

                            // create and open a connection object
                            lSQLConn = new MySqlConnection(connStr);
                            MySqlDataReader myReader;
                            lSQLConn.Open();
                            lSQLCmd.CommandType = CommandType.StoredProcedure;
                            lSQLCmd.CommandTimeout = 600;
                            lSQLCmd.CommandText = "spGetCampaignStats";
                            lSQLCmd.Parameters.Add(new MySqlParameter("@rid", objr.PK_Rid));
                            lSQLCmd.Connection = lSQLConn;
                            myReader = lSQLCmd.ExecuteReader();


                            uniqueUsersToday1 uniqueUsersToday = ((IObjectContextAdapter)dc)
                              .ObjectContext
                              .Translate<uniqueUsersToday1>(myReader, "shorturldatas", MergeOption.AppendOnly).SingleOrDefault();
                            myReader.NextResult();
                            uniqueVisits1 uniqueVisits = ((IObjectContextAdapter)dc)
                                 .ObjectContext
                                 .Translate<uniqueVisits1>(myReader, "shorturldatas", MergeOption.AppendOnly).SingleOrDefault();
                            myReader.NextResult();
                            List<uniqueVisitsToday1> uniqueVisitsToday = ((IObjectContextAdapter)dc)
                                 .ObjectContext
                                 .Translate<uniqueVisitsToday1>(myReader, "shorturldatas", MergeOption.AppendOnly).ToList();
                            myReader.NextResult();
                           // List<recentCampaigns1> recentCampaigns = ((IObjectContextAdapter)dc)
                           //.ObjectContext
                           //.Translate<recentCampaigns1>(myReader, "shorturldatas", MergeOption.AppendOnly).ToList();
                           // myReader.NextResult();
                            //List<uniqueVisitsLast7day1> uniqueVisitsLast7day = ((IObjectContextAdapter)dc)
                            //.ObjectContext
                            //.Translate<uniqueVisitsLast7day1>(myReader, "shorturldatas", MergeOption.AppendOnly).ToList();
                            //myReader.NextResult();
                            //List<visitsLast7days1> visitsLast7days = ((IObjectContextAdapter)dc)
                            //    .ObjectContext
                            //    .Translate<visitsLast7days1>(myReader, "shorturldatas", MergeOption.AppendOnly).ToList();
                            //myReader.NextResult();
                            today1 today1 = ((IObjectContextAdapter)dc)
                           .ObjectContext
                           .Translate<today1>(myReader, "shorturldatas", MergeOption.AppendOnly).SingleOrDefault();
                            stat_counts  st_obj_res=new stat_counts();
                            if (Helper.CurrentUserRole == "admin" && rid==null)
                                st_obj_res = dc.stat_counts.Where(x => x.FK_ClientID == cid && x.FK_Rid == 0).SingleOrDefault();
                            else
                            {
                                
                                st_obj_res = dc.stat_counts.Where(x => x.FK_ClientID == cid && x.FK_Rid == objr.PK_Rid).SingleOrDefault();
                                //List<stat_counts> st_obj = dc.stat_counts.Where(x => x.FK_ClientID == cid && x.FK_Rid 1=0).ToList();

                                //st_obj_res = (from s in st_obj
                                //              group s by s.FK_ClientID into r
                                //              select new stat_counts()
                                //              {
                                //                  TotalUsers = r.Sum(x => x.TotalUsers),
                                //                  UniqueUsers = r.Sum(x => x.UniqueUsers),
                                //                  //UniqueUsersToday=r.Sum(x=>x.UniqueUsersToday),
                                //                  UsersToday = r.Sum(x => x.UsersToday),
                                //                  UniqueUsersYesterday = r.Sum(x => x.UniqueUsersYesterday),
                                //                  UsersYesterday = r.Sum(x => x.UsersYesterday),
                                //                  UniqueUsersLast7days = r.Sum(x => x.UniqueUsersLast7days) + r.Sum(x => x.UniqueUsersYesterday) + uniqueUsersToday.uniqueUsersToday,
                                //                  UsersLast7days = r.Sum(x => x.UsersLast7days) + r.Sum(x=>x.UsersYesterday) + r.Sum(x=>x.UsersToday),

                                //                  TotalVisits = r.Sum(x => x.TotalVisits),
                                //                  VisitsToday = r.Sum(x => x.VisitsToday),
                                //                  VisitsYesterday = r.Sum(x => x.VisitsYesterday),
                                //                  UniqueVisitsYesterday = r.Sum(x => x.UniqueVisitsYesterday),
                                //                  UniqueVisitsLast7day = r.Sum(x => x.UniqueVisitsLast7day) + r.Sum(x => x.UniqueVisitsYesterday) + uniqueVisitsToday.Sum(x => x.uniqueVisitsToday),
                                //                  VisitsLast7days = r.Sum(x => x.VisitsLast7days) + r.Sum(x => x.VisitsYesterday) + r.Sum(x => x.VisitsToday),
                                //                  //UniqueVisitsLast7day = r.Sum(x => x.UniqueVisitsLast7day),
                                //                  //VisitsLast7days = r.Sum(x => x.VisitsLast7days),
                                //                  TotalCamapigns = r.Sum(x => x.TotalCamapigns),
                                //                  CampaignsLast7days = r.Sum(x => x.CampaignsLast7days),
                                //                  CampaignsMonth = r.Sum(x => x.CampaignsMonth),
                                //                  UrlTotal_Today = r.Sum(x => x.UrlTotal_Today),
                                //                  UrlPercent_Today = r.Sum(x => x.UrlPercent_Today),
                                //                  VisitsTotal_Today = r.Sum(x => x.VisitsTotal_Today),
                                //                  VisitsPercent_Today = r.Sum(x => x.VisitsPercent_Today),

                                //                  UrlTotal_Week = r.Sum(x => x.UrlTotal_Week),
                                //                  UrlPercent_Week = r.Sum(x => x.UrlPercent_Week),
                                //                  VisitsTotal_Week = r.Sum(x => x.VisitsTotal_Week),
                                //                  VisitsPercent_Week = r.Sum(x => x.VisitsPercent_Week),
                                //                  RevisitsTotal_Week = r.Sum(x => x.RevisitsTotal_Week),
                                //                  RevisitsPercent_Week = r.Sum(x => x.RevisitsPercent_Week),
                                //                  NoVisitsTotal_Week = r.Sum(x => x.NoVisitsTotal_Week),
                                //                  NoVisitsPercent_Week = r.Sum(x => x.NoVisitsPercent_Week),

                                //                  UrlTotal_Month = r.Sum(x => x.UrlTotal_Month),
                                //                  UrlTotalPercent_Month = r.Sum(x => x.UrlTotalPercent_Month),
                                //                  VisitsTotal_Month = r.Sum(x => x.VisitsTotal_Month),
                                //                  VisitsPercent_Month = r.Sum(x => x.VisitsPercent_Month),
                                //                  RevisitsTotal_Month = r.Sum(x => x.RevisitsTotal_Month),
                                //                  RevisitsPercent_Month = r.Sum(x => x.RevisitsPercent_Month),
                                //                  NoVisitsTotal_Month = r.Sum(x => x.NoVisitsTotal_Month),
                                //                  NoVisitsPercent_Month = r.Sum(x => x.NoVisitsPercent_Month),
                                //              }).SingleOrDefault();
                            }
                            totalUrls_stat totalUrls = new totalUrls_stat();
                            totalUrls.count = st_obj_res.TotalUsers;
                            users_stat users = new users_stat();
                            users.total = st_obj_res.TotalUsers;
                            users.uniqueUsers = st_obj_res.UniqueUsers;
                            users.uniqueUsersToday = uniqueUsersToday.uniqueUsersToday;
                            users.usersToday = st_obj_res.UsersToday;
                            users.uniqueUsersYesterday = st_obj_res.UniqueUsersYesterday;
                            users.usersYesterday = st_obj_res.UsersYesterday;
                            users.uniqueUsersLast7days = st_obj_res.UniqueUsersLast7days;
                            users.usersLast7days = st_obj_res.UsersLast7days;
                            visits_stat visits = new visits_stat();
                            visits.total = st_obj_res.TotalVisits;
                            visits.uniqueVisits = uniqueVisits.uniqueVisits;
                            visits.uniqueVisitsToday = uniqueVisitsToday.Sum(x => x.uniqueVisitsToday);
                            visits.visitsToday = st_obj_res.VisitsToday;
                            visits.uniqueVisitsYesterday = st_obj_res.UniqueVisitsYesterday;
                            visits.visitsYesterday = st_obj_res.VisitsYesterday;
                            visits.uniqueVisitsLast7days = (st_obj_res.FK_Rid != 0) ? st_obj_res.UniqueVisitsLast7day : (uniqueVisitsToday.Sum(x => x.uniqueVisitsToday) + st_obj_res.UniqueVisitsYesterday + st_obj_res.UniqueVisitsLast7day);
                            visits.visitsLast7days = (st_obj_res.FK_Rid != 0) ? st_obj_res.VisitsLast7days : (st_obj_res.VisitsToday + st_obj_res.VisitsYesterday + st_obj_res.VisitsLast7days);
                            //visits.uniqueVisitsLast7days = st_obj_res.UniqueVisitsLast7day;
                            //visits.visitsLast7days = st_obj_res.VisitsLast7days;
                            campaigns_stat campaigns = new campaigns_stat();
                            campaigns.total = st_obj_res.TotalCamapigns;
                            campaigns.campaignsLast7days = st_obj_res.CampaignsLast7days;
                            campaigns.campaignsMonth = st_obj_res.CampaignsMonth;
                            today2 today2 = new today2();
                            today2.urlTotal = st_obj_res.UrlTotal_Today;
                            today2.urlPercent = st_obj_res.UrlPercent_Today;
                            today2.visitsTotal = st_obj_res.VisitsTotal_Today;
                            today2.visitsPercent = st_obj_res.VisitsPercent_Today;
                            today_stat today = new today_stat();
                            today.urlTotal = today2.urlTotal;
                            today.urlPercent = today2.urlPercent;
                            today.visitsTotal = today2.visitsTotal;
                            today.visitsPercent = today2.visitsPercent;
                            today.revisitsTotal = today1.revisitsTotal;
                            today.revisitsPercent = today1.revisitsPercent;
                            today.noVisitsTotal = today1.noVisitsTotal;
                            today.noVisitsPercent = today1.noVisitsPercent;
                            last7days_stat last7days = new last7days_stat();
                            last7days.urlTotal = st_obj_res.UrlTotal_Week;
                            last7days.urlPercent = st_obj_res.UrlPercent_Week;
                            last7days.visitsTotal = st_obj_res.VisitsTotal_Week;
                            last7days.visitsPercent = st_obj_res.VisitsPercent_Week;
                            last7days.revisitsTotal = st_obj_res.RevisitsTotal_Week;
                            last7days.revisitsPercent = st_obj_res.RevisitsPercent_Week;
                            last7days.noVisitsTotal = st_obj_res.NoVisitsTotal_Week;
                            last7days.noVisitsPercent = st_obj_res.NoVisitsPercent_Week;
                            month_stat month = new month_stat();
                            month.urlTotal = st_obj_res.UrlTotal_Month;
                            month.urlPercent = st_obj_res.UrlTotalPercent_Month;
                            month.visitsTotal = st_obj_res.VisitsTotal_Month;
                            month.visitsPercent = st_obj_res.VisitsPercent_Month;
                            month.revisitsTotal = st_obj_res.RevisitsTotal_Month;
                            month.revisitsPercent = st_obj_res.RevisitsPercent_Month;
                            month.noVisitsTotal = st_obj_res.NoVisitsTotal_Month;
                            month.noVisitsPercent = st_obj_res.NoVisitsPercent_Month;


                            
                            activities_stat obj_act = new activities_stat();
                            obj_act.today = today;
                            obj_act.last7days = last7days;
                            obj_act.month = month; 

                            obj.totalUrls = totalUrls;
                            obj.users = users;
                            obj.visits = visits;
                            obj.campaigns = campaigns;
                            obj.activities = obj_act;

                            

                        }
                        return Json(obj, JsonRequestBehavior.AllowGet);

                    }
                    //else
                    //{
                    //    Error obj_err = new Error();
                    //    Errormessage errmesobj = new Errormessage();
                    //    errmesobj.message = "unauthorized user.";
                    //    obj_err.error = errmesobj;

                    //    return Json(obj_err, JsonRequestBehavior.AllowGet);
                    //}
                }
                {
                    Error obj_err = new Error();
                    Errormessage errmesobj = new Errormessage();
                    errmesobj.message = "Session Expired.";
                    obj_err.error = errmesobj;
                    return Json(obj_err, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
                Error obj_err = new Error();
                Errormessage errmesobj = new Errormessage();
                errmesobj.message = "Exception Occured";
                obj_err.error = errmesobj;

                return Json(obj_err, JsonRequestBehavior.AllowGet);
            }
        }
    }
}