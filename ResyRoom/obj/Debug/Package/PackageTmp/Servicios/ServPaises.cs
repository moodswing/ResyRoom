using System.Linq;
using ResyRoom.Models;

namespace ResyRoom.Servicios
{
    public interface IServPaises
    {
        IQueryable<Pais> Todos();
    }

    public class ServPaises : IServPaises
    {
        private readonly ResyRoomEntities _context = new ResyRoomEntities();

        public IQueryable<Pais> Todos()
        {
            var r = from p in _context.Paises select p;

            return r;
        }
    }
}