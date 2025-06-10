
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SADVO.Domain.Entities;

namespace SADVO.Persistence.Data.Configurations
{
    public class DirigentePoliticoConfig : IEntityTypeConfiguration<Dirigente_Politico>
    {
        public void Configure(EntityTypeBuilder<Dirigente_Politico> builder)
        {
            builder.ToTable("AsignacionesDirigentes");

            builder.HasKey(a => a.Id);

            // Relaciones
            builder.HasOne(a => a.Usuario)
                .WithMany()
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.PartidoPolitico)
                .WithMany(p => p.DirigentePoliticos)
                .HasForeignKey(a => a.PartidoPoliticoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Índice único para evitar múltiples asignaciones
            builder.HasIndex(a => a.UsuarioId)
                .IsUnique();
        }
    }
}
