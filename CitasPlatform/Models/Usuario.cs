using System;
using System.ComponentModel.DataAnnotations;

namespace CitasPlatform.Models
{
    public class Usuario
    {
        public int UsuarioId{ get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Matricula { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public int Rol { get; set; }
        public string Pass { get; set; }


    }
}