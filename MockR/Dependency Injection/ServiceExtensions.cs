using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MockR.Service;
using MockR.Services;

namespace MockR.Dependency_Injection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddMockR(this IServiceCollection services) 
        {
            services.AddSingleton<MockRDbContext, MockRDbContext>();
            services.AddScoped<IMockRRepository,MockRRepository>();
            services.AddScoped<IMockRService,MockRService>();
            return services;
        }
    }
}
