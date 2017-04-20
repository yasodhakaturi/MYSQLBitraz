using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Analytics.Helpers.Utility
{
    public class ErrorLogs
    {
        public static void LogErrorData(string stackTraceInfo, string message)
        {
            try
            {
                shortenurlEntities dc = new shortenurlEntities();
                errorlog objErrorLog = new errorlog();
                objErrorLog.StackTrace = stackTraceInfo;
                objErrorLog.ErrorMessage = message;
                objErrorLog.DateCreated = System.DateTime.Now;
                dc.errorlogs.Add(objErrorLog);
                dc.SaveChanges();
                string err = "";
                err = "Error Occured on:" + DateTime.Now.ToString() + "<br>";
                err += "Message: " + message;
                err += "Stack Trace:<br>" + stackTraceInfo;
                //EmailUtil.SendMail("yasodha.bitra@gmail.com", ConfigurationManager.AppSettings["Error Mail"].ToString(), "", "", "Error ShortenURL", " <br>Message : " + err);

            }
            catch (Exception ex)
            {

            }
        }
    }
}