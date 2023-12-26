using Microsoft.Extensions.DependencyInjection;
using MockR.Service;
using MockR.Services;

namespace MockR.Dependency_Injection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddMockR(this IServiceCollection services) 
        {
            services.AddDbContext<CacheDbContext>();
            services.AddScoped<IMockRRepository,MockRRepository>();
            services.AddScoped<IMockRService,MockRService>();
            return services;
        }
    }
}
