using System;
using System.Data;
using System.Linq;
using MoreLinq;
using ResyRoom.Models;
using System.Data.Entity;
using System.Collections.Generic;
using ResyRoom.Infraestructura.Extensiones;
using Estudio = ResyRoom.Models.Estudio;

namespace ResyRoom.Servicios
{
    public interface IServEstudios : IServicio<Estudio>
    {
        IEnumerable<Estudio> EstudiosMasPopulares(int numero, DateTime? desde = null, DateTime? hasta = null);
        IEnumerable<Estudio> EstudiosMejorEvaluados(int numero, DateTime? desde = null, DateTime? hasta = null);
        IEnumerable<Estudio> Busqueda(Busqueda param);
        Estudio CargarEstudio(int idEStudio);
    }

    public class ServEstudios : IServEstudios
    {
        private readonly ResyRoomEntities _context;

        public ServEstudios(ResyRoomEntities context) { _context = context; }

        public IEnumerable<Estudio> Todas()
        {
            var estudios = _context.Estudios.Include(e => e.Comuna.Region).Include(e => e.Salas);
            var resultado = from s in estudios select s;

            return resultado.ToList();
        }

        public IEnumerable<Estudio> EstudiosMasPopulares(int numero, DateTime? desde = null, DateTime? hasta = null)
        {
            var consulta = (from estudio in _context.Estudios.AsNoTracking()
                            join sala in _context.Salas on estudio.IdEstudio equals sala.IdEstudio
                            join reserva in _context.Reservas on sala.IdSala equals reserva.IdSala
                            where ((desde != null && hasta != null && reserva.Fecha >= desde && reserva.Fecha <= hasta)
                                || (desde != null && hasta == null && reserva.Fecha >= desde)
                                || (hasta != null && desde == null && reserva.Fecha <= hasta)
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
                                || (desde != null && hasta == null && comentario.Fecha >= desde)
                                || (hasta != null && desde == null && comentario.Fecha <= hasta)
                                || (desde == null && hasta == null))
                              group comentario by estudio into estudios
                              orderby estudios.Average(k => k.Puntuacion) descending
                              select estudios.Key).Take(numero);

            return resultados;
        }

        public IEnumerable<Estudio> Busqueda(Busqueda param)
        {
            var estudios = _context.Estudios
                .Include(e => e.Comuna.Region)
                .Include(e => e.Salas);
            
            var precioDesde = Convert.ToInt32(param.PrecioDesde);
            var precioHasta = Convert.ToInt32(param.PrecioHasta);

            var precioGrabacionDesde = Convert.ToInt32(param.PrecioPorGrabacionDeCancionDesde);
            var precioGrabacionHasta = Convert.ToInt32(param.PrecioPorGrabacionDeCancionDesde);

            var buscaPorNombre = !param.NombreEstudio.IsEmptyOrWs();

            var resultados = (from estudio in estudios
                              where
                              ((buscaPorNombre && estudio.Nombre.Contains(param.NombreEstudio)) || !buscaPorNombre)

                              && ((param.IdRegion != 0 && estudio.Comuna.IdRegion == param.IdRegion) || param.IdRegion == 0)
                              && ((param.IdComuna != 0 && estudio.Comuna.IdComuna == param.IdComuna) || param.IdComuna == 0)

                              && ((precioDesde != 0 && precioHasta != 0 && estudio.Salas.Any(s => s.Precio >= precioDesde) && estudio.Salas.Any(s => s.Precio <= precioHasta))
                                || (precioDesde != 0 && precioHasta == 0 && estudio.Salas.Any(s => s.Precio >= precioDesde))
                                || (precioHasta != 0 && precioDesde == 0 && estudio.Salas.Any(s => s.Precio <= precioHasta))
                                || (precioDesde == 0 && precioHasta == 0))

                              && ((param.ConComentarios && estudio.Salas.Select(s => s.Comentarios).Any()) || !param.ConComentarios)
                              && ((param.ConSetDePlatos && estudio.Salas.Any(s => s.SetDePlatos ?? false)) || !param.ConSetDePlatos)
                              && ((param.ConDoblePedal && estudio.Salas.Any(s => s.DoblePedal ?? false)) || !param.ConDoblePedal)
                              && ((param.NroDeMicrofonos != 0 && estudio.Salas.Select(s => s.Equipos.Where(e => e.TipoEquipo.Descripcion == "Microfonos")).Count() == param.NroDeMicrofonos) || param.NroDeMicrofonos == 0)

                              && ((precioGrabacionDesde != 0 && precioGrabacionHasta != 0 && estudio.Salas.Any(s => s.Grabaciones.Last().PrecioPorCancion >= precioGrabacionDesde) && estudio.Salas.Any(s => s.Grabaciones.Last().PrecioPorCancion <= precioGrabacionHasta))
                                || (precioGrabacionDesde != 0 && precioGrabacionHasta == 0 && estudio.Salas.Any(s => s.Grabaciones.Last().PrecioPorCancion >= precioGrabacionDesde))
                                || (precioGrabacionHasta != 0 && precioGrabacionDesde == 0 && estudio.Salas.Any(s => s.Grabaciones.Last().PrecioPorCancion <= precioGrabacionHasta))
                                || (precioGrabacionDesde == 0 && precioGrabacionHasta == 0))

                              && ((param.Masterizacion && estudio.Salas.Select(s => s.Grabaciones.Last().Masterizacion).Any()) || !param.Masterizacion)

                              select estudio);

            return resultados.ToList();
        }

        public Estudio CargarEstudio(int idEStudio)
        {
            var estudios = _context.Estudios
                                   .Include(e => e.Comuna.Region)
                                   .Include(e => e.Salas.Select(s => s.Horarios))
                                   .Include(e => e.Salas.Select(s => s.Reservas))
                                   .Include(e => e.Salas.Select(s => s.Grabaciones))
                                   .Include(e => e.Salas.Select(s => s.Equipos));

            return estudios.First(s => s.IdEstudio == idEStudio);
        }

        public void Guardar(Estudio estudio)
        {
            if (estudio.IdEstudio.Equals(default(int))) _context.Estudios.Add(estudio);
            else _context.Entry(estudio).State = EntityState.Modified;
        }

        public void Actualizar() { _context.SaveChanges(); }
    }
}