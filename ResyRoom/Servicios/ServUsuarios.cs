using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Runtime.Serialization;
using ResyRoom.Infraestructura.Extensiones;
using ResyRoom.Models;

namespace ResyRoom.Servicios
{
    public interface IServUsuarios
    {
        User Lee(Guid idUser);
    }

    public class ServUsuarios : IServUsuarios
    {
        private readonly ResyRoomEntities _context = new ResyRoomEntities();

        public User Lee(Guid idUser)
        {
            var usuario = (from user in _context.Users.Include(u => u.Bandas).Include(u => u.Estudios) where user.UserId == idUser select user).First();

            return usuario;
        }
    }
}