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
using System.Web.Caching;

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
        public JsonResult GETSummary_SP(int cid,string rid)
            //public JsonResult GETSummary(int cid,string rid)
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
        public JsonResult GETDashBoardSummary_TotalUrls(int cid, string rid)
        {

            totalUrls totalUrls1 = new totalUrls();
            stat_counts dc_st = new stat_counts(); List<stat_counts> dc_st1 = new List<stat_counts>();
            string role = Helper.CurrentUserRole;
            if (rid == null)
            {
                if (role.ToLower() == "admin")
                {
                    dc_st = dc.stat_counts.Where(x => x.FK_Rid == 0).Select(y => y).SingleOrDefault();
                    if (dc_st != null)
                    totalUrls1.count = (int)(dc_st.TotalUsers);
                }
                else
                {
                    dc_st1 = dc.stat_counts.Where(x => x.FK_ClientID == cid).Select(y => y).ToList();
                    if (dc_st1 != null)
                    totalUrls1.count = (int)(dc_st1.Select(x => x.TotalUsers).Sum());
                }
            }
            else
            {
                int rid1 = Convert.ToInt32(rid);
                string rid2 = Convert.ToString(rid);
                rid1 = dc.riddatas.Where(x => x.ReferenceNumber == rid2).Select(x => x.PK_Rid).SingleOrDefault();
                dc_st = dc.stat_counts.Where(x => x.FK_Rid == rid1).Select(y => y).SingleOrDefault();
                if (dc_st != null)
                totalUrls1.count = (int)(dc_st.TotalUsers);
            }

            return Json(totalUrls1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GETDashBoardSummary_UsersCount(int cid, string rid)
        {

            users users1 = new users();
            stat_counts dc_st = new stat_counts(); List<stat_counts> dc_st1 = new List<stat_counts>();
            string role = Helper.CurrentUserRole;
            if (rid == null)
            {
                if (role.ToLower() == "admin")
                {
                    dc_st = dc.stat_counts.Where(x => x.FK_Rid == 0).Select(y => y).SingleOrDefault();
                    if (dc_st != null)
                    {
                        users1.total = (int)(dc_st.TotalUsers);
                        users1.uniqueUsers = (int)(dc_st.UniqueUsers);
                        users1.uniqueUsersToday = (int)(dc_st.UniqueUsersToday);
                        users1.usersToday = (int)(dc_st.UsersToday);
                        users1.uniqueUsersYesterday = (int)(dc_st.UniqueUsersYesterday);
                        users1.usersYesterday = (int)(dc_st.UsersYesterday);
                        users1.uniqueUsersLast7days = (int)(dc_st.UniqueUsersLast7days);
                        users1.usersLast7days = (int)(dc_st.UsersLast7days);
                    }
                }
                else
                {
                    dc_st1 = dc.stat_counts.Where(x => x.FK_ClientID == cid).Select(y => y).ToList();
                    if (dc_st1 != null)
                    {
                        users1.total = (int)(dc_st1.Select(x => x.TotalUsers).Sum());
                        users1.uniqueUsers = (int)(dc_st1.Select(x => x.UniqueUsers).Sum());
                        users1.uniqueUsersToday = (int)(dc_st1.Select(x => x.UniqueUsersToday).Sum());
                        users1.usersToday = (int)(dc_st1.Select(x => x.UsersToday).Sum());
                        users1.uniqueUsersYesterday = (int)(dc_st1.Select(x => x.UniqueUsersYesterday).Sum());
                        users1.usersYesterday = (int)(dc_st1.Select(x => x.UsersYesterday).Sum());
                        users1.uniqueUsersLast7days = (int)(dc_st1.Select(x => x.UniqueUsersLast7days).Sum());
                        users1.usersLast7days = (int)(dc_st1.Select(x => x.UsersLast7days).Sum());
                    }
                }
            }
            else
            {
                int rid1 = Convert.ToInt32(rid);
                rid1 = dc.riddatas.Where(x => x.ReferenceNumber == rid).Select(x => x.PK_Rid).SingleOrDefault();
                dc_st = dc.stat_counts.Where(x => x.FK_Rid == rid1).Select(y => y).SingleOrDefault();
                if (dc_st != null)
                {
                    users1.total = (int)(dc_st.TotalUsers);
                    users1.uniqueUsers = (int)(dc_st.UniqueUsers);
                    users1.uniqueUsersToday = (int)(dc_st.UniqueUsersToday);
                    users1.usersToday = (int)(dc_st.UsersToday);
                    users1.uniqueUsersYesterday = (int)(dc_st.UniqueUsersYesterday);
                    users1.usersYesterday = (int)(dc_st.UsersYesterday);
                    users1.uniqueUsersLast7days = (int)(dc_st.UniqueUsersLast7days);
                    users1.usersLast7days = (int)(dc_st.UsersLast7days);
                }
            }
            return Json(users1, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GETDashBoardSummary_VisitsCount(int cid, string rid)
        {

            visits visits1 = new visits();
            stat_counts dc_st = new stat_counts(); List<stat_counts> dc_st1 = new List<stat_counts>();
            string role = Helper.CurrentUserRole;
            if (rid == null)
            {
                if (role.ToLower() == "admin")
                {
                    dc_st = dc.stat_counts.Where(x => x.FK_Rid == 0).Select(y => y).SingleOrDefault();
                    if (dc_st != null)
                    {
                        visits1.total = (int)(dc_st.TotalVisits);
                        visits1.uniqueVisits = (int)(dc_st.UniqueVisits);
                        visits1.uniqueVisitsToday = (int)(dc_st.UniqueVisitsToday);
                        visits1.visitsToday = (int)(dc_st.VisitsToday);
                        visits1.uniqueVisitsYesterday = (int)(dc_st.UniqueVisitsYesterday);
                        visits1.visitsYesterday = (int)(dc_st.VisitsYesterday);
                        visits1.uniqueVisitsLast7day = (int)(dc_st.UniqueVisitsLast7day);
                        visits1.visitsLast7days = (int)(dc_st.VisitsLast7days);
                    }
                }
                else
                {
                    dc_st1 = dc.stat_counts.Where(x => x.FK_ClientID == cid).Select(y => y).ToList();
                    if (dc_st1 != null)
                    {
                        visits1.total = (int)(dc_st1.Select(x => x.TotalVisits).Sum());
                        visits1.uniqueVisits = (int)(dc_st1.Select(x => x.UniqueVisits).Sum());
                        visits1.uniqueVisitsToday = (int)(dc_st1.Select(x => x.UniqueVisitsToday).Sum());
                        visits1.visitsToday = (int)(dc_st1.Select(x => x.VisitsToday).Sum());
                        visits1.uniqueVisitsYesterday = (int)(dc_st1.Select(x => x.UniqueVisitsYesterday).Sum());
                        visits1.visitsYesterday = (int)(dc_st1.Select(x => x.VisitsYesterday).Sum());
                        visits1.uniqueVisitsLast7day = (int)(dc_st1.Select(x => x.UniqueVisitsLast7day).Sum());
                        visits1.visitsLast7days = (int)(dc_st1.Select(x => x.VisitsLast7days).Sum());
                    }
                }

            }
            else
            {
                int rid1 = Convert.ToInt32(rid);
                rid1 = dc.riddatas.Where(x => x.ReferenceNumber == rid).Select(x => x.PK_Rid).SingleOrDefault();
                dc_st = dc.stat_counts.Where(x => x.FK_Rid == rid1).Select(y => y).SingleOrDefault();
                if (dc_st != null)
                {
                    visits1.total = (int)(dc_st.TotalVisits);
                    visits1.uniqueVisits = (int)(dc_st.UniqueVisits);
                    visits1.uniqueVisitsToday = (int)(dc_st.UniqueVisitsToday);
                    visits1.visitsToday = (int)(dc_st.VisitsToday);
                    visits1.uniqueVisitsYesterday = (int)(dc_st.UniqueVisitsYesterday);
                    visits1.visitsYesterday = (int)(dc_st.VisitsYesterday);
                    visits1.uniqueVisitsLast7day = (int)(dc_st.UniqueVisitsLast7day);
                    visits1.visitsLast7days = (int)(dc_st.VisitsLast7days);
                }

            }

            return Json(visits1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GETDashBoardSummary_CampaignsCount(int cid)
        {

            campaigns campaigns1 = new campaigns();

            List<stat_counts> dc_st1 = new List<stat_counts>();
            stat_counts dc_st = new stat_counts();
            string role = Helper.CurrentUserRole;
            if (role.ToLower() != "admin")
            {
                dc_st1 = dc.stat_counts.Where(x => x.FK_ClientID == cid).Select(y => y).ToList();
                if (dc_st1 != null)
                {
                    campaigns1.total = (int)(dc_st1.Select(x=>x.TotalCamapigns).Sum());
                    campaigns1.campaignsLast7days = (int)(dc_st1.Select(x=>x.CampaignsLast7days).Sum());
                    campaigns1.campaignsMonth = (int)(dc_st1.Select(x=>x.CampaignsMonth).Sum());
                }
            }
            else
            {
                dc_st1 = dc.stat_counts.Where(x => x.FK_Rid == 0).Select(y => y).ToList();
                if (dc_st1 != null)
                {
                    campaigns1.total = (int)(dc_st1.Select(x => x.TotalCamapigns).Sum());
                    campaigns1.campaignsLast7days = (int)(dc_st1.Select(x => x.CampaignsLast7days).Sum());
                    campaigns1.campaignsMonth = (int)(dc_st1.Select(x => x.CampaignsMonth).Sum());
                }
            }
            return Json(campaigns1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GETDashBoardSummary_RecentCampaignsCount(int cid)
        {

            List<recentCampaigns_stat> objr = new List<recentCampaigns_stat>();
            List<stat_counts> dc_st1 = new List<stat_counts>();
            stat_counts dc_st = new stat_counts();
            string role = Helper.CurrentUserRole;
            if (role.ToLower() == "admin")
            {
                // dc_st = dc.stat_counts.Where(x => x.FK_ClientID == cid).Select(y => y).SingleOrDefault();
                objr = (from r in dc.stat_counts
                        .AsEnumerable()
                        orderby r.CreatedDate descending
                        select new recentCampaigns_stat()
                        {
                            id = r.PK_Stat,
                            rid = r.FK_Rid.ToString(),
                            visits = (int)r.TotalVisits,
                            users = (int)r.TotalUsers,
                            status = true,
                            //crd = r.createdOn.Value.ToString("MM/dd/yyyyThh:mm:ss")
                            createdOn = r.CreatedDate.Value.ToString("yyyy-MM-ddThh:mm:ss")
                            //endDate = (r.endd == null) ? null : (r.endd.Value.ToString("yyyy-MM-ddThh:mm:ss"))

                        }).Take(10).ToList();

            }
            else
            {
                objr = (from r in dc.stat_counts
                        where r.FK_ClientID == cid
                        orderby r.CreatedDate descending
                        select new recentCampaigns_stat()
                        {
                            id = r.PK_Stat,
                            rid = r.FK_Rid.ToString(),
                            visits = (int)r.TotalVisits,
                            users = (int)r.TotalUsers,
                            status = true,
                            //crd = r.createdOn.Value.ToString("MM/dd/yyyyThh:mm:ss")
                            createdOn = r.CreatedDate.Value.ToString("yyyy-MM-ddThh:mm:ss"),
                            //endDate = (r.endd == null) ? null : (r.endd.Value.ToString("yyyy-MM-ddThh:mm:ss"))

                        }).Take(10).ToList();
            }

            return Json(objr, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GETDashBoardSummary_ActivityCount_Today(int cid, string rid)
        {
            today today1 = new today();
            List<stat_counts> dc_st1 = new List<stat_counts>();
            stat_counts dc_st = new stat_counts();
            string role = Helper.CurrentUserRole;
            if (rid == null)
            {
                if (role.ToLower() == "admin")
                {
                    dc_st = dc.stat_counts.Where(x => x.FK_Rid == 0).Select(y => y).SingleOrDefault();
                    if (dc_st != null)
                    {
                        today1.urlTotal = (int)(dc_st.UrlTotal_Today);
                        today1.urlPercent = (double)(dc_st.UrlPercent_Today);
                        today1.visitsTotal = (int)(dc_st.VisitsToday);
                        today1.visitsPercent = (double)(dc_st.VisitsPercent_Today);
                        today1.revisitsTotal = (int)(dc_st.RevisitsTotal_Today);
                        today1.revisitsPercent = (double)(dc_st.RevisitsPercent_Today);
                        today1.noVisitsTotal = (int)(dc_st.NoVisitsTotal_Today);
                        today1.noVisitsPercent = (double)(dc_st.NoVisitsPercent_Today);
                    }
                }
                else
                {
                    dc_st1 = dc.stat_counts.Where(x => x.FK_ClientID == cid).Select(y => y).ToList();
                    if (dc_st1 != null)
                    {
                        today1.urlTotal = (int)(dc_st1.Select(x => x.UrlTotal_Today).Sum());
                        today1.urlPercent = (double)(dc_st1.Select(x => x.UrlPercent_Today).Sum());
                        today1.visitsTotal = (int)(dc_st1.Select(x => x.VisitsToday).Sum());
                        today1.visitsPercent = (double)(dc_st1.Select(x => x.VisitsPercent_Today).Sum());
                        today1.revisitsTotal = (int)(dc_st1.Select(x => x.RevisitsTotal_Today).Sum());
                        today1.revisitsPercent = (double)(dc_st1.Select(x => x.RevisitsPercent_Today).Sum());
                        today1.noVisitsTotal = (int)(dc_st1.Select(x => x.NoVisitsTotal_Today).Sum());
                        today1.noVisitsPercent = (double)(dc_st1.Select(x => x.NoVisitsPercent_Today).Sum());
                    }
                    }
            }
            else
            {
                int rid1 = Convert.ToInt32(rid);
                rid1 = dc.riddatas.Where(x => x.ReferenceNumber == rid).Select(x => x.PK_Rid).SingleOrDefault();
                dc_st = dc.stat_counts.Where(x => x.FK_Rid == rid1).Select(y => y).SingleOrDefault();
                if (dc_st != null)
                {
                    today1.urlTotal = (int)(dc_st.UrlTotal_Today);
                    today1.urlPercent = (double)(dc_st.UrlPercent_Today);
                    today1.visitsTotal = (int)(dc_st.VisitsToday);
                    today1.visitsPercent = (double)(dc_st.VisitsPercent_Today);
                    today1.revisitsTotal = (int)(dc_st.RevisitsTotal_Today);
                    today1.revisitsPercent = (double)(dc_st.RevisitsPercent_Today);
                    today1.noVisitsTotal = (int)(dc_st.NoVisitsTotal_Today);
                    today1.noVisitsPercent = (double)(dc_st.NoVisitsPercent_Today);
                }
            }
            return Json(today1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GETDashBoardSummary_ActivityCount_Week(int cid)
        {
            last7days last7days1 = new last7days();
            List<stat_counts> dc_st1 = new List<stat_counts>();
            stat_counts dc_st = new stat_counts();
            string role = Helper.CurrentUserRole;
            if (role.ToLower() == "admin")
            {
                dc_st = dc.stat_counts.Where(x => x.FK_Rid == 0).Select(y => y).SingleOrDefault();
                if (dc_st != null)
                {
                    last7days1.urlTotal = (int)(dc_st.UrlTotal_Week);
                    last7days1.urlPercent = (double)(dc_st.UrlPercent_Week);
                    last7days1.visitsTotal = (int)(dc_st.VisitsTotal_Week);
                    last7days1.visitsPercent = (double)(dc_st.VisitsPercent_Week);
                    last7days1.revisitsTotal = (int)(dc_st.RevisitsTotal_Week);
                    last7days1.revisitsPercent = (double)(dc_st.RevisitsPercent_Week);
                    last7days1.noVisitsTotal = (int)(dc_st.NoVisitsTotal_Week);
                    last7days1.noVisitsPercent = (double)(dc_st.NoVisitsPercent_Week);
                }
            }
            else
            {
                dc_st1 = dc.stat_counts.Where(x => x.FK_ClientID == cid).Select(y => y).ToList();
                if (dc_st1 != null)
                {
                    last7days1.urlTotal = (int)(dc_st1.Select(x => x.UrlTotal_Week).Sum());
                    last7days1.urlPercent = (double)(dc_st1.Select(x => x.UrlPercent_Week).Sum());
                    last7days1.visitsTotal = (int)(dc_st1.Select(x => x.VisitsTotal_Week).Sum());
                    last7days1.visitsPercent = (double)(dc_st1.Select(x => x.VisitsPercent_Week).Sum());
                    last7days1.revisitsTotal = (int)(dc_st1.Select(x => x.RevisitsTotal_Week).Sum());
                    last7days1.revisitsPercent = (double)(dc_st1.Select(x => x.RevisitsPercent_Week).Sum());
                    last7days1.noVisitsTotal = (int)(dc_st1.Select(x => x.NoVisitsTotal_Week).Sum());
                    last7days1.noVisitsPercent = (double)(dc_st1.Select(x => x.NoVisitsPercent_Week).Sum());
                }
            }

            return Json(last7days1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GETDashBoardSummary_ActivityCount_Month(int cid)
        {

            month month1 = new month();
            List<stat_counts> dc_st1 = new List<stat_counts>();
            stat_counts dc_st = new stat_counts();
            string role = Helper.CurrentUserRole;
            if (role.ToLower() == "admin")
            {
                dc_st = dc.stat_counts.Where(x => x.FK_Rid == 0).Select(y => y).SingleOrDefault();
                if (dc_st != null)
                {
                    month1.urlTotal = (int)(dc_st.UrlTotal_Month);
                    month1.urlPercent = (double)(dc_st.UrlTotalPercent_Month);
                    month1.visitsTotal = (int)(dc_st.VisitsTotal_Month);
                    month1.visitsPercent = (double)(dc_st.VisitsPercent_Month);
                    month1.revisitsTotal = (int)(dc_st.RevisitsTotal_Month);
                    month1.revisitsPercent = (double)(dc_st.RevisitsPercent_Month);
                    month1.noVisitsTotal = (int)(dc_st.NoVisitsTotal_Month);
                    month1.noVisitsPercent = (double)(dc_st.NoVisitsPercent_Month);
                }
            }
            else
            {
                dc_st1 = dc.stat_counts.Where(x => x.FK_ClientID == cid).Select(y => y).ToList();
                if (dc_st1 != null)
                {
                    month1.urlTotal = (int)(dc_st1.Select(x => x.UrlTotal_Month).Sum());
                    month1.urlPercent = (double)(dc_st1.Select(x => x.UrlTotalPercent_Month).Sum());
                    month1.visitsTotal = (int)(dc_st1.Select(x => x.VisitsTotal_Month).Sum());
                    month1.visitsPercent = (double)(dc_st1.Select(x => x.VisitsPercent_Month).Sum());
                    month1.revisitsTotal = (int)(dc_st1.Select(x => x.RevisitsTotal_Month).Sum());
                    month1.revisitsPercent = (double)(dc_st1.Select(x => x.RevisitsPercent_Month).Sum());
                    month1.noVisitsTotal = (int)(dc_st1.Select(x => x.NoVisitsTotal_Month).Sum());
                    month1.noVisitsPercent = (double)(dc_st1.Select(x => x.NoVisitsPercent_Month).Sum());
                }
            }

            return Json(month1, JsonRequestBehavior.AllowGet);

        }

     //   public JsonResult GETDashBoardSummary_TotalUrls(int cid)
     //   {

     //      totalUrls totalUrls1 = new totalUrls();
     //       try
     //       {
     //           if (Session["id"] != null)
     //           {

     //               string role = Helper.CurrentUserRole;
     //               MySqlDataReader myReader;
     //               if (role.ToLower() != "admin")
     //               {
     //                   client obj_client = dc.clients.Where(x => x.PK_ClientID == cid).Select(y => y).SingleOrDefault();
     //                   if (obj_client != null)
     //                   {
     //                       string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;

     //                       // create and open a connection object
     //                       lSQLConn = new MySqlConnection(connStr);
     //                       lSQLConn.Open();
     //                       lSQLCmd.CommandType = CommandType.StoredProcedure;
     //                       lSQLCmd.CommandTimeout = 600;
     //                       lSQLCmd.CommandText = "spGetDashBoardSummary_UrlsCount";
     //                       lSQLCmd.Parameters.Add(new MySqlParameter("@FkclientId", cid));
     //                       lSQLCmd.Connection = lSQLConn;
     //                       //myReader = lSQLCmd.ExecuteReader();
     //                   }
     //               }
     //               else
     //               {
     //                   string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;

     //                   // create and open a connection object
     //                   lSQLConn = new MySqlConnection(connStr);
     //                   lSQLConn.Open();
     //                   lSQLCmd.CommandType = CommandType.StoredProcedure;
     //                   lSQLCmd.CommandTimeout = 600;
     //                   lSQLCmd.CommandText = "spGetDashBoardSummary_UrlsCount";
     //                   lSQLCmd.Parameters.Add(new MySqlParameter("@FkclientId", "0"));
     //                   lSQLCmd.Connection = lSQLConn;
     //               }
     //               myReader = lSQLCmd.ExecuteReader();

     //                totalUrls1 = ((IObjectContextAdapter)dc)
     //                     .ObjectContext
     //                     .Translate<totalUrls>(myReader, "shorturldatas", MergeOption.AppendOnly).SingleOrDefault();

                   
     //           }

     //           return Json(totalUrls1, JsonRequestBehavior.AllowGet);
     //       }
     //       catch (Exception ex)
     //       {
     //           ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
     //           Error obj_err = new Error();
     //           Errormessage errmesobj = new Errormessage();
     //           errmesobj.message = "Exception Occured";
     //           obj_err.error = errmesobj;

     //           return Json(obj_err, JsonRequestBehavior.AllowGet);
     //       }

     //}

     //   public JsonResult GETDashBoardSummary_UsersCount(int cid)
     //   {

     //       users users1 = new users();
     //       try
     //       {
     //           if (Session["id"] != null)
     //           {

     //               string role = Helper.CurrentUserRole;
     //               MySqlDataReader myReader;
     //               if (role.ToLower() != "admin")
     //               {
     //                   client obj_client = dc.clients.Where(x => x.PK_ClientID == cid).Select(y => y).SingleOrDefault();
     //                   if (obj_client != null)
     //                   {
     //                       string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;

     //                       // create and open a connection object
     //                       lSQLConn = new MySqlConnection(connStr);
     //                       lSQLConn.Open();
     //                       lSQLCmd.CommandType = CommandType.StoredProcedure;
     //                       lSQLCmd.CommandTimeout = 600;
     //                       lSQLCmd.CommandText = "spGetDashBoardSummary_UsersCount";
     //                       lSQLCmd.Parameters.Add(new MySqlParameter("@FkclientId", cid));
     //                       lSQLCmd.Connection = lSQLConn;
     //                       //myReader = lSQLCmd.ExecuteReader();
     //                   }
     //               }
     //               else
     //               {
     //                   string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;

     //                   // create and open a connection object
     //                   lSQLConn = new MySqlConnection(connStr);
     //                   lSQLConn.Open();
     //                   lSQLCmd.CommandType = CommandType.StoredProcedure;
     //                   lSQLCmd.CommandTimeout = 600;
     //                   lSQLCmd.CommandText = "spGetDashBoardSummary_UsersCount";
     //                   lSQLCmd.Parameters.Add(new MySqlParameter("@FkclientId", "0"));
     //                   lSQLCmd.Connection = lSQLConn;
     //               }
     //               myReader = lSQLCmd.ExecuteReader();

     //               users1 = ((IObjectContextAdapter)dc)
     //                    .ObjectContext
     //                    .Translate<users>(myReader, "shorturldatas", MergeOption.AppendOnly).SingleOrDefault();


     //           }

     //           return Json(users1, JsonRequestBehavior.AllowGet);
     //       }
     //       catch (Exception ex)
     //       {
     //           ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
     //           Error obj_err = new Error();
     //           Errormessage errmesobj = new Errormessage();
     //           errmesobj.message = "Exception Occured";
     //           obj_err.error = errmesobj;

     //           return Json(obj_err, JsonRequestBehavior.AllowGet);
     //       }

     //   }

     //   public JsonResult GETDashBoardSummary_VisitsCount(int cid)
     //   {

     //       visits visits1 = new visits();
     //       try
     //       {
     //           if (Session["id"] != null)
     //           {

     //               string role = Helper.CurrentUserRole;
     //               MySqlDataReader myReader;
     //               if (role.ToLower() != "admin")
     //               {
     //                   client obj_client = dc.clients.Where(x => x.PK_ClientID == cid).Select(y => y).SingleOrDefault();
     //                   if (obj_client != null)
     //                   {
     //                       string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;

     //                       // create and open a connection object
     //                       lSQLConn = new MySqlConnection(connStr);
     //                       lSQLConn.Open();
     //                       lSQLCmd.CommandType = CommandType.StoredProcedure;
     //                       lSQLCmd.CommandTimeout = 600;
     //                       lSQLCmd.CommandText = "spGetDashBoardSummary_VisitsCount";
     //                       lSQLCmd.Parameters.Add(new MySqlParameter("@FkclientId", cid));
     //                       lSQLCmd.Connection = lSQLConn;
     //                       //myReader = lSQLCmd.ExecuteReader();
     //                   }
     //               }
     //               else
     //               {
     //                   string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;

     //                   // create and open a connection object
     //                   lSQLConn = new MySqlConnection(connStr);
     //                   lSQLConn.Open();
     //                   lSQLCmd.CommandType = CommandType.StoredProcedure;
     //                   lSQLCmd.CommandTimeout = 600;
     //                   lSQLCmd.CommandText = "spGetDashBoardSummary_VisitsCount";
     //                   lSQLCmd.Parameters.Add(new MySqlParameter("@FkclientId", "0"));
     //                   lSQLCmd.Connection = lSQLConn;
     //               }
     //               myReader = lSQLCmd.ExecuteReader();

     //               visits1 = ((IObjectContextAdapter)dc)
     //                    .ObjectContext
     //                    .Translate<visits>(myReader, "shorturldatas", MergeOption.AppendOnly).SingleOrDefault();


     //           }

     //           return Json(visits1, JsonRequestBehavior.AllowGet);
     //       }
     //       catch (Exception ex)
     //       {
     //           ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
     //           Error obj_err = new Error();
     //           Errormessage errmesobj = new Errormessage();
     //           errmesobj.message = "Exception Occured";
     //           obj_err.error = errmesobj;

     //           return Json(obj_err, JsonRequestBehavior.AllowGet);
     //       }

     //   }

     //   public JsonResult GETDashBoardSummary_CampaignsCount(int cid)
     //   {

     //       campaigns campaigns1 = new campaigns();
     //       try
     //       {
     //           if (Session["id"] != null)
     //           {

     //               string role = Helper.CurrentUserRole;
     //               MySqlDataReader myReader;
     //               if (role.ToLower() != "admin")
     //               {
     //                   client obj_client = dc.clients.Where(x => x.PK_ClientID == cid).Select(y => y).SingleOrDefault();
     //                   if (obj_client != null)
     //                   {
     //                       string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;

     //                       // create and open a connection object
     //                       lSQLConn = new MySqlConnection(connStr);
     //                       lSQLConn.Open();
     //                       lSQLCmd.CommandType = CommandType.StoredProcedure;
     //                       lSQLCmd.CommandTimeout = 600;
     //                       lSQLCmd.CommandText = "spGetDashBoardSummary_CampaignsCount";
     //                       lSQLCmd.Parameters.Add(new MySqlParameter("@FkclientId", cid));
     //                       lSQLCmd.Connection = lSQLConn;
     //                       //myReader = lSQLCmd.ExecuteReader();
     //                   }
     //               }
     //               else
     //               {
     //                   string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;

     //                   // create and open a connection object
     //                   lSQLConn = new MySqlConnection(connStr);
     //                   lSQLConn.Open();
     //                   lSQLCmd.CommandType = CommandType.StoredProcedure;
     //                   lSQLCmd.CommandTimeout = 600;
     //                   lSQLCmd.CommandText = "spGetDashBoardSummary_CampaignsCount";
     //                   lSQLCmd.Parameters.Add(new MySqlParameter("@FkclientId", "0"));
     //                   lSQLCmd.Connection = lSQLConn;
     //               }
     //               myReader = lSQLCmd.ExecuteReader();

     //               campaigns1 = ((IObjectContextAdapter)dc)
     //                    .ObjectContext
     //                    .Translate<campaigns>(myReader, "shorturldatas", MergeOption.AppendOnly).SingleOrDefault();


     //           }

     //           return Json(campaigns1, JsonRequestBehavior.AllowGet);
     //       }
     //       catch (Exception ex)
     //       {
     //           ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
     //           Error obj_err = new Error();
     //           Errormessage errmesobj = new Errormessage();
     //           errmesobj.message = "Exception Occured";
     //           obj_err.error = errmesobj;

     //           return Json(obj_err, JsonRequestBehavior.AllowGet);
     //       }

     //   }

     //   public JsonResult GETDashBoardSummary_RecentCampaignsCount(int cid)
     //   {

     //       List<recentCampaigns> objr = new List<recentCampaigns>();

     //       try
     //       {
     //           if (Session["id"] != null)
     //           {

     //               string role = Helper.CurrentUserRole;
     //               MySqlDataReader myReader;
     //               if (role.ToLower() != "admin")
     //               {
     //                   client obj_client = dc.clients.Where(x => x.PK_ClientID == cid).Select(y => y).SingleOrDefault();
     //                   if (obj_client != null)
     //                   {
     //                       string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;

     //                       // create and open a connection object
     //                       lSQLConn = new MySqlConnection(connStr);
     //                       lSQLConn.Open();
     //                       lSQLCmd.CommandType = CommandType.StoredProcedure;
     //                       lSQLCmd.CommandTimeout = 600;
     //                       lSQLCmd.CommandText = "spGetDashBoardSummary_RecentCampaignsCount";
     //                       lSQLCmd.Parameters.Add(new MySqlParameter("@FkclientId", cid));
     //                       lSQLCmd.Connection = lSQLConn;
     //                       //myReader = lSQLCmd.ExecuteReader();
     //                   }
     //               }
     //               else
     //               {
     //                   string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;

     //                   // create and open a connection object
     //                   lSQLConn = new MySqlConnection(connStr);
     //                   lSQLConn.Open();
     //                   lSQLCmd.CommandType = CommandType.StoredProcedure;
     //                   lSQLCmd.CommandTimeout = 600;
     //                   lSQLCmd.CommandText = "spGetDashBoardSummary_RecentCampaignsCount";
     //                   lSQLCmd.Parameters.Add(new MySqlParameter("@FkclientId", "0"));
     //                   lSQLCmd.Connection = lSQLConn;
     //               }
     //               myReader = lSQLCmd.ExecuteReader();

     //               List<recentCampaigns1> recentCampaigns = ((IObjectContextAdapter)dc)
     //                                          .ObjectContext
     //                                          .Translate<recentCampaigns1>(myReader, "shorturldatas", MergeOption.AppendOnly).ToList();
        
     //                                      objr = (from r in recentCampaigns
     //                                             select new recentCampaigns()
     //                                             {
     //                                                 id = r.id,
     //                                                 rid = r.rid,
     //                                                 visits = r.visits,
     //                                                 users = r.users,
     //                                                 status = r.status,
     //                                                 //crd = r.createdOn.Value.ToString("MM/dd/yyyyThh:mm:ss")
     //                                                 createdOn = r.crd.Value.ToString("yyyy-MM-ddThh:mm:ss"),
     //                                                 endDate = (r.endd == null) ? null : (r.endd.Value.ToString("yyyy-MM-ddThh:mm:ss"))

     //                                             }).ToList();
     //           }

     //           return Json(objr, JsonRequestBehavior.AllowGet);
     //       }
     //       catch (Exception ex)
     //       {
     //           ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
     //           Error obj_err = new Error();
     //           Errormessage errmesobj = new Errormessage();
     //           errmesobj.message = "Exception Occured";
     //           obj_err.error = errmesobj;

     //           return Json(obj_err, JsonRequestBehavior.AllowGet);
     //       }

     //   }

     //   public JsonResult GETDashBoardSummary_ActivityCount_Today(int cid)
     //   {

     //       today today1 = new today();
     //       try
     //       {
     //           if (Session["id"] != null)
     //           {

     //               string role = Helper.CurrentUserRole;
     //               MySqlDataReader myReader;
     //               if (role.ToLower() != "admin")
     //               {
     //                   client obj_client = dc.clients.Where(x => x.PK_ClientID == cid).Select(y => y).SingleOrDefault();
     //                   if (obj_client != null)
     //                   {
     //                       string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;

     //                       // create and open a connection object
     //                       lSQLConn = new MySqlConnection(connStr);
     //                       lSQLConn.Open();
     //                       lSQLCmd.CommandType = CommandType.StoredProcedure;
     //                       lSQLCmd.CommandTimeout = 600;
     //                       lSQLCmd.CommandText = "spGetDashBoardSummary_ActivityCount_Today";
     //                       lSQLCmd.Parameters.Add(new MySqlParameter("@FkclientId", cid));
     //                       lSQLCmd.Connection = lSQLConn;
     //                       //myReader = lSQLCmd.ExecuteReader();
     //                   }
     //               }
     //               else
     //               {
     //                   string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;

     //                   // create and open a connection object
     //                   lSQLConn = new MySqlConnection(connStr);
     //                   lSQLConn.Open();
     //                   lSQLCmd.CommandType = CommandType.StoredProcedure;
     //                   lSQLCmd.CommandTimeout = 600;
     //                   lSQLCmd.CommandText = "spGetDashBoardSummary_ActivityCount_Today";
     //                   lSQLCmd.Parameters.Add(new MySqlParameter("@FkclientId", "0"));
     //                   lSQLCmd.Connection = lSQLConn;
     //               }
     //               myReader = lSQLCmd.ExecuteReader();

     //               today1 = ((IObjectContextAdapter)dc)
     //                    .ObjectContext
     //                    .Translate<today>(myReader, "shorturldatas", MergeOption.AppendOnly).SingleOrDefault();


     //           }

     //           return Json(today1, JsonRequestBehavior.AllowGet);
     //       }
     //       catch (Exception ex)
     //       {
     //           ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
     //           Error obj_err = new Error();
     //           Errormessage errmesobj = new Errormessage();
     //           errmesobj.message = "Exception Occured";
     //           obj_err.error = errmesobj;

     //           return Json(obj_err, JsonRequestBehavior.AllowGet);
     //       }

     //   }


     //   public JsonResult GETDashBoardSummary_ActivityCount_Week(int cid)
     //   {

     //       last7days last7days1 = new last7days();
     //       try
     //       {
     //           if (Session["id"] != null)
     //           {

     //               string role = Helper.CurrentUserRole;
     //               MySqlDataReader myReader;
     //               if (role.ToLower() != "admin")
     //               {
     //                   client obj_client = dc.clients.Where(x => x.PK_ClientID == cid).Select(y => y).SingleOrDefault();
     //                   if (obj_client != null)
     //                   {
     //                       string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;

     //                       // create and open a connection object
     //                       lSQLConn = new MySqlConnection(connStr);
     //                       lSQLConn.Open();
     //                       lSQLCmd.CommandType = CommandType.StoredProcedure;
     //                       lSQLCmd.CommandTimeout = 600;
     //                       lSQLCmd.CommandText = "spGetDashBoardSummary_ActivityCount_week";
     //                       lSQLCmd.Parameters.Add(new MySqlParameter("@FkclientId", cid));
     //                       lSQLCmd.Connection = lSQLConn;
     //                       //myReader = lSQLCmd.ExecuteReader();
     //                   }
     //               }
     //               else
     //               {
     //                   string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;

     //                   // create and open a connection object
     //                   lSQLConn = new MySqlConnection(connStr);
     //                   lSQLConn.Open();
     //                   lSQLCmd.CommandType = CommandType.StoredProcedure;
     //                   lSQLCmd.CommandTimeout = 600;
     //                   lSQLCmd.CommandText = "spGetDashBoardSummary_ActivityCount_week";
     //                   lSQLCmd.Parameters.Add(new MySqlParameter("@FkclientId", "0"));
     //                   lSQLCmd.Connection = lSQLConn;
     //               }
     //               myReader = lSQLCmd.ExecuteReader();

     //               last7days1 = ((IObjectContextAdapter)dc)
     //                    .ObjectContext
     //                    .Translate<last7days>(myReader, "shorturldatas", MergeOption.AppendOnly).SingleOrDefault();


     //           }

     //           return Json(last7days1, JsonRequestBehavior.AllowGet);
     //       }
     //       catch (Exception ex)
     //       {
     //           ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
     //           Error obj_err = new Error();
     //           Errormessage errmesobj = new Errormessage();
     //           errmesobj.message = "Exception Occured";
     //           obj_err.error = errmesobj;

     //           return Json(obj_err, JsonRequestBehavior.AllowGet);
     //       }

     //   }

     //   public JsonResult GETDashBoardSummary_ActivityCount_Month(int cid)
     //   {

     //       month month1 = new month();
     //       try
     //       {
     //           if (Session["id"] != null)
     //           {

     //               string role = Helper.CurrentUserRole;
     //               MySqlDataReader myReader;
     //               if (role.ToLower() != "admin")
     //               {
     //                   client obj_client = dc.clients.Where(x => x.PK_ClientID == cid).Select(y => y).SingleOrDefault();
     //                   if (obj_client != null)
     //                   {
     //                       string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;

     //                       // create and open a connection object
     //                       lSQLConn = new MySqlConnection(connStr);
     //                       lSQLConn.Open();
     //                       lSQLCmd.CommandType = CommandType.StoredProcedure;
     //                       lSQLCmd.CommandTimeout = 600;
     //                       lSQLCmd.CommandText = "spGetDashBoardSummary_ActivityCount_month";
     //                       lSQLCmd.Parameters.Add(new MySqlParameter("@FkclientId", cid));
     //                       lSQLCmd.Connection = lSQLConn;
     //                       //myReader = lSQLCmd.ExecuteReader();
     //                   }
     //               }
     //               else
     //               {
     //                   string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;

     //                   // create and open a connection object
     //                   lSQLConn = new MySqlConnection(connStr);
     //                   lSQLConn.Open();
     //                   lSQLCmd.CommandType = CommandType.StoredProcedure;
     //                   lSQLCmd.CommandTimeout = 600;
     //                   lSQLCmd.CommandText = "spGetDashBoardSummary_ActivityCount_month";
     //                   lSQLCmd.Parameters.Add(new MySqlParameter("@FkclientId", "0"));
     //                   lSQLCmd.Connection = lSQLConn;
     //               }
     //               myReader = lSQLCmd.ExecuteReader();

     //               month1 = ((IObjectContextAdapter)dc)
     //                    .ObjectContext
     //                    .Translate<month>(myReader, "shorturldatas", MergeOption.AppendOnly).SingleOrDefault();


     //           }

     //           return Json(month1, JsonRequestBehavior.AllowGet);
     //       }
     //       catch (Exception ex)
     //       {
     //           ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
     //           Error obj_err = new Error();
     //           Errormessage errmesobj = new Errormessage();
     //           errmesobj.message = "Exception Occured";
     //           obj_err.error = errmesobj;

     //           return Json(obj_err, JsonRequestBehavior.AllowGet);
     //       }

     //   }
     //  start GetSummary1
        public JsonResult GETSummary1(int cid, string rid)
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
                                
                                st_obj_res = dc.stat_counts.Where(x => x.FK_ClientID == objr.FK_ClientId && x.FK_Rid == objr.PK_Rid).SingleOrDefault();
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
     // end GetSummary1

      //  //  start GetSummary_stats_count
      //  //[OutputCache(Duration = 60, VaryByParam = "cid")]  
      //public bool statsdata(int cid)
      //  {
      //      stat_counts st_data = new stat_counts();
      //      if (System.Runtime.Caching.MemoryCache.Default["summarydata"] == null)
      //      {
      //          st_data = dc.stat_counts.Where(x => x.FK_ClientID == cid).SingleOrDefault();
      //          var sum_data = System.Runtime.Caching.MemoryCache.Default["summarydata"];
      //          System.Runtime.Caching.MemoryCache.Default["summarydata"] = st_data;
      //      }
      //    else
      //      {
      //          st_data =(stat_counts) System.Runtime.Caching.MemoryCache.Default["summarydata"];

      //      }
      //    //return st_data;
      //  }
        //public JsonResult GetSummary_stats_count(int cid, string rid)
        public JsonResult GetSummary(int cid, string rid)    
    {

            DashBoardStats obj = new DashBoardStats();
            

            try
            {
                if (Session["id"] != null)
                {
                    if (Session["id"] != null && rid == null)
                    {
                        List<stat_counts> st_obj_res = new List<stat_counts>();

                        string role = Helper.CurrentUserRole;
                        if (role == "admin")
                        {
                            //st_obj_res = dc.stat_counts.ToList(); 
                            obj = getcampaigndata(0);
                        }
                        else
                        {
                            st_obj_res = dc.stat_counts.Where(x => x.FK_ClientID == cid).ToList();

                            totalUrls_stat totalUrls = new totalUrls_stat();
                            totalUrls.count = st_obj_res.Select(x => x.TotalUsers).Sum();
                            users_stat users = new users_stat();
                            users.total = st_obj_res.Select(x => x.TotalUsers).Sum();
                            users.uniqueUsers = st_obj_res.Select(x => x.UniqueUsers).Sum();
                            users.uniqueUsersToday = st_obj_res.Select(x => x.UniqueUsersToday).Sum();
                            users.usersToday = st_obj_res.Select(x => x.UsersToday).Sum();
                            users.uniqueUsersYesterday = st_obj_res.Select(x => x.UniqueUsersYesterday).Sum();
                            users.usersYesterday = st_obj_res.Select(x => x.UsersYesterday).Sum();
                            users.uniqueUsersLast7days = st_obj_res.Select(x => x.UniqueUsersLast7days).Sum();
                            users.usersLast7days = st_obj_res.Select(x => x.UsersLast7days).Sum();
                            visits_stat visits = new visits_stat();
                            visits.total = st_obj_res.Select(x => x.TotalVisits).Sum();
                            visits.uniqueVisits = st_obj_res.Select(x => x.UniqueVisits).Sum();
                            visits.uniqueVisitsToday = st_obj_res.Select(x => x.UniqueVisitsToday).Sum();
                            visits.visitsToday = st_obj_res.Select(x => x.VisitsToday).Sum();
                            visits.uniqueVisitsYesterday = st_obj_res.Select(x => x.UniqueVisitsYesterday).Sum();
                            visits.visitsYesterday = st_obj_res.Select(x => x.VisitsYesterday).Sum();
                            //visits.uniqueVisitsLast7days =(st_obj_res.FK_Rid!=0)?st_obj_res.UniqueVisitsLast7day:(uniqueVisitsToday.Sum(x => x.uniqueVisitsToday)+st_obj_res.UniqueVisitsYesterday+st_obj_res.UniqueVisitsLast7day);
                            //visits.visitsLast7days = (st_obj_res.FK_Rid != 0) ? st_obj_res.VisitsLast7days : (st_obj_res.VisitsToday + st_obj_res.VisitsYedewes  fcristerday + st_obj_res.VisitsLast7days);
                            visits.uniqueVisitsLast7days = st_obj_res.Select(x => x.UniqueVisitsLast7day).Sum();
                            visits.visitsLast7days = st_obj_res.Select(x => x.VisitsLast7days).Sum();
                            campaigns_stat campaigns = new campaigns_stat();
                            campaigns.total = st_obj_res.Select(x => x.TotalCamapigns).Sum();
                            campaigns.campaignsLast7days = st_obj_res.Select(x => x.CampaignsLast7days).Sum();
                            campaigns.campaignsMonth = st_obj_res.Select(x => x.CampaignsMonth).Sum();

                            today_stat today = new today_stat();
                            today.urlTotal = st_obj_res.Select(x => x.UrlTotal_Today).Sum();
                            today.urlPercent = st_obj_res.Select(x => x.UrlPercent_Today).Sum();
                            today.visitsTotal = st_obj_res.Select(x => x.VisitsToday).Sum();
                            today.visitsPercent = st_obj_res.Select(x => x.VisitsPercent_Today).Sum();
                            today.revisitsTotal = st_obj_res.Select(x => x.RevisitsTotal_Today).Sum();
                            today.revisitsPercent = st_obj_res.Select(x => x.RevisitsPercent_Today).Sum();
                            today.noVisitsTotal = st_obj_res.Select(x => x.NoVisitsTotal_Today).Sum();
                            today.noVisitsPercent = st_obj_res.Select(x => x.NoVisitsPercent_Today).Sum();
                            last7days_stat last7days = new last7days_stat();
                            last7days.urlTotal = st_obj_res.Select(x => x.UrlTotal_Week).Sum();
                            last7days.urlPercent = st_obj_res.Select(x => x.UrlPercent_Week).Sum();
                            last7days.visitsTotal = st_obj_res.Select(x => x.VisitsTotal_Week).Sum();
                            last7days.visitsPercent = st_obj_res.Select(x => x.VisitsPercent_Week).Sum();
                            last7days.revisitsTotal = st_obj_res.Select(x => x.RevisitsTotal_Week).Sum();
                            last7days.revisitsPercent = st_obj_res.Select(x => x.RevisitsPercent_Week).Sum();
                            last7days.noVisitsTotal = st_obj_res.Select(x => x.NoVisitsTotal_Week).Sum();
                            last7days.noVisitsPercent = st_obj_res.Select(x => x.NoVisitsPercent_Week).Sum();
                            month_stat month = new month_stat();
                            month.urlTotal = st_obj_res.Select(x => x.UrlTotal_Month).Sum();
                            month.urlPercent = st_obj_res.Select(x => x.UrlTotalPercent_Month).Sum();
                            month.visitsTotal = st_obj_res.Select(x => x.VisitsTotal_Month).Sum();
                            month.visitsPercent = st_obj_res.Select(x => x.VisitsPercent_Month).Sum();
                            month.revisitsTotal = st_obj_res.Select(x => x.RevisitsTotal_Month).Sum();
                            month.revisitsPercent = st_obj_res.Select(x => x.RevisitsPercent_Month).Sum();
                            month.noVisitsTotal = st_obj_res.Select(x => x.NoVisitsTotal_Month).Sum();
                            month.noVisitsPercent = st_obj_res.Select(x => x.NoVisitsPercent_Month).Sum();


                            List<recentCampaigns1_stat> objr1 = (from r in dc.stat_counts
                                                                 where r.FK_Rid!=0
                                                               orderby r.CreatedDate descending
                                                               select new recentCampaigns1_stat()
                                                               {
                                                                   id = r.PK_Stat,
                                                                   rid = r.FK_Rid.ToString(),
                                                                   visits = (int)r.TotalVisits,
                                                                   users = (int)r.TotalUsers,
                                                                   status = true,
                                                                   //crd = r.createdOn.Value.ToString("MM/dd/yyyyThh:mm:ss")
                                                                   crd = r.CreatedDate,
                                                                   //endDate = (r.endd == null) ? null : (r.endd.Value.ToString("yyyy-MM-ddThh:mm:ss"))

                                                               }).Take(10).ToList();
                            List<recentCampaigns_stat> objr = (from r in objr1
                                                               orderby r.crd descending
                                                               select new recentCampaigns_stat()
                                                               {
                                                                   id = r.id,
                                                                   rid = r.rid.ToString(),
                                                                   visits = (int)r.visits,
                                                                   users = (int)r.users,
                                                                   status = true,
                                                                   //crd = r.createdOn.Value.ToString("MM/dd/yyyyThh:mm:ss")
                                                                   createdOn = r.crd.Value.ToString("yyyy-MM-ddThh:mm:ss"),
                                                                   //endDate = (r.endd == null) ? null : (r.endd.Value.ToString("yyyy-MM-ddThh:mm:ss"))

                                                               }).Take(10).ToList();


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

                        }

                    }

                    else if (Session["id"] != null && rid != null)
                    {
                        stat_counts st_obj_res = new stat_counts();
                        int fk_rid = dc.riddatas.Where(x => x.ReferenceNumber == rid).Select(y => y.PK_Rid).SingleOrDefault();
                        //string role = Helper.CurrentUserRole;
                        //int? rid1 =Convert.ToInt32(rid);

                        obj = getcampaigndata(fk_rid);
                        
                        
                    }

                    return Json(obj, JsonRequestBehavior.AllowGet);    
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
                errmesobj.message = "Exception Occured";
                obj_err.error = errmesobj;

                return Json(obj_err, JsonRequestBehavior.AllowGet);
            }
        }


        public DashBoardStats getcampaigndata(int? rid)
        {
            DashBoardStats obj = new DashBoardStats();
           stat_counts st_obj_res = dc.stat_counts.Where(x => x.FK_Rid == rid).SingleOrDefault();

            totalUrls_stat totalUrls = new totalUrls_stat();
            totalUrls.count = st_obj_res.TotalUsers;
            users_stat users = new users_stat();
            users.total = st_obj_res.TotalUsers;
            users.uniqueUsers = st_obj_res.UniqueUsers;
            users.uniqueUsersToday = st_obj_res.UniqueUsersToday;
            users.usersToday = st_obj_res.UsersToday;
            users.uniqueUsersYesterday = st_obj_res.UniqueUsersYesterday;
            users.usersYesterday = st_obj_res.UsersYesterday;
            users.uniqueUsersLast7days = st_obj_res.UniqueUsersLast7days;
            users.usersLast7days = st_obj_res.UsersLast7days;
            visits_stat visits = new visits_stat();
            visits.total = st_obj_res.TotalVisits;
            visits.uniqueVisits = st_obj_res.UniqueVisits;
            visits.uniqueVisitsToday = st_obj_res.UniqueVisitsToday;
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

            today_stat today = new today_stat();
            today.urlTotal = st_obj_res.UrlTotal_Today;
            today.urlPercent = st_obj_res.UrlPercent_Today;
            today.visitsTotal = st_obj_res.VisitsToday;
            today.visitsPercent = st_obj_res.VisitsPercent_Today;
            today.revisitsTotal = st_obj_res.RevisitsTotal_Today;
            today.revisitsPercent = st_obj_res.RevisitsPercent_Today;
            today.noVisitsTotal = st_obj_res.NoVisitsTotal_Today;
            today.noVisitsPercent = st_obj_res.NoVisitsPercent_Today;
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


            List<recentCampaigns1_stat> objr1 = (from r in dc.stat_counts
                                                 where r.FK_Rid !=0
                                                 orderby r.CreatedDate descending
                                                 select new recentCampaigns1_stat()
                                                 {
                                                     id = r.PK_Stat,
                                                     rid = r.FK_Rid.ToString(),
                                                     visits = (int)r.TotalVisits,
                                                     users = (int)r.TotalUsers,
                                                     status = true,
                                                     //crd = r.createdOn.Value.ToString("MM/dd/yyyyThh:mm:ss")
                                                     crd = r.CreatedDate,
                                                     //endDate = (r.endd == null) ? null : (r.endd.Value.ToString("yyyy-MM-ddThh:mm:ss"))

                                                 }).Take(10).ToList();
            List<recentCampaigns_stat> objr = (from r in objr1
                                               orderby r.crd descending
                                               select new recentCampaigns_stat()
                                               {
                                                   id = r.id,
                                                   rid = r.rid.ToString(),
                                                   visits = (int)r.visits,
                                                   users = (int)r.users,
                                                   status = true,
                                                   //crd = r.createdOn.Value.ToString("MM/dd/yyyyThh:mm:ss")
                                                   createdOn = r.crd.Value.ToString("yyyy-MM-ddThh:mm:ss"),
                                                   //endDate = (r.endd == null) ? null : (r.endd.Value.ToString("yyyy-MM-ddThh:mm:ss"))

                                               }).Take(10).ToList();



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

            return obj;
        }
        // end GetSummary_stats_count


    }
}