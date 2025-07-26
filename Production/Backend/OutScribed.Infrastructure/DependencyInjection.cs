using OutScribed.Application.Interfaces;
using OutScribed.Infrastructure.Authentication;
using OutScribed.Infrastructure.EmailGateway;
using OutScribed.Infrastructure.ErrorLogging;
using OutScribed.Infrastructure.FileHandling;
using Microsoft.Extensions.DependencyInjection;

namespace OutScribed.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {

            services.AddTransient<IMailSender, MailSender>();
            services.AddTransient<IFileHandler, FileHandler>();
            services.AddTransient<IErrorLogger, ErrorLogger>();
            services.AddSingleton<IJwtProvider, JwtProvider>();
            services.AddTransient<HttpClient, HttpClient>();

            return services;
        }
    }
}
