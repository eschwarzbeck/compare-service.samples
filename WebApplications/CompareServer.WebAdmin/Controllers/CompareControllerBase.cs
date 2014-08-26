using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using CompareServer.Interfaces;

namespace CompareServer.WebAdmin.Controllers
{
    public class CompareControllerBase : Controller
    {
        //
        // GET: /CompareControllerBase/

        protected ILogger Log { get; private set; }
        
        protected CompareControllerBase()
        {
            Log = UnityContainer.UnityManager.Resolve<ILogger>();
        }

        public bool IsCurrentController(string controllername)
        {
            if (this.GetType().Name.ToUpper().StartsWith(controllername.ToUpper()))
                return true;
            return false;
        }
    }
}
