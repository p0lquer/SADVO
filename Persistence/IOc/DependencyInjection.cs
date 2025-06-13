

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SADVO.Domain.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Application.Service;
using SADVO.Application.Service.SADVO.Application.Service;
using SADVO.Persistence.Repository;
using SADVO.Interfaces.Interface.Repository;

namespace SADVO.Persistence.IOc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceInfrastructure(this IServiceCollection services)
        {

            // Repositories
            services.AddScoped(typeof(IGeneryRepository<>), typeof(GeneryRepository<>));
            services.AddScoped<IEleccionRepository, EleccionRepository>();
            services.AddScoped<IDirigentePoliticoRepository, DirigentePoliticoRepository>();
            services.AddScoped<IPartidoPoliticoRepository, PartidoPoliticoRepository>();
            services.AddScoped<ICandidatoRepository, CandidatoRepository>();
            services.AddScoped<IAlianzasPoliticasRepository, AlianzasPoliticasRepository>();
            services.AddScoped<IUsuariosRepository, UsuariosRepository>();
            services.AddScoped<IPuestoElectivoRepository, PuestoElectivoRepository>();
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
            services.AddScoped<IAsignarCandidatoService, AsignarCandidatoService>();
            services.AddScoped<ICiudadanoService, CiudadanoService>();




            return services;

            
    }
        
    }
}
