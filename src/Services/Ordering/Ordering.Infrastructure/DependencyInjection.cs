using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
            // add services to the container.
            services.AddDbContext<ApplicationDbContext>((sp,options) =>
            {
                options.AddInterceptors(sp.GetService<ISaveChangesInterceptor>());
                options.UseSqlServer(connectionString);
            });
            return services;
        }
    }
}
