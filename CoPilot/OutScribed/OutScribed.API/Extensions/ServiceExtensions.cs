using FastEndpoints;
using Hangfire;
using MassTransit;
using OutScribed.Application.Queries;
using OutScribed.Infrastructure;
using OutScribed.Infrastructure.EventConsumers;
using OutScribed.Infrastructure.Hangfire.ScheduledJobs;
using OutScribed.Infrastructure.Persistence.Writes.Jail;
using OutScribed.Infrastructure.Persistence.Writes.Onboarding;
using OutScribed.Infrastructure.Persistence.Writes.Repository;
using OutScribed.Infrastructure.Repositories;
using OutScribed.Modules.Analysis.Application;
using OutScribed.Modules.Discovery.Application;
using OutScribed.Modules.Identity.Application;
using OutScribed.Modules.Jail.Application.Interfaces;
using OutScribed.Modules.Jail.Application.Services;
using OutScribed.Modules.Jail.Domain.Models;
using OutScribed.Modules.Onboarding.Application;
using OutScribed.Modules.Onboarding.Application.Features.Commands.SendToken;
using OutScribed.Modules.Onboarding.Application.Repository;
using OutScribed.Modules.Onboarding.Domain.Models;
using OutScribed.Modules.Publishing.Application;
using OutScribed.Modules.Tagging.Application;
using OutScribed.SharedKernel.Abstract.OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.Interfaces;

namespace OutScribed.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddExtensions(
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

            var hangfireConnectionString = config.GetConnectionString("HangfireConnection");

            //services.AddDbContext<HangfireDbContext>(options => {
            //    options.UseNpgsql(hangfireConnectionString);
            //});

            // Register generic Repositories
            services.AddScoped<IWriteRepository<TempUser>, WriteRepository<TempUser, OnboardingDbContext>>();


            services.AddScoped<IWriteRepository<JailedIpAddress>, WriteRepository<JailedIpAddress, JailDbContext>>();
            services.AddScoped<IJailService, JailService>();
            services.AddScoped<IEventPublisher, EventPublisher>();
            services.AddScoped<ITempUserRepository, TempUserRepository>();

            // Register other module-specific services
            services.AddScoped<SendTokenService>();
            //services.AddScoped<ResendTokenService>();
            //services.AddScoped<VerifyTokenService>();

            services.AddFastEndpoints(config =>
            {
                config.Assemblies =
                [
                    typeof(OnboardingModule).Assembly,
                    typeof(IdentityModule).Assembly,
                    typeof(PublishingModule).Assembly,
                    typeof(AnalysisModule).Assembly,
                    typeof(DiscoveryModule).Assembly,
                    typeof(TaggingModule).Assembly,
                    typeof(QueriesProject).Assembly,

                ];

            });


            //Scheduled Hangfire Jobs
            RecurringJob.AddOrUpdate<TempUserCleanupService>(
                "TempUserCleanup", // Unique Job ID
                service => service.CleanExpiredTempUsersAsync(),
                Cron.Daily(3, 0) // Runs every day at 3:00 AM (UTC, assuming your server is UTC)
            );

            // --- Configure MassTransit for RabbitMQ ---
            services.AddMassTransit(x =>
            {
                // Add all consumers from the assembly containing your current consumers.
                // This simplifies registration when you have many consumers.
                //x.AddConsumersFromNamespaceContaining<EventConsumers>(); // Or Assembly.GetExecutingAssembly() if consumers are in this project

                x.AddConsumers(typeof(InfrastructureProject).Assembly);

                // Configure RabbitMQ as the transport
                x.UsingRabbitMq((context, cfg) =>
                {
                    //cfg.Host(builder.Configuration.GetConnectionString("RabbitMQ")); // Get connection string from config

                    // Configure message retry policies for consumers
                    cfg.UseMessageRetry(r =>
                    {
                        r.Interval(3, TimeSpan.FromSeconds(5)); // Retry 3 times with 5-second intervals
                        r.Ignore<InvalidOperationException>(); // Don't retry if it's a domain validation error
                    });

                    // Automatically configure endpoints for consumers.
                    // This will create queues named after your consumer types (e.g., ip-address-violation-consumer)
                    // and bind them to the appropriate exchanges for published events.
                    cfg.ConfigureEndpoints(context);

                    // Optional: If you want to explicitly name a receive endpoint for a consumer:
                    // cfg.ReceiveEndpoint("ip-violation-queue", e =>
                    // {
                    //     e.ConfigureConsumer<IpAddressViolationConsumer>(context);
                    // });
                });
            });


            return services;
        }

    }

}
