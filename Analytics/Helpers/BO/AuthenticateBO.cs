using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Analytics.Helpers.Utility;
using System.Web.Security;
//using System.Data.Objects;
using Analytics.Helpers.BO;
using System.Data.Entity.Infrastructure;
using System.Data;

namespace Analytics.Helpers.BO
{
    public class AuthenticateBO
    {
        shortenurlEntities dc = new shortenurlEntities();

        public bool GetAuthenticateUser(string userName, string password, out int loginCount, out int CheckCount, out string name)
        {

            name = "";
            loginCount = 1;
            CheckCount = 0;
            int confCount = 0;



            client qry = (from person in dc.clients
                          where person.Email.Trim().ToLower() == userName.Trim().ToLower() && person.IsActive == true
                          select person).SingleOrDefault();
            if (qry == null || qry.Password != password.Trim())
            {
                return false;
            }
            else
            //else if (qry.Role.ToLower() == UserRole.client.ToString())
            {

                name = qry.PK_ClientID.ToString() + '^' + qry.Email + '^' + qry.UserName + '^' + qry.Role+ '^' + qry.IsActive;
                // FormsAuthentication.SetAuthCookie(name, false);
                qry.LoginDate = DateTime.Now;
                if (qry.LoginCount == null)
                {
                    loginCount = 0;
                    qry.LoginCount = 1;
                }
                else
                {
                    loginCount = qry.LoginCount.Value;
                    qry.LoginCount += 1;
                }
                string strQuery = "Update client set LoginDate = '" + Helper.GetUTCTime() + "' ,LoginCount='" + qry.LoginCount + "' where PK_ClientID ='" + qry.PK_ClientID + "'";
                SqlHelper.ExecuteNonQuery(Helper.ConnectionString, CommandType.Text, strQuery);


                loginhistory objloginhistory = new loginhistory();
                objloginhistory.FKPersonId = qry.PK_ClientID;
                objloginhistory.Role = qry.Role;
                objloginhistory.IpAddress = HttpContext.Current.Request.UserHostAddress;
                objloginhistory.LoginDateTime = DateTime.UtcNow;
                dc.loginhistories.Add(objloginhistory);
                dc.SaveChanges();
                return true;
            }
        }
    }
}