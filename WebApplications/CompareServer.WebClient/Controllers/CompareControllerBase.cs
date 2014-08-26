using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareServer.Interfaces;

namespace CompareServer.WebClient.Controllers
{
    public class CompareControllerBase : Controller
    {
        protected ILogger Log { get; set; }

        protected CompareControllerBase()
        {
            Log = UnityContainer.UnityManager.Resolve<ILogger>();
        }

        public bool IsCurrentController(string controllername)
        {
            if (this.GetType().Name.ToLower().StartsWith(controllername))
                return true;
            return false;
        }
    }
}