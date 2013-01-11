using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using ResyRoom.Models;
using System.Collections.Generic;

namespace ResyRoom.Servicios
{
    public interface IServEstudios : IServicio<Estudio>
    {
        IEnumerable<Estudio> EstudiosMasPopulares(int numero, DateTime? desde = null, DateTime? hasta = null);
        IEnumerable<Estudio> EstudiosMejorEvaluados(int numero, DateTime? desde = null, DateTime? hasta = null);
    }

    public class ServEstudios : IServEstudios
    {
        private readonly ResyRoomEntities _context;

        public ServEstudios(ResyRoomEntities context) { _context = context; }

        public IEnumerable<Estudio> Todas() { return from s in _context.Estudios select s; }

        public IEnumerable<Estudio> EstudiosMasPopulares(int numero, DateTime? desde = null, DateTime? hasta = null)
        {
            var consulta = (from estudio in _context.Estudios.AsNoTracking()
                            join sala in _context.Salas on estudio.IdEstudio equals sala.IdEstudio
                            join horario in _context.Horarios on sala.IdSala equals horario.IdSala
                            join reserva in _context.Reservas on horario.IdHorario equals reserva.IdHorario
                            where ((desde != null && hasta != null && reserva.Fecha >= desde && reserva.Fecha <= hasta)
                                || (desde != null && reserva.Fecha >= desde)
                                || (hasta != null && reserva.Fecha <= hasta)
                                || (desde == null && hasta == null))
                            group reserva by estudio into reservas
                            orderby reservas.Count() descending
                            select reservas.Key).Take(numero);

            //Code Comment: Include Code
            //return consulta.Include(e => e.Salas.Select(s => s.Comentarios.Select(c => c.Banda)));
            return consulta;
        }

        public IEnumerable<Estudio> EstudiosMejorEvaluados(int numero, DateTime? desde = null, DateTime? hasta = null)
        {
            var resultados = (from estudio in _context.Estudios
                              join sala in _context.Salas on estudio.IdEstudio equals sala.IdEstudio
                              join comentario in _context.Comentarios on sala.IdSala equals comentario.IdSala
                              where ((desde != null && hasta != null && comentario.Fecha >= desde && comentario.Fecha <= hasta)
                                || (desde != null && comentario.Fecha >= desde)
                                || (hasta != null && comentario.Fecha <= hasta)
                                || (desde == null && hasta == null))
                              group comentario by estudio into estudios
                              orderby estudios.Average(k => k.Puntuacion) descending
                              select estudios.Key).Take(numero);

            return resultados;
        }

        public void Guardar(Estudio estudio)
        {
            if (estudio.IdEstudio.Equals(default(int))) _context.Estudios.Add(estudio);
            else _context.Entry(estudio).State = EntityState.Modified;
        }

        public void Actualizar() { _context.SaveChanges(); }
    }
}