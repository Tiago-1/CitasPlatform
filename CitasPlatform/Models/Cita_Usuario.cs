
using System.ComponentModel.DataAnnotations.Schema;

namespace CitasPlatform.Models

{
    public class Cita_Usuario
    {
        public int Cita_UsuarioId { get; set; }

        [ForeignKey("Cita")]
        public int CitaId { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

    }
}
