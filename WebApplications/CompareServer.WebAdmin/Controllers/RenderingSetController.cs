using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using CompareServer.Domain;
using System.Diagnostics;

namespace CompareServer.WebAdmin.Controllers
{
    public class RenderingSetController : CompareControllerBase
    {
        //
        // GET: /RenderingSet/
        public const string ControllerName = "RenderingSet";

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Load()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                List<string> dd = RenderingSets.GetList();
                
                return Json(new {success = true, data = serializer.Serialize(dd.ToArray())}, "json/application");
            }
            catch (Exception ex)
            {
                Log.Write(TraceEventType.Stop, "Rendering Set Controller, Load | Exception {0}", ex);
                return Json(new { success = false, message = ex.Message }, "json/application");
            }
        }

    }
}
