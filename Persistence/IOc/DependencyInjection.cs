

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
            services.AddTransient(typeof(IGeneryRepository<>), typeof(GeneryRepository<>));
            services.AddTransient<IEleccionRepository, EleccionRepository>();
            services.AddTransient<IDirigentePoliticoRepository, DirigentePoliticoRepository>();
            services.AddTransient<IPartidoPoliticoRepository, PartidoPoliticoRepository>();
            services.AddTransient<ICandidatoRepository, CandidatoRepository>();
            services.AddTransient<IAlianzasPoliticasRepository, AlianzasPoliticasRepository>();
            services.AddTransient<IUsuariosRepository, UsuariosRepository>();
            services.AddTransient<IPuestoElectivoRepository, PuestoElectivoRepository>();
            services.AddTransient<IAsignarCandidatoRepository, AsignarCandidatoRepository>();
            services.AddTransient<ICiudadanoRepository,CiudadanoRepository>();

            //Service 
            services.AddTransient(typeof(IGeneryService<>), typeof(GeneryService<>));
            services.AddTransient<IEleccionService, EleccionService>();
            services.AddTransient<IDirigentePoliticoService, DirigentePoliticoService>();
            services.AddTransient<IPartidoPoliticoService, PartidoPoliticoService>();
            services.AddTransient<ICandidatoService, CandidatoService>();
            //services.AddScoped<IAlianzasPoliticasService, AlianzasPoliticasService>();
            services.AddTransient<IUsuariosService, UsuariosService>();
            services.AddTransient<IPuestoElectivoService, PuestoElectivoService>();
            services.AddTransient<IAsignarCandidatoService, AsignarCandidatoService>();
            services.AddTransient<ICiudadanoService, CiudadanoService>();




            return services;

            
    }
        
    }
}
