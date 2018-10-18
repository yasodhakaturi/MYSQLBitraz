using Analytics.Helpers.BO;
using Analytics.Helpers.Utility;
using Analytics.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Analytics.Controllers
{
    public class AuthController : Controller
    {
        shortenurlEntities dc = new shortenurlEntities();
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }
       
        public JsonResult ReferenceNumberVerification(string ReferenceNumber, string Password)
        {
            try
            {
                CampaignView1 objc = new CampaignView1();
                if (ReferenceNumber != null && ReferenceNumber != "" && Password == null)
                {
                    riddata obj = dc.riddatas.Where(x => x.ReferenceNumber == ReferenceNumber).Select(y => y).SingleOrDefault();
                    if (obj != null)
                    {
                         objc = (from c in dc.riddatas
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
                                              }).SingleOrDefault();
                        objc.CreatedDate = objc.CreatedDateStr.Value.ToString("yyyy-MM-ddThh:mm:ss");
                        return Json(objc, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Error obj_err = new Error();
                        Errormessage errmesobj = new Errormessage();
                        errmesobj.message = "ReferenceNumber Not Found.";
                        obj_err.error = errmesobj;

                        return Json(obj_err, JsonRequestBehavior.AllowGet);
                    }

                }
                else if (ReferenceNumber != null && ReferenceNumber != "" && Password != null && Password != "")
                {
                    riddata obj = dc.riddatas.Where(x => x.ReferenceNumber == ReferenceNumber && x.Pwd == Password).Select(y => y).SingleOrDefault();
                    if (obj != null)
                    {


                         objc = (from c in dc.riddatas
                                              where c.ReferenceNumber == ReferenceNumber && c.Pwd == Password
                                              select new CampaignView1()
                                              {
                                                  Id = c.PK_Rid,
                                                  ReferenceNumber = c.ReferenceNumber,
                                                  CampaignName = c.CampaignName,
                                                  HasPassword = (c.Pwd != null && c.Pwd != string.Empty) ? true : false,
                                                  //pwd = r.Pwd,
                                                  IsActive = c.IsActive,
                                                  CreatedDateStr = c.CreatedDate,
                                              }).SingleOrDefault();
                        objc.CreatedDate = objc.CreatedDateStr.Value.ToString("yyyy-MM-ddThh:mm:ss");
                        return Json(objc, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Error obj_err = new Error();
                        Errormessage errmesobj = new Errormessage();
                        errmesobj.message = "ReferenceNumber and Password doesnot match.";
                        obj_err.error = errmesobj;

                        return Json(obj_err, JsonRequestBehavior.AllowGet);
                    }

                }
                //else
                //{
                //    if (ReferenceNumber == null && Password == null)
                //    {
                //        Error obj_err = new Error();
                //        Errormessage errmesobj = new Errormessage();
                //        errmesobj.message = " pass ReferenceNumber and Password.";
                //        obj_err.error = errmesobj;

                //        return Json(obj_err, JsonRequestBehavior.AllowGet);
                //    }
                //}
                return Json(objc, JsonRequestBehavior.AllowGet);
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
        [System.Web.Http.HttpPost]
        public JsonResult Login(string uname, string password)
     //   [System.Web.Http.HttpPost]
     //public JsonResult Login([FromBody]JToken jObject)    
    {
        //string uname = (string)jObject["uname"];
        //string password = (string)jObject["password"];
            string message = "";
            try
            {
                int loginCount = 0; int checkCount = 0; string contextData = ""; 
                LoginUserInfo obj = new LoginUserInfo();

                if (uname != null && password != null)
                {
                    string credentials = uname + "~" + password + "~";
                    AuthenticateBO objAuthenticateBO = new AuthenticateBO();
                    if (objAuthenticateBO.GetAuthenticateUser(uname, password, out loginCount, out checkCount, out contextData))
                    {
                        // contextData = contextData + "^" + Helper.UrlRef;
                       // byte[] hash = Helper.GetHashKey("yasodha.bitra@gmail.com" + "Analytics");

                        Session["userdata"] = contextData;
                        Session["id"] = Helper.CurrentUserId;
                        UserInfo user_obj = new UserInfo();
                        if (Helper.CurrentUserRole.ToLower() == "admin")
                            obj.redirect_url = "/Admin";
                        else
                            obj.redirect_url = "/Analytics";
                        user_obj.user_id = Helper.CurrentUserId;
                        user_obj.user_name = Helper.CurrentUserName;
                        user_obj.user_role = Helper.CurrentUserRole;
                        obj.user_info = user_obj;
                        return Json(obj, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        Error obj_err = new Error();
                        Errormessage errmesobj = new Errormessage();
                        errmesobj.message = "Authentication Failed";
                        obj_err.error = errmesobj;
                        //Response.StatusCode = 401;
                        return Json(obj_err, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    Error obj_err = new Error();
                    Errormessage errmesobj = new Errormessage();
                    errmesobj.message = "Username and Password should not be null";
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

        public JsonResult UserInfo()
        {
            string message = "";
            try
            {
               if(Session["userdata"]!=null && Session["id"]!=null)
               {
                   UserInfo user_obj = new UserInfo();
                   user_obj.user_id = Helper.CurrentUserId;
                   user_obj.user_name = Helper.CurrentUserName;
                   user_obj.user_role = Helper.CurrentUserRole;
                   return Json(user_obj, JsonRequestBehavior.AllowGet);
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
                message = "Exception Occured";
                return Json(message);
            }
        }
        public JsonResult Logout()
        {
            string message = "";
            try
            {
                if(Session["id"]!=null)
                {
                    Session.Remove("id");
                    Session.Remove("userdata");
                   message="Successfully Logout";
                    return Json(message,JsonRequestBehavior.AllowGet);
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
                message = "Exception Occured";
                return Json(message);
            }
        }


        public JsonResult ReferenceInfo(string rid)
        {
            try
            {
                riddata objrid = dc.riddatas.Where(x => x.ReferenceNumber == rid && x.IsActive == true).Select(y => y).SingleOrDefault();
                Analytics_Share objana = new Analytics_Share();
                if (objrid != null)
                {
                    objana.rid = objrid.ReferenceNumber;
                    if (objrid.Pwd != null)
                        objana.has_authentication = true;
                    else
                        objana.has_authentication = false;
                    if (Session["analyticsshare"] != null)
                        objana.is_authorized = true;
                    else
                        objana.is_authorized = false;
                    return Json(objana, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Error obj_err = new Error();
                    Errormessage errmesobj = new Errormessage();
                    errmesobj.message = "Please Access Correct Campaign Link..";
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

         [System.Web.Http.HttpPost]
        public JsonResult ReferenceInfoPost(string rid, string pwd)
        {
            try
            {
                if (pwd != "")
                {
                    riddata objrid = dc.riddatas.Where(x => x.ReferenceNumber == rid && x.Pwd == pwd && x.IsActive == true).Select(y => y).SingleOrDefault();
                    Analytics_Share objana = new Analytics_Share();
                    if (objrid != null)
                    {
                        objana.rid = objrid.ReferenceNumber;
                        if (objrid.Pwd != null)
                        {
                            objana.has_authentication = true;
                            Session["analyticsshare"] = rid;
                            objana.is_authorized = true;
                        }


                        return Json(objana, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        objana.rid = rid;
                        objana.has_authentication = false;
                        objana.is_authorized = false;

                        return Json(objana, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    Error obj_err = new Error();
                    Errormessage errmesobj = new Errormessage();
                    errmesobj.message = "Please enter password";
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