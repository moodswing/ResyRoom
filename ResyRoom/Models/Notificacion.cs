using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResyRoom.Models
{
    public class Notificacion
    {
        public TipoNotificacion Tipo { get; set; }
        public string Titulo { get; set; }

        public List<string> Mensajes { get; set; }

        private List<Notificacion> ListaNotificaciones { get; set; }

        public List<Notificacion> AgregarNotificacion(TipoNotificacion tipo, List<string> mensajes, string titulo = null)
        {
            if (ListaNotificaciones == null) ListaNotificaciones = new List<Notificacion>();
            if (Mensajes == null) Mensajes = new List<string>();

            Tipo = tipo;
            Titulo = titulo;

            Mensajes.AddRange(mensajes);

            ListaNotificaciones.Add(new Notificacion { Mensajes = Mensajes, Tipo = Tipo, Titulo = Titulo });

            return ListaNotificaciones;
        }
    }

    public enum TipoNotificacion
    {
        Alerta = 1,
        Error = 2,
        Informacion = 3
    }
}