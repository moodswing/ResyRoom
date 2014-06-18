using System;
using System.Linq;
using System.Web.Mvc;
using System.Reflection;
using Microsoft.Practices.Unity;

namespace ResyRoom.Infraestructura
{
    public class MvcConfig
    {
        public static void Register(IUnityContainer container, Guid containerGuid)
        {
            container.RegisterInstance(typeof(IControllerActivator), new UnityControllerActivator(containerGuid));

            foreach (var type in Assembly.GetExecutingAssembly().GetExportedTypes().
                            Where(x => x.GetInterface(typeof(IController).Name) != null))
            {
                container.RegisterType(type);
            }

            DependencyResolver.SetResolver(
                t => container.IsRegistered(t) ? container.Resolve(t) : null,
                t => container.IsRegistered(t) ? container.ResolveAll(t) : Enumerable.Empty<object>());
        }
    }
}