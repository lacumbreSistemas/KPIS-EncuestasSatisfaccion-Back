using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace EncuestaApi.Models
{
    public partial class EncuestaModel : DbContext
    {
        public EncuestaModel()
            : base("name=EncuestaModel")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<encuesta_Detalle> encuesta_Detalle { get; set; }
        public virtual DbSet<encuesta_header> encuesta_header { get; set; }
        public virtual DbSet<preguntas> preguntas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<encuesta_Detalle>()
                .Property(e => e.Comentario)
                .IsUnicode(false);

            modelBuilder.Entity<preguntas>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<preguntas>()
                .Property(e => e.Comentario)
                .IsUnicode(false);
        }
    }
}
