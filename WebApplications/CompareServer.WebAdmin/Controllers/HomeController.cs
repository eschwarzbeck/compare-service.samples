using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareServer.Domain;
using System.Diagnostics;

namespace CompareServer.WebAdmin.Controllers
{
    public class HomeController : CompareControllerBase
    {
        //
        // GET: /Home/

        public const string ControllerName = "Home";

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
                return Json(new { success = false, service = "", comparison = "", message = ex.Message}, "text/json");
            }
        }

        [HttpPost]
        public ActionResult ServerInfo()
        {
            try
            {
                DateTime access = LogEntry.DateFromString(SystemLog.GetLastAccess());
                int num = RenderingSets.Count();
                return Json(new { success = true, access = access.ToString(), rsetnum = num }, "application/json");
            }
            catch (Exception ex)
            {
                Log.Write(TraceEventType.Stop, "Home Controller, ServerInfo | Exception {0}", ex);
                return Json(new { success = false , message = ex.Message }, "text/json");
            }
        }

        [HttpPost]
        public ActionResult RSetsNumber()
        {
            try
            {
                int num = RenderingSets.Count();
                return Json(new { success = true, count = num }, "application/json");
            }
            catch (Exception ex)
            {
                Log.Write(TraceEventType.Stop, "Home Controller, RSetNumber | Exception {0}", ex);
                return Json(new { success = false, message = ex.Message }, "text/json");
            }
        }

        [HttpPost]
        public ActionResult Ping()
        {
            CompareService service = new CompareService(this.Session);

            try
            {
                service.Ping();
                return Json(new { success = true, ping = "ping success" }, "application/json");
            }
            catch (Exception ex)
            {
                Log.Write(TraceEventType.Stop, "Home Controller, Ping | Exception {0}", ex);
                return Json(new { success = false, ping = "ping failed", message = ex.Message }, "text/json");
            }
        }
    }
}
