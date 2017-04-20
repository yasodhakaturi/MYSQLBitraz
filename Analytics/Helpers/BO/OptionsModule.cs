using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Analytics.Helpers.BO
{
    public class OptionsModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += (sender, args) =>
            {
                var app = (HttpApplication)sender;

                if (app.Request.HttpMethod == "OPTIONS")
                {
                    app.Response.StatusCode = 200;
                    app.Response.AddHeader("Access-Control-Allow-Headers", "content-type");
                    //app.Response.AddHeader("Access-Control-Allow-Origin", app.Request.UrlReferrer.ToString().Substring(0, app.Request.UrlReferrer.ToString().Length - 1));
                   // app.Response.AddHeader("Access-Control-Allow-Origin", "http://192.168.1.66:3300");

                    //app.Response.AddHeader("Access-Control-Allow-Credentials", "true");
                    app.Response.AddHeader("Access-Control-Allow-Methods", "POST,GET,OPTIONS");
                    app.Response.AddHeader("Content-Type", "application/json");
                    app.Response.End();
                }
            };
        }

        public void Dispose()
        {
        }
    }
}