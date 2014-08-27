﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ResyRoom.Models
{
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
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "No cerrar sesión")]
        public bool Recordarme { get; set; }
    }

    public class IdentificacionDeUsuarioPorFacebook
    {
        [Required(ErrorMessage = "Ingrese nombre de usuario")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string FacebookId { get; set; }
    }

    public class RegistroDeUsuario
    {
        [Required]
        [Display(Name = "Tu Nombre")]
        public string Nombre { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "La dirección de correo ingresada no es valida.")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La contraseña debe tener al menos {2} caracteres de largo.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "Por favor verifica que ambas contraseñas coincidan.")]
        public string PasswordConfirmacion { get; set; }

        public Banda Banda { get; set; }
        public Estudio Estudio { get; set; }
    }
}