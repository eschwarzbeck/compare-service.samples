using System;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;

using CompareServer.Domain;
using CompareServer.Interfaces;
using System.Diagnostics;


namespace CompareServer.WebClient.Controllers
{
    public class HomeController : CompareControllerBase
    {
        //
        // GET: /Home/

        public const string ControllerName = "Home";

        public HomeController() :base()
        {
            Log.Write("Home Controller Created");
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TestConnection()
        {
            CompareService service = new CompareService(this.Session);
            string serviceVersion;
            string comparisonVersion;

            try
            {
                service.GetVersions(out serviceVersion, out comparisonVersion);
                return Json(new { success = true, service = serviceVersion, comparison = comparisonVersion }, "application/json");
            }
            catch (Exception ex)
            {
                Log.Write(TraceEventType.Stop, "Home Controller, TestConnection | Exception {0}", ex);
                return Json(new { success = false, service = "", comparison = "", message = ex.Message }, "text/json");
            }
        }
    }
}
