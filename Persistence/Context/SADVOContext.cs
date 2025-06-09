

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SADVO.Domain.Entities;
using SADVO.Persistence.Data.Configurations;

namespace SADVO.Persistence.Context
{
    public class SADVOContext : DbContext
    {
        public SADVOContext(DbContextOptions<SADVOContext> options) 
            : base(options)
        { }

        public DbSet<Candidato> Candidatos { get; set; }
        public DbSet<Ciudadano> Ciudadanos { get; set; }
        public DbSet<Eleccion> Elecciones { get; set; }
        public DbSet<Partido_Politico> PartidosPoliticos { get; set; }
        public DbSet<Puesto_Electivo> PuestosElectivos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Alianzas_Politica> AlianzasPoliticas { get; set; }
        public DbSet<Dirigente_Politico> AsignacionesDirigentes { get; set; }
        public DbSet<Asignar_Candidato> AsignacionesCandidatos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.ApplyConfiguration(new CandidatoConfig());
            modelBuilder.ApplyConfiguration(new CiudadanoConfig());
            modelBuilder.ApplyConfiguration(new PartidoPoliticoConfig());
            modelBuilder.ApplyConfiguration(new PuestoElectivoConfig());
            modelBuilder.ApplyConfiguration(new DirigentePoliticoConfig());
            modelBuilder.ApplyConfiguration(new AsignacionCandidatoConfig());
            modelBuilder.ApplyConfiguration(new AlianzaPoliticaConfig());

            ConfigureEleccionRelations(modelBuilder);
        }
        private void ConfigureEleccionRelations(ModelBuilder modelBuilder)
        {
            // Relación Elección - Puestos Electivos (M-M)
            modelBuilder.Entity<Eleccion>()
                .HasMany(e => e.PuestoElectivo)
                .WithMany(p => p.Eleccion)
                .UsingEntity<Dictionary<string, object>>(
                    "EleccionPuestos",
                    j => j.HasOne<Puesto_Electivo>().WithMany().HasForeignKey("PuestoElectivoId"),
                    j => j.HasOne<Eleccion>().WithMany().HasForeignKey("EleccionId"),
                    j => j.ToTable("EleccionPuestos"));
        }
    }
}
