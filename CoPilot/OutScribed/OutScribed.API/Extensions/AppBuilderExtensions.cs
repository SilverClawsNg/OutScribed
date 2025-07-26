using Microsoft.AspNetCore.Builder;
using OutScribed.API.Middlewares;

namespace OutScribed.API.Extensions
{
  
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder UseIpJail(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IpJailMiddleware>();
        }
    }
}
