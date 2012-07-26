using System.Web.Mvc;
using Microsoft.Practices.Unity;
using ResyRoom.Controllers;
using ResyRoom.Models;
using ResyRoom.Servicios;

namespace ResyRoom.Infraestructura
{
    public static class Bootstrapper
    {
        public static void ConfigureUnityContainer()
        {
            var container = GetUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer GetUnityContainer()
        {
            var container = new UnityContainer()
                .RegisterType<UserController>()
                .RegisterType<IServEstudios, ServEstudios>()
                .RegisterType<IServBandas, ServBandas>()
                .RegisterType<IServEmail, ServEmail>()
                .RegisterType<IServGeneros, ServGeneros>()
                .RegisterType<IServPaises, ServPaises>()
                .RegisterType<IServRegiones, ServRegiones>()
                .RegisterType<IServUsuarios, ServUsuarios>()
                .RegisterType<IServComunas, ServComunas>()
                .RegisterType<ResyRoomEntities>(new PerResolveLifetimeManager());

            return container;
        }
    }
}