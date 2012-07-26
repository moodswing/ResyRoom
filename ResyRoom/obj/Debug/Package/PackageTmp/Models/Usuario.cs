using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ResyRoom.Models
{
    public partial class User : IEquatable<User>
    {
        bool IEquatable<User>.Equals(User other) { return UserId.Equals(other.UserId); }
    }

    public class CambioDeContraseña
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña Actual")]
        public string PasswordActual { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La contraseña debe tener al menos {2} caracteres de largo.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva Contraseña")]
        public string NuevoPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nueva contraseña")]
        [Compare("NuevoPassword", ErrorMessage = "Las contraseñas no coinciden.")]
        public string PasswordConfirmacion { get; set; }
    }

    public class IdentificacionDeUsuario
    {
        [Required(ErrorMessage = "Ingrese nombre de usuario")]
        public string Usuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "No cerrar sesión")]
        public bool Recordarme { get; set; }
    }

    public class RegistroDeUsuario
    {
        [Required]
        public string Usuario { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La contraseña debe tener al menos {2} caracteres de largo.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nueva contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string PasswordConfirmacion { get; set; }

        public Banda Banda { get; set; }
        public Estudio Estudio { get; set; }
    }
}