using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Collections.Generic;

using CompareServer.Domain;
using System.Diagnostics;

namespace CompareServer.WebClient.Controllers
{
    public class UploadController : CompareControllerBase
    {
        // GET: /Upload/

        public const string ControllerName = "Upload";

        [HttpPost]
        public ActionResult File(string fileName)
        {
            string ClientName = string.Empty;
            string ServerName = string.Empty;
            try
            {
                Stream stream = null;
                if (Request.ContentType.Contains(@"multipart/form-data"))
                {   // IE
                    HttpPostedFileBase postedFile = Request.Files[0];
                    stream = postedFile.InputStream;
                    ClientName = Request.Files[0].FileName;
                }
                else if (Request.ContentType.Contains("application/octet-stream"))
                {   // Webkit, Mozilla
                    stream = Request.InputStream;
                    ClientName = Request.QueryString["qqfile"];
                }
                else
                {
                    throw new Exception("Unsupported ContentType");
                }
                var buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                ServerFile sf = new CompareService(Session).storeFileOnServer(buffer, ClientName);
                ServerName = sf.ServerName;
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
            return Json(new { success = true, file = ServerName }, "text/html");
        }

        [HttpPost]
        public ActionResult Delete(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                string  path = System.IO.Path.Combine(AppConfig.UploadPath, Session.SessionID.ToString());
                        path = System.IO.Path.Combine(path, fileName);
                if (System.IO.File.Exists(path))
                {
                    try
                    {
                        System.IO.File.Delete(path);
                        return Json(new { success = true, message = "" }, "text/html");
                    }
                    catch (Exception ex)
                    {
                        Log.Write(TraceEventType.Error, "Upload Controller, Delete | Exception {0}", ex);
                        return Json(new { success = false, message = ex.Message }, "text/html");
                    }
                }
                return Json(new { success = false, message = "File not found" }, "text/html");
            }
            return Json(new { success = false, message = "Arguments error" }, "text/html");
        }
    }
}
