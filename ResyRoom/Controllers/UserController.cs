using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ResyRoom.Infraestructura.Exceptions;
using ResyRoom.Infraestructura.Extensiones;
using ResyRoom.Models;
using System.Web.Security;
using System.Data.SqlClient;
using ResyRoom.Servicios;
using ResyRoom.ViewModels;
using Microsoft.Practices.Unity;
using Roles = System.Web.Security.Roles;

namespace ResyRoom.Controllers
{
    public class UserController : Controller
    {
        #region >>> Servicios
        [Dependency]
        public IServEstudios ServEstudios { get; set; }
        [Dependency]
        public IServEmail ServEmail { get; set; }
        [Dependency]
        public IServRegiones ServRegiones { get; set; }
        [Dependency]
        public IServGeneros ServGeneros { get; set; }
        [Dependency]
        public IServBandas ServBandas { get; set; }
        [Dependency]
        public IServUsuarios ServUsuarios { get; set; }
        [Dependency]
        public IServComunas ServComunas { get; set; }
        #endregion

        #region >>> Index's view
        public ActionResult Index()
        {
            var usuario = new IdentificacionDeUsuario();
            var estudiosMejorEvaluados = ServEstudios.EstudiosMejorEvaluados(5);
            var estudiosMasPopulares = ServEstudios.EstudiosMasPopulares(5);

            var listadoRegiones = ServRegiones.RegionesChilenas();
            var listadoComunas = new List<Comuna> { Constantes.ComunaSinSeleccion };

            return View(new IndexUserViewModel(usuario, estudiosMejorEvaluados, estudiosMasPopulares, listadoRegiones, listadoComunas));
        }

        [HttpPost]
        public ActionResult Index(IdentificacionDeUsuario usuario)
        {
            if (ModelState.IsValid)
            {
                var identificado = Membership.ValidateUser(usuario.Email, usuario.Password);
                var user = Membership.GetUser(usuario.Email);

                if (identificado && user != null && user.ProviderUserKey != null)
                {
                    var usuarioIdentificado = ServUsuarios.Lee((Guid)user.ProviderUserKey);

                    FormsAuthentication.SetAuthCookie(usuario.Email, usuario.Recordarme);

                    if (usuarioIdentificado.Bandas.Any())
                    {
                        return RedirectToAction("Index", "User");
                    }
                    if (usuarioIdentificado.Estudios.Any())
                    {
                        var salasSinConfigurar = usuarioIdentificado.Estudios.Where(e => !e.Salas.Any() || e.Salas.Any(s => !s.Horarios.Any()));
                        if (salasSinConfigurar.Any())
                            return RedirectToAction("Configure", "Studio", usuarioIdentificado.Estudios.First());

                        // areglar
                        return RedirectToAction("Configure", "Studio");
                    }
                }

                ModelState.AddModelError("", "Email o contraseña no validos.");
            }

            return View(new IndexUserViewModel());
        }
        #endregion

        #region >>> Register User
        public ActionResult RegisterUser()
        {
            return View(new RegisterUserViewModel(new RegistroDeUsuario()));
        }

        #endregion

        #region >>> Register band's view
        public ActionResult RegisterBand()
        {
            return View(new RegisterBandViewModel(new RegistroDeUsuario(), ServGeneros.ListadoParaCombo()));
        }

        [HttpPost]
        public ActionResult RegisterBand([Bind] RegistroDeUsuario usuario)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus createStatus;
                var membershipUser = Membership.CreateUser(usuario.Usuario, usuario.Password, usuario.Email, null, null, false, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    Roles.AddUserToRole(usuario.Usuario, "Banda");

                    if (membershipUser != null && membershipUser.ProviderUserKey != null)
                        usuario.Banda.UserId = (Guid)membershipUser.ProviderUserKey;

                    ServBandas.Guardar(usuario.Banda);
                    ServBandas.Actualizar();

                    ServEmail.EnviarEmailDeConfirmacion(membershipUser);

                    FormsAuthentication.SetAuthCookie(usuario.Usuario, false /* createPersistentCookie */);

                    return RedirectToAction("SuccessfulRegister", "User");
                }

                ModelState.AddModelError("", ErrorCodeToString(createStatus));
            }

            return View(new RegisterBandViewModel(usuario, ServGeneros.ListadoParaCombo()));
        }
        #endregion

        #region >>> Register Studio's view
        public ActionResult RegisterStudio()
        {
            ViewBag.Regiones = ServRegiones.RegionesChilenas();
            ViewBag.Comunas = new List<Region>();

            return View();
        }

        [HttpPost]
        public ActionResult RegisterStudio([Bind] RegistroDeUsuario model)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus createStatus;
                var membershipUser = Membership.CreateUser(model.Usuario, model.Password, model.Email, null, null, false, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    try
                    {
                        Roles.AddUserToRole(model.Usuario, "Estudio");

                        if (membershipUser != null && membershipUser.ProviderUserKey != null)
                            model.Estudio.UserId = (Guid)membershipUser.ProviderUserKey;

                        ServEstudios.Guardar(model.Estudio);
                        ServEstudios.Actualizar();

                        ServEmail.EnviarEmailDeConfirmacion(membershipUser);

                        if (membershipUser != null && membershipUser.ProviderUserKey != null)
                            model.Estudio.UserId = (Guid)membershipUser.ProviderUserKey;

                        ServEstudios.Guardar(model.Estudio);
                        ServEstudios.Actualizar();

                        ServEmail.EnviarEmailDeConfirmacion(membershipUser);

                        FormsAuthentication.SetAuthCookie(model.Usuario, false /* createPersistentCookie */);


                        return RedirectToAction("SuccessfulRegister", "User");

                    }
                    catch (SqlException e)
                    {
                        throw new ErrorDeCreacionException("Ha ocurrido un error intentando registrar un estudio", e);
                    }
                }

                ModelState.AddModelError("", ErrorCodeToString(createStatus));
            }

            return View(model);
        }
        #endregion

        #region >>> Successful band's register
        public ActionResult SuccessfulRegister()
        {
            var mensaje = new Mensaje
            {
                TituloPagina = "Registro con éxito - ResyRoom",
                Texto = "Se ha creado tu cuenta con éxito. Lo único que falta es que vayas a tu correo y confirmes tu cuenta de email.",
                TituloMensaje = "Cuenta creada con éxito."
            };

            return View(mensaje);
        }
        #endregion

        #region >>> Confirm email
        public ActionResult ConfirmEmail(string id)
        {
            var mensaje = new Mensaje();

            var userId = new Guid(id);
            var usuario = Membership.GetUser(userId);

            if (usuario == null)
            {
                mensaje.TituloPagina = "Error al confirmar cuenta de correo - ResyRoom";
                mensaje.TituloMensaje = "Ha ocurrido un error en la confirmación.";
                mensaje.Texto = "El usuario de la cuenta de correo que trata de confirmar no éxiste, esto puede haber ocurrido por una modificación en el link enviao a su correo.";
            }
            else
            {
                usuario.IsApproved = true;
                Membership.UpdateUser(usuario);

                mensaje.TituloPagina = "Se ha realizado la confimación con éxito - ResyRoom";
                mensaje.TituloMensaje = "Confirmación Exitosa.";
                mensaje.Texto = "Acaba de confirmar su cuenta de correo exitosamente, desde ahora podrá identificarse como usuario en el sitio.";
            }

            return View(mensaje);
        }
        #endregion

        #region >>> Other actions
        [ActionName("ComunasDeUnaRegion")]
        public ActionResult ObtenerComunasDeUnaRegion(int id)
        {
            var comunas = ServComunas.TodasDeUnaRegion(id);
            return Json(comunas, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region >>> Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "El nombre de usuario ya existe, pruebe con otro.";
                case MembershipCreateStatus.DuplicateEmail:
                    return "Ya existe un usuario con esa cuenta de correo.";
                case MembershipCreateStatus.InvalidPassword:
                    return "La contraseña ingresada es invalida, por favor vuelva a ingresarla.";
                case MembershipCreateStatus.InvalidEmail:
                    return "La cuenta de correo es invalida, por favor verifique que sea correcta.";
                case MembershipCreateStatus.InvalidAnswer:
                    return "La respuesta para recuperar la contraseña es incorrecta, por favor vuelva a intentarlo.";
                case MembershipCreateStatus.InvalidQuestion:
                    return "La pregunta para recuperar la contraseña es invalida, por favor vuelva a intentarlo.";
                case MembershipCreateStatus.InvalidUserName:
                    return "El nombre de usuario ingresado es invalido, por favor vuelva a intentarlo.";
                case MembershipCreateStatus.ProviderError:
                    return "Ha ocurrido un error, por favor verifique los datos ingresados.";
                case MembershipCreateStatus.UserRejected:
                    return "El proceso de creación de usuario ha sido cancelado, por favor verifique los datos ingresados.";
                default:
                    return "Ha ocurrido un error, estamos trabajando en resolverlo.";
            }
        }
        #endregion

        #region >>> Search Method

        [HttpPost]
        public ActionResult Buscar(BusquedaRapida busquedaRapida)
        {
            var algo = busquedaRapida;

            return RedirectToAction("Search", "Studio");
        }

        #endregion

        #region >>> Get Comunas xd

        public ActionResult ObtenerComunas(int idItem)
        {
            var comunas = ServComunas.TodasDeUnaRegion(idItem);
            return Json(comunas.ToSelectListItem(), JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
