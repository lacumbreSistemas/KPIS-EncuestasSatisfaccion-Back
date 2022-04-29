namespace EncuestaApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preguntas
    {
        public int id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Comentario { get; set; }
    }
}
