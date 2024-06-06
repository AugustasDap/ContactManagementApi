using ContactManagementApi.BusinessLogic.Interfaces;
using ContactManagementApi.BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;


namespace ContactManagementApi.BusinessLogic
{
    public static class ServiceExtentions
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }

    }
}
