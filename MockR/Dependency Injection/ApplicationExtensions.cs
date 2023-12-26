using Microsoft.AspNetCore.Builder;

namespace MockR.Dependency_Injection
{
    public static class ApplicationExtensions
    {
        public static IApplicationBuilder UseMockR(this IApplicationBuilder app) 
        {
            app.UseMiddleware<MockRMiddleware>();
            app.UseMiddleware<MockRDynaMiddleware>();

            return app;
        }
    }
}
