using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareServer.Domain;
using System.Web.Script.Serialization;
using System.Diagnostics;

namespace CompareServer.WebAdmin.Controllers
{
    public class AuditingController : CompareControllerBase
    {
        //
        // GET: /Auditing/

        public static string ControllerName = "Auditing";

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UsersActivity()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                AuditLog log = new AuditLog();
                IEnumerable<UserActivity> activity = log.GetUsersActivity();

                return Json(new { success = true, data = serializer.Serialize(activity) }, "json/application");
            }
            catch (Exception ex)
            {
                Log.Write(TraceEventType.Stop, "Auditing Controller, UsersActivity | Exception {0}", ex);
                return Json(new { success = false, message = ex.Message }, "json/text");
            }
        }
    }
}
