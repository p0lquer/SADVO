

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SADVO.Application.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Application.Service;
using SADVO.Persistence.Repository;

namespace SADVO.Persistence.IOc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceInfrastructure(this IServiceCollection services)
        {
            // Register DbContext
            //services.AddDbContext<RestaurantDbContext>(options =>
            //{
            //    options.UseNpgsql(
            //        configuration.GetConnectionString("DefaultConnection"),
            //        npgsqlOptions =>
            //        {
            //            npgsqlOptions.MigrationsAssembly(typeof(RestaurantDbContext).Assembly.FullName);
            //            npgsqlOptions.EnableRetryOnFailure(
            //                maxRetryCount: 3,
            //                maxRetryDelay: TimeSpan.FromSeconds(10),
            //                errorCodesToAdd: null);
            //        })
            //        .EnableSensitiveDataLogging()
            //        .LogTo(Console.WriteLine, LogLevel.Information);
            //});

            // Repositories
            services.AddScoped(typeof(IGeneryRepository<>), typeof(GeneryRepository<>));
            services.AddScoped<IEleccionRepository, EleccionRepository>();
            services.AddScoped<IDirigentePoliticoRepository, DirigentePoliticoRepository>();
            services.AddScoped<IPartidoPoliticoRepository, PartidoPoliticoRepository>();
            services.AddScoped<ICandidatoRepository, CandidatoRepository>();
            services.AddScoped<IAlianzasPoliticasRepository, AlianzasPoliticasRepository>();
            services.AddScoped<IUsuariosRepository, UsuariosRepository>();
            services.AddScoped<IPuestoElectivoRepository, PuestoElectivoRepository>();
            services.AddScoped<ISolicitudesRepository, SolicitudesRepository>();
            services.AddScoped<IAsignarCandidatoRepository, AsignarCandidatoRepository>();
            services.AddScoped<ICiudadanoRepository,CiudadanoRepository>();

            //Service 
            services.AddScoped(typeof(IGeneryService<>), typeof(GeneryService<>));
            services.AddScoped<IEleccionService, EleccionService>();
            services.AddScoped<IDirigentePoliticoService, DirigentePoliticoService>();
            services.AddScoped<IPartidoPoliticoService, PartidoPoliticoService>();
            services.AddScoped<ICandidatoService, CandidatoService>();
            //services.AddScoped<IAlianzasPoliticasService, AlianzasPoliticasService>();
            services.AddScoped<IUsuariosService, UsuariosService>();
            services.AddScoped<IPuestoElectivoService, PuestoElectivoService>();
            services.AddScoped<ISolicitudesService, SolicitudesService>();
            services.AddScoped<IAsignarCandidatoService, AsignarCandidatoService>();
            services.AddScoped<ICiudadanoService, CiudadanoService>();




            return services;

            
    }
        
    }
}
