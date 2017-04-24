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
    public class CustomerController : Controller
    {
        shortenurlEntities dc = new shortenurlEntities();

        public JsonResult GetAPIKEY(int? clientid)
        {
            string API_KEY = "";
            client_API_KEY obj = new client_API_KEY();
            if(clientid!=0&&clientid!=null)
            {
                API_KEY = dc.clients.Where(c => c.PK_ClientID == clientid).Select(a=>a.APIKey).SingleOrDefault();
            }
            else if(Helper.CurrentUserId!=0)
            {
                API_KEY = dc.clients.Where(c => c.PK_ClientID == Helper.CurrentUserId).Select(a => a.APIKey).SingleOrDefault();

            }
            obj.API_KEY = API_KEY;
            return Json(obj,JsonRequestBehavior.AllowGet);
        }
        // GET: Customer
        public JsonResult Index(string id)
        {
            //    return View();
            //}
            //public JsonResult GetclientDetails()
            //{
            List<clientView1> objc = new List<clientView1>();
            clientView1 obj = new clientView1();
            int id1 = Convert.ToInt32(id);
            try
            {
                if (Session["id"] != null)
                {
                    if (id == null && Session["id"] != null)
                    {
                        //get all client detials
                        objc = (from c in dc.clients
                                select new clientView1
                                {
                                    id = c.PK_ClientID,
                                    UserName = c.UserName,
                                    Email = c.Email,
                                    IsActive = c.IsActive
                                    //Password = c.Password
                                }).ToList();
                        return Json(objc, JsonRequestBehavior.AllowGet);

                    }
                    else if (id != null && Session["id"] != null)
                    {
                        obj = (from c in dc.clients
                               where c.PK_ClientID == id1
                               select new clientView1
                               {
                                   id = c.PK_ClientID,
                                   UserName = c.UserName,
                                   Email = c.Email,
                                   IsActive = c.IsActive
                                   //Password = c.Password
                               }).SingleOrDefault();
                        return Json(obj, JsonRequestBehavior.AllowGet);

                    }


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
                return Json(objc, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Search(string username, string email, bool? isactive)
        {
            List<clientView1> obj_search = new List<clientView1>();
            string isactivestr = Convert.ToString(isactive);
            try
            {
                //string username = Helper.CurrentUserName;
                //string email = Helper.CurrentUseremail;
                //bool isactive = Helper.CurrentUserActiveStatus;
                if (username != null && (email == "" || email == null) && (isactivestr == "" || isactivestr == null) && Session["id"] != null)
                {
                    obj_search = (from c in dc.clients
                                  where c.UserName.Contains(username.ToString())
                                  select new clientView1()
                                  {
                                      id = c.PK_ClientID,
                                      UserName = c.UserName,
                                      Email = c.Email,
                                      IsActive = c.IsActive
                                  }).ToList();
                }
                else if (email != null && (username == null || username == "") && (isactivestr == null || isactivestr == "") && Session["id"] != null)
                {
                    obj_search = (from c in dc.clients
                                  where c.Email.Contains(email.ToString())
                                  select new clientView1()
                                  {
                                      id = c.PK_ClientID,
                                      UserName = c.UserName,
                                      Email = c.Email,
                                      IsActive = c.IsActive
                                  }).ToList();
                }
                else if (isactivestr != null && (username == null || username == "") && (email == null || email == "") && Session["id"] != null)
                {
                    obj_search = (from c in dc.clients
                                  where c.IsActive == isactive
                                  select new clientView1()
                                  {
                                      id = c.PK_ClientID,
                                      UserName = c.UserName,
                                      Email = c.Email,
                                      IsActive = c.IsActive
                                  }).ToList();
                }
                return Json(obj_search, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
                Error obj_err = new Error();
                Errormessage errmesobj = new Errormessage();
                errmesobj.message = "Exception occur.";
                obj_err.error = errmesobj;

                return Json(obj_err, JsonRequestBehavior.AllowGet);
                //return Json(obj_search,JsonRequestBehavior.AllowGet);

            }
        }


        [System.Web.Mvc.HttpPost]
        public JsonResult Addclient(string UserName, string Email, string Password, bool IsActive)
        //public JsonResult Addclient([FromBody]JToken jObject)
        {
            client obj = new client();
            try
            {
                //string fields = "UserName,Email,IsActive";
                //string UserName = (string)jObject["UserName"];
                //string Email = (string)jObject["Email"];
                //string Password = (string)jObject["Password"];
                //bool IsActive = (bool)jObject["IsActive"];
                client isEmailExists = new OperationsBO().CheckclientEmail(Email, Password);
                //client obj1 = new client();
                if (Session["id"] != null)
                {
                    if (isEmailExists == null && Session["id"] != null)
                    {
                        //add client details
                        string ApiKey = new OperationsBO().GetApiKey();
                        obj.UserName = UserName;
                        obj.Email = Email;
                        obj.Password = Password;
                        obj.APIKey = ApiKey;
                        obj.IsActive = IsActive;
                        obj.Role = "client";
                        obj.CreatedDate = DateTime.Today;
                        dc.clients.Add(obj);
                        dc.SaveChanges();
                    }
                    else
                    {
                        Error obj_err = new Error();
                        Errormessage errmesobj = new Errormessage();
                        errmesobj.message = "client already exists.";
                        obj_err.error = errmesobj;

                        return Json(obj_err, JsonRequestBehavior.AllowGet);
                    }
                    return Json(obj, JsonRequestBehavior.AllowGet);
                    //obj.SetSerializableProperties(fields);
                    //return Json(obj, new Newtonsoft.Json.JsonSerializerSettings()
                    //{
                    //    ContractResolver = new ShouldSerializeContractResolver1()
                    //});

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
                errmesobj.message = "exception occur.";
                obj_err.error = errmesobj;

                return Json(obj_err, JsonRequestBehavior.AllowGet);
                // return Json(obj,JsonRequestBehavior.AllowGet);
            }
        }
        //public JsonResult Updateclient([FromBody]JToken jObject)
        //[System.Web.Mvc.HttpPost]
        public JsonResult Updateclient(int id, string UserName, string Email, bool? IsActive, string Password)
        {
            client obj1 = new client();
            try
            {
                //string id = (string)jObject["id"];
                //string UserName = (string)jObject["UserName"];
                //string Email = (string)jObject["Email"];
                ////string Password = (string)jObject["Password"];
                //bool? IsActive = (bool)jObject["IsActive"];
                // int id1 = Convert.ToInt32(Session["id"]);
                //string fields = "UserName,Email,IsActive";
                //client isEmailExists = new OperationsBO().CheckclientEmail(Email);
                if (Session["id"] != null)
                {
                    client obj = new client();
                    if (Email != null)
                    {
                         obj = dc.clients.Where(c => c.Email == Email).SingleOrDefault();
                        if (obj != null && Session["id"] != null)
                            new OperationsBO().Updateclient(UserName, Email, IsActive,"");
                        else
                            obj = obj1;
                    }
                    if(Password!=null)
                    {
                         obj = dc.clients.Where(c => c.PK_ClientID == id).SingleOrDefault();
                        if (obj != null && Session["id"] != null)
                            new OperationsBO().Updateclient("", obj.Email, IsActive,Password);
                        else
                            obj = obj1;
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
                errmesobj.message = "exception occur.";
                obj_err.error = errmesobj;
                return Json(obj_err, JsonRequestBehavior.AllowGet);
            }

        }

        // public JsonResult Deleteclient([FromBody]JToken jObject)
        [System.Web.Mvc.HttpPost]
        public JsonResult Deleteclient(string Email,string Password)
        {
            client obj1 = new client();
            try
            {
                //string id = (string)jObject["id"];
                //string UserName = (string)jObject["UserName"];
                //string Email = (string)jObject["Email"];
                //string Password = (string)jObject["Password"];
                //bool? IsActive = (bool)jObject["IsActive"];
                //int id1 = Convert.ToInt32(Session["id"]);
                //string fields = "UserName,Email,IsActive";
                if (Session["id"] != null)
                {
                    client obj = new OperationsBO().CheckclientEmail(Email,Password);
                    //client obj = dc.clients.Where(c => c.PK_ClientID == isEmailExists.PK_ClientID).Select(c1 => c1).SingleOrDefault();
                    if (obj != null && Session["id"] != null)
                    {
                        if (obj.IsActive == true)
                        {
                            obj.IsActive = false;
                            obj.UpdatedDate = DateTime.UtcNow;
                            dc.SaveChanges();
                        }
                    }
                    else
                    {
                        obj = obj1;
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
                errmesobj.message = "exception occur.";
                obj_err.error = errmesobj;
                return Json(obj_err, JsonRequestBehavior.AllowGet);
            }
        }
    }
}