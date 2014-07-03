using System;
using System.Collections.Generic;
using System.Linq;

namespace ResyRoom.Models
{
    public partial class Sala
    {
        public Grabacion DatosGrabacion
        {
            get { return Grabaciones.Any() ? Grabaciones.Last() : null; }
        }

        public Horario HorarioActivo
        {
            get { return Horarios.Any() ? Horarios.Last() : null; }
        }

        public struct ReservasPorDía
        {
            public DateTime Dia { get; set; }
            public int NroReservas { get; set; }
        }

        public struct SoloReserva
        {
            public DateTime? Dia { get; set; }
            public string Desde { get; set; }            
            public string Hasta { get; set; }            
        }

        public List<SoloReserva> ListadoReservas
        {
            get { return Reservas.Select(r => new SoloReserva { Dia = r.Fecha != null ? r.Fecha.Value.Date : r.Fecha, Desde = r.Desde, Hasta = r.Hasta }).ToList(); }
        }

        public List<ReservasPorDía> ListadoReservasPorDía
        {
            get
            {
                return Reservas.GroupBy(r => r.Fecha != null ? r.Fecha.Value.Date : new DateTime(), r => r, (key, g) => new ReservasPorDía { Dia = key, NroReservas = g.Count() }).ToList();
            }
        }
    }
}