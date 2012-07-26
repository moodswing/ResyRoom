using System.Data;
using System.Linq;
using ResyRoom.Models;
using ResyRoom.Servicios;
using System.Collections.Generic;

namespace ResyRoom.Servicios
{
    public interface IServBandas : IServicio<Banda>
    {
    }

    public class ServBandas : IServBandas
    {
        private readonly ResyRoomEntities _context;

        public ServBandas(ResyRoomEntities context) { _context = context; }

        public void Guardar(Banda banda)
        {
            if (banda.IdBanda.Equals(default(int))) _context.Bandas.Add(banda);
            else _context.Entry(banda).State = EntityState.Modified;
        }

        public void Actualizar() { _context.SaveChanges(); }

        public IEnumerable<Banda> Todas() { return from b in _context.Bandas select b; }
    }
}