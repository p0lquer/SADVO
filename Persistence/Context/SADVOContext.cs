

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SADVO.Domain.Entities;

namespace SADVO.Persistence.Context
{
  public class SADVOContext : DbContext
    {
        public SADVOContext(DbContextOptions<SADVOContext> options) : base(options)
        {
        }

        public DbSet<Candidato> Candidatos { get; set; }
        public DbSet<Partido_Politico> PartidosPoliticos { get; set; }
        public DbSet<Dirigente_Politico> DirigentesPoliticos { get; set; }
        public DbSet<Alianzas_Politica> AlianzasPoliticas { get; set; }

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Eleccion> Elecciones { get; set; }
        public DbSet<Puesto_Electivo> PuestosElectivos { get; set; }
        public DbSet<Solicitudes> Solicitudes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
          
        }
    }
}
