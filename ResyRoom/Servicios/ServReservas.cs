using System;
using System.Linq;
using ResyRoom.Models;

namespace ResyRoom.Servicios
{
    public interface IServReservas
    {
        IQueryable<Reserva> Todos();
        bool Guardar(Reserva reserva);
    }

    public class ServReservas : IServReservas
    {
        private readonly ResyRoomEntities _context = new ResyRoomEntities();

        public IQueryable<Reserva> Todos()
        {
            var r = from p in _context.Reservas select p;

            return r;
        }

        private bool VerificarHoraDisponible(Reserva reserva)
        {
            var listado = _context.Reservas.Where(r => r.IdSala == reserva.IdSala && r.Fecha.Value.Year == reserva.Fecha.Value.Year && r.Fecha.Value.Month == reserva.Fecha.Value.Month
                && r.Fecha.Value.Day == reserva.Fecha.Value.Day).ToList();

            var consulta = listado.Select(r => new
                {
                    Desde = new DateTime(r.Fecha.Value.Year, r.Fecha.Value.Month, r.Fecha.Value.Day,
                                         Convert.ToInt32(r.Desde.Substring(0, 2).Replace(":", "")),
                                         Convert.ToInt32(r.Desde.Substring(r.Desde.Length - 2, 2).Replace(":", "")), 0),
                    Hasta = new DateTime(r.Fecha.Value.Year, r.Fecha.Value.Month, r.Fecha.Value.Day,
                                         Convert.ToInt32(r.Hasta.Substring(0, 2).Replace(":", "")),
                                         Convert.ToInt32(r.Hasta.Substring(r.Hasta.Length - 2, 2).Replace(":", "")), 0)
                });

            var reservaConsulta = new
                {
                    Desde = new DateTime(reserva.Fecha.Value.Year, reserva.Fecha.Value.Month, reserva.Fecha.Value.Day,
                        Convert.ToInt32(reserva.Desde.Substring(0, 2).Replace(":", "")),
                        Convert.ToInt32(reserva.Desde.Substring(reserva.Desde.Length - 2, 2).Replace(":", "")), 0),
                    Hasta = new DateTime(reserva.Fecha.Value.Year, reserva.Fecha.Value.Month, reserva.Fecha.Value.Day,
                        Convert.ToInt32(reserva.Hasta.Substring(0, 2).Replace(":", "")),
                        Convert.ToInt32(reserva.Hasta.Substring(reserva.Hasta.Length - 2, 2).Replace(":", "")), 0)
                };

            return consulta.Any(r => (r.Desde >= reservaConsulta.Desde && r.Desde <= reservaConsulta.Hasta) ||
                (r.Hasta >= reservaConsulta.Desde && r.Hasta <= reservaConsulta.Hasta));
        }

        public bool Guardar(Reserva reserva)
        {
            if (VerificarHoraDisponible(reserva))
                return false;

            _context.Reservas.Add(reserva);
            _context.SaveChanges();

            return true;
        }
    }
}