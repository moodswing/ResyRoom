using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoreLinq;
using ResyRoom.Models;

namespace ResyRoom.Servicios
{
    public interface IServRegiones
    {
        IQueryable<Region> RegionesDeUnPais(int idPais);
        IQueryable<Region> RegionesChilenas();
    }

    public class ServRegiones : IServRegiones
    {
        private readonly ResyRoomEntities _context = new ResyRoomEntities();

        public IQueryable<Region> RegionesChilenas()
        {
            var r = from s in _context.Regiones
                    where s.Pais.Descripcion == "Chile"
                    select s;

            return r;
        }

        public IQueryable<Region> RegionesDeUnPais(int idPais)
        {
            var r = from s in _context.Regiones
                    where s.IdPais == idPais
                    select s;

            return r;
        }
    }
}