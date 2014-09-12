using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoreLinq;
using ResyRoom.Models;

namespace ResyRoom.Servicios
{
    public interface IServTipoEquipos
    {
        IList<TipoEquipo> TiposDeEquipos();
    }

    public class ServTipoEquipos : IServTipoEquipos
    {
        private readonly ResyRoomEntities _context = new ResyRoomEntities();

        public IList<TipoEquipo> TiposDeEquipos()
        {
            var r = from s in _context.TipoEquipos
                    select s;

            var resultado = r.OrderBy(te => te.Descripcion).ToList();
            resultado.Insert(0, Constantes.TipoEquipoSinSeleccion);

            return resultado;
        }
    }
}