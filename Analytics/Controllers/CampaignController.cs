using Analytics.Helpers.BO;
using Analytics.Helpers.Utility;
using Analytics.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Validation;
//using System.Data.Sqlclient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Analytics.Controllers
{
    public class CampaignController : Controller
    {
        shortenurlEntities dc = new shortenurlEntities();
        OperationsBO objbo = new OperationsBO();
        // GET: Campaign
        public JsonResult Index(string id)
        {
            //    return View();
            //}
            //public JsonResult GetclientDetails()
            //{
           
            List<CampaignView1> objc = new List<CampaignView1>();
            CampaignView1 obj = new CampaignView1();
            int id1 = Convert.ToInt32(id);
            try
            {
                if (id == null && Session["id"] != "")
                {
                    //get all client detials
                    objc = (from r in dc.riddatas
                            select new CampaignView1
                            {
                                Id = r.PK_Rid,
                                ReferenceNumber = r.ReferenceNumber,
                                // pwd = r.Pwd,
                                IsActive = r.IsActive
                            }).ToList();
                    return Json(objc, JsonRequestBehavior.AllowGet);

                }
                else if (id != null && Session["id"] != "")
                {
                    obj = (from r in dc.riddatas
                           where r.PK_Rid == id1
                           select new CampaignView1
                           {
                               Id = r.PK_Rid,
                               ReferenceNumber = r.ReferenceNumber,
                               //pwd = r.Pwd,
                               IsActive = r.IsActive
                           }).SingleOrDefault();
                    return Json(obj, JsonRequestBehavior.AllowGet);

                }


                return Json(objc, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
                return Json(objc, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Search(string referencenumber, bool? isactive)
        {
            List<CampaignView1> obj_search = new List<CampaignView1>();
            string isactivestr = Convert.ToString(isactive);

            try
            {
                if (Session["id"] != null)
                {
                    if (referencenumber != null && isactivestr == "" && Session["id"] != null)
                    {
                        if (Helper.CurrentUserRole.ToLower() == "admin")
                        {
                            obj_search = (from c in dc.riddatas
                                          join c1 in dc.clients on c.FK_ClientId equals c1.PK_ClientID
                                          where c.ReferenceNumber.Contains(referencenumber.ToString()) && c.CampaignName.ToString() != null && c.CampaignName.ToString() != ""
                                          select new CampaignView1()
                                          {
                                              Id = c.PK_Rid,
                                              ReferenceNumber = c.ReferenceNumber,
                                              CampaignName = c.CampaignName,
                                              HasPassword = (c.Pwd != null && c.Pwd != string.Empty) ? true : false,
                                              //pwd = r.Pwd,
                                              IsActive = c.IsActive,
                                              CreatedDateStr = c.CreatedDate,
                                              CreatedUserId = c1.PK_ClientID,
                                              CreatedUserEmail = c1.Email,
                                              CreatedUserName = c1.UserName,
                                              CreatedUserActiveState = c1.IsActive
                                          }).ToList();
                        }
                        else
                        {
                            obj_search = (from c in dc.riddatas
                                          join c1 in dc.clients on c.FK_ClientId equals c1.PK_ClientID
                                          where c.ReferenceNumber.Contains(referencenumber.ToString()) && c.CampaignName.ToString() != null && c.CampaignName.ToString() != "" && c.FK_ClientId == Helper.CurrentUserId
                                          select new CampaignView1()
                                          {
                                              Id = c.PK_Rid,
                                              ReferenceNumber = c.ReferenceNumber,
                                              CampaignName = c.CampaignName,
                                              HasPassword = (c.Pwd != null && c.Pwd != string.Empty) ? true : false,
                                              //pwd = r.Pwd,
                                              IsActive = c.IsActive,
                                              CreatedDateStr = c.CreatedDate,
                                              CreatedUserId = c1.PK_ClientID,
                                              CreatedUserEmail = c1.Email,
                                              CreatedUserName = c1.UserName,
                                              CreatedUserActiveState = c1.IsActive
                                          }).ToList();
                        }
                        obj_search = (from x in obj_search
                                      select new CampaignView1()
                                      {
                                          Id = x.Id,
                                          ReferenceNumber = x.ReferenceNumber,
                                          CampaignName = x.CampaignName,
                                          HasPassword = x.HasPassword,
                                          //pwd = r.Pwd,
                                          IsActive = x.IsActive,
                                          //CreatedDate = x.CreatedDate.ToString(),
                                          CreatedDate = x.CreatedDateStr.Value.ToString("yyyy-MM-ddThh:mm:ss"),
                                          CreatedUserId = x.CreatedUserId,
                                          CreatedUserEmail = x.CreatedUserEmail,
                                          CreatedUserName = x.CreatedUserName,
                                          CreatedUserActiveState = x.CreatedUserActiveState
                                      }).ToList();
                    }

                    else if (isactivestr != "" && referencenumber == null && Session["id"] != null)
                    {
                        if (Helper.CurrentUserRole.ToLower() == "admin")
                        {
                            obj_search = (from c in dc.riddatas
                                          join c1 in dc.clients on c.FK_ClientId equals c1.PK_ClientID
                                          where c.IsActive == isactive && c.CampaignName.ToString() != null && c.CampaignName.ToString() != ""
                                          select new CampaignView1()
                                          {
                                              Id = c.PK_Rid,
                                              ReferenceNumber = c.ReferenceNumber,
                                              CampaignName = c.CampaignName,
                                              HasPassword = (c.Pwd != null && c.Pwd != string.Empty) ? true : false,
                                              //pwd = r.Pwd,
                                              IsActive = c.IsActive,
                                              CreatedDateStr = c.CreatedDate,
                                              CreatedUserId = c1.PK_ClientID,
                                              CreatedUserEmail = c1.Email,
                                              CreatedUserName = c1.UserName,
                                              CreatedUserActiveState = c1.IsActive
                                          }).ToList();
                        }
                        else
                        {
                            obj_search = (from c in dc.riddatas
                                          join c1 in dc.clients on c.FK_ClientId equals c1.PK_ClientID
                                          where c.IsActive == isactive && c.CampaignName.ToString() != null && c.CampaignName.ToString() != "" && c.FK_ClientId == Helper.CurrentUserId
                                          select new CampaignView1()
                                          {
                                              Id = c.PK_Rid,
                                              ReferenceNumber = c.ReferenceNumber,
                                              CampaignName = c.CampaignName,
                                              HasPassword = (c.Pwd != null && c.Pwd != string.Empty) ? true : false,
                                              //pwd = r.Pwd,
                                              IsActive = c.IsActive,
                                              CreatedDateStr = c.CreatedDate,
                                              CreatedUserId = c1.PK_ClientID,
                                              CreatedUserEmail = c1.Email,
                                              CreatedUserName = c1.UserName,
                                              CreatedUserActiveState = c1.IsActive
                                          }).ToList();
                        }
                        obj_search = (from x in obj_search
                                      select new CampaignView1()
                                      {
                                          Id = x.Id,
                                          ReferenceNumber = x.ReferenceNumber,
                                          CampaignName = x.CampaignName,
                                          HasPassword = x.HasPassword,
                                          //pwd = r.Pwd,
                                          IsActive = x.IsActive,
                                          //CreatedDate = x.CreatedDate.ToString(),
                                          CreatedDate = x.CreatedDateStr.Value.ToString("yyyy-MM-ddThh:mm:ss"),
                                          CreatedUserId = x.CreatedUserId,
                                          CreatedUserEmail = x.CreatedUserEmail,
                                          CreatedUserName = x.CreatedUserName,
                                          CreatedUserActiveState = x.CreatedUserActiveState
                                      }).ToList();
                    }
                    else if (isactivestr == "" && referencenumber == null && Session["id"] != null)
                    {
                        if (Helper.CurrentUserRole.ToLower() == "admin")
                        {
                            obj_search = (from c in dc.riddatas
                                          join c1 in dc.clients on c.FK_ClientId equals c1.PK_ClientID
                                          where c.CampaignName.ToString() != null && c.CampaignName.ToString() != ""
                                          select new CampaignView1()
                                          {
                                              Id = c.PK_Rid,
                                              ReferenceNumber = c.ReferenceNumber,
                                              CampaignName = c.CampaignName,
                                              HasPassword = (c.Pwd != null && c.Pwd != string.Empty) ? true : false,
                                              //pwd = r.Pwd,
                                              IsActive = c.IsActive,
                                              CreatedDateStr = c.CreatedDate,
                                              //CreatedDate = c.CreatedDate.Value.ToString("yyyy-MM-ddThh:mm:ss"),
                                              CreatedUserId = c1.PK_ClientID,
                                              CreatedUserEmail = c1.Email,
                                              CreatedUserName = c1.UserName,
                                              CreatedUserActiveState = c1.IsActive
                                          }).ToList();
                        }
                        else
                        {
                            obj_search = (from c in dc.riddatas
                                          join c1 in dc.clients on c.FK_ClientId equals c1.PK_ClientID
                                          where c.CampaignName.ToString() != null && c.CampaignName.ToString() != "" && c.FK_ClientId == Helper.CurrentUserId
                                          select new CampaignView1()
                                          {
                                              Id = c.PK_Rid,
                                              ReferenceNumber = c.ReferenceNumber,
                                              CampaignName = c.CampaignName,
                                              HasPassword = (c.Pwd != null && c.Pwd != string.Empty) ? true : false,
                                              //pwd = r.Pwd,
                                              IsActive = c.IsActive,
                                              CreatedDateStr = c.CreatedDate,
                                              //CreatedDate = c.CreatedDate.Value.ToString("yyyy-MM-ddThh:mm:ss"),
                                              CreatedUserId = c1.PK_ClientID,
                                              CreatedUserEmail = c1.Email,
                                              CreatedUserName = c1.UserName,
                                              CreatedUserActiveState = c1.IsActive
                                          }).ToList();
                        }
                        obj_search = (from x in obj_search
                                      select new CampaignView1()
                                   {
                                       Id = x.Id,
                                       ReferenceNumber = x.ReferenceNumber,
                                       CampaignName = x.CampaignName,
                                       HasPassword = x.HasPassword,
                                       //pwd = r.Pwd,
                                       IsActive = x.IsActive,
                                       //CreatedDate = x.CreatedDate.ToString(),
                                       CreatedDate = x.CreatedDateStr.Value.ToString("yyyy-MM-ddThh:mm:ss"),
                                       CreatedUserId = x.CreatedUserId,
                                       CreatedUserEmail = x.CreatedUserEmail,
                                       CreatedUserName = x.CreatedUserName,
                                       CreatedUserActiveState = x.CreatedUserActiveState
                                   }).OrderByDescending(c => c.CreatedDate).ToList();

                    }
                    return Json(obj_search, JsonRequestBehavior.AllowGet);
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
                return Json(obj_search, JsonRequestBehavior.AllowGet);
            }
        }

        //public JsonResult AddCampaign([FromBody]JToken jObject)
        [System.Web.Mvc.HttpPost]
        public JsonResult AddCampaign(string CampaignName, string CreatedUserId, string Pwd, bool IsActive)
        {
            riddata obj = new riddata();
            CampaignView1 obj_search = new CampaignView1();

            try
            {
                Random randNum = new Random();
                int r = randNum.Next(00000, 99999);
                string ReferenceNumber = r.ToString("D5");
                int cid;
                cid = Helper.CurrentUserId;

                if (CreatedUserId != null&&CreatedUserId!="")
                    cid = Convert.ToInt32(CreatedUserId);
                
                //string fields = "id,ReferenceNumber,isactive";

                //string ReferenceNumber = (string)jObject["ReferenceNumber"];
                //string Pwd = (string)jObject["Pwd"];
                //bool IsActive = (bool)jObject["IsActive"];
                //int id = (int)Session["id"];
                //int id = 1;
                // riddata objc = dc.riddatas.Where(x => x.ReferenceNumber== ReferenceNumber).SingleOrDefault();
                //int FK_ClientID = (int)jObject["clientId"];
                //bool isclientExists = new OperationsBO().CheckclientId(FK_ClientID);
                riddata objr = dc.riddatas.Where(r1 => r1.FK_ClientId == cid && r1.CampaignName == CampaignName).SingleOrDefault();
                if (Session["id"] != null && objr == null && CampaignName != "" && CampaignName != null)
                {
                    //if (Session["id"] != null && CampaignName != "" && CampaignName != null)
                    //{

                        
                            //add campaign details
                            obj.CampaignName = CampaignName;
                            obj.ReferenceNumber = ReferenceNumber;
                            obj.Pwd = Pwd;
                            obj.IsActive = IsActive;
                            obj.CreatedDate = DateTime.UtcNow;
                            obj.CreatedBy = Helper.CurrentUserId;
                            obj.FK_ClientId = cid;
                            dc.riddatas.Add(obj);
                            dc.SaveChanges();
                        
                        obj_search = (from c in dc.riddatas
                                      join c1 in dc.clients on c.FK_ClientId equals c1.PK_ClientID
                                      where c.ReferenceNumber == ReferenceNumber
                                      select new CampaignView1()
                                      {
                                          Id = c.PK_Rid,
                                          ReferenceNumber = c.ReferenceNumber,
                                          CampaignName = c.CampaignName,
                                          HasPassword = (c.Pwd != null && c.Pwd != string.Empty) ? true : false,
                                          //pwd = r.Pwd,
                                          IsActive = c.IsActive,
                                          CreatedDateStr = c.CreatedDate,
                                          CreatedUserId = c1.PK_ClientID,
                                          CreatedUserEmail = c1.Email,
                                          CreatedUserName = c1.UserName,
                                          CreatedUserActiveState = c1.IsActive
                                      }).SingleOrDefault();
                        obj_search.CreatedDate = obj_search.CreatedDateStr.Value.ToString("yyyy-MM-ddThh:mm:ss");
                        //obj_search = (from x in obj_search
                        //              select new CampaignView1()
                        //              {
                        //                  Id = x.Id,
                        //                  ReferenceNumber = x.ReferenceNumber,
                        //                  CampaignName = x.CampaignName,
                        //                  HasPassword = x.HasPassword,
                        //                  //pwd = r.Pwd,
                        //                  IsActive = x.IsActive,
                        //                  //CreatedDate = x.CreatedDate.ToString(),
                        //                  CreatedDate = x.CreatedDateStr.Value.ToString("yyyy-MM-ddThh:mm:ss"),
                        //                  CreatedUserId = x.CreatedUserId,
                        //                  CreatedUserEmail = x.CreatedUserEmail,
                        //                  CreatedUserName = x.CreatedUserName,
                        //                  CreatedUserActiveState = x.CreatedUserActiveState
                        //              }).SingleOrDefault();
                        // new OperationsBO().InsertUIDriddata(ReferenceNumber);
                       // return Json(obj_search, JsonRequestBehavior.AllowGet);
                    //}
                    return Json(obj_search, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Error obj_err = new Error();
                if(objr!=null)
                {
                    Errormessage errmesobj = new Errormessage();
                    errmesobj.message = "CampaignName already Exists for this client.";
                    obj_err.error = errmesobj;
                  }
                else if (Session["id"] == null)
                    {
                        Errormessage errmesobj = new Errormessage();
                        errmesobj.message = "Session Expired.";
                        obj_err.error = errmesobj;
                        //return Json(obj_err, JsonRequestBehavior.AllowGet);
                    }
                    else if (CampaignName == "" || CampaignName == null)
                    {
                        Errormessage errmesobj = new Errormessage();
                        errmesobj.message = "CampaignName is should not be empty.";
                        obj_err.error = errmesobj;
                       // return Json(obj_err, JsonRequestBehavior.AllowGet);
                    }
                    return Json(obj_err, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        //public JsonResult UpdateCampaign([FromBody]JToken jObject)
        [System.Web.Mvc.HttpPost]
        public JsonResult UpdateCampaign(string Id,int CreatedUserId, string CampaignName, string ReferenceNumber, string Pwd, bool IsActive)
        {
            riddata obj = new riddata();
            riddata obj1 = new riddata();
            CampaignView1 objc = new CampaignView1();
            try
            {
                //string fields = "id,ReferenceNumber,isactive";
                //string parameters = new StreamReader(parameter).ReadToEnd(); //email1;
                //JObject jObject = JObject.Parse(parameter);
                //int id = (int)jObject["id"];
                //string ReferenceNumber = (string)jObject["ReferenceNumber"];
                //string Pwd = (string)jObject["Pwd"];
                //bool IsActive = (bool)jObject["IsActive"];
                if (Session["id"] != null)
                {
                    obj = dc.riddatas.Where(r => r.ReferenceNumber == ReferenceNumber).SingleOrDefault();
                    //obj = (from rid in dc.riddatas
                    //       where rid.ReferenceNumber == ReferenceNumber
                    //       select rid).SingleOrDefault();
                    // bool isReferenceNumberExists = new OperationsBO().CheckReferenceNumber(ReferenceNumber);
                    if (obj != null)
                    {
                        riddata objr=new riddata();
                        if (obj.FK_ClientId == CreatedUserId)
                        {
                            objbo.UpdateCampaign(CreatedUserId, ReferenceNumber, CampaignName, Pwd, IsActive);

                        }
                        else if (obj.FK_ClientId != CreatedUserId)
                        {
                            objr = dc.riddatas.Where(r => r.CampaignName == CampaignName && r.FK_ClientId == CreatedUserId).SingleOrDefault();
                            if (objr == null)
                                objbo.UpdateCampaign(CreatedUserId, ReferenceNumber, CampaignName, Pwd, IsActive);
                            else
                            {
                                Error obj_err = new Error();
                                Errormessage errmesobj = new Errormessage();
                                errmesobj.message = "CampaignName Already Exists for this client.";
                                obj_err.error = errmesobj;
                                Response.StatusCode = 401;
                                return Json(obj_err, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    //else
                    // obj = obj1;
                       
                   
                    objc = (from c in dc.riddatas
                            join c1 in dc.clients on c.FK_ClientId equals c1.PK_ClientID
                            where c.ReferenceNumber == ReferenceNumber
                            select new CampaignView1()
                            {
                                Id = c.PK_Rid,
                                ReferenceNumber = c.ReferenceNumber,
                                CampaignName = c.CampaignName,
                                HasPassword = (c.Pwd != null && c.Pwd != string.Empty) ? true : false,
                                //pwd = r.Pwd,
                                IsActive = c.IsActive,
                                CreatedDateStr = c.CreatedDate,
                                CreatedUserId = c1.PK_ClientID,
                                CreatedUserEmail = c1.Email,
                                CreatedUserName = c1.UserName,
                                CreatedUserActiveState = c1.IsActive
                            }).SingleOrDefault();
                    objc.CreatedDate = objc.CreatedDateStr.Value.ToString("yyyy-MM-ddThh:mm:ss");
                    return Json(objc, JsonRequestBehavior.AllowGet);
                    //     return Json(
                    //new CampaignView1()
                    //{

                    //ReferenceNumber=obj.ReferenceNumber,
                    //IsActive=obj.IsActive}
                    //, JsonRequestBehavior.AllowGet
                    //);
                }
                else
                {
                    Error obj_err = new Error();
                    Errormessage errmesobj = new Errormessage();
                    errmesobj.message = "Session Expired.";
                    obj_err.error = errmesobj;
                    Response.StatusCode = 401;
                    return Json(obj_err, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        // public JsonResult DeleteCampaign([FromBody]JToken jObject)
        [System.Web.Mvc.HttpPost]
        public JsonResult DeleteCampaign(string ReferenceNumber)
        {
            riddata obj = new riddata();
            riddata obj1 = new riddata();
            try
            {
                //string fields = "id,ReferenceNumber,isactive";
                //string parameters = new StreamReader(parameter).ReadToEnd(); //email1;
                //JObject jObject = JObject.Parse(parameter);
                //int id = (int)jObject["id"];
                //string ReferenceNumber = (string)jObject["ReferenceNumber"];
                //string Pwd = (string)jObject["Pwd"];
                //bool IsActive = (bool)jObject["IsActive"];
                //obj = dc.riddatas.Where(r => r.ReferenceNumber == ReferenceNumber).Select(r=>r).SingleOrDefault();
                if (Session["id"] != null)
                {
                    obj = dc.riddatas.Where(r => r.ReferenceNumber == ReferenceNumber).SingleOrDefault();

                    //bool isReferenceNumberExists = new OperationsBO().CheckReferenceNumber(ReferenceNumber);
                    if (obj != null && obj.IsActive == true)
                    {
                        obj.IsActive = false;
                        obj.UpdatedDate = DateTime.UtcNow;
                        dc.SaveChanges();

                    }
                    else
                    {
                        obj = obj1;
                    }
                    return Json(
             new
             {
                 referencenumber = obj.ReferenceNumber,
                 IsActive = obj.IsActive
             }
             , JsonRequestBehavior.AllowGet
             );
                    //return Json(obj, JsonRequestBehavior.AllowGet);
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
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetBatchStatus(int BatchID)
        {
            batchuploaddata objb = dc.batchuploaddatas.Where(x => x.PK_Batchid == BatchID).SingleOrDefault();
            BatchStatus objs = new BatchStatus();
            if(objb!=null)
            {
                //if (objb.Status == "Not Started" || objb.Status=="In Progress"|| objb.Status == "Insertion Completed" || objb.Status=="Completed")
                //{
                    objs.Status = objb.Status;
                    objs.BatchID = objb.PK_Batchid;
                //}
                
            }
            return Json(objs,JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBatchIDs(string ReferenceNumber)
        {
            try
            {
                List<BatchIDList> objbs = new List<BatchIDList>();
                List<batchuploaddata> objb = new List<batchuploaddata>();

                if (Session["userdata"] != null)
                {
                    int currentuserid = Helper.CurrentUserId;
                    string role = Helper.CurrentUserRole;
                    if (role.ToLower() == "admin")
                    {
                        objb = dc.batchuploaddatas.Where(x => x.ReferenceNumber == ReferenceNumber).ToList();

                    }
                    else
                        objb = dc.batchuploaddatas.Where(x => x.FK_ClientID == currentuserid && x.ReferenceNumber == ReferenceNumber).ToList();
                    if (objb.Count != 0)
                    {
                        objbs = (from b in objb
                                 select new BatchIDList()
                                 {
                                     CreatedDate = b.CreatedDate.Value.ToString("yyyy-MM-ddThh:mm:ss"),
                                     BatchID = b.PK_Batchid,
                                     BatchName = b.BatchName,
                                     Status = b.Status
                                 }).ToList();
                    }
                    return Json(objbs, JsonRequestBehavior.AllowGet);
                }
                return Json(objbs, JsonRequestBehavior.AllowGet);
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
        public void ExportAnalytics(string ReferenceNumber)
        {
            try
            {
            riddata objr = dc.riddatas.Where(r => r.ReferenceNumber == ReferenceNumber).SingleOrDefault();
            if(objr!=null)
            {
                List<ExportAnalyticsData> objexport = (from s in dc.shorturldatas
                                                       join u in dc.uiddatas on s.FK_Uid equals u.PK_Uid
                                                       join r in dc.riddatas on u.FK_RID equals r.PK_Rid
                                                       join c in dc.clients on r.FK_ClientId equals c.PK_ClientID
                                                       where s.FK_RID == objr.PK_Rid && s.FK_ClientID == objr.FK_ClientId
                                                       select new ExportAnalyticsData()
                                                       {
                                          CampaignName=r.CampaignName,
                                          Mobilenumber=u.MobileNumber,
                                          ShortURL=s.Req_url,
                                          LongUrl=u.Longurl,
                                          GoogleMapUrl = "https://www.google.com/maps?q=loc:"+s.Latitude+","+s.Longitude,
                                          IPAddress=s.Ipv4,
                                          Browser=s.Browser,
                                          BrowserVersion=s.Browser_version,
                                          City=s.City,
                                          Region=s.Region,
                                          Country=s.Country,
                                          CountryCode=s.CountryCode,
                                          PostalCode=s.PostalCode,
                                          Lattitude=s.Latitude,
                                          Longitude=s.Longitude,
                                          MetroCode=s.MetroCode,
                                          DeviceName=s.DeviceName,
                                          DeviceBrand=s.DeviceBrand,
                                          OS_Name=s.OS_Name,
                                          OS_Version=s.OS_Version,
                                          IsMobileDevice=s.IsMobileDevice,
                                          CreatedDate=s.CreatedDate.ToString(),
                                          clientName=c.UserName


                                                       }
                                                         ).ToList();
                string filename = objr.CampaignName + DateTime.UtcNow;
                string attachment = "attachment; filename=" + filename + ".csv";
                HttpContext.Response.Clear();
                HttpContext.Response.ClearHeaders();
                HttpContext.Response.ClearContent();
                HttpContext.Response.AddHeader("content-disposition", attachment);
                HttpContext.Response.ContentType = "text/csv";
                HttpContext.Response.AddHeader("Pragma", "public");
                objbo.WriteColumnName_Export();
                foreach (ExportAnalyticsData export in objexport)
                {
                    objbo.WriteUserInfo_Export(export);
                }
                HttpContext.Response.End();
            }
             }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
            }
        }
        public void GetBatchDownloadedFile(int BatchID)
        {
            try { 
             batchuploaddata objb = dc.batchuploaddatas.Where(x => x.PK_Batchid == BatchID).SingleOrDefault();
            if (objb != null)
            {
                string host = ConfigurationManager.AppSettings["ShortenurlHost"].ToString();
                List<BatchDownload> objd = (from u in dc.uiddatas
                                            where u.FK_Batchid == objb.PK_Batchid
                                            select new BatchDownload()
                                            {
                                                Mobilenumber = u.MobileNumber,
                                                ShortUrl=host+u.UniqueNumber
                                                //ShortUrl="https://g0.pe/" + u.UniqueNumber
                                            }).ToList();
                //var grid = new System.Web.UI.WebControls.GridView();
                string filename = objb.BatchName;
                //export data in excel format
                //grid.DataSource = objd;
                //grid.DataBind();
                //Response.ClearContent();
                //Response.AddHeader("content-disposition", "attachment; filename=" + filename + ".xls");
                //Response.ContentType = "application/excel";
                //StringWriter sw = new StringWriter();
                //HtmlTextWriter htw = new HtmlTextWriter(sw);
                //grid.RenderControl(htw);
                //Response.Write(sw.ToString());
                //Response.End();

                //export data in csv format
                string attachment = "attachment; filename=" + filename + ".csv";
                HttpContext.Response.Clear();
                HttpContext.Response.ClearHeaders();
                HttpContext.Response.ClearContent();
                HttpContext.Response.AddHeader("content-disposition", attachment);
                HttpContext.Response.ContentType = "text/csv";
                HttpContext.Response.AddHeader("Pragma", "public");
                objbo.WriteColumnName_Download();
                foreach (BatchDownload person in objd )
                {
                    objbo.WriteUserInfo_Download(person);
                }
                HttpContext.Response.End();
            }
            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
            }
        }
        
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }
        public void FillHashId(int from, int to)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("PK_Hash_ID", typeof(int)));
            dt.Columns.Add(new DataColumn("HashID", typeof(string)));
            if (from < to)
            {
                for (int i = from; i < to; i++)
                {

                    string referencenumber = Helper.GetHashID(i);
                    DataRow dr = dt.NewRow();
                    dr["PK_Hash_ID"] = i;
                    dr["HashID"] = referencenumber;
                    dt.Rows.Add(dr);
                    //HashIDList hs = new HashIDList();
                    //hs.HashID = referencenumber;
                    //dc.HashIDLists.Add(hs);
                    //dc.SaveChanges();
                }

                //string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;

                //MySqlConnection conn = new MySqlConnection(connStr);
                //MySqlBulkLoader bulkCopy = new MySqlBulkLoader(conn);

                //bulkCopy.Timeout = 10000; // in seconds
                //bulkCopy.TableName = "HashIDList";
                //bulkCopy.Load();
                //bulkCopy.WriteToServer(dt);
                string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;
                string path_tmp = Path.Combine(Server.MapPath("~/UploadFiles"),
                                       "tmp_mysqluploader.txt");
                // + Path.GetExtension(UploadFile.FileName)
               System.IO.File.Create(path_tmp).Dispose();
                StreamWriter swExtLogFile = new StreamWriter(path_tmp);
                int i1;
                //swExtLogFile.Write(Environment.NewLine);
                List<string> columnslist=new List<string>();
                columnslist.Add("PK_Hash_ID");
                swExtLogFile.Write("PK_Hash_ID");
                swExtLogFile.Write(",");
                swExtLogFile.Write("HashID");
                swExtLogFile.Write(Environment.NewLine);
                foreach (DataRow row in dt.Rows)
                {
                    object[] array = row.ItemArray;
                    for (i1 = 0; i1 < array.Length - 1; i1++)
                    {
                        swExtLogFile.Write(array[i1].ToString() + ",");
                        
                    }
                    swExtLogFile.WriteLine(array[i1].ToString());
                }
                //swExtLogFile.Write("*****END OF DATA****" + DateTime.Now.ToString());
                swExtLogFile.Flush();
                swExtLogFile.Close();
                
                //string strFile = @"C:\Users\yasodha\Documents\Visual Studio 2013\Projects\Yasodha Shorten URL documents\mysqlproj\surl2\Analytics\UploadFiles\tmp_mysqluploader.txt";
               
                MySqlConnection connection = new MySqlConnection(connStr);
                MySqlBulkLoader bl = new MySqlBulkLoader(connection);
                //var connection = myConnection as MySqlConnection;
                //var bl = new MySqlBulkLoader(connection);
                bl.TableName = "hashidlist";
                bl.FieldTerminator = ",";
                bl.LineTerminator = swExtLogFile.NewLine;
                bl.FileName = path_tmp;
                bl.NumberOfLinesToSkip = 1;
                bl.Columns.Add("PK_Hash_ID");
                bl.Columns.Add("HashID");
                var inserted = bl.Load();

            }
        }
        public void FillGeoLiteDb1()
        {
            DataTable dt = new DataTable();
            string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;
            
            string path_tmp = Path.Combine(Server.MapPath("~/UploadFiles"),
                                       "GeoLiteCityLocation.CSV");
            dt = ConvertCSVtoDataTable(path_tmp);
            
            string path_tmp1 = Path.Combine(Server.MapPath("~/UploadFiles"),
                                      "GeoLiteCityLocation1.CSV");
            System.IO.File.Create(path_tmp1).Dispose();
            StreamWriter swExtLogFile = new StreamWriter(path_tmp1);
            int i1;

            //List<string> columnslist = new List<string>();
            //columnslist.Add("PK_Hash_ID");

            swExtLogFile.Write("locId,");
            swExtLogFile.Write("country,");
            swExtLogFile.Write("region,");
            swExtLogFile.Write("city,");
            swExtLogFile.Write("postalCode,");
            swExtLogFile.Write("latitude,");
            swExtLogFile.Write("longitude,");
            swExtLogFile.Write("metroCode,");
            swExtLogFile.Write("areaCode");
            swExtLogFile.Write(Environment.NewLine);
            foreach (DataRow row in dt.Rows)
            {
                object[] array = row.ItemArray;
                for (i1 = 0; i1 < array.Length - 1; i1++)
                {
                    swExtLogFile.Write(array[i1].ToString() + " , ");

                }
                swExtLogFile.WriteLine(array[i1].ToString());
            }
            //swExtLogFile.Write("*****END OF DATA****" + DateTime.Now.ToString());
            swExtLogFile.Flush();
            swExtLogFile.Close();

            //string strFile = @"C:\Users\yasodha\Documents\Visual Studio 2013\Projects\Yasodha Shorten URL documents\mysqlproj\surl2\Analytics\UploadFiles\tmp_mysqluploader.txt";

            MySqlConnection connection = new MySqlConnection(connStr);
            MySqlBulkLoader bl = new MySqlBulkLoader(connection);
            //var connection = myConnection as MySqlConnection;
            //var bl = new MySqlBulkLoader(connection);
            bl.TableName = "locations_data";
            bl.FieldTerminator = ",";
            bl.LineTerminator = swExtLogFile.NewLine;
            bl.FileName = path_tmp1;
            bl.NumberOfLinesToSkip = 1;

            bl.Columns.Add("locId");
            bl.Columns.Add("country");
            bl.Columns.Add("region");
            bl.Columns.Add("city");
            bl.Columns.Add("postalCode");
            bl.Columns.Add("latitude");
            bl.Columns.Add("longitude");
            bl.Columns.Add("metroCode");
            

            var inserted = bl.Load();

        }
        public void FillGeoLiteDb()
        {
           DataTable dt = new DataTable();
                string connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;
                string path_tmp = Path.Combine(Server.MapPath("~/UploadFiles"),
                                       "GeoLiteCityBlocks.CSV");
                
               dt= ConvertCSVtoDataTable(path_tmp);
               string path_tmp1 = Path.Combine(Server.MapPath("~/UploadFiles"),
                                          "GeoLiteCityBlocks1.CSV");
               
                System.IO.File.Create(path_tmp1).Dispose();
                StreamWriter swExtLogFile = new StreamWriter(path_tmp1);
                int i1;
                
                //List<string> columnslist = new List<string>();
                //columnslist.Add("PK_Hash_ID");

                swExtLogFile.Write("startIpNum,");
                swExtLogFile.Write("endIpNum,");
                swExtLogFile.Write("locId");
                
                swExtLogFile.Write(Environment.NewLine);
                foreach (DataRow row in dt.Rows)
                {
                    object[] array = row.ItemArray;
                    for (i1 = 0; i1 < array.Length - 1; i1++)
                    {
                        swExtLogFile.Write(array[i1].ToString() + " , ");

                    }
                    swExtLogFile.WriteLine(array[i1].ToString());
                }
                //swExtLogFile.Write("*****END OF DATA****" + DateTime.Now.ToString());
                swExtLogFile.Flush();
                swExtLogFile.Close();

                //string strFile = @"C:\Users\yasodha\Documents\Visual Studio 2013\Projects\Yasodha Shorten URL documents\mysqlproj\surl2\Analytics\UploadFiles\tmp_mysqluploader.txt";

                MySqlConnection connection = new MySqlConnection(connStr);
                MySqlBulkLoader bl = new MySqlBulkLoader(connection);
                //var connection = myConnection as MySqlConnection;
                //var bl = new MySqlBulkLoader(connection);
                bl.TableName = "master_location";
                bl.FieldTerminator = ",";
                bl.LineTerminator = swExtLogFile.NewLine;
                bl.FileName = path_tmp1;
                bl.NumberOfLinesToSkip = 1;
                bl.Columns.Add("startIpNum");
                bl.Columns.Add("endIpNum");
                bl.Columns.Add("locId");
                var inserted = bl.Load();

            }
        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            StreamReader sr = new StreamReader(strFilePath);
            string[] headers = sr.ReadLine().Split(',');
            DataTable dt = new DataTable();
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!sr.EndOfStream)
            {
                string[] rows = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                DataRow dr = dt.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    
                    dr[i] = rows[i].Replace('"', ' ').Trim();
                }
                dt.Rows.Add(dr);
            }
            return dt;
        } 
       
        [System.Web.Http.HttpPost]
        public JsonResult UploadData(string[] MobileNumbers, string LongURL, string ReferenceNumber, string type, HttpPostedFileBase UploadFile)
       {
               
            try
            {
                exportDataModel obje = new exportDataModel();
               
                
                List<string> MobileNumbersList = new List<string>();
                List<string> MobileNumbersFiltered_List = new List<string>();

                List<string> shorturls = new List<string>();
                List<string> outputdat = new List<string>();
                List<int> pkuids = new List<int>();
                string Hashid;

                //FillHashId(1, 1000000);
                //FillHashId(1000000, 2000000);
                //FillHashId(2000000, 3000000);
                //FillHashId(3000000, 4000000);
                //FillHashId(4000000, 5000000);
                //FillHashId(5000000, 6000000);
                //FillHashId(6000000, 7000000);
                //FillHashId(7000000, 8000000);
                //FillHashId(8000000, 9000000);
                //FillHashId(9000000, 10000000);

                //FillGeoLiteDb();
                //FillGeoLiteDb1();

                
                riddata objrid = (from registree in dc.riddatas
                                  where registree.ReferenceNumber.Trim() == ReferenceNumber.Trim()
                                  select registree).SingleOrDefault();
                if (type.ToLower() == "simple" && objrid != null)
                {
                    string mobilenumber = MobileNumbers[0];
                    uiddata objc = new uiddata();
                    uiddata objc1 = dc.uiddatas.Where(u => u.MobileNumber == mobilenumber && u.ReferenceNumber == ReferenceNumber && u.Longurl == LongURL).SingleOrDefault();
                    if (objc1 == null)
                    {
                        objc.MobileNumber= mobilenumber;
                        objc.ReferenceNumber = ReferenceNumber;
                        objc.Longurl = LongURL;
                        objc.FK_ClientID = objrid.FK_ClientId;
                        objc.FK_RID = objrid.PK_Rid;
                        objc.CreatedDate = DateTime.UtcNow;
                        objc.CreatedBy = Helper.CurrentUserId;
                        objc.FK_Batchid=0;
                        dc.uiddatas.Add(objc);
                        dc.SaveChanges();
                        uiddata objuid = dc.uiddatas.Where(u => u.MobileNumber == mobilenumber && u.ReferenceNumber == ReferenceNumber && u.Longurl == LongURL).SingleOrDefault();
                        //Hashid = Helper.GetHashID(objuid.PK_Uid);
                        Hashid = dc.hashidlists.Where(h => h.PK_Hash_ID == objuid.PK_Uid).Select(x => x.HashID).SingleOrDefault();
                        objbo.UpdateHashid(objuid.PK_Uid, Hashid);
                        obje.MobileNumber = mobilenumber;
                        //obje.ShortenUrl = "https://g0.pe/" + Hashid;
                        obje.ShortenUrl = ConfigurationManager.AppSettings["ShortenurlHost"].ToString() + Hashid;
                        obje.CreatedDate = objuid.CreatedDate;
                        obje.Status = "Successfully Uploaded.";
                        //return Json(obje, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        obje.MobileNumber = mobilenumber;
                        obje.ShortenUrl = "cannt be generated.";
                        obje.Status = "MobileNumber and LongUrl already added for this campaign.";
                        //return Json(obje, JsonRequestBehavior.AllowGet);

                    }

                }
                else if (type.ToLower() == "advanced" && objrid != null)
                {
                    string batchname = objrid.CampaignName + "_" + DateTime.UtcNow;
                    string formated_mobilenumbers = String.Join(",", MobileNumbers);
                    List<string> MobileNumbers_List = MobileNumbers.ToList();
                        batchuploaddata objb = new batchuploaddata();
                        objb.ReferenceNumber = ReferenceNumber;
                        objb.MobileNumber = formated_mobilenumbers;
                        objb.Longurl = LongURL;
                        objb.FK_ClientID = objrid.FK_ClientId;
                        objb.FK_RID = objrid.PK_Rid;
                        objb.CreatedDate = DateTime.UtcNow;
                        objb.CreatedBy = Helper.CurrentUserId.ToString();
                        objb.Status = "Not Started";
                        objb.BatchName = batchname;
                        objb.BatchCount = MobileNumbers.Count();
                        dc.batchuploaddatas.Add(objb);
                        dc.SaveChanges();
                    batchuploaddata objo = dc.batchuploaddatas.Where(x => x.BatchName == batchname).SingleOrDefault();
                    string result = objbo.BulkUploaduiddata(ReferenceNumber, LongURL, objo.PK_Batchid, objrid, MobileNumbers_List,"");
                    if (result != null)
                    {
                        objo.Status = "Completed";
                        dc.SaveChanges();
                       
                        //obje.Status = objo.BatchName + " Created.Revert Back to you once upload has done.";
                        obje.Status = objo.BatchName + " Completed.";
                        obje.BatchID = objo.PK_Batchid;
                        obje.CreatedDate = objo.CreatedDate;
                        
                    }

                }
                else if (type.ToLower() == "upload"  && objrid != null)
              //else if (type.ToLower() == "upload" && UploadFile != null && UploadFile.ContentLength > 0 && objrid != null)
                {
                    string path = Path.Combine(Server.MapPath("~/UploadFiles"),
                                       Path.GetFileName(UploadFile.FileName));
                   // + Path.GetExtension(UploadFile.FileName)
                    UploadFile.SaveAs(path);

                    string path_tmp = Path.Combine(Server.MapPath("~/UploadFiles"),
                                       "tmp_mysqluploader.txt");
                    // + Path.GetExtension(UploadFile.FileName)
                    System.IO.File.Create(path_tmp).Dispose();
                    //string path = Server.MapPath("~/UploadFiles/971505878339_5K_MANGED1.txt");
                    FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                    byte[] data = new byte[fs.Length];
                    fs.Read(data, 0, data.Length);
                    fs.Close();
                    if (data.Length > 0)
                    {
                        string fileData = System.Text.Encoding.UTF8.GetString(data);
                        string pattern = @",|\s";//here \s equals [ \f\n\r\t\v]
                        //string pattern = @"\.*?\";
                        if (Regex.IsMatch(fileData, pattern, RegexOptions.Multiline))
                        {
                            List<string> SplitedData = Regex.Split(fileData, pattern).ToList();
                            SplitedData = SplitedData.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
                            if (SplitedData[0].ToLower().Contains("mobile") || SplitedData[0].ToLower().Contains("number") || SplitedData[0].ToLower().Contains("mobilenumber") || SplitedData[0].ToLower().Contains("mobile number"))
                            {
                                SplitedData.Remove(SplitedData[0]);
                                if (SplitedData[0].ToLower().Contains("number"))
                                    SplitedData.Remove(SplitedData[0]);

                            }
                            var mobilenumbersstr = String.Join(",", SplitedData);
                            string batchname = objrid.CampaignName + "_" + DateTime.UtcNow;

                            batchuploaddata objb = new batchuploaddata();
                            objb.ReferenceNumber = ReferenceNumber;
                            objb.MobileNumber = mobilenumbersstr;
                            objb.Longurl = LongURL;
                            objb.FK_ClientID = objrid.FK_ClientId;
                            objb.FK_RID = objrid.PK_Rid;
                            objb.CreatedDate = DateTime.UtcNow;
                            objb.CreatedBy = Helper.CurrentUserId.ToString();
                            objb.Status = "Not Started";
                            objb.BatchName = batchname;
                            objb.BatchCount = SplitedData.Count();
                            dc.batchuploaddatas.Add(objb);
                            dc.SaveChanges();
                            batchuploaddata objo = dc.batchuploaddatas.Where(x => x.BatchName == batchname).SingleOrDefault();
                            string result = objbo.BulkUploaduiddata(ReferenceNumber, LongURL, objo.PK_Batchid, objrid, SplitedData, path_tmp);
                            if (result == "Successfully Uploaded.")
                            {
                                objo.Status = "Completed";
                                dc.SaveChanges();
                                if (objo != null)
                                {
                                    //obje.Status = objo.BatchName + " Created.Revert Back to you once upload has done.";
                                    obje.Status = objo.BatchName + " Completed.";
                                    obje.BatchID = objo.PK_Batchid;
                                    obje.CreatedDate = objo.CreatedDate;
                                    
                                }
                            }
                            else if (result == "File already uploaded.")
                            {
                                Error erobj = new Error();
                                Errormessage ermessage = new Errormessage();
                                ermessage.message = "File already uploaded.";
                                erobj.error = ermessage;
                                return Json(erobj, JsonRequestBehavior.AllowGet);


                            }
                            else if(result==null)
                            {
                                dc.batchuploaddatas.Remove(objb);
                                dc.SaveChanges();
                                Error erobj = new Error();
                                Errormessage ermessage = new Errormessage();
                                ermessage.message = "File already uploaded.";
                                erobj.error = ermessage;
                                return Json(erobj, JsonRequestBehavior.AllowGet);
                            }
                            if ((System.IO.File.Exists(path)))
                            {
                                System.IO.File.Delete(path);
                            }
                            if ((System.IO.File.Exists(path_tmp)))
                            {
                                System.IO.File.Delete(path_tmp);
                            }
                        }
                        else
                        {
                            if ((System.IO.File.Exists(path)))
                            {
                                System.IO.File.Delete(path);
                            }
                            if ((System.IO.File.Exists(path_tmp)))
                            {
                                System.IO.File.Delete(path_tmp);
                            }
                            Error erobj = new Error();
                            Errormessage ermessage = new Errormessage();
                            ermessage.message = "Invalid Input file.";
                            erobj.error = ermessage;
                            return Json(erobj, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        if ((System.IO.File.Exists(path)))
                        {
                            System.IO.File.Delete(path);
                        }
                        if ((System.IO.File.Exists(path_tmp)))
                        {
                            System.IO.File.Delete(path_tmp);
                        }
                        Error erobj = new Error();
                        Errormessage ermessage = new Errormessage();
                        ermessage.message = "Invalid Input file.";
                        erobj.error = ermessage;
                        return Json(erobj, JsonRequestBehavior.AllowGet);
                    }
                    //string extension = Path.GetExtension(file.FileName);
                    //string fileName = Guid.NewGuid().ToString().Substring(0, 25) + extension;
                    //file.SaveAs(Server.MapPath("~/UploadFiles/" + fileName));
                    //file.SaveAs(Path.Combine(Server.MapPath("/uploads"), Guid.NewGuid() + Path.GetExtension(file.FileName)));
                }
                return Json(obje, JsonRequestBehavior.AllowGet);
            }
               
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
                return null;
            }
        }
        

        public void UploadData1(string[] MobileNumbers,string LongURL,string ReferenceNumber,string type)
         {
            int clientid = 0; int rid = 0;
            var mobilenumbers = "{\"MobileNumbers\":[\"8331877564\",\"9848745783\"]}";
            ReferenceNumber = "50793";
            LongURL = "google.com";
            JavaScriptSerializer ser = new JavaScriptSerializer();
            List<string> MobileNumbersList = new List<string>();
            List<string> MobileNumbersFiltered_List = new List<string>();

            List<string> shorturls = new List<string>();
            List<string> outputdat = new List<string>();
            List<int> pkuids = new List<int>();
            string Hashid;
            MobileNumbersList MobileNumbersList1 = ser.Deserialize<MobileNumbersList>(mobilenumbers);
            MobileNumbersList = MobileNumbersList1.MobileNumbers;
            //clientid = 2;
            //rid = 41;
           
            riddata objrid = (from registree in dc.riddatas
                              where registree.ReferenceNumber.Trim() == ReferenceNumber.Trim()
                              select registree).SingleOrDefault();
            if (objrid != null)
            {
                clientid = objrid.FK_ClientId;
                rid = objrid.PK_Rid;
                List<string> mobilenumberdata = dc.uiddatas.AsNoTracking().Where(x => MobileNumbersList.Contains(x.MobileNumber) && x.ReferenceNumber == ReferenceNumber && x.FK_RID == rid && x.FK_ClientID == clientid && x.Longurl == LongURL).Select(r=>r.MobileNumber).ToList();
                if (mobilenumberdata.Count != 0)
                {
                    MobileNumbersFiltered_List = MobileNumbersList.Except(mobilenumberdata).ToList();
                }
                else
                { MobileNumbersFiltered_List = MobileNumbersList; }
                if (MobileNumbersFiltered_List.Count > 0)
                {
                    foreach (string m in MobileNumbersFiltered_List)
                    {
                        new DataInsertionBO().Insertuiddata(rid, clientid, ReferenceNumber, LongURL, m);
                    }

                    pkuids = dc.uiddatas.AsNoTracking().Where(x => MobileNumbersFiltered_List.Contains(x.MobileNumber) && x.ReferenceNumber == ReferenceNumber && x.FK_RID == rid && x.FK_ClientID == clientid && x.Longurl == LongURL).Select(r => r.PK_Uid).ToList();
                    foreach (int uid in pkuids)
                    {
                        Hashid = "";
                        Hashid = Helper.GetHashID(uid);
                        objbo.UpdateHashid(uid, Hashid);
                        // shorturls.Add("https://g0.pe/" + Hashid);
                    }
                }
                List<exportDataModel> exportdata = dc.uiddatas.AsNoTracking().Where(x => MobileNumbersList.Contains(x.MobileNumber) && x.ReferenceNumber == ReferenceNumber && x.FK_RID == rid && x.FK_ClientID == clientid && x.Longurl == LongURL).Select(r=>new exportDataModel(){MobileNumber=r.MobileNumber,ShortenUrl=r.UniqueNumber}).ToList();
                exportdata = exportdata.Select(x => { x.ShortenUrl = "https://g0.pe/" + x.ShortenUrl; return x; }).ToList();
                //DataTable dt = new DataTable();
                //dt.Columns.Add("Mobilenumber");
                //dt.Columns.Add("ShortUrl");
                //foreach (exportDataModel e in exportdata)
                //{
                //    dt.Rows.Add(e.MobileNumber, "https://g0.pe/" + e.ShortenUrl);
                //}

                var grid = new System.Web.UI.WebControls.GridView();

                grid.DataSource = exportdata;
                grid.DataBind();
                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename=ShortURLSList.xls");
                Response.ContentType = "application/excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                grid.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();

            }

        }

    }
}