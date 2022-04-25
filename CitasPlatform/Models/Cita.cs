using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitasPlatform.Models
{
    public class Cita

    {
        public string CitaId { get; set; }

        [Key]
        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
        public decimal Hora_Inicio { get; set; }
        public decimal Hora_Final { get; set; }
        public string Tipo { get; set; }
        public string Estatus { get; set; }
        public string? Descripcion { get; set; }
    }
}
