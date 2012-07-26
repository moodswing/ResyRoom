using System;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using System.Collections.Generic;

namespace ResyRoom.Infraestructura
{
    public class UnityDependencyResolver : IDependencyResolver
    {
        readonly IUnityContainer _container;

        public UnityDependencyResolver(IUnityContainer container) { _container = container; }

        public object GetService(Type serviceType)
        {
            try { return _container.Resolve(serviceType); }
            catch { return null; }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try { return _container.ResolveAll(serviceType); }
            catch { return new List<object>(); }
        }
    }

}