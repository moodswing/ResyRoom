using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using ResyRoom.Controllers;
using ResyRoom.Models;
using ResyRoom.Servicios;
using Microsoft.Practices.Unity;
using ResyRoom.ViewModels;

namespace ResyRoom.Infraestructura
{
    public static class Bootstrapper
    {
        public static IUnityContainer ConfigureUnityContainer()
        {
            var container = GetUnityContainer();
            //DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer GetUnityContainer()
        {
            var container = new UnityContainer()
                .RegisterType<IServEstudios, ServEstudios>()
                .RegisterType<IServBandas, ServBandas>()
                .RegisterType<IServEmail, ServEmail>()
                .RegisterType<IServGeneros, ServGeneros>()
                .RegisterType<IServReservas, ServReservas>()
                .RegisterType<IServPaises, ServPaises>()
                .RegisterType<IServRegiones, ServRegiones>()
                .RegisterType<IServUsuarios, ServUsuarios>()
                .RegisterType<IServComunas, ServComunas>()
                .RegisterType<IEnumerable<Estudio>, Estudio[]>()
                .RegisterType<IEnumerable<Region>, Region[]>()
                .RegisterType<IEnumerable<Comuna>, Comuna[]>()
                .RegisterType<IBusquedaRapida, Busqueda>()
                .RegisterType<ISearchStudioViewModel, SearchStudioViewModel>()
                .RegisterType<IViewStudioViewModel, ViewStudioViewModel>()
                //.RegisterType<ResyRoomEntities>(new PerResolveLifetimeManager())
                .RegisterType<ResyRoomEntities>(new HierarchicalLifetimeManager());

            return container;
        }
    }
}