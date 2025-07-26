using Backend.Application.Interfaces;
using Backend.Infrastructure.Authentication;
using Backend.Infrastructure.EmailGateway;
using Backend.Infrastructure.ErrorLogging;
using Backend.Infrastructure.FileHandling;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Infrastructure
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
