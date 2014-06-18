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
        bool EsUsuarioNuevo(IdentificacionDeUsuarioPorFacebook usuario);
        void Guardar(RegistroDeUsuario usuario);
        void Guardar(IdentificacionDeUsuarioPorFacebook usuario);
        Usuario AutenticarUsuario(IdentificacionDeUsuario usuario);
        Usuario AutenticarUsuario(IdentificacionDeUsuarioPorFacebook usuario);
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

        public bool EsUsuarioNuevo(IdentificacionDeUsuarioPorFacebook usuario)
        {
            return !(from user in _context.Usuarios where user.Email == usuario.Email select user).Any();
        }

        public void Guardar(RegistroDeUsuario usuario)
        {
            var user = new Usuario
                {
                    Email = usuario.Email,
                    Nombre = usuario.Nombre,
                    Password = usuario.Password
                };

            _context.Usuarios.Add(user);
            _context.SaveChanges();
        }

        public void Guardar(IdentificacionDeUsuarioPorFacebook usuario)
        {
            var user = new Usuario
                {
                    Email = usuario.Email,
                    EntraPorFacebook = true,
                    Nombre = usuario.Nombre,
                };

            _context.Usuarios.Add(user);
            _context.SaveChanges();
        }

        public Usuario AutenticarUsuario(IdentificacionDeUsuario usuario)
        {
            return _context.Usuarios.Include(u => u.Roles).FirstOrDefault(u => u.Email == usuario.Email && u.Password == usuario.Password);
        }

        public Usuario AutenticarUsuario(IdentificacionDeUsuarioPorFacebook usuario)
        {
            return _context.Usuarios.Include(u => u.Roles).FirstOrDefault(u => u.Email == usuario.Email);
        }
    }
}