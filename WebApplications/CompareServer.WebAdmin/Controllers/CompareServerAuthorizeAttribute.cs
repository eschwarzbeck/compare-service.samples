using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Globalization;
using CompareServer.Domain;

namespace CompareServer.WebAdmin.Controllers
{
    public class CompareServerAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string _adminRole;

        public CompareServerAuthorizeAttribute()
        {
            _adminRole = AppConfig.AdministratorRole;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "{0} is Authenticated", httpContext.User.Identity.Name), "CS_AUTH");
                if (httpContext.User.IsInRole(_adminRole))
                {
                    Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Is in {0} role ", _adminRole), "CS_AUTH");
                    return true;
                }
                Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Is not in {0} role ", _adminRole), "CS_AUTH");
                return false;
            }
            Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "{0} not Authenticated", httpContext.User.Identity.Name), "CS_AUTH");
            return false;
        }
    }
}