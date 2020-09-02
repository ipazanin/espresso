using System;
using Espresso.Common.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Espresso.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {

        #region Fields
        private static IConfiguration _configuration = null!;
        #endregion

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
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(configureOptions =>
                {
                    var environmentName = Environment.GetEnvironmentVariable(EnviromentVariableNamesConstants.AspNetCoreEnvironment) ?? "";
                    _configuration = configureOptions
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
                    loggingBuilder.AddConfiguration(_configuration);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
