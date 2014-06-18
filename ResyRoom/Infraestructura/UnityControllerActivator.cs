using System;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;

namespace ResyRoom.Infraestructura
{
    public class UnityControllerActivator : IControllerActivator
    {
        private Guid _containerGuid;

        public UnityControllerActivator(Guid containerGuid)
        {
            _containerGuid = containerGuid;
        }
        
        public IController Create(RequestContext requestContext, Type controllerType)
        {
            var container = requestContext.HttpContext.Items[_containerGuid] as IUnityContainer;
            if (container != null)
                return container.Resolve(controllerType) as IController;
        
            return null;
        }
    }
}