
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SADVO.Domain.Entities;

namespace SADVO.Persistence.Data.Configurations
{
    public class CiudadanoConfig : IEntityTypeConfiguration<Ciudadano>
    {
        public void Configure(EntityTypeBuilder<Ciudadano> builder)
        {
            builder.ToTable("Ciudadanos");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Apellido)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.NumeroIdentificacion)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.EsActivo)
                .IsRequired()
                .HasDefaultValue(true);

            // Índice único para documento de identidad
            builder.HasIndex(c => c.NumeroIdentificacion)
                .IsUnique();
        }
    }
  
}
