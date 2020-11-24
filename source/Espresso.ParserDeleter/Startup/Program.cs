using System;
using Espresso.Common.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Espresso.ParserDeleter.Startup
{
    internal sealed class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(configureOptions =>
                {
                    var environmentName = Environment.GetEnvironmentVariable(EnviromentVariableNamesConstants.AspNetCoreEnvironment) ?? "";
                    var databaseName = Environment.GetEnvironmentVariable(EnviromentVariableNamesConstants.DatabaseName) ?? "";

                    var configuration = configureOptions
                        .AddJsonFile(path: $"AppSettings/GeneralSettings/app-settings.json", optional: false)
                        .AddJsonFile(path: $"AppSettings/GeneralSettings/app-settings.{environmentName}.json", optional: false)
                        .AddJsonFile(path: $"AppSettings/LoggingSettings/logging-settings.json", optional: false)
                        .AddJsonFile(path: $"AppSettings/LoggingSettings/logging-settings.{environmentName}.json", optional: false)
                        .AddJsonFile(path: $"AppSettings/DatabaseSettings/database-settings.json", optional: false)
                        .AddJsonFile(path: $"AppSettings/DatabaseSettings/database-settings.{databaseName}.json", optional: false)
                        .AddEnvironmentVariables()
                        .Build();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
