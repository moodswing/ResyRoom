using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using ResyRoom.App_Start;
using ResyRoom.Infraestructura;
using ResyRoom.Models;

namespace ResyRoom
{
    public class MvcApplication : HttpApplication
    {
        private static Guid _unityGuid;
        private static IUnityContainer _container;

        protected void Application_Start()
        {
            _unityGuid = Guid.NewGuid();

            AreaRegistration.RegisterAllAreas();

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            _container = Bootstrapper.ConfigureUnityContainer();
            MvcConfig.Register(_container, _unityGuid);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
        }

        protected void Application_BeginRequest()
        {
            var childContainer = _container.CreateChildContainer();
            HttpContext.Current.Items[_unityGuid] = childContainer;
        }

        protected void Application_EndRequest()
        {
            var childContainer = HttpContext.Current.Items[_unityGuid] as IUnityContainer;
            if (childContainer != null)
            {
                childContainer.Dispose();
                HttpContext.Current.Items.Remove(_unityGuid);
            }
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                var deserializedModel = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);
                var newUser = new CustomPrincipal(authTicket.Name);
                newUser.IdUsuario = deserializedModel.IdUsuario;
                newUser.Nombre = deserializedModel.Nombre;
                newUser.Email = deserializedModel.Email;
                newUser.IsFacebookLogin = deserializedModel.IsFacebookLogin;
                newUser.FacebookId = deserializedModel.FacebookId;
                newUser.Roles = deserializedModel.Roles;

                HttpContext.Current.User = newUser;
            }
        }
    }
}