using System.Linq;
using ResyRoom.Models;

namespace ResyRoom.Servicios
{
    public interface IServReservas
    {
        IQueryable<Reserva> Todos();
        int NroDeReservasPorEstudio(int idEstudio);
        int NroDeReservasPorSala(int idSala);
    }

    public class ServReservas : IServReservas
    {
        private readonly ResyRoomEntities _context = new ResyRoomEntities();

        public IQueryable<Reserva> Todos()
        {
            var r = from p in _context.Reservas select p;

            return r;
        }

        public int NroDeReservasPorEstudio(int idEstudio)
        {
            throw new System.NotImplementedException();
        }

        public int NroDeReservasPorSala(int idSala)
        {
            throw new System.NotImplementedException();
        }
    }
}