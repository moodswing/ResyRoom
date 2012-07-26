using System.Linq;
using ResyRoom.Models;

namespace ResyRoom.Servicios
{
    public interface IServComunas
    {
        IQueryable<Comuna> TodasDeUnaRegion(int idRegion);
    }

    public class ServComunas : IServComunas
    {
        private readonly ResyRoomEntities _context = new ResyRoomEntities();

        public IQueryable<Comuna> TodasDeUnaRegion(int idRegion)
        {
            var r = from c in _context.Comunas
                    where c.IdRegion == idRegion
                    select c;

            return r;
        }
    }
}