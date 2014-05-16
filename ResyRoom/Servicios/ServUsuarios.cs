using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using ResyRoom.Models;

namespace ResyRoom.Servicios
{
    public interface IServUsuarios
    {
        Usuario Lee(int idUser);
        bool EsUsuarioNuevo(RegistroDeUsuario usuario);
        void Guardar(RegistroDeUsuario usuario);
    }

    public class ServUsuarios : IServUsuarios
    {
        private readonly ResyRoomEntities _context = new ResyRoomEntities();

        public Usuario Lee(int idUser)
        {
            var usuarios = (from user in _context.Usuarios where user.IdUsuario == idUser select user);
            var usuario = usuarios.First();

            return usuario;
        }

        public bool EsUsuarioNuevo(RegistroDeUsuario usuario)
        {
            return !(from user in _context.Usuarios where user.Email == usuario.Email || user.Nombre == usuario.Nombre select user).Any();
        }

        public void Guardar(RegistroDeUsuario usuario)
        {
            var user = new Usuario
                {
                    Email = usuario.Email,
                    EntraPorFacebook = usuario.IsFacebookLogin,
                    Nombre = usuario.Nombre,
                    Password = usuario.Password
                };


            _context.Usuarios.Add(user);
            _context.SaveChanges();
        }
    }
}