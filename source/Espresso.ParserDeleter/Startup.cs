using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Application.IServices;
using Espresso.Common.Enums;
using Espresso.ParserDeleter.Configuration;
using Espresso.Persistence.Database;
using Espresso.Persistence.IRepositories;
using Espresso.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Espresso.ParserDeleter.Application.Initialization;
using Espresso.ParserDeleter.Application.IServices;
using Espresso.Application.Services;
using Espresso.ParserDeleter.Application.Services;
using Espresso.Wepi.Application.IServices;
using Espresso.ParserDeleter.ParseRssFeeds.Validators;
using Espresso.ParserDeleter.ParseRssFeeds;
using FluentValidation;
using Espresso.WebApi.Extensions;
using System;
using Espresso.ParserDeleter.CronJobs;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace Espresso.ParserDeleter
{
    public class Startup
    {
        #region Fields
        private readonly ParserDeleterConfiguration _parserDeleterConfiguration;
        #endregion

        #region Constructors
        public Startup(IConfiguration configuration)
        {
            _parserDeleterConfiguration = new ParserDeleterConfiguration(configuration);
        }
        #endregion

        #region Methods
        public void ConfigureServices(IServiceCollection services)
        {
            #region Configuration
            services.AddSingleton<IParserDeleterConfiguration, ParserDeleterConfiguration>();
            #endregion

            #region Validators
            services.AddValidatorsFromAssembly(typeof(ArticleDataValidator).Assembly);
            #endregion

            #region Services
            services.AddScoped<ISlackService, SlackService>(o => new SlackService(
                memoryCache: o.GetRequiredService<IMemoryCache>(),
                httpClientFactory: o.GetRequiredService<IHttpClientFactory>(),
                loggerFactory: o.GetRequiredService<ILoggerFactory>(),
                webHookUrl: _parserDeleterConfiguration.AppConfiguration.SlackWebHook
            ));
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<ILoadRssFeedsService, LoadRssFeedsService>();
            services.AddScoped<ICreateArticlesService, CreateArticlesService>();
            services.AddScoped<IScrapeWebService, ScrapeWebService>();
            services.AddScoped<IParseHtmlService, ParseHtmlService>();
            services.AddScoped<ISortArticlesService, SortArticlesService>();
            #endregion

            #region Database
            services.AddDbContext<IApplicationDatabaseContext, ApplicationDatabaseContext>(options =>
             {
                 options.UseSqlServer(
                     connectionString: _parserDeleterConfiguration.DatabaseConfiguration.ConnectionString,
                     sqlServerOptionsAction: sqlServerOptions =>
                     {
                         sqlServerOptions.CommandTimeout(_parserDeleterConfiguration.DatabaseConfiguration.CommandTimeoutInSeconds);
                     }
                 );
                 options.UseQueryTrackingBehavior(_parserDeleterConfiguration.DatabaseConfiguration.QueryTrackingBehavior);
                 options.EnableDetailedErrors(_parserDeleterConfiguration.DatabaseConfiguration.EnableDetailedErrors);
                 options.EnableSensitiveDataLogging(_parserDeleterConfiguration.DatabaseConfiguration.EnableSensitiveDataLogging);
             });

            services.AddScoped<IDatabaseConnectionFactory>(o => new DatabaseConnectionFactory(
                    connectionString: _parserDeleterConfiguration.DatabaseConfiguration.ConnectionString
                )
            );
            services.AddScoped<IApplicationDownloadRepository, ApplicationDownloadRepository>();
            services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            #endregion

            #region MemoryCache
            services.AddMemoryCache();
            services.AddTransient<IParserDeleterInit, ParserDeleterInit>();
            #endregion

            #region Http
            services.AddHttpClient();
            #endregion

            #region MediatR
            services.AddMediatR(typeof(ParseRssFeedsCommandHandler).Assembly);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggerPipeline<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ApplicationLifeTimePipeline<,>));
            #endregion

            #region Jobs
            services.AddCronJob<DeleteArticlesCronJob>(cronJobConfiguration =>
            {
                cronJobConfiguration.CronExpression = _parserDeleterConfiguration
                    .CronJobsConfiguration
                    .DeleteArticlesCronExpression;
                cronJobConfiguration.TimeZoneInfo = TimeZoneInfo.Utc;
                cronJobConfiguration.AppEnvironment = _parserDeleterConfiguration.AppConfiguration.AppEnvironment;
                cronJobConfiguration.Version = _parserDeleterConfiguration.AppConfiguration.Version;
            });
            services.AddCronJob<ParserDeleterReportCronJob>(cronJobConfiguration =>
            {
                cronJobConfiguration.CronExpression = _parserDeleterConfiguration
                    .CronJobsConfiguration
                    .ParserDeleterReportCronExpression;
                cronJobConfiguration.TimeZoneInfo = TimeZoneInfo.Utc;
                cronJobConfiguration.AppEnvironment = _parserDeleterConfiguration.AppConfiguration.AppEnvironment;
                cronJobConfiguration.Version = _parserDeleterConfiguration.AppConfiguration.Version;
            });
            services.AddCronJob<ParseArticlesCronJob>(cronJobConfiguration =>
            {
                cronJobConfiguration.CronExpression = _parserDeleterConfiguration
                    .CronJobsConfiguration
                    .ParseArticlesCronExpression;
                cronJobConfiguration.TimeZoneInfo = TimeZoneInfo.Utc;
                cronJobConfiguration.AppEnvironment = _parserDeleterConfiguration.AppConfiguration.AppEnvironment;
                cronJobConfiguration.Version = _parserDeleterConfiguration.AppConfiguration.Version;
            });
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="memoryCacheInit"></param>
        public static void Configure(
            IParserDeleterInit memoryCacheInit
        )
        {
            memoryCacheInit.InitParserDeleter().GetAwaiter().GetResult();
        }
        #endregion
    }
}
