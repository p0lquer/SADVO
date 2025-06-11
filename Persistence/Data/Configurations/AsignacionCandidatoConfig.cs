

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SADVO.Domain.Entities;

namespace SADVO.Persistence.Data.Configurations
{
    public class AsignacionCandidatoConfig : IEntityTypeConfiguration<Asignar_Candidato>
    {
        public void Configure(EntityTypeBuilder<Asignar_Candidato> builder)
        {
            builder.ToTable("AsignacionesCandidatos");

            builder.HasKey(a => a.Id);

            // Relaciones
            builder.HasOne(a => a.Candidato)
                .WithMany(a => a.Asignar_Candidato)
                .HasForeignKey(a => a.CandidatoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a._Electivo)
                .WithMany()
                .HasForeignKey(a => a.PuestoElectivoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.PartidoPolitico)
                .WithMany()
                .HasForeignKey(a => a.PartidoPoliticoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Índice único por partido-puesto
            builder.HasIndex(a => new { a.PartidoPoliticoId, a.PuestoElectivoId })
                .IsUnique();
        }
    }
}
