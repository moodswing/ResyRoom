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
                    select c;

            var resultado = r.ToList();

            if (!resultado.Any()) resultado.Add(Constantes.ComunaSinSeleccion);

            return resultado;
        }
    }
}