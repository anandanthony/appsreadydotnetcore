using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ananddotnetlin.middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class addCustomHeaderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<addCustomHeaderMiddleware> _logger;

        public addCustomHeaderMiddleware(RequestDelegate next, ILogger<addCustomHeaderMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            //Check article: https://www.thecodebuzz.com/add-custom-headers-to-response-in-asp-net-core/

            httpContext.Request.Headers.TryGetValue("apps-ready-version", out var traceValue);

            if (string.IsNullOrWhiteSpace(traceValue))
            {
                traceValue = "v2";
                httpContext.Response.OnStarting((state) =>
                {
                    httpContext.Response.Headers.Add("apps-ready-version", traceValue);
                    return Task.FromResult(0);
                }, httpContext);
            }

            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class addCustomHeaderExtensions
    {
        public static IServiceCollection AddCustomHeaderMiddleware(this IServiceCollection service)
        {
            return service;
        }

        public static IApplicationBuilder UseAddCustomHeaderMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<addCustomHeaderMiddleware>();
        }
    }
}
