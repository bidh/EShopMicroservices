using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Data;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            //adding auditable entity interceptor to update the audit fields when saving changes
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

            //adding dispatch domain events interceptor to dispatch domain events when saving changes
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
            // add services to the container.
            services.AddDbContext<ApplicationDbContext>((sp,options) =>
            {
                //adding the interceptors to the db context
                options.AddInterceptors(sp.GetService<ISaveChangesInterceptor>());
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            return services;
        }
    }
}
