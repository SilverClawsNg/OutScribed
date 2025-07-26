using Backend.Application.Repositories;
using Backend.Persistence.Repositories;
using Backend.Persistence.EntityConfigurations;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {

            services.AddScoped<BackendDbContext, BackendDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
