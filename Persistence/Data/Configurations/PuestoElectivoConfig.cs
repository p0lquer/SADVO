

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SADVO.Domain.Entities;

namespace SADVO.Persistence.Data.Configurations
{
    public class PuestoElectivoConfig : IEntityTypeConfiguration<Puesto_Electivo>
    {
        public void Configure(EntityTypeBuilder<Puesto_Electivo> builder)
        {

            builder.ToTable("PuestosElectivos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .IsRequired();

            builder.Property(p => p.EsActivo)
                .IsRequired()
                .HasDefaultValue(true);
        }
    }
}
