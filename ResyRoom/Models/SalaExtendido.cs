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
            public string Dia { get; set; }
            public int NroReservas { get; set; }
        }

        public struct SoloReserva
        {
            public string Dia { get; set; }
            public string Desde { get; set; }
            public string Hasta { get; set; }
        }

        public List<SoloReserva> ListadoReservas
        {
            get { return Reservas.Select(r => new SoloReserva { Dia = r.Fecha != null ? r.Fecha.Value.Date.ToString("yyyy-MM-dd") : r.Fecha.ToString(), Desde = r.Desde, Hasta = r.Hasta }).ToList(); }
        }

        public List<ReservasPorDía> ListadoReservasPorDía
        {
            get
            {
                var listado = Reservas.GroupBy(r => r.Fecha != null ? r.Fecha.Value.Date : new DateTime(), r => r, (key, g) => new { Dia = key, Reservas = g.ToList() }).ToList();

                var reservasPorDia = new List<ReservasPorDía>();
                foreach (var dia in listado)
                {
                    long nroReservas = 0;
                    foreach (var reserva in dia.Reservas)
                    {
                        var desde = new TimeSpan(Convert.ToInt32(reserva.Desde.Substring(0, 2).Replace(":", "")),
                                                                 Convert.ToInt32(reserva.Desde.Substring(reserva.Desde.Length - 2, 2).Replace(":", "")), 0);
                        var hasta = new TimeSpan(Convert.ToInt32(reserva.Hasta.Substring(0, 2).Replace(":", "")),
                                                                 Convert.ToInt32(reserva.Hasta.Substring(reserva.Hasta.Length - 2, 2).Replace(":", "")), 0);
                        var bloque = new TimeSpan(Convert.ToInt32(reserva.Sala.HorarioActivo.DuracionBloque.Substring(0, 2).Replace(":", "")),
                                                                 Convert.ToInt32(reserva.Sala.HorarioActivo.DuracionBloque.Substring(reserva.Sala.HorarioActivo.DuracionBloque.Length - 2, 2).Replace(":", "")), 0);

                        long remainder;
                        nroReservas += Math.DivRem(hasta.Subtract(desde).Ticks, bloque.Ticks, out remainder);
                    }

                    reservasPorDia.Add(new ReservasPorDía
                        {
                            Dia = dia.Dia.ToString("yyyy-MM-dd"),
                            NroReservas = (int) nroReservas
                        });
                }

                return reservasPorDia;
            }
        }
    }
}