using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ananddotnetlin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        serverOptions.AllowSynchronousIO = true;
                        serverOptions.Limits.MaxConcurrentConnections = 100;
                    });
                    //webBuilder.ConfigureKestrel(serverOptions =>
                    //{
                    //    serverOptions.Limits.MaxConcurrentConnections = 100;
                    //    serverOptions.Limits.MaxConcurrentUpgradedConnections = 100;
                    //    serverOptions.Limits.MaxRequestBodySize = 10 * 1024;
                    //    serverOptions.Limits.MinRequestBodyDataRate =
                    //        new MinDataRate(bytesPerSecond: 100,
                    //            gracePeriod: TimeSpan.FromSeconds(10));
                    //    serverOptions.Limits.MinResponseDataRate =
                    //        new MinDataRate(bytesPerSecond: 100,
                    //            gracePeriod: TimeSpan.FromSeconds(10));
                    //    serverOptions.Listen(IPAddress.Loopback, 5000);
                    //    serverOptions.Listen(IPAddress.Loopback, 5001,
                    //        listenOptions =>
                    //        {
                    //            listenOptions.UseHttps("testCert.pfx",
                    //                "testPassword");
                    //        });
                    //    serverOptions.Limits.KeepAliveTimeout =
                    //        TimeSpan.FromMinutes(2);
                    //    serverOptions.Limits.RequestHeadersTimeout =
                    //        TimeSpan.FromMinutes(1);
                    //});
                });
    }
}
