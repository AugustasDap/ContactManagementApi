using ContactManagementApi.Database.Context;
using ContactManagementApi.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;



namespace ContactManagementApi.Database
{
    public static class ServiceExtentions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, string connnectionString)
        {
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connnectionString));

            return services;
        }
    }
}
