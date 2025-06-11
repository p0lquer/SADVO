

using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SADVO.Domain.Entities;

namespace SADVO.Persistence.Data.Configurations
{
    public class VotoConfig : IEntityTypeConfiguration<Voto>
    {
        public void Configure(EntityTypeBuilder<Voto> builder)
        {

            builder.ToTable("Votos");
            builder.HasKey(p => p.Id);

            builder
         .HasOne(v => v.Ciudadano)
         .WithMany() 
         .HasForeignKey(v => v.CiudadanoId);

             builder
             .HasOne(v => v.Eleccion)
                .WithMany() 
                .HasForeignKey(v => v.EleccionId);
        }
    }
}
