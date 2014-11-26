using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AutoMapper;
using MoreLinq;
using ResyRoom.Infraestructura;
using ResyRoom.Infraestructura.Exceptions;
using ResyRoom.Infraestructura.Extensiones;
using ResyRoom.Models;
using System.Web.Security;
using System.Data.SqlClient;
using ResyRoom.Servicios;
using ResyRoom.ViewModels;
using Microsoft.Practices.Unity;
using ResyRoom.ViewModels.RegisterStudio;
using Roles = System.Web.Security.Roles;

namespace ResyRoom.Controllers
{
    public class UserController : BaseController
    {
        #region >>> Dependencias
        [Dependency]
        public IServEstudios ServEstudios { get; set; }
        [Dependency]
        public IServEmail ServEmail { get; set; }
        [Dependency]
        public IServRegiones ServRegiones { get; set; }
        [Dependency]
        public IServTipoEquipos ServTipoEquipos { get; set; }
        [Dependency]
        public IServGeneros ServGeneros { get; set; }
        [Dependency]
        public IServComunas ServComunas { get; set; }
        [Dependency]
        public IRegisterStudioViewModel RegisterStudioViewModel { get; set; }
        #endregion

        #region >>> Index's view
        public ActionResult Index()
        {
            var estudiosMejorEvaluados = ServEstudios.EstudiosMejorEvaluados(5);
            var estudiosMasPopulares = ServEstudios.EstudiosMasPopulares(5);

            var listadoRegiones = ServRegiones.RegionesChilenas();
            var listadoComunas = new List<Comuna> { Constantes.ComunaSinSeleccion };

            return View(new IndexUserViewModel(estudiosMejorEvaluados, estudiosMasPopulares, listadoRegiones, listadoComunas));
        }

        [HttpPost]
        public ActionResult Index(IndexUserViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                var roles = AutenticarUsuario(usuario.LoginUsuario);
                if (roles != null)
                {
                    if (roles.Contains("Estudio"))
                        return RedirectToAction("Index", "User");
                    if (roles.Contains("Usuario"))
                        return RedirectToAction("Index", "User");
                }

                ModelState.AddModelError("", "Incorrect username and/or password");
            }

            return View(new IndexUserViewModel());
        }
        #endregion

        #region >>> Register User
        public ActionResult RegisterUser()
        {
            return View(new RegisterUserViewModel());
        }

        [HttpPost]
        public ActionResult RegisterUser(RegisterUserViewModel model)
        {
            var usuario = model.RegistroUsuario;
            if (ServUsuarios.EsUsuarioNuevo(usuario))
            {
                ServUsuarios.Guardar(usuario);
            }

            var notificacion = new Notificacion();
            model = new RegisterUserViewModel
            {
                MostrarNotificacion = true,
                Notificaciones = notificacion.AgregarNotificacion(TipoNotificacion.Error, new List<string> { Constantes.MensajeErrorUsuarioExistente }, "Titulo")
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult RegisterUserFacebook(IdentificacionDeUsuarioPorFacebook usuario)
        {
            if (ModelState.IsValid)
            {
                if (ServUsuarios.EsUsuarioNuevo(usuario))
                    ServUsuarios.Guardar(usuario);

                var roles = AutenticarUsuario(usuario);
                if (roles != null)
                {
                    if (roles.Contains("Estudio"))
                        return Json(Url.Action("Index", "User"));
                    if (roles.Contains("Usuario"))
                        return Json(Url.Action("Index", "User"));
                }

                ModelState.AddModelError("", "Incorrect username and/or password");
            }

            return Json(Url.Action("Index", "User"));
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
                var membershipUser = Membership.CreateUser(usuario.Nombre, usuario.Password, usuario.Email, null, null, false, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    Roles.AddUserToRole(usuario.Nombre, "Banda");

                    if (membershipUser != null && membershipUser.ProviderUserKey != null)
                        //usuario.Banda.UserId = (Guid)membershipUser.ProviderUserKey;

                        //ServBandas.Guardar(usuario.Banda);
                        //ServBandas.Actualizar();

                        ServEmail.EnviarEmailDeConfirmacion(membershipUser);

                    FormsAuthentication.SetAuthCookie(usuario.Nombre, false /* createPersistentCookie */);

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
            RegisterStudioViewModel.Regiones = ServRegiones.RegionesChilenas();
            RegisterStudioViewModel.Comunas = new List<Comuna>();

            return View(RegisterStudioViewModel);
        }

        [HttpPost]
        public ActionResult RegisterStudio([Bind] RegistroDeUsuario model)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus createStatus;
                var membershipUser = Membership.CreateUser(model.Nombre, model.Password, model.Email, null, null, false, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    try
                    {
                        Roles.AddUserToRole(model.Nombre, "Estudio");

                        if (membershipUser != null && membershipUser.ProviderUserKey != null)
                            //model.Estudio.UserId = (Guid)membershipUser.ProviderUserKey;

                            //ServEstudios.Guardar(model.Estudio);
                            ServEstudios.Actualizar();

                        ServEmail.EnviarEmailDeConfirmacion(membershipUser);

                        //if (membershipUser != null && membershipUser.ProviderUserKey != null)
                        //model.Estudio.UserId = (Guid)membershipUser.ProviderUserKey;

                        //ServEstudios.Guardar(model.Estudio);
                        ServEstudios.Actualizar();

                        ServEmail.EnviarEmailDeConfirmacion(membershipUser);

                        FormsAuthentication.SetAuthCookie(model.Nombre, false /* createPersistentCookie */);


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

        [HttpPost]
        public ActionResult LoadStepView(RegisterStudioViewModel model)
        {
            if (model.PasoNumero == 2) // Información general
            {
                model.Regiones = ServRegiones.RegionesChilenas();

                if (model.Estudio.IdRegion != 0)
                    model.Comunas = ServComunas.TodasDeUnaRegion(model.Estudio.IdRegion);
                else
                    model.Comunas = new List<Comuna>(); 
            }
            else if (model.PasoNumero == 4) // Información de equipos
            {
                ViewBag.TipoEquipos = ServTipoEquipos.TiposDeEquipos();
            }

            string view;
            StepsViews.TryGetValue(model.PasoNumero, out view);

            model.Estudio.Salas.ForEach(s => s.Equipos.ForEach(e => e.ViewState = EnumCollection.ViewState.Display));
            var sala = model.Estudio.Salas.FirstOrDefault();
            if (sala != null)
            {
                var equipo = sala.Equipos.LastOrDefault();
                if (equipo != null)
                    equipo.ViewState = EnumCollection.ViewState.Edit;
            }

            model.JsonModelResult = new JavaScriptSerializer().Serialize(model);

            return PartialView(view, model);
        }

        [HttpPost]
        public ActionResult EditRoom(RegisterStudioViewModel model)
        {
            switch (model.Accion)
            {
                case "AddNewRoom":
                    model.Estudio.Salas.Add(new RoomViewModel { Nombre = "noix" + (model.Estudio.Salas.Count() + 1).ToString() });
                    model.Estudio.Salas.ForEach(s => s.Equipos.ForEach(e => e.ViewState = EnumCollection.ViewState.Display));
                    var sala = model.Estudio.Salas.FirstOrDefault();
                    if (sala != null)
                    {
                        var equipo = sala.Equipos.FirstOrDefault();
                        if (equipo != null) equipo.ViewState = EnumCollection.ViewState.Edit;
                    }
                    break;
                case "DeleteRoom":
                    model.Estudio.Salas.RemoveAt(model.Estudio.IndiceSeleccionado);

                    break;
            }

            model.JsonModelResult = new JavaScriptSerializer().Serialize(model);

            return PartialView("Partial/_RegisterStudioRoomsInfo", model);
        }

        [HttpPost]
        public ActionResult EditEquipment(RegisterStudioViewModel model)
        {
            var sala = model.Estudio.Salas.ElementAt(model.Estudio.IndiceSeleccionado);

            switch (model.Accion)
            {
                case "AddNewEquipment":
                    sala.Equipos.ForEach(e => e.ViewState = EnumCollection.ViewState.Display);
                    sala.Equipos.Add(new RoomEquipmentViewModel { ViewState = EnumCollection.ViewState.Edit, Nombre = "equipo noix" + (sala.Equipos.Count() + 1).ToString() });

                    break;
                case "EditEquipment":
                    sala.Equipos.ForEach(e => e.ViewState = EnumCollection.ViewState.Display);
                    var equipo = sala.Equipos.ElementAt(sala.IndiceSeleccionado);
                    equipo.ViewState = EnumCollection.ViewState.Edit;

                    break;
                case "DeleteEquipment":
                    sala.Equipos.RemoveAt(sala.IndiceSeleccionado);

                    break;
                case "DisableEquipment":
                    sala.SinEquipos = true;
                    sala.Equipos = new List<RoomEquipmentViewModel>();

                    break;
                case "EnableEquipment":
                    sala.SinEquipos = false;
                    sala.Equipos = new List<RoomEquipmentViewModel>();

                    break;
            }

            if (ViewBag.TipoEquipos == null) ViewBag.TipoEquipos = ServTipoEquipos.TiposDeEquipos();
            model.JsonModelResult = new JavaScriptSerializer().Serialize(model);

            return PartialView("Partial/_RegisterStudioEquipmentInfo", model);
        }

        [HttpPost]
        public ActionResult SaveStudio(RegisterStudioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = model.Usuario;
                ServUsuarios.Guardar(usuario);

                var estudio = new Estudio();
                Mapper.Map(model.Estudio, estudio);
                
                ServEstudios.Guardar(estudio);

                var roles = AutenticarUsuario(new IdentificacionDeUsuario { Email = usuario.Email, Password = usuario.Password, Recordarme = true });
                if (roles != null)
                {
                    if (roles.Contains("Estudio"))
                        return RedirectToAction("SuccessfulRegister", "User");
                    if (roles.Contains("Usuario"))
                        return RedirectToAction("SuccessfulRegister", "User");
                }

                ModelState.AddModelError("", "Incorrect username and/or password");
            }

            return RedirectToAction("SuccessfulRegister", "User");
        }

        private Dictionary<int, string> StepsViews
        {
            get
            {
                var stepsViews = new Dictionary<int, string>
                    {
                        { 1, "Partial/_RegisterStudioLoginInfo" },
                        { 2, "Partial/_RegisterStudioGeneralInfo" },
                        { 3, "Partial/_RegisterStudioRoomsInfo" },
                        { 4, "Partial/_RegisterStudioEquipmentInfo" },
                        { 5, "Partial/_RegisterStudioScheduleInfo" }
                    };

                return stepsViews;
            }
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

        public ActionResult CloseSession()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("index");
        }

        public ActionResult AccountData()
        {
            return RedirectToAction("index");
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
