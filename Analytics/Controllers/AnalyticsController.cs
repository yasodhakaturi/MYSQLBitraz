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
        public JsonResult GETSummary(int cid,string rid)
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

                            // Move to locations result 
                            myReader.NextResult();
                            campaigns campaigns = ((IObjectContextAdapter)dc)
                           .ObjectContext
                           .Translate<campaigns>(myReader, "shorturldatas", MergeOption.AppendOnly).SingleOrDefault();

                            // Move to locations result 
                            myReader.NextResult();
                            List<recentCampaigns1> recentCampaigns = ((IObjectContextAdapter)dc)
                           .ObjectContext
                           .Translate<recentCampaigns1>(myReader, "shorturldatas", MergeOption.AppendOnly).ToList();

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
                ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException);
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
                ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException);
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
                ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException);
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
                ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException);
                Error obj_err = new Error();
                Errormessage errmesobj = new Errormessage();
                errmesobj.message = "Exception Occured";
                obj_err.error = errmesobj;

                return Json(obj_err, JsonRequestBehavior.AllowGet);
            }
        }
        //public ActionResult Analytics()
        //{

        //    return View();
        //}
    }
}