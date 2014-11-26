using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using ResyRoom.Models;
using ResyRoom.Servicios;

namespace ResyRoom.Infraestructura
{
    public abstract class BaseController : Controller
    {
        [Dependency]
        public IServUsuarios ServUsuarios { get; set; }

        public string[] AutenticarUsuario(IdentificacionDeUsuario usuario)
        {
            var user = ServUsuarios.AutenticarUsuario(usuario);

            return ObtenerRolesUsuario(user);
        }

        public string[] AutenticarUsuario(IdentificacionDeUsuarioPorFacebook usuarioFb)
        {
            var user = ServUsuarios.AutenticarUsuario(usuarioFb);

            return ObtenerRolesUsuario(user);
        }

        private string[] ObtenerRolesUsuario(Usuario user)
        {
            if (user != null)
            {
                var roles = user.Roles.Select(m => m.Nombre).ToArray();

                var userData = JsonConvert.SerializeObject(new CustomPrincipalSerializeModel
                {
                    IdUsuario = user.IdUsuario,
                    Nombre = user.Nombre,
                    Email = user.Email,
                    FacebookId = user.FacebookId,
                    IsFacebookLogin = user.EntraPorFacebook ?? false,
                    Roles = roles
                });

                var authTicket = new FormsAuthenticationTicket(1, user.Email, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData);

                var encTicket = FormsAuthentication.Encrypt(authTicket);
                var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Cookies.Add(faCookie);

                return roles;
            }

            return null;
        }

        protected new CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }
    }
}