using Application.eServicesPortal.ApplicationServices;
using Application.eServicesPortal.Users.Services;
using Microsoft.Extensions.DependencyInjection;


namespace Application.eServicesPortal
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddScoped<UserService>();
            services.AddScoped<AppService>();

            return services;
        }
    }
}
