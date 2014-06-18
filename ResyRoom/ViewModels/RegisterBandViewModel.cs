using ResyRoom.Models;
using System.Collections.Generic;

namespace ResyRoom.ViewModels
{
    public class RegisterBandViewModel : BaseViewModel
    {
        public IEnumerable<Genero> ListaDeGeneros { get; private set; }
        public RegistroDeUsuario Usuario { get; set; }

        public RegisterBandViewModel(RegistroDeUsuario usuario, IEnumerable<Genero> listaDeGeneros)
        {
            Usuario = usuario;
            ListaDeGeneros = listaDeGeneros;
        }
    }
}