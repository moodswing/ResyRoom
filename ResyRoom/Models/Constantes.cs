using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResyRoom.Models
{
    public static class Constantes
    {
        public static readonly Comuna ComunaSinSeleccion = new Comuna { Descripcion = "Seleccione Comuna", IdComuna = 0 };
        public static readonly Region RegionSinSeleccion = new Region { Descripcion = "Seleccione ...", IdRegion = 0 };
        public static readonly TipoEquipo TipoEquipoSinSeleccion = new TipoEquipo { Descripcion = "Seleccione ...", IdTipoEquipo = 0 };

        public static string SessionKey = "UserSession";

        public const string MensajeErrorUsuarioExistente = "El usuario que intenta registrar ya existe. Favor verificar sus datos.";

        public const string ReservaRealizadaConExito = "Tu reserva se ha realizado con éxito!";
        public const string SubtituloReservaRealizadaConExito = "Te envíaremos un correo de confirmación con los datos de tu reserva.";

        public const string ReservaFallida = "Lo sentimos, no se ha podido realizar tu reserva";
        public const string SubtituloReservaFallida = "Por favor vuelve a verificar que la hora que has intentado reservar esté disponible.";
    }
}