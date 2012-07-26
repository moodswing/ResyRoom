using System.Linq;
using System.Web.Helpers;
using System.Web.Security;
using ResyRoom.Models;
using System.Collections.Generic;

namespace ResyRoom.Servicios
{
    public interface IServEmail
    {
        void EnviarEmailDeConfirmacion(MembershipUser usuario);
    }

    public class ServEmail : IServEmail
    {
        public ServEmail()
        {
            WebMail.SmtpServer = Properties.Settings.Default.SmtpServer;
            WebMail.UserName = Properties.Settings.Default.ConfirmationUserAccount;
            WebMail.Password = Properties.Settings.Default.ConfirmationPasswordAccount;
            WebMail.EnableSsl = true;
        }

        public void EnviarEmailDeConfirmacion(MembershipUser usuario)
        {
            const string asunto = "Confirma tu cuenta en ResyRoom.";
            const string de = "confirm-email@resyroom.com";
            var linkDeConfirmación = Properties.Settings.Default.DomainName + "User/ConfirmEmail/" + usuario.ProviderUserKey;

            var cuerpo = "<p style='font-size: 1.3em;'>Hola, " + usuario.UserName + "</p><br />" +
                         "Por favor, confirma tu cuenta de usuario en ResyRoom haciendo click en el siguiente enlace:<br />" +
                         "<a href='" + linkDeConfirmación + "'>" + linkDeConfirmación + "</a>" +
                         "<br /><br />" +
                         "Al confirmar tu cuenta tendrás acceso a la información de los mejores estudios de música.<br /><br />" +
                         "Atentamente, el equipo de ResyRoom.<br /><br /><hr>" +
                         "<p style='font-size: 0.8em;'>Si recibiste este mensaje por error y no te registrastes para una cuenta en ResyRoom, haz click esta cuenta no es mía." +
                         "Por favor, no respondas a este mensaje; fue enviado desde una dirección de correo electrónico no monitoreada.</p>";

            WebMail.Send(usuario.Email, asunto, cuerpo, de);
        }
    }
}