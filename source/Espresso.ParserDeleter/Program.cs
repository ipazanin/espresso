using System;
using Espresso.Common.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Espresso.ParserDeleter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(configureOptions =>
                {
                    var environmentName = Environment.GetEnvironmentVariable(EnviromentVariableNamesConstants.DotnetEnvironment) ?? "";
                    var configuration = configureOptions
                        .AddJsonFile($"appsettings.json", optional: false)
                        .AddJsonFile($"appsettings.{environmentName}.json", optional: false)
                        .AddEnvironmentVariables()
                        .Build();
                })
                .ConfigureLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddConsole(options =>
                    {
                        options.TimestampFormat = $"\n{DateTimeConstants.LoggerDateTimeFormat} - ";
                    });
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
                    var startup = new Startup(configuration);
                    startup.ConfigureServices(services);
                });
    }
}
