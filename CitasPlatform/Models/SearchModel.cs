using System;
using System.ComponentModel.DataAnnotations;

namespace CitasPlatform.Models
{
    public class SearchModel
    {
        public int usuarioId { get; set; }
        public string searchNombre { get; set; }
        public string searchEstado { get; set; }
        public string searchFecha { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}")]
        public DateTime Fecha { get; set; }

    }
}
