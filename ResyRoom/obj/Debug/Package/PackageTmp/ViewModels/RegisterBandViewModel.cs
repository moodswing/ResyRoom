using ResyRoom.Models;
using System.Collections.Generic;

namespace ResyRoom.ViewModels
{
    public class RegisterBandViewModel
    {
        public RegistroDeUsuario Usuario { get; private set; }
        public IEnumerable<Genero> ListaDeGeneros { get; private set; }

        public RegisterBandViewModel(RegistroDeUsuario usuario, IEnumerable<Genero> listaDeGeneros)
        {
            Usuario = usuario;
            ListaDeGeneros = listaDeGeneros;
        }
    }
}