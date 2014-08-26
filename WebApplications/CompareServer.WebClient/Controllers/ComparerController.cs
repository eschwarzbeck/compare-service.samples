using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using CompareServer.Domain;
using CompareServer.Interfaces;
using CompareServer.WebClient.Models;
using System.Threading;
using System.Diagnostics;

namespace CompareServer.WebClient.Controllers
{
    public class ComparerController : CompareControllerBase
    {
        // GET: /Comparer/

        public ComparerController() : base()
        {
        
        }

        #region private properties
        private string _uploadPath;

        public const string ControllerName = "Comparer";
        #endregion

        #region properties

        /// <summary>
        ///     Return current session temporary directory
        /// </summary>
        private string UploadPath
        {
            get
            {
                if (_uploadPath == null)
                {
                    string path = AppConfig.UploadPath;
                    path = System.IO.Path.Combine(path, Session.SessionID.ToString());
                    if (Directory.Exists(path))
                    {
                        _uploadPath = path;
                    }
                    else
                    {
                        try
                        {
                            System.IO.Directory.CreateDirectory(path);
                            _uploadPath = path;
                        }
                        catch (Exception ex)
                        {
                            Log.Write(TraceEventType.Error, "Comparer Controller, UploadPath | Exception {0}", ex);
                            _uploadPath = null;
                        }
                    }
                }
                return _uploadPath;
            }
        }
       
        #endregion


        #region action methods
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            var model = new ComparerModel();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(ComparerModel model)
        {
            return View(model);
        }

        [Authorize]
//        [HttpPost]
        public ActionResult Results(ComparerModel model)
        {
            /* Set Original document Path */
//            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ComparerResultModel result = new ComparerResultModel();
            if (ModelState.IsValid)
            {
                CompareService cs = new CompareService(Session);
                ComparerResult res = new ComparerResult();
                try
                {
                    res = cs.DoComparison(HttpContext.User, model.GetComparerArguments(UploadPath));
                }
                catch (Exception ex)
                {
                    Log.Write(TraceEventType.Stop, "Upload Controller, Results | Exception {0}", ex);
                    res.Errors.Add(ex.Message);
                }
                result = new ComparerResultModel(res);                
                //return Json(new { success = true, results = serializer.Serialize(new ComparerResultModel(res))}, "application/json");
            }
            else 
            {
                result.Errors.Add("Input values is not filled or is not valid");
                //return Json(new { success = false, message = serializer.Serialize(result) }, "application/json");
            }
            return View(result);
        }
        /// <summary>
        ///    Action to download result files
        /// </summary>
        /// <param name="file">id of file in results list</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Download(string id, string name)
        {
            FileStream stream = null;
            List<string> downloadable_exts = new List<string> { "rtf", "doc", "docx", "wdf" };
            
            try
            {
                string ext = System.IO.Path.GetExtension(name);
                string path = System.IO.Path.Combine(UploadPath, id);
                if (System.IO.File.Exists(path))
                {
                    stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                    if (downloadable_exts.Contains(ext))
                    {
                        Response.AddHeader("Content-Disposition", "attachment");
                    }
                    return File(stream, MimeType.getContentType(ext));
                }
            }
            catch (Exception ex)
            {
                Log.Write(TraceEventType.Stop, "Upload Controller, Download | Exception {0}", ex);
            }
            return null;
        }

        #endregion
    }
}
