

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SADVO.Domain.Entities;

namespace SADVO.Persistence.Data.Configurations
{
   public class AlianzaPoliticaConfig :  IEntityTypeConfiguration<Alianzas_Politica>
    {
        public void Configure(EntityTypeBuilder<Alianzas_Politica> builder)
        {
            builder.ToTable("AlianzasPoliticas");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.FechaRespuesta)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(a => a.FechaSolicitud)
             .IsRequired()
             .HasColumnType("datetime");

            builder.Property(a => a.Estado)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);


            builder.HasOne(a => a.PartidoSolicitante)
          .WithMany(p => p.AlianzasSolicitadas)
          .HasForeignKey(a => a.PartidoSolicitanteId)
          .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.PartidoReceptor)
                   .WithMany(p => p.AlianzasRecibidas)
                   .HasForeignKey(a => a.PartidoReceptorId)
                   .OnDelete(DeleteBehavior.Restrict);
            
            // Restricción única para evitar duplicados
                        builder.HasIndex(a => new { a.PartidoSolicitanteId, a.PartidoReceptorId })
                .IsUnique();
        }
    }
}
