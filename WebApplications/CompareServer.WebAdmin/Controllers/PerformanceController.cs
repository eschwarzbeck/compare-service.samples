using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareServer.Domain;
using System.Globalization;
using System.Web.Script.Serialization;
using System.Diagnostics;

namespace CompareServer.WebAdmin.Controllers
{
    public class PerformanceController : CompareControllerBase
    {
        //
        // GET: /Performance/

        public const string ControllerName = "Performance";

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Statistics(string start, string end)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                DateTime startDate = DateTime.ParseExact(start, "MM-dd-yyyy", CultureInfo.CurrentCulture);
                DateTime endDate = DateTime.ParseExact(end, "MM-dd-yyyy", CultureInfo.CurrentCulture);
                PerformanceData log = new PerformanceData(startDate, endDate);
                object result = log.PerDayStatistics.Select(x => new {date = x.Key.ToString("MM-dd-yyyy"),
                                                                      comparisons = x.Value.Comparisons,
                                                                      avgcomplen = x.Value.AvarageComparisonLength,
                                                                      avgproctime = x.Value.AvarageProcessingTime,
                                                                      avgdoclen = x.Value.AvarageDocumentLength
                });
                return Json(new { success = true, statistics = serializer.Serialize(result),
                                                  totalcomp = log.TotalNumberOfComparisons,
                                                  totalbytes = log.TotalNumberOfBytes  }, "json/application");
            }
            catch (Exception ex)
            {
                Log.Write(TraceEventType.Stop, "Performance Controller, Statistics | Exception {0}", ex);
                return Json( new {success = false, message = ex.Message}, "json/application");
            }
        }

        [HttpPost]
        public ActionResult StartService()
        {
            return Json(new { success = true, status = CompareService.StartService()} );
        }

        [HttpPost]
        public ActionResult StopServcie()
        {
            return Json(new { success = true, status = CompareService.StopService()} );
        }

        [HttpPost]
        public ActionResult GetServiceStatus()
        {
            return Json(new { success = true, status = CompareService.GetServiceStatus() });
        }
    }
}
