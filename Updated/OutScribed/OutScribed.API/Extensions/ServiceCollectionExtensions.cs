using OutScribed.Infrastructure.Providers;
using OutScribed.Onboarding.Application.Interfaces;
using OutScribed.Onboarding.Application.Services;
using OutScribed.Onboarding.Domain.Interfaces;
using OutScribed.Onboarding.Infrastructure.Providers;
using OutScribed.Onboarding.Infrastructure.Repositories;
using OutScribed.SharedKernel.Interfaces;

namespace OutScribed.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOutscribedPersistence(
            this IServiceCollection services,
            IConfiguration config)
        {
            // Register DbContext against PostgreSQL
            //services.AddDbContext<OutscribedDbContext>(opts =>
            //    opts.UseNpgsql(
            //        config.GetConnectionString("DefaultConnection"),
            //        npgsql =>
            //            // tell EF where to find migrations
            //            npgsql.MigrationsAssembly(typeof(OutscribedDbContext).Assembly.FullName)
            //    )
            //);

            // Repositories
            //services.AddScoped<IAccountRepository, AccountRepository>();
            //services.AddScoped<ITempUserRepository, TempUserRepository>();
            // …register other module repos here

            return services;
        }

        public static IServiceCollection AddOutscribedInfrastructure(this IServiceCollection services)
        {
            // Onboarding
            services.AddScoped<IOnboardingService, OnboardingService>();
            services.AddScoped<ITempUserRepository, TempUserRepository>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<ITokenProvider, VerificationTokenProvider>();
            services.AddScoped<IIpThrottleChecker, IpThrottleChecker>();
            // …other module services

            return services;
        }
    }

}
