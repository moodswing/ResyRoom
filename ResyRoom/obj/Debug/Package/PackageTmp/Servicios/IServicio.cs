using System.Collections.Generic;

namespace ResyRoom.Servicios
{
    public interface IServicio<T>
    {
        IEnumerable<T> Todas();
        void Guardar(T objeto);
        void Actualizar();
    }
}
