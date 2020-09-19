using System;
using System.Reflection;
using Espresso.Application.CQRS.NewsPortals.Queries.GetNewsPortals;
using Espresso.Application.DomainServices;
using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Application.Initialization;
using Espresso.Common.Enums;
using Espresso.Domain.IServices;
using Espresso.Domain.IValidators;
using Espresso.Domain.Services;
using Espresso.Domain.Validators;
using Espresso.ParserDeleter.Configuration;
using Espresso.Persistence.Database;
using Espresso.Persistence.IRepositories;
using Espresso.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Espresso.ParserDeleter
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            #region Configuration
            services.AddHostedService<ParserDeleter>();
            services.AddTransient<IParserDeleterConfiguration, ParserDeleterConfiguration>();
            #endregion

            #region Services
            services.AddSingleton<ISlackService, SlackService>();
            services.AddSingleton<IHttpService, HttpService>();
            services.AddScoped<IArticleParserService, ArticleParserService>();
            services.AddScoped<IWebScrapingService, WebScrapingService>();
            #endregion

            #region Validators
            services.AddScoped<IArticleValidator, ArticleValidator>();
            #endregion

            #region MemoryCache
            services.AddMemoryCache();
            services.AddTransient<IApplicationInit, ApplicationInit>();
            #endregion

            #region Http
            services.AddHttpClient();
            #endregion

            #region WebSockets
            services.AddSignalR();
            #endregion

            #region MediatR
            services.AddMediatR(typeof(GetNewsPortalsQuery).GetTypeInfo().Assembly);
            _ = services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggerPipelineBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ApplicationLifeTimePipelineBehavior<,>));
            #endregion

            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IParserDeleterConfiguration>();

            #region Database
            services.AddDbContext<IApplicationDatabaseContext, ApplicationDatabaseContext>(options =>
             {
                 options.UseSqlServer(
                     connectionString: configuration.DatabaseConfiguration.ConnectionString,
                     sqlServerOptionsAction: sqlServerOptions =>
                     {
                         sqlServerOptions.CommandTimeout((int)TimeSpan.FromMinutes(2).TotalSeconds);
                     }
                 );
                 options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                 switch (configuration.AppConfiguration.AppEnvironment)
                 {
                     case AppEnvironment.Undefined:
                     case AppEnvironment.Local:
                     case AppEnvironment.Dev:
                     default:
                         options.EnableDetailedErrors();
                         options.EnableSensitiveDataLogging(true);
                         break;
                     case AppEnvironment.Prod:
                         break;
                 }
             });

            services.AddScoped<IDatabaseConnectionFactory>(o => new DatabaseConnectionFactory(configuration.DatabaseConfiguration.ConnectionString));
            services.AddScoped<IApplicationDownloadRepository, ApplicationDownloadRepository>();
            services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            #endregion
        }
    }
}
