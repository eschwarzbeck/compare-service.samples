using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CompareServer.UnityContainer;
using CompareServer.Interfaces;
using System.Diagnostics;
using CompareServer;
using CompareServer.Domain;

namespace CompareServer.WebApplication
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public ILogger logger { get; private set; }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               @"Results", //Route name
               @"comparer/download/{id}/{name}", // URL with parameters
               new { controller = "Comparer", action = "Download", id = "", name = "" }  //Parameter defauls
            );

            routes.MapRoute(
                @"Default", // Route name
                @"{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {

            logger = GetLogger();
            logger.Write(TraceEventType.Information, "The Compare Server web application is starting");
            
            AreaRegistration.RegisterAllAreas();
            
            ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory());
            RegisterRoutes(RouteTable.Routes);
        }

        private static ILogger GetLogger()
        {
            return UnityContainer.UnityManager.Resolve<ILogger>();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // Use app directory
            System.IO.Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            // This line for make session to not chage id
            HttpContext.Current.Session.Add("__MyAppSession", string.Empty);
            string path = System.IO.Path.Combine(AppConfig.UploadPath, Session.SessionID.ToString());
            if (!System.IO.Directory.Exists(path))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                catch (Exception ex)
                {
                    logger.Write(TraceEventType.Error, "{0}", ex);
                }
            }
        }

        protected void Session_End(object sender, EventArgs e)
        { 
            /* Remove Session Derectory */
            string path = System.IO.Path.Combine(AppConfig.UploadPath, Session.SessionID.ToString());
            if (System.IO.Directory.Exists(path))
            {
                try
                {
                    System.IO.Directory.Delete(path, true);
                }
                catch (Exception ex)
                {
                    logger.Write(TraceEventType.Error, "{0}", ex);
                }
            }
        }

        private void Log(Exception exception)
        {
            try
            {
                var logger = GetLogger();
                if (logger == null)
                {
                    return;
                }
                var eventType = Response.StatusCode == 500 ? TraceEventType.Error : TraceEventType.Information;
                logger.Write(eventType, "{0}", exception);
            }
            catch (Exception)
            {
                // Ignore, as we don't want any side effects.
            }
        }

    }
}