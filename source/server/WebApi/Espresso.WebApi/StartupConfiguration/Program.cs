// Program.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Espresso.WebApi.StartupConfiguration;

/// <summary>
///
/// </summary>
internal static class Program
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="args"></param>
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(configureOptions =>
            {
                var environmentName = Environment.GetEnvironmentVariable(EnvironmentVariableNamesConstants.AspNetCoreEnvironment) ?? string.Empty;

                var parentDirectory = AppDomain.CurrentDomain.BaseDirectory;

                _ = configureOptions
                    .AddJsonFile(path: $"{parentDirectory}/configuration/app-settings.json", optional: false)
                    .AddJsonFile(path: $"{parentDirectory}/configuration/app-settings.{environmentName}.json", optional: false)
                    .AddJsonFile(path: "AppSettings/app-settings.json", optional: false)
                    .AddJsonFile(path: $"AppSettings/app-settings.{environmentName}.json", optional: false)
                    .AddEnvironmentVariables()
                    .Build();
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                _ = webBuilder.UseStartup<Startup>();
                _ = webBuilder.ConfigureLogging(loggingBuilder =>
                {
                    _ = loggingBuilder.ClearProviders();
                    _ = loggingBuilder.AddSimpleConsole();
                });
            });
}
