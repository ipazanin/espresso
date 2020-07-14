using System;
using System.Reflection;
using Espresso.Application.CQRS.NewsPortals.Queries.GetNewsPortals;
using Espresso.Application.DomainServices;
using Espresso.Application.Infrastructure;
using Espresso.Application.Initialization;
using Espresso.Common.Configuration;
using Espresso.Common.Constants;
using Espresso.DataAccessLayer.IRepository;
using Espresso.DataAccessLayer.Repository;
using Espresso.Domain.IServices;
using Espresso.Domain.IValidators;
using Espresso.Domain.Services;
using Espresso.Domain.Validators;
using Espresso.Persistence.Database;
using Espresso.Workers.ParserDeleter.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Espresso.Common.Enums;

namespace Espresso.Workers.ParserDeleter
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
                    #region Configuration
                    services.AddHostedService<ParserDeleter>();
                    services.AddTransient<IParserDeleterConfiguration, ParserDeleterConfiguration>();
                    services.AddTransient<ICommonConfiguration, ParserDeleterConfiguration>();
                    #endregion

                    #region Services
                    services.AddSingleton<ILoggerService, LoggerService>();
                    services.AddSingleton<ISlackService, SlackService>();
                    services.AddScoped<IArticleParserService, ArticleParserService>();
                    services.AddScoped<IWebScrapingService, WebScrapingService>();
                    services.AddScoped<IHttpService, HttpService>();
                    #endregion

                    #region Validators
                    services.AddScoped<IArticleValidator, ArticleValidator>();
                    #endregion

                    #region MemoryCache
                    services.AddMemoryCache();
                    services.AddTransient<IMemoryCacheInit, MemoryCacheInit>();
                    #endregion

                    #region Http
                    services.AddHttpClient();
                    #endregion

                    #region WebSockets
                    services.AddSignalR();
                    #endregion

                    #region MediatR
                    services.AddMediatR(typeof(GetNewsPortalsQuery).GetTypeInfo().Assembly);
                    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggerPipelineBehavior<,>));
                    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
                    services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ApplicationLifeTimePipelineBehavior<,>));
                    #endregion

                    var serviceProvider = services.BuildServiceProvider();
                    var configuration = serviceProvider.GetService<IParserDeleterConfiguration>();

                    #region Database
                    services.AddDbContext<IEspressoDatabaseContext, EspressoDatabaseContext>(options =>
                     {
                         options.UseSqlServer(hostContext.Configuration.GetConnectionString(ConfigurationKeyNameConstants.DefaultConnectionStringKeyName));
                         switch (configuration.AppEnvironment)
                         {
                             case AppEnvironment.Undefined:
                             case AppEnvironment.Local:
                             case AppEnvironment.Dev:
                             default:
                                 options.EnableDetailedErrors();
                                 options.UseLoggerFactory(LoggerFactory.Create(builder =>
                                 {
                                     builder.AddConsole();
                                 }));
                                 options.EnableSensitiveDataLogging(true);
                                 break;
                             case AppEnvironment.Prod:
                                 break;
                         }
                     });

                    services.AddScoped<IDatabaseConnectionFactory>(o => new DatabaseConnectionFactory(hostContext.Configuration.GetConnectionString(ConfigurationKeyNameConstants.DefaultConnectionStringKeyName)));
                    services.AddScoped<IApplicationDownloadRepository, ApplicationDownloadRepository>();
                    services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
                    services.AddScoped<IArticleRepository, ArticleRepository>();
                    #endregion
                });
    }
}
