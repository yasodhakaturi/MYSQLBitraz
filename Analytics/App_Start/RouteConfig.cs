using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Analytics
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.svc/{*pathInfo}");
           // routes.MapMvcAttributeRoutes();

            //routes.MapRoute(
            //    name: "Admin",
            //    url: "admin",
            //    defaults: new { controller = "Admin", action = "AdminLogin" }
            //);

            //routes.MapRoute(
            //    name: "Admin",
            //    url: "admin/{name}",
            //    defaults: new { controller = "Admin", action = "Admin" }
            //);


       //     routes.MapRoute(
       //    name: "Login",
       //    url: "{id}",
       //    defaults: new { controller = "Home", action = "LoginRid" }
       //);

            
             routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            //);
        }
    }
}
