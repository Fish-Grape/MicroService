using Feng.Basic.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Feng.Basic.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorLog(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorLogMiddleware>();
        }
    }
}
