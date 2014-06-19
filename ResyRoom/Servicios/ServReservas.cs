using System.Linq;
using ResyRoom.Models;

namespace ResyRoom.Servicios
{
    public interface IServReservas
    {
        IQueryable<Reserva> Todos();
    }

    public class ServReservas : IServReservas
    {
        private readonly ResyRoomEntities _context = new ResyRoomEntities();

        public IQueryable<Reserva> Todos()
        {
            var r = from p in _context.Reservas select p;

            return r;
        }
    }
}