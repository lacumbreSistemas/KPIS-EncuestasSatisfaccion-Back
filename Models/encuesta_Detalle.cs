namespace EncuestaApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class encuesta_Detalle
    {
        public int id { get; set; }

        public int? idPregunta { get; set; }

        public int? Valoracion { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Fecha { get; set; }

        public int? Sucursal { get; set; }

        public string Comentario { get; set; }

        public int? idHeader { get; set; }
    }
}
