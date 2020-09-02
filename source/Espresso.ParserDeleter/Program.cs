using System;
using System.Reflection;
using Espresso.Application.CQRS.NewsPortals.Queries.GetNewsPortals;
using Espresso.Application.DomainServices;
using Espresso.Application.Infrastructure;
using Espresso.Application.Initialization;
using Espresso.Common.Constants;
using Espresso.Common.Enums;

using Espresso.Domain.IServices;
using Espresso.Domain.IValidators;
using Espresso.Domain.Services;
using Espresso.Domain.Validators;
using Espresso.Persistence.Database;
using Espresso.Persistence.IRepositories;
using Espresso.Persistence.Repositories;
using Espresso.ParserDeleter.Configuration;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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
                    var startup = new Startup();
                    startup.ConfigureServices(services);
                });
    }
}
