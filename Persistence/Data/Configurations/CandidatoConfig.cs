

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SADVO.Domain.Entities;

namespace SADVO.Persistence.Data.Configurations
{
    public class CandidatoConfig : IEntityTypeConfiguration<Candidato>
    {
        public void Configure(EntityTypeBuilder<Candidato> builder)
        {
            builder.ToTable("Candidatos");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Apellido)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Foto)
                .HasColumnName("Foto")
                .HasMaxLength(500);

            builder.Property(c => c.EsActivo)
                .IsRequired()
                .HasDefaultValue(true);

            // Relaciones
            builder.HasOne(c => c.Partido)
              .WithMany(p => p.Candidatos)
              .HasForeignKey(c => c.Id)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Puesto_Electivo)
                .WithMany(p => p.Candidatos)
                .HasForeignKey(c => c.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
