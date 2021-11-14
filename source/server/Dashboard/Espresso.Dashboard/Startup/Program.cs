// Program.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace Espresso.Dashboard.Startup
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(configureOptions =>
                {
                    var environmentName = Environment.GetEnvironmentVariable(EnvironmentVariableNamesConstants.AspNetCoreEnvironment) ?? string.Empty;

                    var parentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;

                    _ = configureOptions
                        .AddJsonFile(path: $"{parentDirectory}/configuration/app-settings.json", optional: false)
                        .AddJsonFile(path: $"{parentDirectory}/configuration/app-settings.{environmentName}.json", optional: false)
                        .AddJsonFile(path: $"AppSettings/app-settings.json", optional: false)
                        .AddJsonFile(path: $"AppSettings/app-settings.{environmentName}.json", optional: false)
                        .AddEnvironmentVariables()
                        .Build();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureAppConfiguration((ctx, _) =>
                    {
                        StaticWebAssetsLoader.UseStaticWebAssets(
                            environment: ctx.HostingEnvironment,
                            configuration: ctx.Configuration);
                    });
                });
    }
}
