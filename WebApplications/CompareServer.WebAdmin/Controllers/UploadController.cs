using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.IO;
using CompareServer.Domain;
using System.Diagnostics;

namespace CompareServer.WebAdmin.Controllers
{
    public class UploadController : CompareControllerBase
    {

        [HttpPost]
        public ActionResult File(string fileName)
        {
            try
            {
                Stream stream = null;
                if (Request.ContentType.Contains(@"multipart/form-data"))
                {   // IE
                    HttpPostedFileBase postedFile = Request.Files[0];
                    stream = postedFile.InputStream;
                    fileName = Request.Files[0].FileName;
                }
                else if (Request.ContentType.Contains("application/octet-stream"))
                {   // Webkit, Mozilla
                    stream = Request.InputStream;
                    fileName = Request.QueryString["qqfile"];
                }
                else
                {
                    throw new Exception("Unsupported ContentType");
                }
                fileName = Path.GetFileName(fileName);
                RenderingSets.SaveFile(fileName, stream);
            }
            catch (System.IO.IOException ex)
            {
                Log.Write(TraceEventType.Stop, "Upload Controller, File | Exception {0}", ex);
                return Json(new { success = false, message = ex.Message }, "text/html");
            }
            catch (Exception ex)
            {
                Log.Write(TraceEventType.Stop, "Upload Controller, File | Exception {0}", ex);
                return Json(new { success = false, message = ex.Message }, "text/html");
            }
            return Json(new { success = true }, "text/html");
        }

        [HttpPost]
        public ActionResult Delete(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                try
                {
                    RenderingSets.DeleteFile(fileName);
                    return Json(new { success = true, message = "" }, "application/json");
                }
                catch (Exception ex)
                {
                    Log.Write(TraceEventType.Stop, "Upload Controller, Delete | Exception {0}", ex);
                    return Json(new { success = false, message = ex.Message }, "application/json");
                }
            }
            return Json(new { success = false, message = "Arguments error"}, "application/json");
        }
    }
}
