using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CompareServer.Domain;
using CompareServer.Interfaces;
using System.IO;
using System.Web.Script.Serialization;
using System.Globalization;
using System.Diagnostics;

namespace CompareServer.WebAdmin.Controllers
{
    public class LoggingController : CompareControllerBase
    {
        //
        // GET: /Logging/

        public const string ControllerName = "Logging";

       
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        // Audit Log
        [Authorize]
        public ActionResult Audit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AuditLogs(string start, string end)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                DateTime startDate = DateTime.ParseExact(start, "MM-dd-yyyy", CultureInfo.CurrentCulture);
                DateTime endDate = DateTime.ParseExact(end, "MM-dd-yyyy", CultureInfo.CurrentCulture);
                AuditLog log = new AuditLog();
                IEnumerable<AuditLogEntry> logs = log.GetLogs(startDate, endDate);
                return Json(new { success = true, data = jss.Serialize(logs) }, "application/json");
            }
            catch (Exception ex)
            {
                Log.Write(TraceEventType.Stop, "Logging Controller, AuditLogs | Exception {0}", ex);
                return Json(new { success = false, message = ex.Message }, "text/json");
            }
        }

        // Host Log
        [Authorize]
        public ActionResult Host()
        {
            return View();
        }

        [HttpPost]
        public ActionResult HostLogs(string start, string end)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                DateTime startDate = DateTime.ParseExact(start, "MM-dd-yyyy", CultureInfo.CurrentCulture);
                DateTime endDate = DateTime.ParseExact(end, "MM-dd-yyyy", CultureInfo.CurrentCulture);
                HostLog log = new HostLog();
                IEnumerable<HostLogEntry> logs = log.GetLogs(startDate, endDate);
                return Json(new { success = true, data = jss.Serialize(logs) }, "application/json");
            }
            catch (Exception ex)
            {
                Log.Write(TraceEventType.Stop, "Logging Controller, HostLogs | Exception {0}", ex);
                return Json(new { success = false, message = ex.Message }, "text/json");
            }
        }


        // System Log
        [Authorize]
        public ActionResult System()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SystemLogs(string start, string end)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                DateTime startDate = DateTime.ParseExact(start, "MM-dd-yyyy", CultureInfo.CurrentCulture);
                SystemLog log = new SystemLog();
                IEnumerable<LogEntry> logs = log.GetLogs(startDate, startDate);
                return Json(new { success = true, data = jss.Serialize(logs) }, "json/application");
            }
            catch (Exception ex)
            {
                Log.Write(TraceEventType.Stop, "Logging Controller, SystemLogs | Exception {0}", ex);
                return Json(new { success = false, message = ex.Message }, "json/text");
            }
        }

        /*-- This method returns List of System Log Files --*/
        [HttpPost]
        public ActionResult SystemLogsList()
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                IEnumerable<String> list = SystemLog.GetFiles().Where(file => file.Contains(SystemLog.SystemLogFile))
                                                         .Select(file => file.Replace(SystemLog.SystemLogFile, String.Empty));
                return Json(new { success = true, data = jss.Serialize(list) }, "json/application");
            }
            catch (Exception ex)
            {
                Log.Write(TraceEventType.Stop, "Logging Controller, SystemLogsList | Exception {0}", ex);
                return Json(new { success = false, message = ex.Message }, "json/text");
            }
        }
    }
}
