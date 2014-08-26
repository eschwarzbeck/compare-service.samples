using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareServer.UnityContainer;

namespace CompareServer.WebAdmin
{
    public class UnityControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, String.Format("The controller for path '{0}' could not be found" + "or it does not implement IController.", requestContext.HttpContext.Request.Path));
            }

            if (!typeof(IController).IsAssignableFrom(controllerType))
            {
                throw new UnityControllerFactoryException(string.Format("Failed to create an instance of the controller from a non controller type was '{0}'", controllerType.AssemblyQualifiedName));
            }

            var obj = UnityManager.Resolve(controllerType);
            return obj as IController;
        }
    }
}