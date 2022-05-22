using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitasPlatform.Models
{
    public class Cita

    {
        public int CitaId { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
        public decimal Hora_Inicio { get; set; }
        public decimal Hora_Final { get; set; }
        public string Tipo { get; set; }
        public string Estatus { get; set; }
        public string? Descripcion { get; set; }

        // Informacion complementaria
        [NotMapped]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}")]
        public DateTime citaDateTime { get; set; }

        [NotMapped]
        public string H_Inicio { get; set; }
        [NotMapped]
        public string H_Final { get; set; }
    }
}
