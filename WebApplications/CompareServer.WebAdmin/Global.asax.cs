using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using CompareServer.Domain;

namespace CompareServer.WebAdmin
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory());
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start()
        {
            CompareService cs = new CompareService(new HttpSessionStateWrapper(this.Session));
            cs.StoreClientInSession = true;
        }

        protected void Session_End()
        {
            /* Close Compare Server Connection */
            CompareService cs = new CompareService(new HttpSessionStateWrapper(this.Session));
            cs.Close();
        }
    }
}