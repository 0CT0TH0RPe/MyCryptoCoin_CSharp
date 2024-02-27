// To use the middleware, add the following line in the Configure method of your Startup.cs file: - app.UseMyCustomMiddleware();
// Make sure to add the necessary using statements at the top of your files to reference the correct namespaces.

// MyCustomMiddleware.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MyCryptoCoinAPI.Middleware
{
    public class MyCustomMiddleware
    {
        private readonly RequestDelegate _next;

        public MyCustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext) // (HttpRequest request, HttpResponse response)
        {
            // Custom middleware logic goes here

            await _next(context);
        }
    }

    public static class MyCustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder application)
        {
            return builder.UseMiddleware<MyCustomMiddlewareExtensions>();
        }
    }
}