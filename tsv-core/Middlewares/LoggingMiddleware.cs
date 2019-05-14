using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using tsv_core.Models;

namespace tsv_core.Infrastructure
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, EFCDatabaseRequestLogger logger)
        {           
            Request request = new Request
            {
                Path = httpContext.Request.Path.ToString(),
                IPAddressClient = httpContext.Connection.RemoteIpAddress.ToString(),
                Time = DateTime.Now.ToString(),
                UserAgent = httpContext.Request.Headers.ContainsKey("User-Agent") ? httpContext.Request.Headers["User-Agent"].ToString() : "no User-Agent"
            };
            await logger.LogRequest(request);

            await _next.Invoke(httpContext);
        }
    }
}
