using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestaApi.Models
{
    public class Puntaje
    {
        public DateTime? Fecha { get; set; }

        public int Valoracion { get; set; }

        public string Nombre { get; set; }
    }
}