using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Microsoft.Practices.Unity;
using ResyRoom.Servicios;

namespace ResyRoom.Models
{
    public partial class Estudio
    {
        [Dependency]
        public IServReservas ServRespaldo { get; set; }

        [DisplayName("Precio por hora desde")]
        public string PrecioPorHoraDesde
        {
            get
            {
                return Convert.ToDecimal(Salas.Select(s => s.Precio).Min()).ToString("C0", CultureInfo.CreateSpecificCulture("es-CL"));
            }
        }

        [DisplayName("Número de reservas en el sitio")]
        public int NroDeReservasTotales
        {
            get
            {
                if (Salas == null || !Salas.Any(s => s.Reservas != null && s.Reservas.Any()))
                    return 0;

                return Salas.Sum(s => s.Reservas.Count());
            }
        }

        [DisplayName("Posee alguna sala con doble pedal")]
        public bool PoseeSalaConDoblePedal { get { return Salas.Any(s => s.DoblePedal == true); } }

        [DisplayName("Posee alguna sala con set de platos")]
        public bool PoseeSalaConSetDePlatos { get { return Salas.Any(s => s.SetDePlatos == true); } }

        [DisplayName("Existen comentarios")]
        public bool PoseeComentarios { get { return Salas.Any(s => s.Comentarios.Any()); } }

        [DisplayName("Posee la información de los equipos")]
        public bool PoseeInfoDeEquipos { get { return Salas.Any(s => s.Equipos.Any()); } }

        [DisplayName("Posee salas habilitadas para grabación")]
        public bool PoseeGrabacion { get { return Salas.Any(s => s.Grabaciones.Any()); } }
    }
}