using System.Data;
using System.Linq;
using MoreLinq;
using ResyRoom.Models;
using System.Collections.Generic;

namespace ResyRoom.Servicios
{
    public interface IServGeneros : IServicio<Genero>
    {
        IList<Genero> ListadoParaCombo();
    }

    public class ServGeneros : IServGeneros
    {
        private readonly ResyRoomEntities _context = new ResyRoomEntities();

        public IList<Genero> ListadoParaCombo()
        {
            var resultados = new List<Genero> { new Genero { IdGenero = 0, Descripcion = "Seleccionar..." } };
            (from genero in _context.Generos select genero).ForEach(resultados.Add);

            return resultados;
        }

        public void Guardar(Genero genero)
        {
            if (genero.IdGenero.Equals(default(int))) _context.Generos.Add(genero);
            else _context.Entry(genero).State = EntityState.Modified;
        }

        public IEnumerable<Genero> Todas() { return from s in _context.Generos select s; }

        public void Actualizar() { _context.SaveChanges(); }
    }
}