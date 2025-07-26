using OutScribed.Application.Repositories;
using OutScribed.Persistence.Repositories;
using OutScribed.Persistence.EntityConfigurations;
using Microsoft.Extensions.DependencyInjection;

namespace OutScribed.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {

            services.AddScoped<OutScribedDbContext, OutScribedDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
