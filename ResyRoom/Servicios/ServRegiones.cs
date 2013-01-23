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
        IList<Region> RegionesDeUnPais(int idPais);
        IList<Region> RegionesChilenas();
    }

    public class ServRegiones : IServRegiones
    {
        private readonly ResyRoomEntities _context = new ResyRoomEntities();

        public IList<Region> RegionesChilenas()
        {
            var r = from s in _context.Regiones
                    where s.Pais.Descripcion == "Chile"
                    select s;

            var resultado = r.ToList();
            resultado.Insert(0, Constantes.RegionSinSeleccion);

            return resultado;
        }

        public IList<Region> RegionesDeUnPais(int idPais)
        {
            var r = from s in _context.Regiones
                    where s.IdPais == idPais
                    select s;

            var resultado = r.ToList();
            resultado.Insert(0, Constantes.RegionSinSeleccion);

            return resultado;
        }
    }
}