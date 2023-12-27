using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using MockR.Dtos;
using MockR.Service;
using System;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace MockR
{
    public class MockRDynaMiddleware
    {
        private readonly RequestDelegate _next;

        public MockRDynaMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context,IMockRService service)
        {
        
            //Si no empieza por este segmento, no me sirve, siguiente.
            if (!context.Request.Path.StartsWithSegments("/v1/mocks")) { await _next(context); return; }

            PageDto? page = service.GetBy(context.Request.Path, context.Request.Method);

            if (page == null)
            {
                context.Response.StatusCode = 404;
                page = new PageDto()
                {
                    Body = @"{""Message"":""404 - Not found""}"
                };
                return;
            }

            context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
            context.Response.Headers.Append("Access-Control-Allow-Methods", "GET, POST, OPTIONS, PUT, PATCH");
            context.Response.Headers.Append("Access-Control-Allow-Headers", "Content-Type, Accept");

            context.Response.ContentType = "application/json";
            context.Response.ContentLength = page.Body.Length;

            await context.Response.WriteAsync(page.Body, UTF8Encoding.UTF8);
            
        }
    }
}
