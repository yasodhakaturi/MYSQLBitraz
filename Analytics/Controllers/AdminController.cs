using Analytics.Helpers.BO;
using Analytics.Helpers.Utility;
using Analytics.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Cors;


namespace Analytics.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]

    public class AdminController : Controller
    {
      shortenurlEntities dc = new shortenurlEntities();

        
        // GET: Admin
      public ActionResult Index()
      {
          UserViewModel obj = new UserViewModel();
          string url = Request.Url.ToString();
          obj = new OperationsBO().GetViewConfigDetails(url);

          return View(obj);
      }
    //    public ActionResult Admin()
    //{
    //       UserViewModel obj = new UserViewModel();
    //       string url = Request.Url.ToString();
    //       obj = new OperationsBO().GetViewConfigDetails(url);

    //      return View(obj);
    //  }
      public ActionResult AdminLogin()
        {
            //string strResult = string.Empty;
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:55492/admin/Users");
            //// HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:2053/Services/MoozupService.svc/StatusUpdate/28879/1134");
            //request.Method = "POST";
            //request.ContentType = "application/json; charset=utf-8";
            //request.KeepAlive = true;
            //request.Timeout = 100000;
            //UTF8Encoding utfenc = new UTF8Encoding();
            ////byte[] bytes = Encoding.ASCII.GetBytes("shivadeepakv@meraevents.com");
            //// byte[] bytes = Encoding.ASCII.GetBytes("this is test status");
            //Stream os = null;
            ////ADDclientView p = new ADDclientView();
            //clientView obj = new clientView();
            //obj.UserName = "chitra";
            ////obj.Password = "chitra";
            //obj.Email = "chitra1@gmail.com";
            ////p.obj = obj;
            //string val = JsonConvert.SerializeObject(obj);
            //byte[] bytes = Encoding.ASCII.GetBytes(val);
            //request.ContentLength = bytes.Length;
            //os = request.GetRequestStream();
            //os.Write(bytes, 0, bytes.Length);
            //HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();
            //Stream responseStream = webResponse.GetResponseStream();
            //StreamReader responseStreamReader = new StreamReader(responseStream);
            //strResult = responseStreamReader.ReadToEnd();//parse token from result
            //responseStreamReader.Close();
            //webResponse.Close(); 
         return View();

        }
      
    //    public string Validate(string email,string password, string chkRemember)
    //    {
    //        try
    //        {
    //            client obj=(from c in dc.clients
    //                            where c.Email==email && c.Password==password
    //                            select c).SingleOrDefault();
    //            if(obj!=null)
    //            return "Success~/../Admin/Index";
    //            else
    //            return "Failed~Wrong Credentials";

    //        }
    //         catch (Exception ex)
    //        {
    //            ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException);
    //            return new HttpStatusCodeResult(400, ex.Message).ToString();
    //        }
    //     }
      
    //    //public ActionResult Index()
    //    // {
    //    //     //ADDclientView obj = new ADDclientView();
    //    //     List<client> objc = (from c in dc.clients
    //    //                          select c).ToList();
    //    //     //obj.clientLst = objc;

    //    //     return View(objc);
    //    // }

    //    public JsonResult Users(clientView objclient, int? id)
    
    //    {
    //        try
    //        {
    //            client obj1 = new client();

    //            if (objclient == null ||objclient.Email==null)
    //            {
    //                //get all client detials
    //                List<clientView> objc = (from c in dc.clients
    //                                         select new clientView
    //                                         {
    //                                             id=c.PK_ClientID,
    //                                             UserName = c.UserName,
    //                                             Email = c.Email,
    //                                             IsActive=c.IsActive
    //                                             //Password = c.Password
    //                                         }).ToList();
    //                HttpContext.Response.AppendHeader("Content-Length", objc.ToString());
    //                return Json(objc, JsonRequestBehavior.AllowGet);
    //            }
    //            else if (objclient != null && objclient.Email!=null)
    //            {
    //                bool isEmailExists = new OperationsBO().CheckclientEmail1(objclient.Email);
    //                if (isEmailExists == false)
    //                {
    //                    //add client details
    //                    string ApiKey = new OperationsBO().GetApiKey();
    //                    obj1.UserName = objclient.UserName;
    //                    obj1.Email = objclient.Email;
    //                    obj1.Password = objclient.Password;
    //                    obj1.APIKey = ApiKey;
    //                    dc.clients.Add(obj1);
    //                    dc.SaveChanges();
    //                }
    //                return Json(obj1, JsonRequestBehavior.AllowGet);
    //            }
    //            if (id != 0 && id != null && objclient != null)
    //            {
    //                //update cleint detials
    //                bool isEmailExists = new OperationsBO().CheckclientEmail1(objclient.Email);
    //                if(isEmailExists==true)
    //                new OperationsBO().Updateclient(objclient.UserName, objclient.Email,objclient.IsActive);
    //                return Json(objclient, JsonRequestBehavior.AllowGet);
    //            }

    //            return Json("wrong input");
    //        }
    //        catch (Exception ex)
    //        {

    //            ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException);
    //            return Json(ex.StackTrace, ex.InnerException);
    //        }
    //}

    //    public JsonResult Campaigns(CampaignView obj, int? cid)
    //    {
    //        try
    //        {
    //            CampaignView objc = new CampaignView();
    //            //CampaignModel objm = new CampaignModel();
                
    //            if (obj == null || obj.ReferenceNumber==null)
    //            {
    //                //get all Campaigns
    //                List<CampaignView> listcamps = (from r in dc.riddatas
    //                                                select new CampaignView
    //                                                {
    //                                                    id=r.PK_Rid,
    //                                                    ReferenceNumber = r.ReferenceNumber,
    //                                                    pwd = r.Pwd,
    //                                                    isactive = r.IsActive
    //                                                }).ToList();
                    
    //                return Json(listcamps, JsonRequestBehavior.AllowGet);
    //            }
    //            else if (obj != null)
    //            {
    //                riddata objr = new riddata();
    //                bool isReferenceNumberExists = new OperationsBO().CheckReferenceNumber(obj.ReferenceNumber);
    //                if (isReferenceNumberExists == false)
    //                {
    //                    //add campaign details
    //                    objr.ReferenceNumber = obj.ReferenceNumber;
    //                    objr.Pwd = obj.pwd;
    //                    objr.IsActive = obj.isactive;
    //                    dc.riddatas.Add(objr);
    //                    dc.SaveChanges();
    //                }
    //                return Json(objr, JsonRequestBehavior.AllowGet);
    //            }
    //            if (cid != 0 && cid != null && obj != null)
    //            {
    //                //update campaign detials
    //                bool isReferenceNumberExists = new OperationsBO().CheckReferenceNumber(obj.ReferenceNumber);
    //                if (isReferenceNumberExists == true)
    //                new OperationsBO().UpdateCampaign(0,"", obj.ReferenceNumber, obj.pwd, obj.isactive);
    //                return Json(obj, JsonRequestBehavior.AllowGet);
    //            }
    //            return Json("wrong input");
    //        }
    //        catch (Exception ex)
    //        {

    //            ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException);
    //            return Json(ex.StackTrace, ex.InnerException);
    //        }
    //    }

    //    public JsonResult Login(clientView obj)
    //    {
    //        try
    //        {
    //            client objc = (from c in dc.clients
    //                           where c.Email == obj.Email && c.Password == obj.Password
    //                           select c).SingleOrDefault();
    //            Session["clientid"] = objc.PK_ClientID;
    //            if (objc != null)
    //                return Json(objc, JsonRequestBehavior.AllowGet);
    //            else
    //                return Json("User Not Found");
    //        }
    //        catch (Exception ex)
    //        {

    //            ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException);
    //            return Json(ex.StackTrace, ex.InnerException);
    //        }
    //    }
       
    //    public void Addclient(string username, string email, string password)
    //{
    //    try
    //    {
    //        string ApiKey = new OperationsBO().GetApiKey();
    //        client obj = new client();
    //        obj.UserName = username;
    //        obj.Email = email;
    //        obj.Password = password;
    //        obj.APIKey = ApiKey;
    //        dc.clients.Add(obj);
    //        dc.SaveChanges();

    //        Response.Redirect("/Admin/Index");
    //    }
    //    catch (Exception ex)
    //    {

    //        ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException);
    //    }
    //   // return RedirectToAction("Index","Admin");
    //}


    //    public void Updateclient(string username, string email, string password,bool? isactive)
    //{
    //    bool isEmailExists = new OperationsBO().CheckclientEmail1(email);

    //    new OperationsBO().Updateclient(username,email,isactive);

    //    Response.Redirect("/Admin/Index");
    //}


    }

    
}