// To use the middleware, add the following line in the Configure method of your Startup.cs file: app.UseMeshTorrentNetworkMiddleware();
// Make sure to add the necessary using statements at the top of your files to reference the correct namespaces and ensure that the IMeshTorrentNetwork interface is registered in the dependency injection container of your application.

// MeshTorrentNetworkMiddleware.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MyCryptoCoin.Core.Contracts;
using MyCryptoCoin.Core.Services;
using System.Threading.Tasks;

namespace MyCryptoCoinAPI.Middleware
{
    public class MeshTorrentNetworkMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMeshTorrentNetwork _meshTorrentNetwork;

        public MeshTorrentNetworkMiddleware(RequestDelegate next, IMeshTorrentNetwork meshTorrentNetwork)
        {
            _next = next;
            _meshTorrentNetwork = meshTorrentNetwork;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Perform operations with MeshTorrentNetwork
            // Example:
            var networkStatus = _meshTorrentNetwork.GetNetworkStatus();

            // Custom middleware logic goes here

            await _next(context);
        }
    }

    public static class MeshTorrentNetworkMiddlewareExtensions
    {
        public static IApplicationBuilder UseMeshTorrentNetworkMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MeshTorrentNetworkMiddleware>();
        }
    }
}
