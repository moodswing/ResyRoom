using System;
using System.Data.Entity;
using System.Linq;
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
            var usuarios = (from user in _context.Users where user.UserId == idUser select user).Include(u => u.Estudios).Include(u => u.Bandas);
            var usuario = usuarios.First();

            return usuario;
        }
    }
}