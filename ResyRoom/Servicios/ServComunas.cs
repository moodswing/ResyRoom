using System.Collections.Generic;
using System.Linq;
using ResyRoom.Models;

namespace ResyRoom.Servicios
{
    public interface IServComunas
    {
        IList<Comuna> TodasDeUnaRegion(int idRegion);
    }

    public class ServComunas : IServComunas
    {
        private readonly ResyRoomEntities _context = new ResyRoomEntities();

        public IList<Comuna> TodasDeUnaRegion(int idRegion)
        {
            var r = from c in _context.Comunas
                    where c.IdRegion == idRegion
                    orderby c.Descripcion
                    select c;

            var resultado = r.ToList();
            resultado.Insert(0, Constantes.ComunaSinSeleccion);

            return resultado;
        }
    }
}