

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SADVO.Domain.Entities;

namespace SADVO.Persistence.Data.Configurations
{
    public class PartidoPoliticoConfig : IEntityTypeConfiguration<Partido_Politico>
    {
        public void Configure(EntityTypeBuilder<Partido_Politico> builder)
        {
            builder.ToTable("PartidosPoliticos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description);

            builder.Property(p => p.Siglas)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(p => p.Logo)
                .HasColumnName("Logo")
                .HasMaxLength(500);

            builder.Property(p => p.EsActivo)
                .IsRequired()
                .HasDefaultValue(true);

            // Índice único para siglas
            builder.HasIndex(p => p.Siglas)
                .IsUnique();
        }
    }
}
